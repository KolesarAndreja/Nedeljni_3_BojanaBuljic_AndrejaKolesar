using Nedeljni_3.Command;
using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_3.ViewModel
{
    class AddIngredientsViewModel:ViewModelBase
    {

        AddIngredients addIngredientsView;
        Service.Service service = new Service.Service();        

        public AddIngredientsViewModel(AddIngredients addIngredientsOpen, tblRecipe recipeCreated)
        {
            addIngredientsView = addIngredientsOpen;           
            recipe = recipeCreated;
            Ingredient = new tblIngredient();            
            ingredient.recipeId = recipeCreated.recipeId;
        }
        private tblRecipe recipe;
        public tblRecipe Recipe
        {
            get
            {
                return recipe;
            }
            set
            {
                recipe = value;
                OnPropertyChanged("Recipe");
            }
        }

        private tblIngredient ingredient;
        public tblIngredient Ingredient
        {
            get
            {
                return ingredient;
            }
            set
            {
                ingredient = value;
                OnPropertyChanged("Ingredient");
            }
        }

        private List<tblIngredient> ingredientList;
        public List<tblIngredient> IngredientList
        {
            get
            {
                return ingredientList;
            }
            set
            {
                ingredientList = value;
                OnPropertyChanged("IngredientList");
            }
        }

        private ICommand removeIngredient;
        public ICommand RemoveIngredient
        {
            get
            {
                if (removeIngredient == null)
                {
                    removeIngredient = new RelayCommand(param => RemoveIngredientExecute(), param => CanRemoveIngredientExecute());
                }
                return removeIngredient;
            }
        }

        private ICommand addIngredient;
        public ICommand AddIngredient
        {
            get
            {
                if (addIngredient == null)
                {
                    addIngredient = new RelayCommand(param => AddIngredientExecute(), param => CanAddIngredientExecute());
                }
                return addIngredient;
            }
        }

        private ICommand cancelRecipe;
        public ICommand CancelRecipe
        {
            get
            {
                if (cancelRecipe == null)
                {
                    cancelRecipe = new RelayCommand(param => CancelRecipeExecute(), param => CanCancelRecipeExecute());
                }
                return cancelRecipe;
            }
        }

        private ICommand saveRecipe;
        public ICommand SaveRecipe
        {
            get
            {
                if (saveRecipe == null)
                {
                    saveRecipe = new RelayCommand(param => SaveRecipeExecute(), param => CanSaveRecipeExecute());
                }
                return saveRecipe;
            }
        }

        public void RemoveIngredientExecute()
        {
            try
            {
                if (Ingredient != null)
                {
                    //invokes method to delete ingredient
                    service.DeleteIngredient(Ingredient);
                    //invokes method to update list of ingredients
                    IngredientList = service.AllIngredientForRecipe(Recipe.recipeId);
                    Ingredient = new tblIngredient();
                    Ingredient.recipeId= Recipe.recipeId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanRemoveIngredientExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for adding ingredient to recipe.
        /// </summary>
        public void AddIngredientExecute()
        {
            if (String.IsNullOrEmpty(Ingredient.name) || String.IsNullOrEmpty(Ingredient.name.ToString()) || Ingredient.quantity == 0)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    service.AddIngredient(Ingredient);
                    
                    //invokes method to update a list of ingredients
                    IngredientList = service.AllIngredientForRecipe(Recipe.recipeId);
                    Ingredient = new tblIngredient();
                    Ingredient.recipeId = Recipe.recipeId;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanAddIngredientExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for canceling ingredint add.
        /// </summary>
        public void CancelRecipeExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel adding ingredients to recipe? \nRecipe will not be complete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {                    
                    MessageBox.Show("Adding ingredients to recipe is canceled.", "Notification", MessageBoxButton.OK);
                    addIngredientsView.Close();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelRecipeExecute()
        {
            return true;
        }

        public void SaveRecipeExecute()
        {
            if (IngredientList == null || IngredientList.Count == 0)
            {
                MessageBox.Show("Please first add ingredients.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save ingredients to the recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        service.AddRecipe(Recipe);
                        if (Recipe.recipeId != 0)
                        {
                            //if user select item from list and then changes
                            foreach (var ingredient in IngredientList)
                            {
                                service.AddIngredient(Ingredient);
                            }
                            MessageBox.Show("Recipe is completed.", "Notification", MessageBoxButton.OK);                        
                             addIngredientsView.Close();
                             
                            
                        }
                        else
                        {
                            MessageBox.Show("Ingredient cannot be added.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanSaveRecipeExecute()
        {
            return true;
        }
    }
}
