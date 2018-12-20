using LojaAppApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaAppApi.Services
{
    public interface IOrderService
    {
        Task<System.Object> GetAll();
        Task<Tuple<object, object>> GetById(int? id);
        Task<int> Add(Order entity);
        Task<int> Delete(int? id);
        Task Update(Order entity);
    }

    public class OrderService : IOrderService
    {
        Model.AppContext db;
        public OrderService(Model.AppContext _db)
        {
            db = _db;
        }

        public async Task<int> Add(Order entity)
        {
            if (db != null)
            {
                await db.Orders.AddAsync(entity);
                await db.SaveChangesAsync();

                return entity.OrderID;
            }

            return 0;
        }

        public async Task<int> Delete(int? id)
        {
            int result = 0;

            if (db != null)
            {
                Order order = db.Orders.Include(y => y.OrderItems)
                .SingleOrDefault(x => x.OrderID == id);

                foreach (var item in order.OrderItems.ToList())
                {
                    db.OrderItems.Remove(item);
                }

                db.Orders.Remove(order);

                result = await db.SaveChangesAsync();
            }

            return result;
        }

        public async Task<System.Object> GetAll()
        {
            if (db != null)
            {
                var result = (from a in db.Orders
                              join b in db.Customers on a.CustomerID equals b.CustomerID

                              select new
                              {
                                  a.OrderID,
                                  a.OrderNo,
                                  Customer = b.Name,
                                  a.PMethod,
                                  a.GTotal
                              }).ToListAsync();

                return await result;
            }
            return null;
        }

        public async Task Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<object, object>> GetById(int? id)
        {

            var order = await (from a in db.Orders
                               where a.OrderID == id
                               select new
                               {
                                   a.OrderID,
                                   a.OrderNo,
                                   a.CustomerID,
                                   a.PMethod,
                                   a.GTotal,
                                   DeletedOrderItemIDs = "",
                               }).FirstOrDefaultAsync();

            var orderDetails = await (from a in db.OrderItems
                                      join b in db.Items on a.ItemID equals b.ItemID
                                      where a.OrderID == id

                                      select new
                                      {
                                          a.OrderID,
                                          a.OrderItemID,
                                          a.ItemID,
                                          ItemName = b.Name,
                                          b.Price,
                                          a.Quantity,
                                          Total = a.Quantity * b.Price
                                      }).ToListAsync();


            return new Tuple<object, object>(order, orderDetails);
        }
    }
}
