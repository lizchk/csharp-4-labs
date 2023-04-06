using KMA.Lab04.Yakovenko.ViewModels;
using System;
using System.Windows.Controls;

namespace KMA.Lab04.Yakovenko.Views
{
    public partial class FormView : UserControl
    {
        private FormViewModel _viewModel;
        public FormView(Action openUsersView)
        {
            InitializeComponent();
            DataContext = _viewModel = new FormViewModel(openUsersView);
        }
    }
}
