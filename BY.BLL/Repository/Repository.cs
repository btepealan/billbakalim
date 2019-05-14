using BY.DAL.Context;
using BY.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BY.BLL.Repository
{
    
    public class Repository<T> : IRepository<T> where T : class
    {
        private BillBakalimContext _bbContext; //veritabanı
        private DbSet<T> _dbSet;   //tablo

        public Repository(BillBakalimContext context)
        {
            if (context != null)
            {
                _bbContext = context;
                _dbSet = _bbContext.Set<T>();
            }
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public IList<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> sorgu = _dbSet;
            if (filter != null)
                sorgu = sorgu.Where(filter);
            if (orderby != null)
                sorgu = orderby(sorgu);
            foreach (Expression<Func<T, object>> tablo in includes)
            {
                sorgu = sorgu.Include(tablo);
            }
            return sorgu.ToList();
        }
        public T Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> sorgu = _dbSet;
            if (filter != null)
                sorgu = sorgu.Where(filter);
            if (orderby != null)
                sorgu = orderby(sorgu);
            foreach (Expression<Func<T, object>> tablo in includes)
            {
                sorgu = sorgu.Include(tablo);
            }
            return sorgu.FirstOrDefault();
        }

        public bool Add(T entity)
        {
                bool Sonuc = false;
                try
                {
                    _dbSet.Add(entity);
                    Sonuc = Convert.ToBoolean(_bbContext.SaveChanges());
                }
                catch (Exception ex)
                {
                    string hata = ex.Message;
                    //throw new Exception("Kayıt yapılamadı!");
                }
                return Sonuc;
            }

        public bool Update()
        {
            bool Sonuc = false;
            try
            {
             _bbContext.SaveChanges();
                Sonuc = true;
            }
            catch (Exception ex)
            {
                string hata = ex.Message;
            }
            return Sonuc;
        }
        public decimal TotalBalance(string Id)
        {
          decimal totalb= _bbContext.UserTrans.Where(x=>x.UserId==Id).OrderByDescending(x=>x.Date).Take(1).Select(x=>x.Balance).FirstOrDefault();
            return totalb;
        }

 
    }
    }

