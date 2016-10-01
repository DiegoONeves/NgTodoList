using NgTodoList.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace NgTodoList.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");

            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired().HasMaxLength(60);
            
            Property(x => x.Email).IsRequired().HasMaxLength(160).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_USUARIO_EMAIL") { IsUnique = true }));
            
            Property(x => x.Password).IsRequired().HasMaxLength(32).IsFixedLength();

            HasMany(x => x.Todos);
        }
    }
}