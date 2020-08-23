using Nedeljni_3.ViewModel;
using System.Windows;


namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for ChooseIngredients.xaml
    /// </summary>
    public partial class ChooseIngredients : Window
    {
        public ChooseIngredients()
        {
            InitializeComponent();
            this.DataContext = new ChooseIngredientsViewModel(this);
        }
    }
}
