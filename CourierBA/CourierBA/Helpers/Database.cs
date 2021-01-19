using CourierBA.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourierBA.Helpers
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ProductoUso>().Wait();
        }

        public Task<List<ProductoUso>> GetDbProductos()
        {
            return _database.Table<ProductoUso>().ToListAsync();
        }

        public Task<int> SaveDbProductos(ProductoUso dbProductos)
        {
            return _database.InsertAsync(dbProductos);
        }
    }
}
