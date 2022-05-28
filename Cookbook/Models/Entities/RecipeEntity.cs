using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class RecipeEntity
    {
        public long RecipeID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
