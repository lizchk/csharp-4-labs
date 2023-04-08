using KMA.Lab04.Yakovenko.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace KMA.Lab04.Yakovenko.Models
{
    internal class Person : INotifyPropertyChanged
    {
        public Person(string name, string surname, string email, DateTime dateOfBirth)
        {
            try
            {
                if (ExcFutureDate(CalcAge(dateOfBirth)))
                    throw new ExcFutureDate();

                if (ExcPastDate(CalcAge(dateOfBirth)))
                    throw new ExcPastDate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                EmailExceptions(email);
            }
            catch (ExcEmail e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        private DateTime _dateOfBirth;

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                IsAdult = CalcAge(_dateOfBirth) >= 18;
                SunSign = CalcWestSign();
                ChineseSign = CalcChinSign();
                IsBirthday = CalcBirth();
                OnPropertyChanged(nameof(IsAdult));
                OnPropertyChanged(nameof(SunSign));
                OnPropertyChanged(nameof(ChineseSign));
                OnPropertyChanged(nameof(IsBirthday));
            }
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool IsAdult { get; set; }
        public SunSign SunSign { get; set; }
        public ChineseSign ChineseSign { get; set; }
        public bool IsBirthday { get; set; }

        private int CalcAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.Month < dateOfBirth.Month ||
                DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day)
            {
                age--;
            }

            return age;
        }

        private bool CalcBirth()
            => DateTime.Now.Day == DateOfBirth.Day && DateTime.Now.Month == DateOfBirth.Month;

        private ChineseSign CalcChinSign()
            => (ChineseSign)(DateOfBirth.Year % 12);

        private SunSign CalcWestSign()
        {
            var month = DateOfBirth.Month;
            var day = DateOfBirth.Day;

            return month switch
            {
                1 => day <= 19 ? SunSign.Capricorn : SunSign.Aquarius,
                2 => day <= 18 ? SunSign.Aquarius : SunSign.Pisces,
                3 => day <= 20 ? SunSign.Pisces : SunSign.Aries,
                4 => day <= 19 ? SunSign.Aries : SunSign.Taurus,
                5 => day <= 20 ? SunSign.Taurus : SunSign.Gemini,
                6 => day <= 20 ? SunSign.Gemini : SunSign.Cancer,
                7 => day <= 22 ? SunSign.Cancer : SunSign.Leo,
                8 => day <= 22 ? SunSign.Leo : SunSign.Virgo,
                9 => day <= 22 ? SunSign.Virgo : SunSign.Libra,
                10 => day <= 22 ? SunSign.Libra : SunSign.Scorpio,
                11 => day <= 21 ? SunSign.Scorpio : SunSign.Sagittarius,
                _ => day <= 21 ? SunSign.Sagittarius : SunSign.Capricorn
            };
        }

        private bool ExcPastDate(int age)
            => age > 135;

        private bool ExcFutureDate(int age)
            => age < 0;

        private void EmailExceptions(string email)
        {
            if (!email.Contains('@'))
                throw new ExcEmail();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}