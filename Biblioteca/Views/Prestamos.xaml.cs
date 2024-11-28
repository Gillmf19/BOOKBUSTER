using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Biblioteca.Views
{
    public partial class Prestamos : UserControl
    {
        public Prestamos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento para limpiar el texto predeterminado cuando se hace clic en el campo de búsqueda.
        /// </summary>
        private void TxtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBuscar.Text == "Buscar por usuario o libro")
            {
                TxtBuscar.Text = string.Empty; // Limpiar texto inicial
                TxtBuscar.Foreground = Brushes.Black; // Cambiar el color del texto a negro
            }
        }

        /// <summary>
        /// Evento para restaurar el texto predeterminado si el usuario no escribe nada.
        /// </summary>
        private void TxtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                TxtBuscar.Text = "Buscar por usuario o libro"; // Restaurar texto inicial
                TxtBuscar.Foreground = Brushes.Gray; // Cambiar el color del texto a gris
            }
        }

        /// <summary>
        /// Evento para manejar el clic en el botón de búsqueda.
        /// </summary>
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            var textoBusqueda = TxtBuscar.Text;

            if (textoBusqueda != "Buscar por usuario o libro" && !string.IsNullOrWhiteSpace(textoBusqueda))
            {
                // Lógica para buscar por usuario o libro (modificar según sea necesario)
                MessageBox.Show($"Buscando: {textoBusqueda}");
            }
            else
            {
                MessageBox.Show("Por favor, introduce un texto válido para buscar.");
            }
        }

        /// <summary>
        /// Evento para manejar el clic en el botón Registrar Préstamo.
        /// </summary>
        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para registrar un préstamo (modificar según sea necesario)
            MessageBox.Show("Préstamo registrado exitosamente.");
        }
    }
}
