using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteApp.Infraestructure.Models;

namespace NoteApp.Infraestructure.Data;

public class NoteAppContext : IdentityDbContext<Usuarios, IdentityRole<Guid>, Guid>
{
    public NoteAppContext(DbContextOptions<NoteAppContext> options) : base(options)
    {
        
    }

    public DbSet<Lembretes> Lembretes { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Lembretes>()
            .HasKey(x => x.Id);

        builder.Entity<Lembretes>()
            .HasOne<Usuarios>()
            .WithMany()
            .HasForeignKey(x => x.UserId);


        builder.Entity<Lembretes>()
            .Property(x => x.Titulo)
            .HasMaxLength(100);
        
        builder.Entity<Lembretes>()
            .Property(x => x.Descricao)
            .HasMaxLength(300);

        builder.Entity<Lembretes>()
            .Property(x => x.DataCriacao);
        
        builder.Entity<Lembretes>()
            .Property(x => x.DataModificacao);
        
        builder.Entity<Lembretes>()
            .Property(x => x.DataAlerta);

        builder.Entity<Usuarios>()
            .Property(x => x.UserName)
            .HasMaxLength(50);

        base.OnModelCreating(builder);
    }
}
