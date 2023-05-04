using Cookbook.Models.Entities;
using System.Collections.Generic;

namespace Cookbook.Models.Contracts
{
    public class GetSpecificRecipeContract
    {
        public class GetSpecificRecipeRequest
        {
            public long RecipeID { get; set; }
        }
        
        public class GetSpecificRecipeResponse 
        { 
            public DirectionModel Directions { get; set; }
            public IngredientModel Ingredients { get; set;}
            public RecipeEntity Recipe { get; set; }
            public ErrorResponse Error { get; set; }
        }
    }
}
