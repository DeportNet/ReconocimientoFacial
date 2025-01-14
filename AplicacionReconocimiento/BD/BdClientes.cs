using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeportNetReconocimiento.BD
{
    public class BdClientes
    {
        private static string nombreBaseDeDatos = "clientes.db";
        private static string rutaBaseDeDatos = "Data Source=" + nombreBaseDeDatos + ";Version=3;";

        public BdClientes()
        {
            CrearTablaClientes();

        }

        private void CrearBaseDeDatos()
        {
            if (!System.IO.File.Exists(nombreBaseDeDatos)){
                SQLiteConnection.CreateFile(nombreBaseDeDatos);
            }

            Console.WriteLine("Base de datos creada.");
        }

        public void CrearTablaClientes()
        {
            CrearBaseDeDatos();


            using (var conexion = new SQLiteConnection(rutaBaseDeDatos))
            {
                conexion.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Clientes (
                    IdRegistro INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdCliente INTEGER NOT NULL,
                    NombreCliente VARCHAR(50) NOT NULL,
                    DiaIngreso DATETIME NOT NULL
                );";

                using (var command = new SQLiteCommand(createTableQuery, conexion))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Tabla creada.");
            }
        }

        public bool InsertarCliente(int idCliente, string nombreCliente)
        {
            bool flag = false;


            if(nombreCliente.Length > 50)
            {
                Console.WriteLine("El nombre del cliente no puede tener mas de 50 caracteres.");
                return flag;
            }

            using (var conexion = new SQLiteConnection(rutaBaseDeDatos))
            {
                conexion.Open();

                string insertQuery = @"
                INSERT INTO Clientes (IdCliente, NombreCliente, HoraIngreso)
                VALUES (@IdCliente, @NombreCliente, @HoraIngreso);";

                using (var command = new SQLiteCommand(insertQuery, conexion))
                {
                    

                    command.Parameters.AddWithValue("@IdCliente", idCliente);
                    command.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                    command.Parameters.AddWithValue("@HoraIngreso", DateTime.Now);

                    command.ExecuteNonQuery();
                    flag = true;
                    Console.WriteLine($"Cliente {nombreCliente} insertado.");
                }
            }
            return flag;
        }

        static void LeerClientes()
        {
            using (var conexion = new SQLiteConnection(rutaBaseDeDatos))
            {
                conexion.Open();

                string selectQuery = "SELECT * FROM Clientes;";

                using (var command = new SQLiteCommand(selectQuery, conexion))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Clientes registrados:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"IdRegistro: {reader["IdRegistro"]}, IdCliente: {reader["IdCliente"]}, Nombre: {reader["NombreCliente"]}, HoraIngreso: {reader["HoraIngreso"]}");
                    }
                }
            }
        }

        public int ContarClientePorId(int idCliente)
        {
            int nroDeOcurrencia = 0;
            using (var conexion = new SQLiteConnection(rutaBaseDeDatos))
            {
                conexion.Open();

                string selectQuery = "SELECT COUNT(IdCliente) FROM Clientes WHERE IdCliente = @IdCliente;";
                using (var command = new SQLiteCommand(selectQuery, conexion))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine($"Veces que vino el cliente con id:{idCliente} ");
                    while (reader.Read())
                    {
                        if (reader.Read())
                        {
                            nroDeOcurrencia = reader.GetInt32(0);
                        }

                    }
                }

            }
            return nroDeOcurrencia;
        }

        public int ContarTotalClientesDiaActual()
        {
            int totalClientes = 0;
            using(var conexion = new SQLiteConnection(rutaBaseDeDatos))
            {
                conexion.Open();

                string selectQuery = "SELECT COUNT(IdRegistro) FROM Clientes WHERE DATE(HoraIngreso) = DATE('now');";

                using (var command = new SQLiteCommand(selectQuery, conexion))
                {
                    
                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            totalClientes = reader.GetInt32(0);
                        }

                    }
                }

            }

            return totalClientes;
        }
    }
}
