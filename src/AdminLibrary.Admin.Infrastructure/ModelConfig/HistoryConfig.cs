using AdminLibrary.Admin.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminLibrary.Models.Configs
{
    public class HistoryConfig : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("MATERIALS_HISTORY", "masters");

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .HasColumnName("ID_HISTORY");

            builder.Property(x => x.MaterialsId)
                .HasColumnType("int")
                .HasColumnName("ID_MATERIAL");

            builder.Property(x => x.UserId)
                .HasColumnType("int")
                .HasColumnName("ID_USER");

            builder.Property(x => x.Observations)
                .HasColumnType("varchar")
                .HasColumnName("OBSERVATIONS");

            builder.Property(x => x.MovementType)
                .HasColumnType("varchar")
                .HasColumnName("MOVEMENT_TYPE");

            builder.Property(x => x.MovementDate)
                .HasColumnType("datetime")
                .HasColumnName("MOVEMENT_DATE");

            Auditory(builder);
        }

        private static void Auditory(EntityTypeBuilder<History> builder)
        {
            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");

            builder.Property(x => x.CreateUser)
                .HasColumnType("varchar")
                .HasColumnName("CREATE_USER");

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");

            builder.Property(x => x.UpdateUser)
                .HasColumnType("varchar")
                .HasColumnName("UPDATE_USER");
        }
    }
}
