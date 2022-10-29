using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace appPersonas
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string InsertPersondetails(PersonsDetails persons)
        {
            SqlConnection conn = new SqlConnection("Data Source=A2NWPLSK14SQL-v05.shr.prod.iad2.secureserver.net;Initial Catalog=App-MorarciGroup-db;UID=appMorarci;PWD=51ezx&U5;MultipleActiveResultSets=True");
            SqlConnection conn1 = new SqlConnection("Data Source=A2NWPLSK14SQL-v05.shr.prod.iad2.secureserver.net;Initial Catalog=App-MorarciGroup-db;UID=appMorarci;PWD=51ezx&U5;MultipleActiveResultSets=True");
            string resultado = "Proceso no realizado";
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Personas(docIdentidad,nombres,apellidos,fechaNacimiento,fechaCreacion) values(@docIdentidad,@nombres,@apellidos,@fechaNacimiento,getdate())",conn);
            cmd.Parameters.AddWithValue("@docIdentidad", persons.IdentidadPerson);
            cmd.Parameters.AddWithValue("@nombres", persons.NombrePerson);
            cmd.Parameters.AddWithValue("@apellidos", persons.ApellidoPerson);
            cmd.Parameters.AddWithValue("@fechaNacimiento", persons.NacimientoPerson);
            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    resultado = "La persona " + persons.NombrePerson + " " + persons.ApellidoPerson + " a sido creada satisfactoriamente";
                    string[] telefonosList = persons.TelefonoPerson.Split(',');
                    string[] tipotelefonosList = persons.TipoTelefonoPerson.Split(',');
                    string[] direccionList = persons.DireccionPerson.Split(',');
                    string[] tipodireccionList = persons.TipoDireccionPerson.Split(',');
                    string[] correoList = persons.CorreoPerson.Split(',');
                    int pos = 0;
                    foreach (string telefono in telefonosList)
                    {
                        if (pos < 2 && telefono != "")
                        {
                            conn1.Open();
                            SqlCommand cmd1 = new SqlCommand("insert into Personas_Telefono(docIdentidad,telefono,descripcion) values(@docIdentidad,@telefono,@descripcion)", conn1);
                            cmd1.Parameters.AddWithValue("@docIdentidad", persons.IdentidadPerson);
                            cmd1.Parameters.AddWithValue("@telefono", telefono);
                            cmd1.Parameters.AddWithValue("@descripcion", tipotelefonosList[pos]);
                            try
                            {
                                int resultTel = cmd1.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            conn1.Close();
                        }                       
                        pos++;                        
                    }
                    pos = 0;
                    foreach (string direccion in direccionList)
                    {
                        if(pos < 2 && direccion != "")
                        {
                            conn1.Open();
                            SqlCommand cmd1 = new SqlCommand("insert into Personas_Direcciones(docIdentidad,direccion,descripcion) values(@docIdentidad,@direccion,@descripcion)", conn1);
                            cmd1.Parameters.AddWithValue("@docIdentidad", persons.IdentidadPerson);
                            cmd1.Parameters.AddWithValue("@direccion", direccion);
                            cmd1.Parameters.AddWithValue("@descripcion", tipotelefonosList[pos]);
                            try
                            {
                                int resultDir = cmd1.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            conn1.Close();
                        }                        
                        pos++;                        
                    }
                    pos = 0;
                    foreach (string correo in correoList)
                    {
                        if (pos < 2 && correo != "")
                        {
                            conn1.Open();
                            SqlCommand cmd1 = new SqlCommand("insert into Personas_Correos(docIdentidad,correo) values(@docIdentidad,@correo)", conn1);
                            cmd1.Parameters.AddWithValue("@docIdentidad", persons.IdentidadPerson);
                            cmd1.Parameters.AddWithValue("@correo", correo);
                            try
                            {
                                int resultCorreo = cmd1.ExecuteNonQuery();
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            conn1.Close();
                        }                         
                    }
                }
                else
                {
                    resultado = "La persona " + persons.NombrePerson + " " + persons.ApellidoPerson + " ya se encuentra registrada o se presento un inconveniente. Volver a intentar";
                }
            }
            catch (Exception ex)
            {
                resultado = "La persona " + persons.NombrePerson + " " + persons.ApellidoPerson + " ya se encuentra registrada o se presento un inconveniente. Volver a intentar. Detaller error: " + ex.Message;
            }            
            conn.Close();
            return resultado;
        }

        public DataSet GetPersonRecords()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=A2NWPLSK14SQL-v05.shr.prod.iad2.secureserver.net;Initial Catalog=App-MorarciGroup-db;UID=appMorarci;PWD=51ezx&U5;MultipleActiveResultSets=True");
                string sql = "select docIdentidad,nombres,apellidos,fechaNacimiento=convert(varchar(10),fechaNacimiento,103),fechaCreacion,direccion=STUFF((SELECT ', ' + direccion FROM Personas_Direcciones where docIdentidad=Personas.docIdentidad FOR XML PATH('')), 1, 1, ''),correo=isnull(STUFF((SELECT ', ' + correo FROM Personas_Correos where docIdentidad=Personas.docIdentidad FOR XML PATH('')), 1, 1, ''),''),telefonos=isnull(STUFF((SELECT ', ' + telefono FROM Personas_Telefono where docIdentidad=Personas.docIdentidad FOR XML PATH('')), 1, 1, ''),'') from Personas order by fechaCreacion desc";

                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
            }
            catch (FaultException fex)
            {
                throw new FaultException<string>("Error: " + fex);
            }

            return ds;
        }
    }
}
