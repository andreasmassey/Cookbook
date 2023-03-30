using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class RecipeEntity
    {
        public long RecipeID { get; set; }
        public string RecipeName { get; set; }
        public long UserID { get; set; }
        public string Servings { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
