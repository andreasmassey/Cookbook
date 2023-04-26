using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
