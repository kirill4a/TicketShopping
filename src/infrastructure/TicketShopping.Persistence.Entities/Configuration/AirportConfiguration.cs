using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketShopping.Persistence.Entities.Models;

namespace TicketShopping.Persistence.Entities.Configuration;

internal class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        var tableName = nameof(Airport);

        builder.ToTable(tableName);
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name).HasMaxLength(512).IsRequired();
        builder.Property(a => a.Iata).HasMaxLength(3).IsRequired(false);
        builder.Property(a => a.Icao).HasMaxLength(4).IsRequired(false);
        builder.Property(a => a.Latitude).IsRequired();
        builder.Property(a => a.Longitude).IsRequired();

        builder.HasIndex(a => a.Iata)
               .HasDatabaseName($"UIX_{tableName}_{nameof(Airport.Iata)}")
               .IsUnique();

        builder.HasIndex(a => a.Icao)
               .HasDatabaseName($"UIX_{tableName}_{nameof(Airport.Icao)}")
               .IsUnique();
    }
}
