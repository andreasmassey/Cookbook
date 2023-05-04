using System;

namespace Cookbook.Models.Entities
{
    public class DirectionEntity
    {
        public long Direction_ID { get; set; }
        public int StepNumber { get; set; }
        public string DirectionDescription { get; set; }
        public long RecipeID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
