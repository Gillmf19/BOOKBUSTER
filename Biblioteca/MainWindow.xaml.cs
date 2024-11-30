using System.Windows;
using System.Windows.Controls;
using Biblioteca.Views;

namespace Biblioteca
{
    public partial class MainWindow : Window
    {
        private object contenidoOriginal;

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

        private void NavegarA(UserControl vista)
        {
            var grid = new Grid();

            grid.Children.Add(vista);

            var volverMenuButton = new Button
            {
                Content = "Volver al menú",
                Foreground = System.Windows.Media.Brushes.White,
                Background = System.Windows.Media.Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };

            volverMenuButton.Click += (s, e) => Content = contenidoOriginal;

            grid.Children.Add(volverMenuButton);

            Content = grid;
        }
    }
}
