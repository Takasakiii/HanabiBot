using Hanabi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hanabi.Core.Database.Configurations;

public class ServerConfigurationsConfiguration : IEntityTypeConfiguration<ServerConfiguration>
{
    public void Configure(EntityTypeBuilder<ServerConfiguration> builder)
    {
        builder.ToTable("server_configurations");

        builder.HasKey(x => x.Id);
    }
}