using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class RecipeCategoryEntity
    {
        public long RecipeCategoryID { get; set; }
        public long RecipeID { get; set; }
        public long CategoryID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
