//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PuntoVenta.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PEDIDOS_W
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEDIDOS_W()
        {
            this.PEDIDOS_DETALLE_W = new HashSet<PEDIDOS_DETALLE_W>();
        }
    
        public int ID { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
        public Nullable<System.DateTime> DATE_SALE { get; set; }
        public string USERNAME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDOS_DETALLE_W> PEDIDOS_DETALLE_W { get; set; }
        public virtual USUARIOS_W USUARIOS_W { get; set; }
    }
}
