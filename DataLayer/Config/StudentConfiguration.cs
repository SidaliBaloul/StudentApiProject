using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Student.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Data.Config
{
    public class StudentConfiguration : IEntityTypeConfiguration<Studentt>
    {
        public void Configure(EntityTypeBuilder<Studentt> builder)
        {
            builder.ToTable("Students");
        }
    }
}
