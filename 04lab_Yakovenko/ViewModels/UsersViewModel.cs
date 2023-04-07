using KMA.Lab04.Yakovenko.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using KMA.Lab04.Yakovenko.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KMA.Lab04.Yakovenko.ViewModels
{
    internal class UsersViewModel: INotifyPropertyChanged
    {
        private Action formView;
        private RelayCommand<object> _openFormViewCommand;
        private ObservableCollection<Person> _collection;
        private Serializer serializer= new Serializer();

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
