using Nedeljni_3.Model;
using Nedeljni_3.ViewModel;
using System.Windows;

namespace Nedeljni_3.View
{
    /// <summary>
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser(tblUser user)
        {
            InitializeComponent();
            this.DataContext = new EditUserViewModel(this, user);
        }
    }
}
