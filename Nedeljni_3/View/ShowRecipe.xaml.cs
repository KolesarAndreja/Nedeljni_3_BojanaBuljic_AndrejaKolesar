using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for ShowRecipe.xaml
    /// </summary>
    public partial class ShowRecipe : Window
    {
        public ShowRecipe(tblRecipe recipe)
        {
            InitializeComponent();
            this.DataContext = new ShowRecipeViewModel(this, recipe);
        }
    }
}
