using Nedeljni_3.Command;
using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_3.ViewModel
{
    class AddRecipeViewModel:ViewModelBase
    {
        AddRecipe addRecipe;
        Service.Service service = new Service.Service();

        #region constructor
        public AddRecipeViewModel(AddRecipe open, tblUser user)
        {
            addRecipe = open;
            recipe = new tblRecipe();
            author = user;            
            recipe.authorId = user.userId; 
        }
        #endregion

        #region property
        private List<string> _recipeType= new List<string> { "appetizer", "main course", "dessert" };
        public List<string> recipeType
        {

            get
            {
                return _recipeType;
            }
            set
            {
                _recipeType = value;
                OnPropertyChanged("recipeType");
            }
        }

        private string _selectedType;
        public string selectedType
        {

            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                OnPropertyChanged("selectedType");
            }
        }

        private tblRecipe _recipe;
        public tblRecipe recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                _recipe = value;
                OnPropertyChanged("recipe");
            }
        }

        private tblUser _author;
        public tblUser author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged("author");
            }
        }

        public bool isUpdated = false;
        #endregion


        #region save
        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param=>SaveExecute(), param=>CanSaveExecute());
                }
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                    recipe.type = selectedType;
                    service.AddRecipe(recipe);
                    tblRecipe addedRecipe = service.AddRecipe(recipe);
                    MessageBox.Show("Recipe has been added. Add ingredients for this recipe");
                    addRecipe.Close();                    
                    AddIngredients addIngredients = new AddIngredients(addedRecipe);                    
                    addIngredients.ShowDialog();
                    isUpdated = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if(selectedType!=null && !String.IsNullOrEmpty(recipe.description) && !String.IsNullOrEmpty(recipe.title) && recipe.numberOfPersons>0)
                return true;
            else
            {
                return false;
            }
        }
        #endregion

        #region logout
        private ICommand _logOut;
        public ICommand logOut
        {
            get
            {
                if (_logOut == null)
                {
                    _logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return _logOut;
            }
        }

        private void LogOutExecute()
        {
            try
            {
                addRecipe.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLogOutExecute()
        {
            return true;
        }
        #endregion
    }
}
