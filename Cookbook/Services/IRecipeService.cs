using Cookbook.Models.Contracts;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    public interface IRecipeService
    {
        Task<GetSpecificRecipeContract.GetSpecificRecipeResponse> GetSpecificRecipeAsync(GetSpecificRecipeContract.GetSpecificRecipeRequest request);

        Task<CreateUserContract.CreateUserResponse> SaveUserAsync(CreateUserContract.CreateUserRequest request);

        Task<UserLoginContract.UserLoginResponse> UserLoginAsync(UserLoginContract.UserLoginRequest request);

        Task<CreateRecipeContract.CreateRecipeResponse> CreateRecipeAsync(CreateRecipeContract.CreateRecipeRequest request);
    }
}
