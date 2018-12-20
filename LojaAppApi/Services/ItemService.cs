using LojaAppApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaAppApi.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetAll();
    }
    public class ItemService : IItemService
    {
        Model.AppContext db;
        public ItemService(Model.AppContext _db)
        {
            db = _db;
        }

        public async Task<List<Item>> GetAll()
        {
            if (db != null)
            {
                return await db.Items.ToListAsync();
            }

            return null;
        }
    }
}
