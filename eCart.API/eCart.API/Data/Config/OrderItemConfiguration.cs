using System;
using eCart.API.Data.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCart.API.Data.Config
{
	public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem>
	{

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io =>
            {
                io.WithOwner();
            });

            builder.Property(i => i.Price)
                .HasColumnType("decimal(18.2)");


        }
    }
}

