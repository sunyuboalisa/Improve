using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Improve.Module.Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        public LoginWindowViewModel()
        {

        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private DelegateCommand _loginCmd;
        public DelegateCommand LoginCmd =>
            _loginCmd ?? (_loginCmd = new DelegateCommand(ExecuteLoginCmd));

        void ExecuteLoginCmd()
        {
            if (VerifyUser(UserName, Password))
            {

            }
            else
            {
                
            }
        }

        bool VerifyUser(string name,string password)
        {
            bool res = false;
            return res;
        }
    }
}
