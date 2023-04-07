using KMA.Lab04.Yakovenko.Tools;
using KMA.Lab04.Yakovenko.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.Lab04.Yakovenko.ViewModels
{
    internal class FormViewModel : INotifyPropertyChanged
    {
        private Person person;
        private RelayCommand<object> _proceedCommand;
        private bool _isEnabled = true;
        private Action usersView;
        
        public DateTime DateOfBirth
        {
            get;
            set;
        } = DateTime.Today;
        public string Name
        {
            get;
            set;
        }
        public string Surname
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ??= new RelayCommand<object>(_ => Proceed(), CanExecute);
            }
        }

        public FormViewModel(Action usersView)
        {
            this.usersView = usersView;
        }
        
        private bool CanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && !string.IsNullOrWhiteSpace(Email);
        }
        internal async void Proceed()
        {
            IsEnabled = false;
            try
            {
                await Task.Run( () =>
                {
                    Person p = new Person(Name, Surname, Email, DateOfBirth);
                    if (string.IsNullOrEmpty(p.Name) || string.IsNullOrEmpty(p.Surname) || string.IsNullOrEmpty(p.Email))
                    {
                        IsEnabled = true;
                        return;
                    }
                    AddPerson addPerson  = new AddPerson();
                    addPerson.Add(p);
                });
                usersView.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsEnabled = true;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
