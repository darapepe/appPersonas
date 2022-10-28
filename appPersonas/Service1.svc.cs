using System;
using System.Collections.Generic;
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
            string resultado = "Proceso no realizado";
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Personas(docIdentidad,nombres,apellidos,fechaNacimiento) values(@docIdentidad,@nombres,@apellidos,@fechaNacimiento)",conn);
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
    }
}
