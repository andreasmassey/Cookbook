using Cookbook.Data.Repository;
using Cookbook.Helpers;
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

        public RecipeService(IRecipeRepository recipeRepository, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
        }

        public async Task<GetRecipesContract.GetRecipesResponse> GetAllRecipesAsync()
        {
            try
            {
                var response = await _recipeRepository.GetAllAsync();

                //var test = await _recipeRepository.GetAllAsync(x => x.DateCreated > DateTime.Now.AddMonths(-2)); //using a predicate/where clause
                return new GetRecipesContract.GetRecipesResponse {
                    Recipes = response.ToList()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<CreateUserContract.CreateUserResponse> SaveUserAsync(CreateUserContract.CreateUserRequest request)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserEmail == request.Email);

                if (user != null)
                {
                    throw new Exception("User already exists.");
                }

                var salt = EncryptPassword.CreateSalt();
                var passwordHash = Convert.ToBase64String(salt);
                var password = EncryptPassword.HashPassword(request.Password, salt);

                var userEntity = new UserEntity
                {
                    UserEmail = request.Email,
                    UserPassword = password,
                    UserFirstName = request.FirstName,
                    UserLastName = request.LastName,
                    DateCreated = DateTime.UtcNow,
                    UserPasswordHash = passwordHash
                };
                await _userRepository.AddAndSaveAsync(userEntity);
                user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserEmail == request.Email);
                return await Task.FromResult(new CreateUserContract.CreateUserResponse { UserID = user.UserID });
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
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserEmail == request.Email);

                if(user == null)
                {
                    throw new Exception("Username or Password is incorrect.");
                }

                var saltByte = Convert.FromBase64String(user.UserPasswordHash);
                var password = EncryptPassword.HashPassword(request.Password, saltByte);

                var isValid = password == user.UserPassword;

                if (isValid)
                {
                    return new UserLoginContract.UserLoginResponse
                    {
                        UserID = user.UserID
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
