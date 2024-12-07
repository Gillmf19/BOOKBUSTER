using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class Libros : UserControl
    {
        // Ruta del archivo JSON para guardar la lista de libros
        private const string ArchivoLibros = "libros.json";

        public ObservableCollection<Libro> LibrosList { get; set; }

        public Libros()
        {
            InitializeComponent();

            // Inicializar la lista de libros y cargar los datos
            LibrosList = new ObservableCollection<Libro>();
            CargarLibros();

            // Vincular la lista de libros a la DataGrid
            TablaLibros.ItemsSource = LibrosList;
        }

        private void BtnAgregarLibro_Click(object sender, RoutedEventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(TxtTitulo.Text) || string.IsNullOrWhiteSpace(TxtAutor.Text) || string.IsNullOrWhiteSpace(TxtAnio.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxtAnio.Text, out int anio))
            {
                MessageBox.Show("El año debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear un nuevo libro
            var nuevoLibro = new Libro
            {
                Titulo = TxtTitulo.Text,
                Autor = TxtAutor.Text,
                Anio = anio
            };

            // Agregarlo a la lista
            LibrosList.Add(nuevoLibro);

            // Guardar los datos
            GuardarLibros();

            // Limpiar los campos
            TxtTitulo.Clear();
            TxtAutor.Clear();
            TxtAnio.Clear();

            MessageBox.Show("Libro añadido exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminarLibro_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay un libro seleccionado
            var libroSeleccionado = TablaLibros.SelectedItem as Libro;
            if (libroSeleccionado != null)
            {
                // Confirmar eliminación
                var resultado = MessageBox.Show($"¿Está seguro de eliminar el libro '{libroSeleccionado.Titulo}'?",
                    "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    // Eliminar libro
                    LibrosList.Remove(libroSeleccionado);

                    // Guardar cambios
                    GuardarLibros();

                    MessageBox.Show("Libro eliminado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un libro para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = TxtBuscar.Text.ToLower();
            var librosFiltrados = LibrosList.Where(l =>
                l.Titulo.ToLower().Contains(textoBusqueda) || l.Autor.ToLower().Contains(textoBusqueda)).ToList();

            TablaLibros.ItemsSource = librosFiltrados;
        }

        private void TxtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBuscar.Text == "Buscar por título o autor")
            {
                TxtBuscar.Text = string.Empty;
                TxtBuscar.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TxtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                TxtBuscar.Text = "Buscar por título o autor";
                TxtBuscar.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void CargarLibros()
        {
            if (File.Exists(ArchivoLibros))
            {
                try
                {
                    var json = File.ReadAllText(ArchivoLibros);
                    var librosGuardados = JsonSerializer.Deserialize<ObservableCollection<Libro>>(json);

                    if (librosGuardados != null)
                    {
                        foreach (var libro in librosGuardados)
                        {
                            LibrosList.Add(libro);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los libros: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GuardarLibros()
        {
            try
            {
                var json = JsonSerializer.Serialize(LibrosList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ArchivoLibros, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los libros: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // Clase de libro
    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
    }
}
