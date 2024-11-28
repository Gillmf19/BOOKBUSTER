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
        // Ruta del archivo JSON donde guardaremos los datos
        private const string ArchivoPrestamos = "prestamos.json";

        public ObservableCollection<Prestamo> PrestamosList { get; set; }

        public Prestamos()
        {
            InitializeComponent();

            // Inicializar la lista de préstamos y cargar datos desde el archivo
            PrestamosList = new ObservableCollection<Prestamo>();
            CargarPrestamos();

            // Vincular la lista a la DataGrid
            TablaPrestamos.ItemsSource = PrestamosList;
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Validar datos antes de agregar el préstamo
            if (string.IsNullOrWhiteSpace(TxtUsuario.Text) || string.IsNullOrWhiteSpace(TxtLibro.Text))
            {
                MessageBox.Show("Por favor, complete los campos Usuario y Libro.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (FechaPrestamo.SelectedDate == null || FechaDevolucion.SelectedDate == null)
            {
                MessageBox.Show("Por favor, seleccione las fechas de préstamo y devolución.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear un nuevo objeto de préstamo
            var nuevoPrestamo = new Prestamo
            {
                Usuario = TxtUsuario.Text,
                Libro = TxtLibro.Text,
                FechaPrestamo = FechaPrestamo.SelectedDate.Value,
                FechaDevolucion = FechaDevolucion.SelectedDate.Value
            };

            // Agregar el préstamo a la lista
            PrestamosList.Add(nuevoPrestamo);

            // Guardar los datos en el archivo JSON
            GuardarPrestamos();

            // Limpiar los campos
            TxtUsuario.Clear();
            TxtLibro.Clear();
            FechaPrestamo.SelectedDate = null;
            FechaDevolucion.SelectedDate = null;

            MessageBox.Show("Préstamo registrado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CargarPrestamos()
        {
            // Verificar si el archivo existe antes de intentar cargarlo
            if (File.Exists(ArchivoPrestamos))
            {
                try
                {
                    // Leer el archivo JSON y deserializar los datos
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
                // Serializar la lista de préstamos y guardarla en un archivo JSON
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
            // Filtro básico para buscar préstamos
            string textoBusqueda = TxtBuscar.Text.ToLower();
            var prestamosFiltrados = PrestamosList
                .Where(p => p.Usuario.ToLower().Contains(textoBusqueda) || p.Libro.ToLower().Contains(textoBusqueda));

            TablaPrestamos.ItemsSource = prestamosFiltrados;
        }

        private void TxtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Buscar por usuario o libro")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TxtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Buscar por usuario o libro";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }

    // Clase para representar un préstamo
    public class Prestamo
    {
        public string Usuario { get; set; }
        public string Libro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
    }
}
