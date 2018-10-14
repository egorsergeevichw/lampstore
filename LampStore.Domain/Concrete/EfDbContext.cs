using LampStore.Domain.Entities;
using System.Data.Entity;

namespace LampStore.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("LampStoreDb") {}

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<FeedbackEntity> Feedbacks { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
    }
}
