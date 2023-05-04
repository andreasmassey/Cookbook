using System;

namespace Cookbook.Models.Entities
{
    public class IngredientEntity
    {
        public long Ingredient_ID { get; set; }
        public string IngredientName { get; set; }
        public long RecipeID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
