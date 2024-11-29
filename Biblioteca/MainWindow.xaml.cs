using System.Windows;
using System.Windows.Controls;
using Biblioteca.Views;

namespace Biblioteca
{
    public partial class MainWindow : Window
    {
        private object contenidoOriginal; // Cambiado a "object" para coincidir con el tipo de Content

        public MainWindow()
        {
            InitializeComponent();

            // Guardar el contenido original al iniciar
            contenidoOriginal = Content;

            // Asignar eventos Click a los botones
            UsuariosButton.Click += (s, e) => NavegarA(new Usuarios());
            LibrosButton.Click += (s, e) => NavegarA(new Libros());
            PrestamosButton.Click += (s, e) => NavegarA(new Prestamos());
        }

        /// <summary>
        /// Método para navegar a una vista específica.
        /// </summary>
        /// <param name="vista">Vista (UserControl) que se mostrará en la ventana principal</param>
        private void NavegarA(UserControl vista)
        {
            // Crear un contenedor para la vista
            var grid = new Grid();

            // Agregar la nueva vista al contenedor
            grid.Children.Add(vista);

            // Crear un botón para regresar al menú principal
            var volverMenuButton = new Button
            {
                Content = "Volver al menú",
                Foreground = System.Windows.Media.Brushes.White,
                Background = System.Windows.Media.Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };

            // Evento para restaurar el contenido original (el menú principal)
            volverMenuButton.Click += (s, e) => Content = contenidoOriginal;

            // Agregar el botón al contenedor
            grid.Children.Add(volverMenuButton);

            // Reemplazar el contenido de la ventana principal con el nuevo contenedor
            Content = grid;
        }
    }
}
