using System.Collections.Generic;

namespace Cookbook.Models.Contracts
{
    public class CreateRecipeContract
    {
        public class CreateRecipeRequest
        {
            public string RecipeName { get; set; }
            public string Servings { get; set; }
            public int PrepTime { get; set; }
            public int CookTime { get; set; }
            public int CategoryID { get; set; }
            public IList<string> Directions { get; set; }
            public IList<string> Ingredients { get; set; }
            public byte[] ImageData { get; set; }
            public long UserID { get; set; }
        }

        public class CreateRecipeResponse
        {
            public long RecipeID { get; set; }
            public Recipes Recipe { get; set; }
            public ErrorResponse Error { get; set; }
        }
    }
}
