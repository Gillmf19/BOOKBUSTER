using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class Prestamos : UserControl
    {
        private const string ArchivoPrestamos = "prestamos.json";

        public ObservableCollection<Prestamo> PrestamosList { get; set; }

        public Prestamos()
        {
            InitializeComponent();

            PrestamosList = new ObservableCollection<Prestamo>();
            CargarPrestamos();

            TablaPrestamos.ItemsSource = PrestamosList;
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUsuario.Text) || string.IsNullOrWhiteSpace(TxtLibro.Text) ||
                FechaPrestamo.SelectedDate == null || FechaDevolucion.SelectedDate == null)
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nuevoPrestamo = new Prestamo
            {
                Usuario = TxtUsuario.Text,
                Libro = TxtLibro.Text,
                FechaPrestamo = FechaPrestamo.SelectedDate.Value,
                FechaDevolucion = FechaDevolucion.SelectedDate.Value
            };

            PrestamosList.Add(nuevoPrestamo);
            GuardarPrestamos();

            TxtUsuario.Clear();
            TxtLibro.Clear();
            FechaPrestamo.SelectedDate = null;
            FechaDevolucion.SelectedDate = null;

            MessageBox.Show("Préstamo registrado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CargarPrestamos()
        {
            if (File.Exists(ArchivoPrestamos))
            {
                try
                {
                    var json = File.ReadAllText(ArchivoPrestamos);
                    var prestamosGuardados = JsonSerializer.Deserialize<ObservableCollection<Prestamo>>(json);

                    if (prestamosGuardados != null)
                    {
                        foreach (var prestamo in prestamosGuardados)
                        {
                            PrestamosList.Add(prestamo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GuardarPrestamos()
        {
            try
            {
                var json = JsonSerializer.Serialize(PrestamosList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ArchivoPrestamos, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = TxtBuscar.Text.ToLower();
            var prestamosFiltrados = PrestamosList
                .Where(p => p.Usuario.ToLower().Contains(textoBusqueda) || p.Libro.ToLower().Contains(textoBusqueda));

            TablaPrestamos.ItemsSource = prestamosFiltrados;
        }

        private void TxtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBuscar.Text == "Buscar por usuario o libro")
            {
                TxtBuscar.Text = string.Empty;
                TxtBuscar.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TxtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                TxtBuscar.Text = "Buscar por usuario o libro";
                TxtBuscar.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var prestamoSeleccionado = TablaPrestamos.SelectedItem as Prestamo;

            if (prestamoSeleccionado != null)
            {
                var resultado = MessageBox.Show($"¿Estás seguro de eliminar el préstamo de '{prestamoSeleccionado.Libro}'?",
                    "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    PrestamosList.Remove(prestamoSeleccionado);
                    GuardarPrestamos();
                    MessageBox.Show("Préstamo eliminado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un préstamo para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class Prestamo
    {
        public string Usuario { get; set; }
        public string Libro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
    }
}
