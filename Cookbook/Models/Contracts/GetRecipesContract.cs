using Cookbook.Models.Entities;
using System.Collections.Generic;

namespace Cookbook.Models.Contracts
{
    public class GetRecipesContract
    {
        public class Response { 
            public List<RecipeEntity> Recipes { get; set; }
        }
    }
}
