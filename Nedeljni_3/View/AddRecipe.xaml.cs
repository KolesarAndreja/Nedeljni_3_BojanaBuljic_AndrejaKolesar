using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Window
    {
        public AddRecipe(tblRecipe rec)
        {
            InitializeComponent();
            this.DataContext = new AddRecipeViewModel(this, rec);
        }

        public AddRecipe()
        {
            InitializeComponent();
            this.DataContext = new AddRecipeViewModel(this);
        }
    }
}
