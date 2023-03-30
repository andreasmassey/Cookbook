using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class DirectionEntity
    {
        public int DirectionID { get; set; }
        public int StepNum { get; set; }
        public string DirectionDesc { get; set; }
        public int RecipeID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
