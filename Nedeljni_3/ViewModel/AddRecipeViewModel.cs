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

        private tblIngredient _singleIngreient;
        public tblIngredient singleIngreient
        {
            get
            {
                return _singleIngreient;
            }
            set
            {
                _singleIngreient = value;
                OnPropertyChanged("singleIngreient");
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

        private List<tblIngredient> _myIngredients;
        public List<tblIngredient> myIngredients
        {
            get
            {
                return _myIngredients;
            }
            set
            {
                _myIngredients = value;
                OnPropertyChanged("myIngredients");
            }
        }

        private bool isEditingWindow = false;

        #endregion

        #region constructor
        public AddRecipeViewModel(AddRecipe open, tblUser user)
        {
            addRecipe = open;
            recipe = new tblRecipe();
            author = user;
            myIngredients = new List<tblIngredient>(15);
            for (int i = 0; i < 15; i++)
            {
                tblIngredient ing = new tblIngredient();
                ing.name = "";
                myIngredients.Add(ing);
            }
        }

        public AddRecipeViewModel(AddRecipe open,tblRecipe rec, tblUser user)
        {
            addRecipe = open;
            recipe = rec;
            author = user;
            isEditingWindow = true;
            myIngredients = new List<tblIngredient>(15);
            for (int i = 0; i < 11; i++)
            {
                tblIngredient ing = new tblIngredient();
                ing.name = "";
                myIngredients.Add(ing);
            }
        }

        public AddRecipeViewModel(AddRecipe open)
        {
            addRecipe = open;
            myIngredients = new List<tblIngredient>(15);
            recipe = new tblRecipe();
            for (int i = 0; i < 15; i++)
            {
                tblIngredient ing = new tblIngredient();
                ing.name = "";
                myIngredients.Add(ing);
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
                    _save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return _save;
            }
        }

        private void SaveExecute(object obj)
        {
            try
            {
                var b = (from x in myIngredients where x.name!= "" && x.quantity > 0 select x).Any();
                if (b)
                {
                    var list1 = (from x in myIngredients where x.name!=""|| x.quantity > 0 select x).ToList();
                    var list2 = (from x in myIngredients where x.name!="" && x.quantity > 0 select x).ToList();
                    if (list1.Count == list2.Count )
                    {
                        recipe.authorId = author.userId;
                        recipe.type = selectedType;
                        tblRecipe addedRecipe = service.AddRecipe(recipe);
                        if (addedRecipe != null)
                        {
                            int id = addedRecipe.recipeId;
                            for (int i = 0; i < 15; i++)
                            {
                                if (myIngredients[i].name != "" && myIngredients[i].quantity > 0)
                                {
                                    myIngredients[i].recipeId = id;
                                    myIngredients[i].status = "unresolved";
                                    service.AddIngredient(myIngredients[i]);
                                }
                            }

                            if (isEditingWindow)
                            {
                                MessageBox.Show("Recipe has been edited");
                                addRecipe.Close();
                            }
                            else
                            {
                                MessageBox.Show("Recipe has been added");
                                addRecipe.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingredient quantity must be positive. Ingredient name cannot be empty.");
                    }
                }
                else
                {
                    MessageBox.Show("Recipe must include at least one ingredient");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute(object obj)
        {
            if (!String.IsNullOrEmpty(recipe.description) && !String.IsNullOrEmpty(recipe.title) && recipe.numberOfPersons != 0 )
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
