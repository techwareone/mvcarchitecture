using System.Data.Entity.ModelConfiguration;
using Store.Model;

namespace Store.Data.Configuration
{
    // The EntityTypeConfiguration class is part of the EntityFramework (v6.1.2 for this solution).
    // https://msdn.microsoft.com/en-us/library/gg696117(v=vs.113).aspx
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            // https://msdn.microsoft.com/en-us/library/gg696407(v=vs.113).aspx
            ToTable("Categories");

            // https://msdn.microsoft.com/en-us/library/gg696156(v=vs.113).aspx
            Property(c => c.Name)
                .IsRequired() // https://msdn.microsoft.com/en-us/library/system.data.entity.modelconfiguration.configuration.stringpropertyconfiguration(v=vs.113).aspx
                .HasMaxLength(50);
        }
    }
}