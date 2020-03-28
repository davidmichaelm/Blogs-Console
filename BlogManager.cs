using System.Collections.Generic;
using System.Linq;
using BlogsConsole.Models;

namespace BlogsConsole
{
    public class BlogManager
    {
        private UserInterface _userInterface;

        public BlogManager()
        {
            this._userInterface = new UserInterface();
        }

        public void Run()
        {
            var keepRunning = true;
            while (keepRunning)
            {
                _userInterface.ShowMenu();
                var menuInput = _userInterface.GetMenuInput();
                switch (menuInput)
                {
                    case "1":
                        ShowAllBlogs();
                        break;
                    case "2":
                        CreateNewBlog();
                        break;
                    case "3":
                        CreateNewPost();
                        break;
                    case "4":
                        DisplayPosts();
                        break;
                    case "5":
                        keepRunning = false;
                        break;
                }
            }
        }

        private void ShowAllBlogs()
        {
            using (var db = new BloggingContext())
            {
                _userInterface.ShowAllBlogs(db.Blogs.ToList());
            }
        }

        private void CreateNewBlog()
        {
            var name = _userInterface.GetBlogName();
            var blog = new Blog { Name = name };
            using (var db = new BloggingContext())
            {
                db.AddBlog(blog);
            }
        }

        private void CreateNewPost()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.ToList();
                
                var userChoice = _userInterface.GetBlogChoice(blogs);
                var chosenBlog = db.Blogs.First(b => b.BlogId.ToString() == userChoice);

                var title = _userInterface.GetPostTitle();
                var content = _userInterface.GetPostContent();

                var post = new Post()
                {
                    Title = title,
                    Content = content,
                    Blog = chosenBlog
                };
                
                db.AddPost(post);
            }
        }

        private void DisplayPosts()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include("Posts").ToList();

                var userChoice = _userInterface.GetPostDisplayChoice(blogs);
                if (userChoice == "0")
                {
                    foreach (var blog in blogs)
                    {
                        _userInterface.DisplayBlogPosts(blog);
                    }
                }
                else
                {
                    var chosenBlog = db.Blogs.First(b => b.BlogId.ToString() == userChoice);
                    _userInterface.DisplayBlogPosts(chosenBlog);
                }
            }
        }
    }
}