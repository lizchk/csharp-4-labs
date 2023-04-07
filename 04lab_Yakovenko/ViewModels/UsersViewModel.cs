using KMA.Lab04.Yakovenko.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KMA.Lab04.Yakovenko.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Windows;

namespace KMA.Lab04.Yakovenko.ViewModels
{
    internal class UsersViewModel: INotifyPropertyChanged
    {
        private Action formView;
        private RelayCommand<object> _openFormViewCommand;
        private RelayCommand<object> _deletePersonCommand;
        private ObservableCollection<Person> _collection;
        private Serializer serializer= new Serializer();
        private static string _dateBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab04Users");
        

        public UsersViewModel(Action formView)
        {
            this.formView = formView;
            this._collection = new ObservableCollection<Person>(serializer.ShowPersons());
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
        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _deletePersonCommand ??= new RelayCommand<object>(_ => DeletePerson());
            }
        }
        public void DeletePerson()
        {
            string file = Path.Combine(_dateBase, SelectedPerson.Surname);
            if (!File.Exists(file))
            {
                return;
            }
            File.Delete(file);
            Collection = new ObservableCollection<Person> (serializer.ShowPersons());
        }

        public Person SelectedPerson
        {
            get; set;
        }

        public ObservableCollection<Person> Collection
        {
            get
            {
                return _collection;
            }
            set
            {
                _collection = value;
                NotifyPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
