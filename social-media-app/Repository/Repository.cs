using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;


namespace social_media_app.Repository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        protected readonly Context Context;

        public Repository(Context _context)
        {
            Context = _context;
        }

        //******************************************************

        public void Delete(T item)
        {
            Context.Remove(item);
        }

        public List<T> GetAll(string[]? includes = null) 
        {
            if (includes == null)
            {
                return Context.Set<T>().ToList();
            }
            else
            {
                switch (includes.Length)
                {
                    case 1: return Context.Set<T>().Include(includes[0]).ToList();
                    case 2: return Context.Set<T>().Include(includes[0]).Include(includes[1]).ToList();
                    case 3: return Context.Set<T>().Include(includes[0]).Include(includes[1]).Include(includes[2]).ToList();
                    case 4: return Context.Set<T>().Include(includes[0]).Include(includes[1]).Include(includes[2]).Include(includes[3]).ToList();
                    default: return Context.Set<T>().ToList();
                }
            }
            
            //if (include == null)  // from default or passed from a calling function
            //{ 
            //    return Context.Set<T>().ToList();
            //}
            //return Context.Set<T>().Include(include).ToList();
        }

        public T Get(int Id)
        {
            return Context.Set<T>().Find(Id);
        }

        public List<T> Get(Func<T, bool> where)
        {
            return Context.Set<T>().Where(where).ToList();
        }

        public void Insert(T item)
        {
            Context.Add(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(T item)
        {
            Context.Update(item);
        }
    }
}
