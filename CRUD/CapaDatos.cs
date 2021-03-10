using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CRUD
{
    public class CapaDatos
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormsContacts;Data Source=LAPTOP-IGL63IO0\\SQLEXPRESS");
            
        public void InsertarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                string query = @"
                                  INSERT INTO Contactos(nombre, apellido, telefono, direccion)
                                         VALUES(@Nombre, @Apellido, @Telefono, @Direccion) ";

                //SqlParameter nombre = new SqlParameter();
                //             nombre.ParameterName = "@Nombre";
                //             nombre.Value = contacto.Nombre;
                //             nombre.DbType = System.Data.DbType.String;

                SqlParameter nombre = new SqlParameter("@nombre", contacto.Nombre);
                SqlParameter apellido = new SqlParameter("@apellido", contacto.Apellido);
                SqlParameter telefono = new SqlParameter("@telefono", contacto.Telefono);
                SqlParameter direccion = new SqlParameter("@direccion", contacto.Direccion);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(nombre);
                command.Parameters.Add(apellido);
                command.Parameters.Add(telefono);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                conn.Close();
            }
        }

        public void ActualizarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                string query = @" UPDATE contactos
                                   SET Nombre = @nombre,
                                       Apellido = @apellido,
                                       Telefono = @telefono,
                                       Direccion = @direccion
                                WHERE Id = @Id";

                SqlParameter id = new SqlParameter("@id", contacto.Id);
                SqlParameter nombre = new SqlParameter("@nombre", contacto.Nombre);
                SqlParameter apellido = new SqlParameter("@apellido", contacto.Apellido);
                SqlParameter telefono = new SqlParameter("@telefono", contacto.Telefono);
                SqlParameter direccion = new SqlParameter("@direccion", contacto.Direccion);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(id);
                command.Parameters.Add(nombre);
                command.Parameters.Add(apellido);
                command.Parameters.Add(telefono);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            finally { conn.Close(); }
        }

        public void EliminarContacto(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contactos WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }
        public List<Contacto>GetContactos(string TextoBuscar = null)
        {
            List<Contacto> contactos = new List<Contacto>();

            try
            {
                conn.Open();
                string query = @"SELECT Id, Nombre, Apellido, Telefono, Direccion
                                  FROM Contactos";

                SqlCommand command = new SqlCommand();

                if(!string.IsNullOrEmpty(TextoBuscar))
                {
                    query += @" WHERE Nombre LIKE @textoBuscar OR Apellido LIKE @textoBuscar OR Telefono LIKE @textoBuscar
                               OR Direccion LIKE @textoBuscar ";
                    command.Parameters.Add(new SqlParameter("@textoBuscar", $"%{TextoBuscar}%"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contactos.Add(new Contacto
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString()

                    }) ;
                }
            }
            catch (Exception)
            {

                throw;
            }

            finally { conn.Close(); }

            return contactos;
        }
    }
}
