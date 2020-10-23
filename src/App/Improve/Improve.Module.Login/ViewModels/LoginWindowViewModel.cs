using Improve.LoggerLib;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
                Login();
            }
            else
            {
                ShowErrorMsg();
            }
        }

        bool VerifyUser(string name,string password)
        {
            bool res = false;
            return res;
        }

        void Login()
        {
            Logger.Log(Level.Debug, $"{UserName} is logining.");

        }

        void ShowErrorMsg()
        {
            var win = Application.Current.MainWindow as MetroWindow;
            win.ShowMessageAsync("Error", "用户名或者密码错误");
        }
    }
}
