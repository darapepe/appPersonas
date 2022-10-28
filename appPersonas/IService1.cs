using System;
using System.Collections.Generic;
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
    }
}
