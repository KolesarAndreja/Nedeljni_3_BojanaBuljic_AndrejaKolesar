using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for AddIngredients.xaml
    /// </summary>
    public partial class AddIngredients : Window
    {
        public AddIngredients(tblRecipe recipe)
        {
            InitializeComponent();
            this.DataContext = new AddIngredientsViewModel(this, recipe);
        }
    }
}
