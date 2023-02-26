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
        public DateTime DateCreated { get; set; }
        public string RecipeDescription { get; set; }
        public long FamilyID { get; set; }
        public string RecipeInstructions { get; set; }
        public long CategoryID { get; set; }

    }
}
