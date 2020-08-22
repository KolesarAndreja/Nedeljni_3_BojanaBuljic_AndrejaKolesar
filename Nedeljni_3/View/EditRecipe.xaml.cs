using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for EditRecipe.xaml
    /// </summary>
    public partial class EditRecipe : Window
    {
        public EditRecipe(tblUser user, tblRecipe recipeToEdit)
        {
            InitializeComponent();
            this.DataContext = new EditRecipeViewModel(this, user, recipeToEdit);
        }
    }
}
