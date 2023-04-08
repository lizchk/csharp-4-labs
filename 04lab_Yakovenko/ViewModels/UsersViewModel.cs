using KMA.Lab04.Yakovenko.Tools;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using KMA.Lab04.Yakovenko.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Linq;

namespace KMA.Lab04.Yakovenko.ViewModels
{
    internal class UsersViewModel : INotifyPropertyChanged
    {
        private Action formView;
        private RelayCommand<object> _openFormViewCommand;
        private ObservableCollection<Person> _collection;
        private Serializer _serializer = new Serializer();

        private SunSign selectedSunSign = SunSign.All;
        private ChineseSign selectedChineseSign = ChineseSign.All;

        private static string _dateBase =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab04Users");

        public SunSign SelectedSunSign
        {
            set
            {
                People.Clear();

                selectedSunSign = value;

                if (value == SunSign.All && selectedChineseSign == ChineseSign.All)
                {
                    foreach (var item in _collection)
                        People.Add(item);
                }
                else if (value == SunSign.All)
                {
                    foreach (var item in _collection.Where(x => x.ChineseSign == SelectedChineseSign).ToArray())
                        People.Add(item);
                }
                else if (SelectedChineseSign == ChineseSign.All)
                {
                    foreach (var item in _collection.Where(x => x.SunSign == value).ToArray())
                        People.Add(item);
                }
                else
                {
                    foreach (var item in _collection.Where(x => x.SunSign == value && x.ChineseSign == SelectedChineseSign).ToArray())
                        People.Add(item);
                }

            }

            get { return selectedSunSign; }
        }

        public ChineseSign SelectedChineseSign
        {
            set
            {
                People.Clear();

                selectedChineseSign = value;

                if (value == ChineseSign.All && selectedSunSign == SunSign.All)
                {
                    foreach (var item in _collection)
                        People.Add(item);
                }
                else if (value == ChineseSign.All)
                {
                    foreach (var item in _collection.Where(x => x.SunSign == selectedSunSign).ToArray())
                        People.Add(item);
                }
                else if (selectedSunSign == SunSign.All)
                {
                    foreach (var item in _collection.Where(x => x.ChineseSign == value).ToArray())
                        People.Add(item);
                }
                else
                {
                    foreach (var item in _collection.Where(x => x.ChineseSign == value && x.SunSign == selectedSunSign).ToArray())
                        People.Add(item);
                }


            }
            get
            {
                return selectedChineseSign;
            }
        }

        public ObservableCollection<Person> People { get; set; }

        public UsersViewModel(Action formView)
        {
            this.formView = formView;

            if (!Directory.EnumerateFileSystemEntries(Serializer.DbPath).Any())
                foreach (var person in InitialData.InitialPeople)
                    Serializer.AddPerson(person);

            _collection = new ObservableCollection<Person>(_serializer.ShowPersons());
            People = new(_collection);
            People.CollectionChanged += OnUsersChanged;
        }

        private void OnUsersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    foreach (Person person in e.OldItems)
                    {
                        Serializer.DeletePerson(person);
                        _collection.Remove(person);
                    }

                    break;
            }
        }

        public RelayCommand<object> OpenFormViewCommand
        {
            get { return _openFormViewCommand ??= new RelayCommand<object>(_ => OpenFormView()); }
        }

        public void OpenFormView()
        {
            formView?.Invoke();
        }


        public Person SelectedPerson { get; set; }

        public ObservableCollection<Person> Collection
        {
            get { return _collection; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}