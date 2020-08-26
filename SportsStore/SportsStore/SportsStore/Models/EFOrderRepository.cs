using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        //Include и ThenInclude позволяют извлечь из БД
        //связанные данные
        //
        public IQueryable<Order> Orders => context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            //AttachRange позволяет НЕ сохранять данные из заказа -
            //в данном случае данные о продуктах
            //("пока они не будут модифицированы")
            //
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}