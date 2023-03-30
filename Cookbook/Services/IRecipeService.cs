using Cookbook.Models.Contracts;
using Cookbook.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Services
{
    public interface IRecipeService
    {
        Task<GetRecipesContract.Response> GetAllRecipesAsync();

        Task<SaveUserContract.Response> SaveUserAsync(SaveUserContract.Request request);
    }
}
