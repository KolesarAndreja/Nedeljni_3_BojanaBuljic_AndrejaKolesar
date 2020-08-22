using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for EditIngredients.xaml
    /// </summary>
    public partial class EditIngredients : Window
    {
        public EditIngredients(tblRecipe recipeForEdit)
        {
            InitializeComponent();
            this.DataContext = new EditIngredientsViewModel(this, recipeForEdit);
        }
    }
}
