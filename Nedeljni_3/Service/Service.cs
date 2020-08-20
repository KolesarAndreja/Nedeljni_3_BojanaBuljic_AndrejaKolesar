using Nedeljni_3.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_3.Service
{
    class Service
    {

        #region recipes list by type, name, ingredients
        //get all
        public List<tblRecipe> GetAllRecipes()
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {

                    List<tblRecipe> list = new List<tblRecipe>();
                    list = (from x in context.tblRecipes select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>list of users</returns>
        public List<tblUser> GetAllUsers()

        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }


        //return a list of those recipes that contain the given string in its title
        public List<tblRecipe> GetRecipesByTitle(List<tblRecipe> recipes, string title)
        {
            List<tblRecipe> result = new List<tblRecipe>();
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].title.Contains(title))
                {
                    result.Add(recipes[i]);
                }
            }
            return result;
        }


        //return a list of those recipes that have given type
        public List<tblRecipe> GetRecipesByType(List<tblRecipe> recipes, string type)
        {
            List<tblRecipe> result = new List<tblRecipe>();
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].type == type)
                {
                    result.Add(recipes[i]);
                }
            }
            return result;
        }

        public List<tblRecipe> GetRecipesByIngredients(List<tblRecipe> recipes, List<string> ingredients)
        {
            List<tblRecipe> result = new List<tblRecipe>();
            for (int i = 0; i < recipes.Count; i++)
            {
                int id = recipes[i].recipeId;
                //the number of ingredients that match
                int num = 0;

                for (int j = 0; j < ingredients.Count; j++)
                {
                    if (IsRecipeIngredient(id, ingredients[j]))
                    {
                        num++;
                    }
                }

                if (num == ingredients.Count)
                {
                    result.Add(recipes[i]);
                }
            }
            return result;
        }


        public bool IsRecipeIngredient(int recipeId, string ingredient)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    bool r = (from x in context.tblIngredients where x.recipeId == recipeId && x.name == ingredient select x).Any();
                    return r;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public  bool IsRegisteredUser(string username)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    bool result = (from x in context.tblUsers where x.username == username select x).Any();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }


        //get all indgredians for one recipe
        public List<tblIngredient> AllIngredientForRecipe(int recipeId)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    List<tblIngredient> r = (from x in context.tblIngredients where x.recipeId == recipeId select x).ToList();
                    return r;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="userName"></param>    
        /// <param name="pass"></param>
        /// <returns>user</returns>
        public tblUser AddUser(string userName, string pass)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    tblUser user = new tblUser();
                    user.fullname = userName;
                    user.username = userName;
                    user.password = pass;
                    user.role = "user";
                    context.tblUsers.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        #endregion

        #region sort lists
        //title
        public List<tblRecipe> SortByTitleAsc(List<tblRecipe> recipes)
        {
            List<tblRecipe> sortedList = recipes.OrderBy(o => o.title).ToList();
            return sortedList;
        }

        public List<tblRecipe> SortByTitleDesc(List<tblRecipe> recipes)
        {
            List<tblRecipe> sortedList = recipes.OrderByDescending(o => o.title).ToList();
            return sortedList;
        }

        //date
        public List<tblRecipe> SortByDateDesc(List<tblRecipe> recipes)
        {
            List<tblRecipe> sortedList = recipes.OrderByDescending(o => o.creationDate).ToList();
            return sortedList;
        }

        public List<tblRecipe> SortByDateAsc(List<tblRecipe> recipes)
        {
            List<tblRecipe> sortedList = recipes.OrderBy(o => o.creationDate).ToList();
            return sortedList;
        }

        //number of ingredience
        public List<tblRecipe> SortByNumberOfIngredianceAsc(List<tblRecipe> recipes)
        {
            List<tblRecipe> list = new List<tblRecipe>();
            Dictionary<tblRecipe, int> dict = new Dictionary<tblRecipe, int>();
            for (int i = 0; i < recipes.Count; i++)
            {
                dict.Add(recipes[i], AllIngredientForRecipe(recipes[i].recipeId).Count);
            }
            var orderedDict = dict.OrderBy(x => x.Value);
            foreach (var v in orderedDict)
            {
                list.Add(v.Key);
            }
            return list;
        }

        public List<tblRecipe> SortByNumberOfIngredianceDesc(List<tblRecipe> recipes)
        {
            List<tblRecipe> list = new List<tblRecipe>();
            Dictionary<tblRecipe, int> dict = new Dictionary<tblRecipe, int>();
            for (int i = 0; i < recipes.Count; i++)
            {
                dict.Add(recipes[i], AllIngredientForRecipe(recipes[i].recipeId).Count);
            }
            var orderedDict = dict.OrderByDescending(x => x.Value);
            foreach (var v in orderedDict)
            {
                list.Add(v.Key);
            }
            return list;
        }
        #endregion

        #region delete recipe
        public void DeleteRecipe(tblRecipe recipe)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    //first, we must delete ingredients with this recipeId
                    List<tblIngredient> ing = AllIngredientForRecipe(recipe.recipeId);
                    for (int i = 0; i < ing.Count; i++)
                    {
                        tblIngredient ingToDelete = (from u in context.tblIngredients where u.ingridientId == ing[i].ingridientId select u).First();
                        context.tblIngredients.Remove(ingToDelete);
                        context.SaveChanges();
                    }
                    //now we can remove our recipe
                    tblRecipe toDelete = (from u in context.tblRecipes where u.recipeId == recipe.recipeId select u).First();
                    context.tblRecipes.Remove(toDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        #endregion


        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="userName"></param>    
        /// <param name="pass"></param>
        /// <returns>user</returns>
        public tblUser EditUser(tblUser user)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    tblUser userToEdit = (from x in context.tblUsers where x.userId == user.userId select x).First();
                    userToEdit.fullname = user.fullname;
                    userToEdit.username = user.username;                    
                    userToEdit.password = user.password;
                    userToEdit.role = "user";
                    userToEdit.userId = user.userId;
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        #region add
        public tblRecipe AddRecipe(tblRecipe recipe)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    //add 
                    if (recipe.recipeId == 0)
                    {
                        
                        tblRecipe newRecipe = new tblRecipe();
                        newRecipe.authorId = recipe.authorId;
                        newRecipe.creationDate = DateTime.Now;
                        newRecipe.description = recipe.description;
                        newRecipe.numberOfPersons = recipe.numberOfPersons;
                        newRecipe.title = recipe.title;
                        newRecipe.type = recipe.type;
                        context.tblRecipes.Add(newRecipe);
                        context.SaveChanges();
                        recipe.recipeId = newRecipe.recipeId;
                        return recipe;
                    }
                    //edit
                    else
                    {
                        tblRecipe recipeToEdit = (from x in context.tblRecipes where x.recipeId == recipe.recipeId select x).FirstOrDefault();
                        recipeToEdit.authorId = recipe.authorId;
                        recipeToEdit.creationDate = DateTime.Now;
                        recipeToEdit.description = recipe.description;
                        recipeToEdit.numberOfPersons = recipe.numberOfPersons;
                        recipeToEdit.title = recipe.title;
                        recipeToEdit.type = recipe.type;
                        context.SaveChanges();
                        return recipe;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }


        public tblIngredient AddIngredient(tblIngredient ingredient)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    if ( ingredient.ingridientId == 0)
                    {
                        //add 
                        tblIngredient newIng = new tblIngredient();
                        newIng.name = ingredient.name;
                        newIng.quantity = ingredient.quantity;
                        context.SaveChanges();
                        ingredient.ingridientId = newIng.ingridientId;
                        return ingredient;
                    }
                    else
                    {
                        tblIngredient ingToEdit = (from x in context.tblIngredients where x.ingridientId == ingredient.ingridientId select x).FirstOrDefault();

                        ingToEdit.name = ingredient.name;
                        ingToEdit.quantity = ingredient.quantity;
                        context.SaveChanges();
                        return ingredient;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }
        #endregion
        /// <summary>
        /// Gets user by forwarded username.
        /// </summary>
        /// <param name="userName">User's username</param>   
        /// <param name="pass">User's password</param>
        /// <returns>User.</returns>
        public tblUser GetUserByUsernameAndPass(string userName, string pass)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {

                    return context.tblUsers.Where(x => x.username == userName && x.password == pass).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
            
        }

    }
}
