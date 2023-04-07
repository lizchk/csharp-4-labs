using KMA.Lab04.Yakovenko.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

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
   

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
