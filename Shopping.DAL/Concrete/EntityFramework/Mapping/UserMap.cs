using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.DAL.Concrete.EntityFramework.Mapping.BaseMap;
using ShoppingAPI.Entity.Poco;

namespace Shopping.DAL.Concrete.EntityFramework.Mapping
{
    public class UserMap:BaseMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(q => q.FirstName).HasMaxLength(200);
            builder.Property(q=>q.LastName).HasMaxLength(200).IsRequired();
            builder.Property(q=>q.Username).HasMaxLength(200).IsRequired();
            builder.Property(q => q.Password).HasMaxLength(50).IsRequired();
            builder.Property(q => q.Email).HasMaxLength(100).IsRequired();
            builder.Property(q => q.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(q => q.Adress).IsRequired();
        }
    }
}
