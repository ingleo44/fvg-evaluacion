using System.Runtime.Serialization;

namespace Promociones.Domain.Entities.Entities
{
    [DataContract]
    public class MedioPago
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int? IdEntidadFinanciera { get; set; }
        [DataMember]
        public int? IdTipoPago { get; set; }
        [DataMember]
        public bool Activo { get; set; }

    }
}