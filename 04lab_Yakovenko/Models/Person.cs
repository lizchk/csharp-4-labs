using KMA.Lab04.Yakovenko.Tools;
using System;
using System.Windows;

namespace KMA.Lab04.Yakovenko.Models
{
    internal class Person
    {
        private string _name;
        private string _surname;
        private string _email;

        public Person(string name, string surname, string email, DateTime dateOfBirth)
        {
            try
            {
                if (ExcFutureDate(CalcAge(dateOfBirth)) == true)
                {
                    throw new ExcFutureDate();
                }
                if (ExcPastDate(CalcAge(dateOfBirth)) == true)
                {
                    throw new ExcPastDate();
                }
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

            _name = name;
            _surname = surname;
            _email = email;
            DateOfBirth = dateOfBirth;
            isAdult = CalcAge(DateOfBirth) >= 18;
            sunSign = CalcWestSign();
            chineseSign = CalcChinSign();
            isBirthday = CalcBirth();
        }
        public Person(string name, string surname, string email) : this(name, surname, email, default)
        {

        }
        public Person(string name, string surname, DateTime dateOfBirth) : this(name, surname, null, dateOfBirth)
        {

        }

        public DateTime DateOfBirth { get; private set; } = DateTime.Today;

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }
        public string Surname
        {
            get { return _surname; }
            private set { _surname = value; }
        }
        public string Email
        {
            get { return _email; }
            private set { _email = value; }
        }
        public bool IsAdult
        {
            get { return isAdult; }
        }
        public SunSign SunSign
        {
            get { return sunSign; }
        }
        public ChineseSign ChineseSign
        {
            get { return chineseSign; }
        }
        public bool IsBirthday
        {
            get { return isBirthday; }
        }

        private bool isAdult;
        private int CalcAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.Month < dateOfBirth.Month || DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day)
            {
                age--;
            }
            return age;
        }
        private bool isBirthday;
        private bool CalcBirth()
        {
            if (DateTime.Now.Day == DateOfBirth.Day && DateTime.Now.Month == DateOfBirth.Month)
            {
                MessageBox.Show("Happy birthday!💖");
                return true;
            }
            return false;

        }

        private ChineseSign chineseSign;
        private ChineseSign CalcChinSign()
        {
            return (ChineseSign)(DateOfBirth.Year % 12);
        }

        private SunSign sunSign;
        private SunSign CalcWestSign()
        {

            int month = DateOfBirth.Month;
            int day = DateOfBirth.Day;

            switch (month)
            {
                case 1:
                    return day <= 19 ? SunSign.Capricorn : SunSign.Aquarius;
                case 2:
                    return day <= 18 ? SunSign.Aquarius : SunSign.Pisces;
                case 3:
                    return day <= 20 ? SunSign.Pisces : SunSign.Aries;
                case 4:
                    return day <= 19 ? SunSign.Aries : SunSign.Taurus;
                case 5:
                    return day <= 20 ? SunSign.Taurus : SunSign.Gemini;
                case 6:
                    return day <= 20 ? SunSign.Gemini : SunSign.Cancer;
                case 7:
                    return day <= 22 ? SunSign.Cancer : SunSign.Leo;
                case 8:
                    return day <= 22 ? SunSign.Leo : SunSign.Virgo;
                case 9:
                    return day <= 22 ? SunSign.Virgo : SunSign.Libra;
                case 10:
                    return day <= 22 ? SunSign.Libra : SunSign.Scorpio;
                case 11:
                    return day <= 21 ? SunSign.Scorpio : SunSign.Sagittarius;
                default:
                    return day <= 21 ? SunSign.Sagittarius : SunSign.Capricorn;
            }
        }

        private bool ExcPastDate(int age)
        {
            if (age <= 135)
            {
                return false;
            }
            return true;
        }
        private bool ExcFutureDate(int age)
        {
            if (age >= 0)
            {
                return false;
            }
            return true;
        }

        private bool ExcEmail(string email)
        {
            bool excEmail;
            excEmail = email.Contains('@');
            return excEmail;
        }
        private void EmailExceptions(string email)
        {
            if (ExcEmail(email) == false)
            {
                throw new ExcEmail();
            }
        }
    }
}
