﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExamenDatabase : DbContext
    {
        public ExamenDatabase()
            : base("name=ExamenDatabase")
        {
            //Configuration.ProxyCreationEnabled = true;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PEDIDOS_DETALLE_W> PEDIDOS_DETALLE_W { get; set; }
        public virtual DbSet<PEDIDOS_W> PEDIDOS_W { get; set; }
        public virtual DbSet<PRODUCTO_W> PRODUCTO_W { get; set; }
        public virtual DbSet<USUARIOS_W> USUARIOS_W { get; set; }
    }
}
