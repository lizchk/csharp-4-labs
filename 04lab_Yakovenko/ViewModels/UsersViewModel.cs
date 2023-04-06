using KMA.Lab04.Yakovenko.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.Lab04.Yakovenko.ViewModels
{
    internal class UsersViewModel
    {
        private Action formView;
        private RelayCommand<object> _openFormViewCommand;

        public UsersViewModel(Action formView)
        {
            this.formView = formView;
        }
        public RelayCommand<object> OpenFormViewCommand
        {
            get
            {
                return _openFormViewCommand ??= new RelayCommand<object>(_ => OpenFormView());
            }
        }
        public void OpenFormView()
        {
            formView?.Invoke();
        }
    }
}
