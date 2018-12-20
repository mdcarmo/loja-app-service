using LojaAppApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaAppApi.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int? id);
        Task<int> Add(Customer entity);
        Task<int> Delete(int? id);
        Task Update(Customer entity);
    }

    public class CustomerService : ICustomerService
    {
        Model.AppContext db;
        public CustomerService(Model.AppContext _db)
        {
            db = _db;
        }

        public async Task<int> Add(Customer entity)
        {
            if (db != null)
            {
                await db.Customers.AddAsync(entity);
                await db.SaveChangesAsync();

                return entity.CustomerID;
            }

            return 0;
        }

        public async Task<int> Delete(int? id)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var entity = await db.Customers.FirstOrDefaultAsync(x => x.CustomerID == id);

                if (entity != null)
                {
                    //Delete that post
                    db.Customers.Remove(entity);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<List<Customer>> GetAll()
        {
            if (db != null)
            {
                return await db.Customers.ToListAsync();
            }

            return null;
        }

        public async Task<Customer> GetById(int? id)
        {
            if (db != null)
            {
                return await db.Customers
                    .SingleOrDefaultAsync<Customer>(rr => rr.CustomerID == id);
            }

            return null;
        }

        public async Task Update(Customer entity)
        {
            if (db != null)
            {
                //Delete that post
                db.Customers.Update(entity);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }
}
