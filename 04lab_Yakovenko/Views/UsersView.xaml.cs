using KMA.Lab04.Yakovenko.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMA.Lab04.Yakovenko.Views
{
    public partial class UsersView : UserControl
    {
        private UsersViewModel _viewModel;

        public UsersView(Action openFormView)
        {
            InitializeComponent();
            DataContext = _viewModel = new UsersViewModel(openFormView);
        }

    }
}
