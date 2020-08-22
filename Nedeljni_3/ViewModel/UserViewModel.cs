using Nedeljni_3.Model;
using Nedeljni_3.View;
using Nedeljni_3.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nedeljni_3.Command;
using System.Windows;

namespace Nedeljni_3.ViewModel
{
    class UserViewModel:ViewModelBase
    {

        public User userWindow;
        Service.Service service = new Service.Service();
        #region property
        private tblUser _currentUser;
        public tblUser currentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged("currentUser");
            }
        }

        private List<tblRecipe> _recipeList;
        public List<tblRecipe> RecipeList
        {
            get
            {
                return _recipeList;
            }
            set
            {
                _recipeList = value;
                OnPropertyChanged("RecipeList");
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

        private bool isDeleted = false;
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
        #endregion

        #region constructor
        public UserViewModel(User open, tblUser u)
        {
            userWindow = open;
            currentUser = u;
            RecipeList = service.GetAllRecipes();

        }
        #endregion

        #region sort
        private ICommand _titleAsc;
        public ICommand titleAsc
        {
            get
            {
                if (_titleAsc == null)
                {
                    _titleAsc = new RelayCommand(param => TitleAscExecute(), param => CanTitleAscExecute());
                }
                return _titleAsc;
            }
        }
        private void TitleAscExecute()
        {

            try
            {
                RecipeList = service.SortByTitleAsc(RecipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanTitleAscExecute()
        {
            return true;
        }


        private ICommand _titleDesc;
        public ICommand titleDesc
        {
            get
            {
                if (_titleDesc == null)
                {
                    _titleDesc = new RelayCommand(param => TitleDescExecute(), param => CanTitleDescExecute());
                }
                return _titleDesc;
            }
        }
        private void TitleDescExecute()
        {

            try
            {
                RecipeList = service.SortByTitleDesc(RecipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanTitleDescExecute()
        {
            return true;
        }

        private ICommand _dateDesc;
        public ICommand dateDesc
        {
            get
            {
                if (_dateDesc == null)
                {
                    _dateDesc = new RelayCommand(param => DateDescExecute(), param => CanDateDescExecute());
                }
                return _dateDesc;
            }
        }
        private void DateDescExecute()
        {

            try
            {
                RecipeList = service.SortByDateDesc(RecipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanDateDescExecute()
        {
            return true;
        }

        private ICommand _dateAsc;
        public ICommand dateAsc
        {
            get
            {
                if (_dateAsc == null)
                {
                    _dateAsc = new RelayCommand(param => DateAscExecute(), param => CanDateAscExecute());
                }
                return _dateAsc;
            }
        }
        private void DateAscExecute()
        {

            try
            {
                RecipeList = service.SortByDateAsc(RecipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanDateAscExecute()
        {
            return true;
        }

        private ICommand _numberAsc;
        public ICommand numberAsc
        {
            get
            {
                if (_numberAsc == null)
                {
                    _numberAsc = new RelayCommand(param => NumberAscExecute(), param => CanNumberAscExecute());
                }
                return _numberAsc;
            }
        }
        private void NumberAscExecute()
        {

            try
            {
                RecipeList = service.SortByNumberOfIngredianceAsc(RecipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanNumberAscExecute()
        {
            return true;
        }

        private ICommand _numberDesc;
        public ICommand numberDesc
        {
            get
            {
                if (_numberDesc == null)
                {
                    _numberDesc = new RelayCommand(param => NumberDescExecute(), param => CanNumberDescExecute());
                }
                return _numberDesc;
            }
        }
        private void NumberDescExecute()
        {

            try
            {
                RecipeList = service.SortByNumberOfIngredianceDesc(RecipeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanNumberDescExecute()
        {
            return true;
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
                Login login = new Login();
                userWindow.Close();
                login.Show();

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


        #region Visibility
        private Visibility _visibilityAdmin;
        public Visibility visibilityAdmin
        {
            get
            {
                if (currentUser.role == "admin")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                _visibilityAdmin = value;
                OnPropertyChanged("visibilityAdmin");
            }
        }

        private Visibility _visibilityUser;
        public Visibility visibilityUser
        {
            get
            {
                if (currentUser.role == "user")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                _visibilityUser = value;
                OnPropertyChanged("visibilityUser");
            }
        }
        #endregion

        #region add command
        private ICommand _addRecipe;
        public ICommand addRecipe
        {
            get
            {
                if (_addRecipe == null)
                {
                    _addRecipe = new RelayCommand(param => AddRecipeExecute(), param => CanAddRecipeExecute());
                }
                return _addRecipe;
            }
        }

        private void AddRecipeExecute()
        {
            try
            {
                AddRecipe addView = new AddRecipe();
                addView.ShowDialog();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddRecipeExecute()
        {
            return true;
        }
        #endregion

        #region edit
        private ICommand _edit;
        public ICommand edit
        {
            get
            {
                if (_edit == null)
                {
                    _edit = new RelayCommand(param => EditExecute(), param => CanEditExecute());
                }
                return _edit;
            }
        }

        private void EditExecute()
        {

            try
            {
                //author can edit only his recipes
                if(currentUser.role == "user")
                {
                    if(currentUser.userId == recipe.authorId)
                    {
                        AddRecipe addRecipe = new AddRecipe(recipe,currentUser);
                        addRecipe.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Only author or admin can edit this Recipe");
                    }
                }
                //admin can edit all recipes
                else
                {                    
                    AddRecipe addRecipe = new AddRecipe(recipe,currentUser);
                    addRecipe.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditExecute()
        {
            return true;
        }
        #endregion

        #region delete
        private ICommand _delete;
        public ICommand delete
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new Command.RelayCommand(param => DeleteExecute(), param => CanDeleteExecute());

                }
                return _delete;
            }
        }

        private void DeleteExecute()
        {
            MessageBoxResult result = MessageBox.Show("Do you realy want to delete this Recipe?", "Delete Report", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                service.DeleteRecipe(recipe);
                IsDeleted = true;
                MessageBox.Show("Recipe is deleted.", "Notification", MessageBoxButton.OK);
                RecipeList = service.GetAllRecipes();                
            }
        }
        private bool CanDeleteExecute()
        {
            return true;
        }
        #endregion

        #region Read recipe
        private ICommand _readRecipe;
        public ICommand readRecipe
        {
            get
            {
                if (_readRecipe == null)
                {
                    _readRecipe = new RelayCommand(param => ReadRecipeExecute(), param => CanReadRecipeExecute());
                }
                return _readRecipe;
            }
        }

        private void ReadRecipeExecute()
        {
            try
            { 
                recipe=service.GetSelectedRecipe(recipe.recipeId);
                ShowRecipe showRecipe = new ShowRecipe(recipe);
                showRecipe.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanReadRecipeExecute()
        {
            return true;
        }
        #endregion
    }
}
