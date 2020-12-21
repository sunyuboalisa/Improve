using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Improve.Module.NavigationBar.ViewModels
{
    public class NavigationBarDefaultViewModel : BindableBase
    {

        private bool _isVisibled;
        public bool IsVisibled
        {
            get { return _isVisibled; }
            set { SetProperty(ref _isVisibled, value); }
        }

        public NavigationBarDefaultViewModel()
        {

        }
    }
}
