using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class ReactRepository : Repository<React> , IReactRepository
    {
        protected readonly Context Context;

        public ReactRepository(Context _context) : base(_context)
        {
            Context = _context;
        }

        //******************************************************

        public void Delete(React item)
        {
            Context.Remove(item);
        }

        //public List<React> GetAll(string[] includes = null)
        //{

        //    switch (includes.Length)
        //    {
        //        case 1: return Context.Set<React>().Include(includes[0]).ToList();
        //        case 2: return Context.Set<React>().Include(includes[0]).Include(includes[1]).ToList();
        //        case 3: return Context.Set<React>().Include(includes[0]).Include(includes[1]).Include(includes[2]).ToList();
        //        case 4: return Context.Set<React>().Include(includes[0]).Include(includes[1]).Include(includes[2]).Include(includes[3]).ToList();
        //        default: return Context.Set<React>().ToList();
        //    }
        //    //if (include == null)  // from default or passed from a calling function
        //    //{ 
        //    //    return Context.Set<React>().ToList();
        //    //}
        //    //return Context.Set<React>().Include(include).ToList();
        //}

        public React Get(int Id)
        {
            return Context.Set<React>().Find(Id);
        }

        public List<React> Get(Func<React, bool> where)
        {
            return Context.Set<React>().Where(where).ToList();
        }


        public int? GetReactCount(int postId, bool isLike)
        {
            return Context.React
                .Count(r => r.PostId == postId && r.Value == isLike);
        }




        public void Insert(React item)
        {
            Context.Add(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(React item)
        {
            Context.Update(item);
        }
    }
}
