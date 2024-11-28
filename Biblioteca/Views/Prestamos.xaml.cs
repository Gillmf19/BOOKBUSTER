using System;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class Prestamos : UserControl
    {
        public Prestamos()
        {
            InitializeComponent();
        }

        // Evento del botón "Registrar Préstamo"
        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsuario.Text;
            string libro = TxtLibro.Text;
            DateTime? fechaPrestamo = FechaPrestamo.SelectedDate;
            DateTime? fechaDevolucion = FechaDevolucion.SelectedDate;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(libro) ||
                !fechaPrestamo.HasValue || !fechaDevolucion.HasValue)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Préstamo registrado:\nUsuario: {usuario}\nLibro: {libro}\nFecha Préstamo: {fechaPrestamo.Value.ToShortDateString()}\nFecha Devolución: {fechaDevolucion.Value.ToShortDateString()}",
                            "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Evento del botón "Buscar"
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = TxtBuscar.Text;

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                MessageBox.Show("Ingrese un término de búsqueda.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Simulación de búsqueda (sin base de datos implementada aún)
            MessageBox.Show($"Buscando préstamos para: {textoBusqueda}", "Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Evento del botón "Volver al Menú"
        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            // Simulación de regreso al menú principal
            MessageBox.Show("Volviendo al menú principal.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
