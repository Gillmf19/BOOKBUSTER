using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Biblioteca.Views; // Asegúrate de tener el modelo adecuado
using System.Collections.ObjectModel;

namespace Biblioteca.Views
{
    public partial class Usuarios : UserControl
    {
        // Lista de usuarios (simula la base de datos en este caso)
        private ObservableCollection<Usuario> usuarios;

        public Usuarios()
        {
            InitializeComponent();
            usuarios = new ObservableCollection<Usuario>();
            TablaUsuarios.ItemsSource = usuarios; // Vinculando la lista de usuarios al DataGrid
        }

        // Evento para buscar usuarios por nombre o correo
        private void TxtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = TxtBuscar.Text.ToLower();

            // Filtrar usuarios por nombre o correo
            var usuariosFiltrados = usuarios.Where(u => u.Nombre.ToLower().Contains(query) || u.Email.ToLower().Contains(query)).ToList();

            // Actualizar la fuente de datos del DataGrid
            TablaUsuarios.ItemsSource = new ObservableCollection<Usuario>(usuariosFiltrados);
        }

        // Evento para guardar un nuevo usuario
        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los datos del formulario
            string nombre = TxtNombre.Text;
            string apellido = TxtApellido.Text;
            string email = TxtEmail.Text;
            string telefono = TxtTelefono.Text;
            string direccion = TxtDireccion.Text;

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Por favor, complete todos los campos requeridos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear el nuevo usuario
            Usuario nuevoUsuario = new Usuario
            {
                Id = usuarios.Count + 1, // Asigna un ID único basado en la cantidad de usuarios actuales
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono,
                Direccion = direccion
            };

            // Agregar el usuario a la lista
            usuarios.Add(nuevoUsuario);

            // Limpiar los campos del formulario
            TxtNombre.Clear();
            TxtApellido.Clear();
            TxtEmail.Clear();
            TxtTelefono.Clear();
            TxtDireccion.Clear();

            // Actualizar la tabla
            TablaUsuarios.ItemsSource = new ObservableCollection<Usuario>(usuarios);
        }

        // Evento para editar un usuario seleccionado
        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var usuario = button?.DataContext as Usuario;

            if (usuario != null)
            {
                // Rellenar el formulario con los datos del usuario seleccionado
                TxtNombre.Text = usuario.Nombre;
                TxtApellido.Text = usuario.Apellido;
                TxtEmail.Text = usuario.Email;
                TxtTelefono.Text = usuario.Telefono;
                TxtDireccion.Text = usuario.Direccion;

                // Eliminar el usuario de la lista (para luego agregarlo editado)
                usuarios.Remove(usuario);
            }
        }

        // Evento para eliminar un usuario seleccionado
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var usuario = button?.DataContext as Usuario;

            if (usuario != null)
            {
                // Eliminar el usuario de la lista
                usuarios.Remove(usuario);

                // Actualizar la tabla
                TablaUsuarios.ItemsSource = new ObservableCollection<Usuario>(usuarios);
            }
        }
    }

    // Modelo de Usuario (representa la estructura de datos)
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
