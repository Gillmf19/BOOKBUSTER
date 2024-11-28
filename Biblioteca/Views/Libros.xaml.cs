using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class Libros : UserControl
    {
        private const string ArchivoLibros = "libros.json";

        public ObservableCollection<Libro> LibrosList { get; set; }

        public Libros()
        {
            InitializeComponent();
            LibrosList = new ObservableCollection<Libro>();
            CargarLibros();
            TablaLibros.ItemsSource = LibrosList;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(TxtTitulo.Text) || string.IsNullOrWhiteSpace(TxtAutor.Text) || string.IsNullOrWhiteSpace(TxtAno.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear el nuevo libro
            var nuevoLibro = new Libro
            {
                Titulo = TxtTitulo.Text,
                Autor = TxtAutor.Text,
                Ano = int.Parse(TxtAno.Text)
            };

            // Agregar el libro a la lista
            LibrosList.Add(nuevoLibro);

            // Guardar los libros en el archivo JSON
            GuardarLibros();

            // Limpiar los campos
            TxtTitulo.Clear();
            TxtAutor.Clear();
            TxtAno.Clear();

            MessageBox.Show("Libro agregado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var libro = button?.DataContext as Libro;

            if (libro != null)
            {
                var result = MessageBox.Show($"¿Estás seguro de eliminar el libro '{libro.Titulo}'?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    LibrosList.Remove(libro);
                    GuardarLibros();
                }
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

    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
    }
}
