using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this, user);
        }
    }
}
