using Cookbook.Models.Contracts;
using Cookbook.Models.Entities;
using System;

namespace Cookbook.Mappings
{
    public static class Mappings
    {
        public static RecipeEntity RecipeEntity(CreateRecipeContract.CreateRecipeRequest request, ImageEntity image)
        {
            var recipeEntity = new RecipeEntity
            {
                RecipeName = request.RecipeName,
                UserID = request.UserID,
                Servings = request.Servings,
                PrepTime = request.PrepTime,
                CookTime = request.CookTime,
                DateCreated = DateTime.UtcNow,
                CategoryID = request.CategoryID,
                ImageID = image.Image_ID
            };

            return recipeEntity;
        }
    }
}
