using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;  // Necesario para el OpenFileDialog

namespace Biblioteca.Views
{
    public partial class Libros : UserControl
    {
        private List<Libro> libros = new List<Libro>();
        private Libro libroSeleccionado;

        public Libros()
        {
            InitializeComponent();
            CargarLibros();
        }

        // Método para cargar los libros en el ListBox
        private void CargarLibros()
        {
            libros.Add(new Libro { Id = 1, Titulo = "Cien Años de Soledad", Autor = "Gabriel García Márquez", Ano = "1967", ImagenRuta = "ruta_a_imagen.jpg" });
            libros.Add(new Libro { Id = 2, Titulo = "1984", Autor = "George Orwell", Ano = "1949", ImagenRuta = "ruta_a_imagen2.jpg" });
            ListaLibros.ItemsSource = null;
            ListaLibros.ItemsSource = libros;
        }

        // Método para guardar un libro en la lista
        private void GuardarLibro()
        {
            string titulo = TxtTitulo.Text;
            string autor = TxtAutor.Text;
            string ano = TxtAno.Text;
            string imagenRuta = ImgLibro.Tag?.ToString(); // Usamos el Tag para guardar la ruta de la imagen

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(autor) || string.IsNullOrEmpty(ano))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int nuevoId = libros.Count > 0 ? libros.Max(x => x.Id) + 1 : 1;
            var nuevoLibro = new Libro { Id = nuevoId, Titulo = titulo, Autor = autor, Ano = ano, ImagenRuta = imagenRuta };
            libros.Add(nuevoLibro);

            TxtTitulo.Clear();
            TxtAutor.Clear();
            TxtAno.Clear();
            ImgLibro.Source = null;  // Limpiar la imagen seleccionada
            CargarLibros();
        }

        // Método para seleccionar una imagen
        private void SeleccionarImagen_Click(object sender, RoutedEventArgs e)
        {
            var dialogo = new OpenFileDialog();
            dialogo.Filter = "Archivos de Imagen (*.jpg; *.png; *.gif)|*.jpg;*.png;*.gif";
            if (dialogo.ShowDialog() == true)
            {
                string rutaImagen = dialogo.FileName;
                ImgLibro.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(rutaImagen));  // Mostrar la imagen seleccionada
                ImgLibro.Tag = rutaImagen;  // Guardar la ruta de la imagen en el Tag
            }
        }

        // Método para editar un libro
        private void EditarLibro()
        {
            if (libroSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un libro para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TxtTitulo.Text = libroSeleccionado.Titulo;
            TxtAutor.Text = libroSeleccionado.Autor;
            TxtAno.Text = libroSeleccionado.Ano;
            ImgLibro.Tag = libroSeleccionado.ImagenRuta;  // Recuperar la ruta de la imagen
            if (!string.IsNullOrEmpty(libroSeleccionado.ImagenRuta))
            {
                ImgLibro.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(libroSeleccionado.ImagenRuta)); // Mostrar la imagen
            }

            libroSeleccionado.Titulo = TxtTitulo.Text;
            libroSeleccionado.Autor = TxtAutor.Text;
            libroSeleccionado.Ano = TxtAno.Text;

            CargarLibros();
        }

        // Método para eliminar un libro
        private void EliminarLibro()
        {
            if (libroSeleccionado == null)
            {
                MessageBox.Show("Por favor, seleccione un libro para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show("¿Está seguro que desea eliminar este libro?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                libros.Remove(libroSeleccionado);
                CargarLibros();
            }
        }

        // Evento para el botón de guardar
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarLibro();
        }

        // Evento para el botón de editar
        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarLibro();
        }

        // Evento para el botón de eliminar
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            EliminarLibro();
        }

        // Evento para la selección de un libro en la lista
        private void ListaLibros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            libroSeleccionado = (Libro)ListaLibros.SelectedItem;
        }

        private void TxtAutor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Ano { get; set; }
        public string ImagenRuta { get; set; }
    }
}

