using Microsoft.EntityFrameworkCore;
using System.Linq;
using NSE.Catalogo.API.Models;
using NSE.Core.Data;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork // padrão de projeto, interface que commita as mudanças
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuração default caso uma propriedade string não seja definida no mapping
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            // qualquer clase que implemente IEntityTypeConfiguration e que está sendo representada no contexto vai ser mapeada
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            //SaveChangesAsync() traz o numero de linhas afetadas
            //então se retornar acima de zero deu certo, se não deu errado
            return await base.SaveChangesAsync() > 0;
        }
    }
}