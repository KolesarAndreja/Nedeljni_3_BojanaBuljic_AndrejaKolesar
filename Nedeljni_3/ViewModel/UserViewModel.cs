using Nedeljni_3.Model;
using Nedeljni_3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_3.ViewModel
{
    class UserViewModel:ViewModelBase
    {
        User userView;

        public UserViewModel(User view,tblUser user)
        {
            userView = view;
            User = user;
        }

        private tblUser user;
        public tblUser User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
    }
}
