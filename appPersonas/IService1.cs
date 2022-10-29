using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace appPersonas
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        DataSet GetPersonRecords();

        [OperationContract]
        string InsertPersondetails(PersonsDetails persons);

        // TODO: agregue aquí sus operaciones de servicio
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    public class PersonsDetails
    {
        string docIdentidad = string.Empty;
        string nombres = string.Empty;
        string apellidos = string.Empty;
        string telefono = string.Empty;
        string tipoTelefono = string.Empty;
        string direccion = string.Empty;
        string tipoDireccion = string.Empty;
        string correo = string.Empty;
        DateTime fechaNacimiento = DateTime.Today.Date;
        [DataMember]
        public string IdentidadPerson
        {
            get { return docIdentidad; }
            set { docIdentidad = value; }
        }
        [DataMember]
        public string NombrePerson
        {
            get { return nombres; }
            set { nombres = value; }
        }
        [DataMember]
        public string ApellidoPerson
        {
            get { return apellidos; }
            set { apellidos = value; }
        }
        [DataMember]
        public DateTime NacimientoPerson
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }
        [DataMember]
        public string DireccionPerson
        {
            get { return direccion; }
            set { direccion = value; }
        }
        [DataMember]
        public string TipoDireccionPerson
        {
            get { return tipoDireccion; }
            set { tipoDireccion = value; }
        }
        [DataMember]
        public string CorreoPerson
        {
            get { return correo; }
            set { correo = value; }
        }
        [DataMember]
        public string TelefonoPerson
        {
            get { return telefono; }
            set { telefono = value; }
        }
        [DataMember]
        public string TipoTelefonoPerson
        {
            get { return tipoTelefono; }
            set { tipoTelefono = value; }
        }
    }
}
