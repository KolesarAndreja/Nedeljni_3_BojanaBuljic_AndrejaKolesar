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
        public List<tblRecipe> recipeList
        {
            get
            {
                return _recipeList;
            }
            set
            {
                _recipeList = value;
                OnPropertyChanged("recipeList");
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
        #endregion

        #region constructor
        public UserViewModel(User open, tblUser u)
        {
            userWindow = open;
            currentUser = u;
            recipeList = service.GetAllRecipes();

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
                recipeList = service.SortByTitleAsc(recipeList);
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
                recipeList = service.SortByTitleDesc(recipeList);
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
                recipeList = service.SortByDateDesc(recipeList);
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
                recipeList = service.SortByDateAsc(recipeList);
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
                recipeList = service.SortByNumberOfIngredianceAsc(recipeList);
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
                recipeList = service.SortByNumberOfIngredianceDesc(recipeList);
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
                //CreateMaintenance create = new CreateMaintenance();
                //create.ShowDialog();
                //if ((create.DataContext as CreateMaintenanceViewModel).isUpdated == true)
                //{
                //    recipeList = Service.Service.GetMaintenanceList();
                //}
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
                    _edit = new RelayCommand(param => EditDateExecute(), param => CanEditExecute());
                }
                return _edit;
            }
        }

        private void EditDateExecute()
        {

            try
            {
                //user can edit only his recipes
                if(currentUser.role == "user")
                {
                    if(currentUser.userId == recipe.authorId)
                    {
                        AddRecipe addRecipe = new AddRecipe(recipe);
                        addRecipe.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Only author or admin can edit this recipe");
                    }
                }
                //admin can edit all recipes
                else
                {
                    AddRecipe addRecipe = new AddRecipe(recipe);
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
    }
}
