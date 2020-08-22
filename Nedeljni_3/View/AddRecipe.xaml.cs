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
        public AddRecipe(tblUser u)
        {
            InitializeComponent();
            this.DataContext = new AddRecipeViewModel(this, u);
        }
    }
}
