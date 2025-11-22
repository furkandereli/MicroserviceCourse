using MongoDB.EntityFrameworkCore.Extensions;
using DiscountEntity = MicroserviceCourse.Discount.Api.Features.Discounts.Discount;

namespace MicroserviceCourse.Discount.Api.Repositories;

public class DiscountEntityConfiguration : IEntityTypeConfiguration<DiscountEntity>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DiscountEntity> builder)
    {
        builder.ToCollection("discounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasElementName("_id").ValueGeneratedNever();
        builder.Property(x => x.Code).HasElementName("code").HasMaxLength(10);
        builder.Property(x => x.Rate).HasElementName("rate");
        builder.Property(x => x.UserId).HasElementName("user_id");
        builder.Property(x => x.Created).HasElementName("created");
        builder.Property(x => x.Updated).HasElementName("updated");
        builder.Property(x => x.Expired).HasElementName("expired");
    }
}
