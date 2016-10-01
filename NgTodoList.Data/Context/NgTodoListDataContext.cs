using NgTodoList.Data.Mapping;
using NgTodoList.Domain;
using System.Data.Entity;

namespace NgTodoList.Data.Context
{
    public class NgTodoListDataContext: DbContext
    {
        public NgTodoListDataContext()
            : base("NgTodoListConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false; //dificulta na criação de json 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new TodoMap());
        }
    }

    public class NgTodoListDataContextInitializer : DropCreateDatabaseAlways<NgTodoListDataContext>
    {
    }
}