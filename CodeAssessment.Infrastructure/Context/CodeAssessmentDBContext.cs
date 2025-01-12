using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace CodeAssessment.Infrastructure.Context
{


    public partial class CodeAssessmentDBContext : DbContext
    {
        public CodeAssessmentDBContext(DbContextOptions<CodeAssessmentDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__3214EC07854A0750");

                entity.ToTable("Products");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductDescrption)
                    .HasMaxLength(500);

                entity.Property(e => e.SearchKeyWord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasQueryFilter(c => !c.IsDeleted);

            });
        }
    }
}


