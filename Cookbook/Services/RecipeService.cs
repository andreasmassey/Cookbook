using Cookbook.Data.Repository;
using Cookbook.Helpers;
using Cookbook.Models.Contracts;
using Cookbook.Models.Entities;
using System;
using System.Threading.Tasks;
using Cookbook.Mappings;

namespace Cookbook.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDirectionsRepository _directionsRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        public RecipeService(IRecipeRepository recipeRepository, IUserRepository userRepository, IDirectionsRepository directionsRepository, IImageRepository imageRepository, IIngredientsRepository ingredientsRepository)
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
            _directionsRepository = directionsRepository;
            _imageRepository = imageRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<GetSpecificRecipeContract.GetSpecificRecipeResponse> GetSpecificRecipeAsync(GetSpecificRecipeContract.GetSpecificRecipeRequest request)
        {
            try
            {
                var recipe = await _recipeRepository.GetFirstOrDefaultAsync(x => x.Recipe_ID == request.RecipeID);
                var directions = await _recipeRepository.GetDirectionsAsync(recipe.Recipe_ID);
                var ingredients = await _recipeRepository.GetIngredientsAsync(recipe.Recipe_ID);


                return new GetSpecificRecipeContract.GetSpecificRecipeResponse
                {
                    Directions = directions,
                    Ingredients = ingredients,
                    Recipe = recipe
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new GetSpecificRecipeContract.GetSpecificRecipeResponse
                {
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }

        public async Task<CreateUserContract.CreateUserResponse> SaveUserAsync(CreateUserContract.CreateUserRequest request)
        {
            try
            {
                //Check if the email already exists
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);

                if (user != null)
                {
                    throw new Exception("User already exists.");
                }

                //Create a salt and hash for the password
                var salt = EncryptPassword.CreateSalt();
                var passwordHash = Convert.ToBase64String(salt);
                var password = EncryptPassword.HashPassword(request.Password, salt);

                //Create the user
                var userEntity = Mapping.UserEntity(request, password, passwordHash);

                //Save the user to database and get the user's ID to return
                await _userRepository.AddAndSaveAsync(userEntity);
                user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);

                return await Task.FromResult(new CreateUserContract.CreateUserResponse
                {
                    UserID = user.User_ID
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CreateUserContract.CreateUserResponse
                {
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }

        public async Task<UserLoginContract.UserLoginResponse> UserLoginAsync(UserLoginContract.UserLoginRequest request)
        {
            try
            {
                //Check if the email exists, if it does, get the user's information
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email);

                if (user == null)
                {
                    throw new Exception("Username or Password is incorrect.");
                }

                //Check if the password matches
                var saltByte = Convert.FromBase64String(user.PasswordHash);
                var password = EncryptPassword.HashPassword(request.Password, saltByte);

                var isValid = password == user.Password;

                if (isValid)
                {
                    //Get all the recipes the user has
                    var recipes = await _recipeRepository.GetRecipesAsync(user.User_ID);

                    return Mapping.LoginResponse(user, recipes);
                }

                throw new Exception("Username or Password is incorrect.");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new UserLoginContract.UserLoginResponse
                {
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }

        public async Task<CreateRecipeContract.CreateRecipeResponse> CreateRecipeAsync(CreateRecipeContract.CreateRecipeRequest request)
        {
            try
            {
                //Check if user already has a recipe with this recipe name
                var recipes = await _recipeRepository.GetAllAsync(x => x.RecipeName == request.RecipeName);
                foreach (RecipeEntity r in recipes)
                {
                    if (r.UserID == request.UserID)
                    {
                        throw new Exception("You already have this recipe.");
                    }
                }

                //Check if the image is already in the database, if it is, just use existing image
                var image = await _imageRepository.GetFirstOrDefaultAsync(x => x.Image == request.ImageData);

                if (image == null)
                {
                    //Add Image to the database
                    var imageEntity = new ImageEntity
                    {
                        Image = request.ImageData
                    };

                    //Save image to database and get the image_id
                    await _imageRepository.AddAndSaveAsync(imageEntity);
                    image = await _imageRepository.GetFirstOrDefaultAsync(x => x.Image == request.ImageData);
                }

                //Add Recipe to the database
                var recipeEntity = Mapping.RecipeEntity(request, image);

                //Get the recipe_id
                await _recipeRepository.AddAndSaveAsync(recipeEntity);
                recipes = await _recipeRepository.GetAllAsync(x => x.RecipeName == request.RecipeName);
                var recipe = new RecipeEntity{ };
                foreach(RecipeEntity r in recipes)
                {
                    if(r.UserID == request.UserID)
                    {
                        recipe = r;
                    }
                }
                
                //Add the directions to the database
                int stepCounter = 1;
                foreach (string dir in request.Directions)
                {
                    var directionEntity = new DirectionEntity
                    {
                        StepNumber = stepCounter,
                        DirectionDescription = dir,
                        RecipeID = recipe.Recipe_ID,
                        DateCreated = DateTime.UtcNow
                    };

                    await _directionsRepository.AddAndSaveAsync(directionEntity);
                    stepCounter++;
                }

                //Add the ingredients to the database
                foreach (string ing in request.Ingredients)
                {
                    var ingredientEntity = new IngredientEntity
                    {
                        IngredientName = ing,
                        RecipeID = recipe.Recipe_ID,
                        DateCreated = DateTime.UtcNow
                    };

                    await _ingredientsRepository.AddAndSaveAsync(ingredientEntity);
                }

                //Get the RecipeModel of the recipe just made to add to user's list of recipes
                var recipeModel = await _recipeRepository.GetOneRecipeAsync(recipe.Recipe_ID);

                return new CreateRecipeContract.CreateRecipeResponse
                {
                    RecipeID = recipe.Recipe_ID,
                    Recipe = recipeModel
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CreateRecipeContract.CreateRecipeResponse
                {
                    Error = new Models.ErrorResponse { ErrorMessage = e.Message }
                };
            }
        }
    }
}
