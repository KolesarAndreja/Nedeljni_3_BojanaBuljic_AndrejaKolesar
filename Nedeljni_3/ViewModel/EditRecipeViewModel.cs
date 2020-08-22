using Nedeljni_3.Command;
using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_3.ViewModel
{
    class EditRecipeViewModel:ViewModelBase
    {
        EditRecipe editView;
        Service.Service service = new Service.Service();

        #region Constructor

        public EditRecipeViewModel(EditRecipe editView, tblUser user, tblRecipe recipeToEdit)
        {
            this.editView = editView;
            recipe = recipeToEdit;
            author = user;            
            recipe.authorId = recipeToEdit.authorId;
            
        }
        #endregion

        #region property
        private List<string> _recipeType = new List<string> { "appetizer", "main course", "dessert" };
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
        #endregion

        #region save
        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to save edits to the recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    recipe.type = selectedType;
                    recipe.authorId = author.userId;
                    service.AddRecipe(recipe);
                    tblRecipe editedRecipe = service.AddRecipe(recipe);                    
                    MessageBox.Show("Recipe has been edited.");
                    editView.Close();
                    MessageBoxResult result2 = MessageBox.Show("Do you want to change ingredients too?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        EditIngredients editIngredients = new EditIngredients(editedRecipe);
                        editIngredients.ShowDialog();
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (selectedType != null)
            {
                return true;
            }
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
                editView.Close();

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
