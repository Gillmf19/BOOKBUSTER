using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class Usuarios : UserControl
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usuario guardado correctamente.");
        }

        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Aquí puedes filtrar la tabla por nombre o correo
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Editar usuario seleccionado.");
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Eliminar usuario seleccionado.");
        }
    }
}
