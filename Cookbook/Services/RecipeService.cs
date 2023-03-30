using Cookbook.Data.Repository;
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

        public async Task<GetRecipesContract.Response> GetAllRecipesAsync()
        {
            try
            {
                var response = await _recipeRepository.GetAllAsync();

                //var test = await _recipeRepository.GetAllAsync(x => x.DateCreated > DateTime.Now.AddMonths(-2)); //using a predicate/where clause
                return new GetRecipesContract.Response {
                    Recipes = response.ToList()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<SaveUserContract.Response> SaveUserAsync(SaveUserContract.Request request)
        {
            try
            {
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserEmail == request.Email);

                if (user != null)
                {
                    throw new Exception("User already exists.");
                }

                var userEntity = new UserEntity
                {
                    UserEmail = request.Email,
                    UserPassword = request.Password,
                    UserFirstName = request.FirstName,
                    UserLastName = request.LastName,
                    DateCreated = DateTime.UtcNow
                };
                await _userRepository.AddAndSaveAsync(userEntity);
                user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserEmail == request.Email);
                return await Task.FromResult(new SaveUserContract.Response { UserID = user.UserID });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
