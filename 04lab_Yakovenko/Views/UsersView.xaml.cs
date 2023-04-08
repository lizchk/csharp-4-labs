using KMA.Lab04.Yakovenko.ViewModels;
using System;
using System.Windows.Controls;
using KMA.Lab04.Yakovenko.Models;
using KMA.Lab04.Yakovenko.Tools;

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

        private async void EditUser(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editedPerson = e.EditingElement.DataContext as Person;

            await Serializer.AddPerson(editedPerson);
        }

        private void DataGrid_OnPreparingCellForEdit(object? sender, DataGridPreparingCellForEditEventArgs e)
        {
            Serializer.DeletePerson(e.EditingElement.DataContext as Person);
        }
    }
}








