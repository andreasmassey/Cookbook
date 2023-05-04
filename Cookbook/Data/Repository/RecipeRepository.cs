using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook.Models;
using Cookbook.Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cookbook.Helpers;

namespace Cookbook.Data.Repository
{
    public class RecipeRepository : EntityBaseRepository<RecipeEntity>, IRecipeRepository
    {
        private CookbookContext _context;

        public RecipeRepository(CookbookContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Recipes> GetOneRecipeAsync(long recipeId)
        {
            var recipes = from r in _context.Recipes
                          join rc in _context.Categories on r.CategoryID equals rc.Category_ID
                          join i in _context.Images on r.ImageID equals i.Image_ID
                          where r.Recipe_ID == recipeId
                          select new RecipeModel
                          {
                              Recipe_ID = recipeId,
                              RecipeName = r.RecipeName,
                              Servings = r.Servings,
                              PrepTime = r.PrepTime,
                              CookTime = r.CookTime,
                              DateCreated = r.DateCreated,
                              CategoryID = r.CategoryID,
                              ImageID = r.ImageID,
                              UserID = r.UserID,
                              CategoryName = rc.CategoryName,
                              ImageData = i.Image
                          };
            var recipeModels = await recipes.ToListAsync();
            return new Recipes
            {
                RecipeModels = recipeModels
            };
        }

        public async Task<Recipes> GetRecipesAsync(long userId)
        {
            var recipes = from r in _context.Recipes
                          join rc in _context.Categories on r.CategoryID equals rc.Category_ID
                          join i in _context.Images on r.ImageID equals i.Image_ID
                          where r.UserID == userId
                          select new RecipeModel
                          {
                              Recipe_ID = r.Recipe_ID,
                              RecipeName = r.RecipeName,
                              Servings = r.Servings,
                              PrepTime = r.PrepTime,
                              CookTime = r.CookTime,
                              DateCreated = r.DateCreated,
                              CategoryID = r.CategoryID,
                              ImageID = r.ImageID,
                              UserID = userId,
                              CategoryName = rc.CategoryName,
                              ImageData = i.Image
                          };
            var recipeModels = await recipes.ToListAsync();
            return new Recipes
            {
                RecipeModels = recipeModels
            };
        }

        public async Task<DirectionModel> GetDirectionsAsync(long recipeId)
        {
            var directions = from d in _context.Directions
                             where d.RecipeID == recipeId
                             select d;

            var directionsList = await directions.ToListAsync();

            return new DirectionModel
            {
                Directions = directionsList
            };
        }

        public async Task<IngredientModel> GetIngredientsAsync(long recipeId)
        {
            var ingredients = from i in _context.Ingredients
                              where i.RecipeID == recipeId
                              select i;

            var ingredientsList = await ingredients.ToListAsync();

            return new IngredientModel
            {
                Ingredients = ingredientsList
            };
        }
    }
}
