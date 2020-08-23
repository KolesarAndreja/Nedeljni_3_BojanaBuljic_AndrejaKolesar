using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for CalculateQuantity.xaml
    /// </summary>
    public partial class CalculateQuantity : Window
    {
        public CalculateQuantity(tblRecipe recipe)
        {
            InitializeComponent();
            this.DataContext = new CalculateQuantityViewModel(this, recipe);
        }
    }
}
