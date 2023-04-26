﻿using Cookbook.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cookbook.Models
{
    public class RecipeModel
    {
        public long Recipe_ID { get; set; }
        public string RecipeName { get; set; }
        public long UserID { get; set; }
        public string Servings { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public DateTime DateCreated { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
