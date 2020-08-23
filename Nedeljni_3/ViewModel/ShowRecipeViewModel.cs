using Nedeljni_3.Command;
using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_3.ViewModel
{
    class ShowRecipeViewModel:ViewModelBase
    {
        ShowRecipe view;
        Service.Service service = new Service.Service();

        public ShowRecipeViewModel(ShowRecipe view,tblRecipe recipe)
        {
            this.view = view;
            Recipe = recipe;

        }

        private tblRecipe _recipe;
        public tblRecipe Recipe
        {
            get { return _recipe; }
            set { _recipe=value; }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// Method executing close command 
        /// </summary>
        public void CloseExecute()
        {

            try
            {
                    view.Close();
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
        public bool CanCloseExecute()
        {
            return true;
        }
    }
}
