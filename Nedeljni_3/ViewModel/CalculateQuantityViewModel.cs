using Nedeljni_3.Command;
using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nedeljni_3.ViewModel
{
    class CalculateQuantityViewModel:ViewModelBase
    {
        CalculateQuantity view;
        Service.Service service = new Service.Service();

        public CalculateQuantityViewModel(CalculateQuantity open, tblRecipe rec)
        {
            view = open;
            recipe = rec;
            allIngredients = service.AllIngredientForRecipe(rec.recipeId);
            list = new List<Quantity>();
        }

        #region property
        private tblRecipe _recipe;
        public tblRecipe recipe
        {
            get { return _recipe; }
            set { _recipe = value; }
        }

        private int _number;
        public int number
        {

            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("number");
            }
        }

        private Quantity _quantityObject;
        public Quantity quantityObject
        {

            get
            {
                return _quantityObject;
            }
            set
            {
                _quantityObject = value;
                OnPropertyChanged("quantityObject");
            }
        }

        private List<tblIngredient> _allIngredients;
        public List<tblIngredient> allIngredients
        {

            get
            {
                return _allIngredients;
            }
            set
            {
                _allIngredients = value;
                OnPropertyChanged("allIngredients");
            }
        }

        private List<Quantity> _list;
        public List<Quantity> list
        {

            get
            {
                return _list;
            }
            set
            {
                _list = value;
                OnPropertyChanged("list");
            }
        }


        private List<Quantity> _tempList;
        public List<Quantity> tempList
        {

            get
            {
                return _tempList;
            }
            set
            {
                _tempList = value;
                OnPropertyChanged("tempList");
            }
        }
        #endregion

        #region close
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

        #endregion

        #region calculate
        private ICommand _calculate;
        public ICommand calculate
        {
            get
            {
                if (_calculate == null)
                {
                    _calculate = new RelayCommand(param => CalculateExecute(), param => CanCalculateExecute());
                }
                return _calculate;
            }
        }
        /// <summary>
        /// Method executing calulate command 
        /// </summary>
        public void CalculateExecute()
        {
            try
            {
                if (number <= 0)
                {
                    MessageBox.Show("Please enter positive number.", "Notification");
                }
                else
                {
                    tempList = new List<Quantity>();
                    if (allIngredients.Count == 0)
                    {
                        MessageBox.Show("Recipe is not completed, so you cannot calculate quantity for it");
                    }
                    else
                    {
                        for (int i = 0; i < allIngredients.Count; i++)
                        {
                            Quantity q = Quantity.calculateOneIngredient(allIngredients[i], recipe.numberOfPersons, number);
                            tempList.Add(q);
                        }
                        list = tempList;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to check if calulate is possible to be executed
        /// </summary>
        /// <returns>true</returns>
        public bool CanCalculateExecute()
        {
            return true;
        }
        #endregion
    }
}
