using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_3.ViewModel
{
    class AddRecipeViewModel:ViewModelBase
    {
        AddRecipe addRecipe;
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
        #region constructor
        public AddRecipeViewModel(AddRecipe open)
        {
            addRecipe = open;
            recipe = new tblRecipe();
        }

        public AddRecipeViewModel(AddRecipe open,tblRecipe rec)
        {
            addRecipe = open;
            recipe = rec;
        }
        #endregion

    }
}
