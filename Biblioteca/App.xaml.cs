using System.Windows;

namespace Biblioteca
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Mostrar la ventana de Login al iniciar la aplicación
            var loginWindow = new Views.LoginWindow();

            // Mostrar la ventana de Login
            if (loginWindow.ShowDialog() == true) // Solo procede si el login es exitoso
            {
                // Abrir la ventana principal si el login es válido
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                // Salir de la aplicación si el login falla o se cierra
                Shutdown();
            }
        }
    }
}
