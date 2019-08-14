using System;
using System.Collections.Generic;
using LDSData.DBContext;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LDSData.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private LDSEntities _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new LDSEntities();
            table = _context.Set<T>();
        }
        public GenericRepository(LDSEntities _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }


        public IEnumerable<T> GetAll()
        {
            return table.ToList();
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            return table.Find(id);
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            throw new NotImplementedException();
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            throw new NotImplementedException();
        }
        public void Save()
        {
            _context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}