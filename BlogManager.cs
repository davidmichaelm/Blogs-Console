using System;
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
                _userInterface.DisplayMenu();
                var validInputs = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9"};
                var menuInput = _userInterface.GetMenuInput(validInputs);
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
                        EditBlog();
                        break;
                    case "6":
                        EditPost();
                        break;
                    case "7":
                        DeleteBlog();
                        break;
                    case "8":
                        DeletePost();
                        break;
                    case "9":
                        keepRunning = false;
                        break;
                }
            }
        }

        private void ShowAllBlogs()
        {
            using (var db = new BloggingContext())
            {
                _userInterface.DisplayBlogs(db.Blogs.ToList());
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
                
                var blog = _userInterface.GetExistingBlogChoice(blogs);

                var title = _userInterface.GetPostTitle();
                var content = _userInterface.GetPostContent();

                var post = new Post()
                {
                    Title = title,
                    Content = content,
                    Blog = blog
                };
                
                db.AddPost(post);
            }
        }

        private void DisplayPosts()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include("Posts").ToList();

                var chosenBlogs = _userInterface.GetExistingBlogListChoice(blogs);
                chosenBlogs = chosenBlogs.Where(b => b.Posts.Count > 0).ToList();
                _userInterface.DisplayBlogPosts(chosenBlogs);
            }
        }

        private void EditBlog()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include("Posts").ToList();

                var blog = _userInterface.GetExistingBlogChoice(blogs);
                var name = _userInterface.GetBlogName();

                db.EditBlog(blog.BlogId, name);
            }
        }
        
        private void EditPost()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include("Posts").ToList();
                var postId = _userInterface.GetExistingPostIdChoice(blogs);
                if (postId == -1) return; // No posts found
                
                var title = _userInterface.GetPostTitle();
                var content = _userInterface.GetPostContent();
                
                db.EditPost(postId, title, content);
            }
        }
        
        private void DeleteBlog()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.ToList();

                var blog = _userInterface.GetExistingBlogChoice(blogs);

                if (_userInterface.GetDeleteConfirmation(blog))
                {
                    db.RemoveBlog(blog);
                    _userInterface.showMessage("Blog deleted");
                }
                else
                {
                    _userInterface.showMessage("Blog not deleted");
                }

            }
        }
        
        private void DeletePost()
        {
            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include("Posts").ToList();
                var postId = _userInterface.GetExistingPostIdChoice(blogs);
                if (postId == -1) return; // No posts found

                var post = db.Posts.First(p => p.PostId == postId);
                
                if (_userInterface.GetDeleteConfirmation(post))
                {
                    db.RemovePost(post);
                    _userInterface.showMessage("Post deleted");
                }
                else
                {
                    _userInterface.showMessage("Post not deleted");
                }
            }
        }
        
    }
}