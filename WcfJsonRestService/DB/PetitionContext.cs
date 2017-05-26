using System.Data.Entity;
using WcfJsonRestService.Model;

namespace WcfJsonRestService.DB
{
    public class PetitionContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating ( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Petition> ()
                .HasRequired ( s => s.Creator )
                .WithMany ( s => s.OwnedPetitions )
                .HasForeignKey ( s => s.CreatorId )
                .WillCascadeOnDelete ( false );

            modelBuilder.Entity<Petition> ()
                .HasMany ( s => s.Members )
                .WithMany ( s => s.SignedPetitions );

            modelBuilder.Entity<Petition> ()
                .HasMany ( s => s.Tags )
                .WithMany ( s => s.PetitionsWithTag );
        }

    }
}