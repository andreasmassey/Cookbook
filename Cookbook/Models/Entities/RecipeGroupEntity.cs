﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Models.Entities
{
    public class RecipeGroupEntity
    {
        public int RecipeGroupID { get; set; }
        public int RecipeID { get; set;}
        public int GroupID { get; set; }
    }
}