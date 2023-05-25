using Cookbook.Models;
using Cookbook.Models.Contracts;
using Cookbook.Models.Entities;
using System;

namespace Cookbook.Mappings
{
    public static class Mapping
    {
        public static UserLoginContract.UserLoginResponse LoginResponse(UserEntity user, Recipes recipes)
        {
            var loginResponse = new UserLoginContract.UserLoginResponse
            {
                UserID = user.User_ID,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Recipes = recipes
            };

            return loginResponse;
        }
        public static UserEntity UserEntity(CreateUserContract.CreateUserRequest request, string password, string passwordHash)
        {
            var userEntity = new UserEntity
            {
                Email = request.Email,
                Password = password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateCreated = DateTime.UtcNow,
                PasswordHash = passwordHash
            };

            return userEntity;
        }

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
