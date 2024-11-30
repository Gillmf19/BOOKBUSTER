using System.Windows;

namespace Biblioteca
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Abrir la ventana de Login al iniciar la aplicación
            var loginWindow = new Views.LoginWindow();
            loginWindow.Show();
        }
    }
}
