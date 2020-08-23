using Nedeljni_3.Command;
using Nedeljni_3.Helper;
using Nedeljni_3.Model;
using Nedeljni_3.Validation;
using Nedeljni_3.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Nedeljni_3.ViewModel
{
    class LoginViewModel:ViewModelBase
    {
        Login login;
        Service.Service service = new Service.Service();
        ValidationClass validation = new ValidationClass();

        #region Constructor
        public LoginViewModel(Login login)
        {
            this.login = login;
            CurrentUser = new tblUser();
            UserList = service.GetAllUsers();
            WriteAdminInDb();

        }
        #endregion

        #region Properties   
        private tblUser currentUser;
        public tblUser CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }


        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Log in command
        /// </summary>
        private ICommand logIn;
        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(LogInExecute, CanLogInExecute);
                }
                return logIn;
            }
        }

        /// <summary>
        /// This method checks if username and password are valid.
        /// </summary>
        /// <param name="password">User input for password.</param>
        public void LogInExecute(object obj)
        {
            CurrentUser.password = (obj as PasswordBox).Password;
            bool registered = service.IsRegisteredUser(currentUser.username);
            if (registered)
            {
                tblUser anUser = service.GetUserByUsernameAndPass(currentUser.username, currentUser.password);
                if (anUser != null)
                {
                    if (PasswordHasher.Verify(CurrentUser.password, anUser.password))
                    {
                        MessageBox.Show("Invalid password. Try again");
                    }
                    else
                    {
                        User userview = new User(anUser);
                        login.Close();
                        userview.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid password. Try again");
                }

            }
            else
            {
                if (validation.PasswordChecker(currentUser.password) == true)
                {
                    tblUser newUser = service.AddUser(currentUser.username, currentUser.password);
                    UserList = service.GetAllUsers();
                    MessageBox.Show("Successful registration.", "Notification");                    
                    EditUser edit = new EditUser(newUser);
                    edit.ShowDialog();                    
                    User userview = new User(newUser);
                    login.Close();
                    userview.ShowDialog();
                }
                else
                {
                    MessageBox.Show("The password must have minimum 5 characters.", "Notification");
                }
            }
            
        }

        /// <summary>
        /// Method checks if login can be executed.
        /// </summary>
        /// <param name="password">User input for password.</param>
        /// <returns>True if login can execute, false if not.</returns>
        public bool CanLogInExecute(object obj)
        {
            CurrentUser.password = (obj as PasswordBox).Password;
            if (!String.IsNullOrEmpty(CurrentUser.username) && !String.IsNullOrEmpty(CurrentUser.password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// Method to write admin credentials into Db on starting application if the list of all users are empty
        /// </summary>
        /// <returns></returns>
        private tblUser WriteAdminInDb()
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    if (UserList.Count() == 0)
                    {
                        tblUser admin = new tblUser
                        {
                            username = "Admin",
                            password = PasswordHasher.Hash("Admin123"),
                            fullname = "Administrator",
                            role = "admin"
                        };
                        context.tblUsers.Add(admin);
                        context.SaveChanges();
                        return admin;
                    }
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        #endregion
    }
}
