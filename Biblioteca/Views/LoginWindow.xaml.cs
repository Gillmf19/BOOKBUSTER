using System.Collections.Generic;
using System.Windows;

namespace Biblioteca.Views
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        // Diccionario con los usuarios válidos y sus contraseñas
        private readonly Dictionary<string, (string Contraseña, string Rol)> usuariosValidos = new Dictionary<string, (string, string)>
        {
            { "admin", ("1234", "Administrador") },
            { "usuario", ("contraseña", "Usuario Regular") }
        };

        private int intentosFallidos = 0; // Contador de intentos fallidos
        private const int MaxIntentos = 3; // Máximo de intentos permitidos

        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lógica del botón "Iniciar Sesión".
        /// </summary>
        private void IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (intentosFallidos >= MaxIntentos)
            {
                // Mostrar mensaje de bloqueo si se exceden los intentos
                BloqueoTextBlock.Visibility = Visibility.Visible;
                return;
            }

            var usuario = UsuarioTextBox.Text.Trim();
            var contraseña = PasswordBox.Password.Trim();

            // Validar existencia del usuario
            if (!usuariosValidos.ContainsKey(usuario))
            {
                MostrarError("Error: El usuario no existe.");
                RegistrarIntentoFallido();
                return;
            }

            // Validar contraseña
            var datosUsuario = usuariosValidos[usuario];
            if (datosUsuario.Contraseña != contraseña)
            {
                MostrarError("Error: La contraseña es incorrecta.");
                RegistrarIntentoFallido();
                return;
            }

            // Login exitoso
            AutenticacionExitosa(usuario, datosUsuario.Rol);
        }

        /// <summary>
        /// Muestra un mensaje de error.
        /// </summary>
        private void MostrarError(string mensaje)
        {
            ErrorTextBlock.Text = mensaje;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Registra un intento fallido y verifica si se exceden los intentos permitidos.
        /// </summary>
        private void RegistrarIntentoFallido()
        {
            intentosFallidos++;
            if (intentosFallidos >= MaxIntentos)
            {
                BloqueoTextBlock.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Maneja la autenticación exitosa.
        /// </summary>
        private void AutenticacionExitosa(string usuario, string rol)
        {
            MessageBox.Show($"Bienvenido {usuario}.\nRol: {rol}", "Login Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

            // Abrir la ventana principal
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Cerrar el formulario de login
            this.Close();
        }
    }
}