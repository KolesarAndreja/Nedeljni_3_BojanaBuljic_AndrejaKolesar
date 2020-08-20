using Nedeljni_3.Command;
using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_3.ViewModel
{
    class EditUserViewModel:ViewModelBase
    {
        EditUser view;
        Service.Service service = new Service.Service();

        public EditUserViewModel(EditUser view, tblUser user)
        {
            this.view = view;
            User = user;
        }

        #region Properties
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// Cheks if the user was updated
        /// </summary>
        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command for saving user
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        /// <summary>
        /// Method to execute save command and saves added song
        /// </summary>
        public void SaveExecute()
        {

            try
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    service.EditUser(User);
                    IsUpdateUser = true;
                    MessageBox.Show("Your details succesfully changed.", "Notification", MessageBoxButton.OK);
                    view.Close();
                }
                else
                {
                    view.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
     

        /// <summary>
        /// Method to check if save execution is possible
        /// </summary>
        /// <returns></returns>
        public bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(User.username) || String.IsNullOrEmpty(User.fullname) || String.IsNullOrEmpty(User.password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Cancel command
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Method executing cancel command and not saving song 
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel changes?", "Be sure", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    view.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to check if cancel is possible to be executed
        /// </summary>
        /// <returns>true</returns>
        public bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
