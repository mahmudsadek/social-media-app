using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using social_media_app.DBContext;
using social_media_app.Models;

namespace social_media_app.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        //private readonly PostContext _context;

        //public PostRepository(PostContext context)
        //{
        //    _context = context;
        //}

        //public List<Post> GetAll()
        //{
        //    return _context.Posts.ToList();
        //}

        //public Post GetById(int id)
        //{
        //    return _context.Posts.FirstOrDefault(p => p.Id == id);
        //}

        //public void Insert(Post post)
        //{
        //    _context.Posts.Add(post);
        //}

        //public void Update(Post post)
        //{
        //    _context.Posts.Update(post);
        //}

        //public void Delete(int id)
        //{
        //    var post = GetById(id);
        //    if (post != null)
        //        _context.Posts.Remove(post);
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        //public List<Post> GetAll(string[] includes = null)
        //{
        //    IQueryable<Post> query = _context.Posts;
        //    if (includes != null)
        //    {
        //        foreach (var include in includes)
        //        {
        //            query = query.Include(include);
        //        }
        //    }
        //    return query.ToList();
        //}

        //public Post Get(int id)
        //{
        //    return _context.Posts.Find(id);
        //}

        //public List<Post> Get(Func<Post, bool> where)
        //{
        //    return _context.Posts.Where(where).ToList();
        //}

        //public void Delete(Post item)
        //{
        //    _context.Posts.Remove(item);
        //}
        public PostRepository(Context _context) : base(_context)
        {
        }

        public void DeleteWithRelatedEntities(Post post)
        {
        
            var postToDelete = Context.Post
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Replays) 
                .Include(p => p.Reactions)
                .FirstOrDefault(p => p.Id == post.Id);

            if (postToDelete != null)
            {
               
                foreach (var comment in postToDelete.Comments)
                {
                  
                    Context.Replay.RemoveRange(comment.Replays);
                }
                Context.Comment.RemoveRange(postToDelete.Comments);

                
                Context.React.RemoveRange(postToDelete.Reactions);

                Context.Post.Remove(postToDelete);
            }

         
            Context.SaveChanges();
        }
    }
}
