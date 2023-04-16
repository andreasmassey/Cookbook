using Cookbook.Models.Contracts;
using Cookbook.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    public interface IRecipeService
    {
        Task<GetRecipesContract.GetRecipesResponse> GetAllRecipesAsync();

        Task<CreateUserContract.CreateUserResponse> SaveUserAsync(CreateUserContract.CreateUserRequest request);

        Task<UserLoginContract.UserLoginResponse> UserLoginAsync(UserLoginContract.UserLoginRequest request);
    }
}
