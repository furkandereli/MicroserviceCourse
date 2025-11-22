using MicroserviceCourse.Catalog.Api.Features.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroserviceCourse.Catalog.Api.Repositories;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToCollection("categories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasElementName("_id").ValueGeneratedNever();
        builder.Ignore(x => x.Courses);
    }
}
