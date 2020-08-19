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

        //return a list of those recipes that contain the given string in its title
        public List<tblRecipe> GetRecipesByTitle(List<tblRecipe> recipes, string title)
        {
            List<tblRecipe> result = new List<tblRecipe>();
            for(int i = 0; i<recipes.Count; i++)
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

                for(int j = 0; j<ingredients.Count; j++)
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
            for(int i = 0; i<recipes.Count; i++)
            {
                dict.Add(recipes[i], AllIngredientForRecipe(recipes[i].recipeId).Count);
            }
            var orderedDict =  dict.OrderBy(x => x.Value);
            foreach(var v in orderedDict)
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
    }
}
