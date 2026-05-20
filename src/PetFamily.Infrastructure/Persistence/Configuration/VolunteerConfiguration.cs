using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Persistence.Configuration;

public sealed class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
                .HasConversion(id => id.Value,
                                value => VolunteerId.Create(value));

        builder.Property(p => p.FullName)
            .HasConversion(
                fn => fn.Value,
                value => FullName.Create(value).Value)
            .IsRequired()
            .HasColumnName("full_name")
            .HasMaxLength(FullName.MAX_LENGTH);

        builder.Property(p => p.Email)
            .HasConversion(
                e => e.Value,
                value => Email.Create(value).Value)
            .IsRequired()
            .HasColumnName("email")
            .HasMaxLength(Email.MAX_LENGTH);

        builder.Property(p => p.Phone)
            .HasConversion(
                p => p.Value,
                value => PhoneNumber.Create(value).Value)
            .IsRequired()
            .HasColumnName("phone")
            .HasMaxLength(PhoneNumber.MAX_LENGTH);

        builder.Property(p => p.Description)
            .HasConversion(
                d => d.Value,
                value => Description.Create(value).Value)
            .IsRequired()
            .HasColumnName("description")
            .HasMaxLength(Description.MAX_LENGTH);

        builder.Property(p => p.ExperienceInYears)
            .IsRequired(false)
            .HasColumnName("experience_in_years");

        builder.HasMany(p => p.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(p => p.RequisitesDetails, r =>
        {
            r.ToJson();
            r.OwnsMany(x => x.RequisitesList, n =>
            {
                n.Property(v => v.Name)
                    .IsRequired()
                    .HasColumnName("requisites_name")
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                n.Property(v => v.Description)
                    .IsRequired()
                    .HasColumnName("requisites_description")
                    .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH);
            });
        });

        builder.OwnsOne(p => p.SocialNetworkDetails, s =>
        {
            s.ToJson();
            s.OwnsMany(x => x.SocialNetworks, n =>
            {
                n.Property(l => l.Link)
                    .IsRequired()
                    .HasColumnName("social_networks_link")
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                n.Property(l => l.Title)
                    .IsRequired(false)
                    .HasColumnName("social_networks_title")
                    .HasMaxLength(Constants.MAX_MEDIUM_TEXT_LENGTH);
            });
        });
    }
}
