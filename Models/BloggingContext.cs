using System.Data.Entity;
using System.Linq;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }

        public void AddPost(Post post)
        {
            this.Posts.Add(post);
            this.SaveChanges();
        }
        
        public void RemoveBlog(Blog blog)
        {
            var posts = this.Posts.Where(p => p.BlogId == blog.BlogId);
            this.Posts.RemoveRange(posts);
            this.Blogs.Remove(blog);
            this.SaveChanges();
        }

        public void RemovePost(Post post)
        {
            this.Posts.Remove(post);
            this.SaveChanges();
        }

        public void EditBlog(int blogId, string name)
        {
            var blog = Blogs.First(b => b.BlogId == blogId);
            if (blog != null)
            {
                blog.Name = name;
                this.SaveChanges();
            }
            else
            {
                NLog.LogManager.GetCurrentClassLogger().Error("Blog not found");
            }
        }

        public void EditPost(int postId, string title, string content)
        {
            var post = Posts.First(p => p.PostId == postId);
            if (post != null)
            {
                post.Title = title;
                post.Content = content;
                this.SaveChanges();
            }
            else
            {
                NLog.LogManager.GetCurrentClassLogger().Error("Post not found");
            }
        }
    }
}
