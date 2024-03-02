using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;

namespace Repository_Layer.Context
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions<FundoContext> options) : base(options) { }

        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<NoteEntity> NotesTable { get; set; }
        public DbSet<LabelEntity> LabelTable { get; set; }
    }
}
