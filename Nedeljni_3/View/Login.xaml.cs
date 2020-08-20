using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
        }
    }
}
