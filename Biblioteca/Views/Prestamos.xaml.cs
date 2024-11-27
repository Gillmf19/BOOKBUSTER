using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteca.Views
{
    public partial class Prestamos : UserControl
    {
        private List<Prestamo> _prestamos; // Lista simulada de préstamos

        public Prestamos()
        {
            InitializeComponent();
            InicializarDatosSimulados(); // Inicializamos datos ficticios
            CargarPrestamos();
        }

        // Método para inicializar datos simulados
        private void InicializarDatosSimulados()
        {
            _prestamos = new List<Prestamo>
            {
                new Prestamo { Id = 1, Usuario = "Juan Pérez", Libro = "Cien Años de Soledad", FechaPrestamo = DateTime.Now.AddDays(-3), DiasRestantes = 12 },
                new Prestamo { Id = 2, Usuario = "Ana López", Libro = "Don Quijote", FechaPrestamo = DateTime.Now.AddDays(-5), DiasRestantes = 10 },
                new Prestamo { Id = 3, Usuario = "Carlos Ruiz", Libro = "El Principito", FechaPrestamo = DateTime.Now.AddDays(-7), DiasRestantes = 8 }
            };
        }

        // Cargar todos los préstamos en la tabla
        private void CargarPrestamos()
        {
            TablaPrestamos.ItemsSource = _prestamos;
        }

        // Registrar un nuevo préstamo
        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = TxtUsuario.Text.Trim();
            string libro = TxtLibro.Text.Trim();
            DateTime? fechaPrestamo = FechaPrestamo.SelectedDate;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(libro) || fechaPrestamo == null)
            {
                MessageBox.Show("Por favor, llena todos los campos.");
                return;
            }

            var nuevoPrestamo = new Prestamo
            {
                Id = _prestamos.Any() ? _prestamos.Max(p => p.Id) + 1 : 1,
                Usuario = usuario,
                Libro = libro,
                FechaPrestamo = fechaPrestamo.Value,
                DiasRestantes = 15 // Simulando un tiempo de préstamo fijo de 15 días
            };

            _prestamos.Add(nuevoPrestamo);
            CargarPrestamos();

            // Limpiar campos después de registrar
            TxtUsuario.Clear();
            TxtLibro.Clear();
            FechaPrestamo.SelectedDate = null;

            MessageBox.Show("Préstamo registrado correctamente.");
        }

        // Buscar préstamos por usuario o libro
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string criterio = TxtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(criterio))
            {
                CargarPrestamos(); // Si no hay criterio, mostrar todos
                return;
            }

            

        }
    }

    // Clase para representar un préstamo
    public class Prestamo
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Libro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public int DiasRestantes { get; set; }
    }
}
