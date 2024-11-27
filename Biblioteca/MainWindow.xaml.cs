using System.Windows;
using System.Windows.Controls;
using Biblioteca.Views; // Asegúrate de que este espacio de nombres coincide con el de tus UserControls

namespace Biblioteca
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Asignar eventos Click a los botones
            UsuariosButton.Click += (s, e) => NavegarA(new Usuarios());
            LibrosButton.Click += (s, e) => NavegarA(new Libros());
            PrestamosButton.Click += (s, e) => NavegarA(new Prestamos());
            ConfiguracionButton.Click += (s, e) => NavegarA(new Configuracion());
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

            // Agregar un botón para regresar al menú principal
            var volverMenuButton = new Button
            {
                Content = "Volver al menú",
                Foreground = System.Windows.Media.Brushes.White,
                Background = System.Windows.Media.Brushes.Orange,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };

            volverMenuButton.Click += (s, e) => Content = this.Content; // Volver al contenido principal

            // Agregar el botón al contenedor
            grid.Children.Add(volverMenuButton);

            // Reemplazar el contenido de la ventana principal con el nuevo contenedor
            Content = grid;
        }
    }
}
