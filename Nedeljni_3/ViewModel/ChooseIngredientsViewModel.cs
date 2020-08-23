using Nedeljni_3.Command;
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
    class ChooseIngredientsViewModel:ViewModelBase
    {
        public static List<string> staticSelectedIngredients;

        ChooseIngredients chooseView;
        Service.Service service = new Service.Service();

        #region constructor
        public ChooseIngredientsViewModel(ChooseIngredients open)
        {
            chooseView = open;
            IngredientList = new List<string>();
        }
        #endregion

        #region property
        private string ingredientName;
        public string IngredientName
        {
            get
            {
                return ingredientName;
            }
            set
            {
                ingredientName = value;
                OnPropertyChanged("IngredientName");
            }
        }

        private List<string> ingredientList;
        public List<string> IngredientList
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

        private string ingredientSingle;
        public string IngredientSingle
        {
            get
            {
                return ingredientSingle;
            }
            set
            {
                ingredientSingle = value;
                OnPropertyChanged("IngredientSingle");
            }
        }
        #endregion

        #region add one
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
        /// <summary>
        /// This method invokes method for selecting ingredient.
        /// </summary>
        public void AddIngredientExecute()
        {
            if (String.IsNullOrEmpty(IngredientName))
            {
                MessageBox.Show("Please fill name.", "Notification");
            }
            else
            {
                IngredientList.Add(IngredientName);
                MessageBox.Show("Ingredient has been selected.");
                IngredientName = null;
            }
        }

        public bool CanAddIngredientExecute()
        {
            return true;
        }
        #endregion

        #region save
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveRecipeExecute(), param => CanSaveRecipeExecute());
                }
                return save;
            }
        }
        public void SaveRecipeExecute()
        {
            if (IngredientList == null || IngredientList.Count == 0)
            {
                MessageBox.Show("Please add ingredients.", "Notification");
            }
            else
            {
                staticSelectedIngredients = IngredientList;
                MessageBox.Show("Ingredients are added.", "Notification", MessageBoxButton.OK);
                chooseView.Close();
            }
        }

        public bool CanSaveRecipeExecute()
        {
            return true;
        }
        #endregion

        #region cancel
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelRecipeExecute(), param => CanCancelRecipeExecute());
                }
                return cancel;
            }
        }
        /// <summary>
        /// This method invokes method for canceling ingredint add.
        /// </summary>
        public void CancelRecipeExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel selecting ingredients?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    chooseView.Close();

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
        #endregion

        #region delete
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
        public void RemoveIngredientExecute()
        {
            try
            {
                if (IngredientSingle != null)
                {
                    //delete ingredient
                    IngredientList.Remove(IngredientSingle);
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
        #endregion
    }
}
