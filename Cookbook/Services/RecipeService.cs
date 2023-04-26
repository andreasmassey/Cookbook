using Cookbook.Data.Repository;
using Cookbook.Helpers;
using Cookbook.Models;
using Cookbook.Models.Contracts;
using Cookbook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDirectionsRepository _directionsRepository;

        public RecipeService(IRecipeRepository recipeRepository, IUserRepository userRepository, IDirectionsRepository directionsRepository)
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
            _directionsRepository = directionsRepository;
        }

        public async Task<GetSpecificRecipeContract.GetSpecificRecipeResponse> GetSpecificRecipeAsync(GetSpecificRecipeContract.GetSpecificRecipeRequest request)
        {
            try
            {
                var recipe = await _recipeRepository.GetFirstOrDefaultAsync(x => x.Recipe_ID == request.RecipeID);

                var directions = await _recipeRepository.GetDirectionsAsync(recipe.Recipe_ID);
                var ingredients = await _recipeRepository.GetIngredientsAsync(recipe.Recipe_ID);

                return new GetSpecificRecipeContract.GetSpecificRecipeResponse {
                    Directions = directions,
                    Ingredients = ingredients
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new GetSpecificRecipeContract.GetSpecificRecipeResponse
                {
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }

        public async Task<CreateUserContract.CreateUserResponse> SaveUserAsync(CreateUserContract.CreateUserRequest request)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);

                if (user != null)
                {
                    throw new Exception("User already exists.");
                }

                var salt = EncryptPassword.CreateSalt();
                var passwordHash = Convert.ToBase64String(salt);
                var password = EncryptPassword.HashPassword(request.Password, salt);

                var userEntity = new UserEntity
                {
                    Email = request.Email,
                    Password = password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateCreated = DateTime.UtcNow,
                    PasswordHash = passwordHash
                };
                await _userRepository.AddAndSaveAsync(userEntity);
                user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);
                return await Task.FromResult(new CreateUserContract.CreateUserResponse { UserID = user.User_ID });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CreateUserContract.CreateUserResponse
                {
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }

        public async Task<UserLoginContract.UserLoginResponse> UserLoginAsync(UserLoginContract.UserLoginRequest request)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);

                if(user == null)
                {
                    throw new Exception("Username or Password is incorrect.");
                }

                var saltByte = Convert.FromBase64String(user.PasswordHash);
                var password = EncryptPassword.HashPassword(request.Password, saltByte);

                var isValid = password == user.Password;

                if (isValid)
                {                    
                    var recipes = await _recipeRepository.GetRecipesAsync(user.User_ID);

                    return new UserLoginContract.UserLoginResponse
                    {
                        UserID = user.User_ID,
                        Recipes = recipes
                    };
                }

                throw new Exception("Username or Password is incorrect.");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UserLoginContract.UserLoginResponse { 
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }
    }
}
