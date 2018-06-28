using System.Data.Entity;

namespace SECI.DataAccess
{
    public class AmgenContext : DbContext
    {
        public AmgenContext()
        {
        }

        public AmgenContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=VORTIZ\\SQLSERVER2012;Initial Catalog=OXXO;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Usuarios>(Usuario => {
            //    Usuario.ToTable("Usuarios");
            //    Usuario.HasKey("UsuarioId");
            //});
            //modelBuilder.Entity<Proveedores>(Proveedor => {
            //    Proveedor.ToTable("Proveedores");
            //    Proveedor.HasKey("ProveedorId");
            //});

            //modelBuilder.Entity<Productos>(Producto => {
            //    Producto.ToTable("Productos");
            //    Producto.HasKey("ProductoId");
            //});

            base.OnModelCreating(modelBuilder);
        }

    }
}
