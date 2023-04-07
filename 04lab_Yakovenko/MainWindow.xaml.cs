using KMA.Lab04.Yakovenko.Views;
using System.Windows;

namespace KMA.Lab04.Yakovenko
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenUsersView();
        }
        public void OpenUsersView()
        {
            Content = new UsersView(OpenFormView);


        }
        public void OpenFormView()
        {
            Content = new FormView(OpenUsersView);
        }
    }
}
