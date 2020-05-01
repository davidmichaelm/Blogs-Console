using System;
using System.Collections.Generic;
using System.Linq;
using BlogsConsole.Models;

namespace BlogsConsole
{
    public class UserInterface
    {
        public void DisplayMenu()
        {
            Console.WriteLine("Enter the number of one of the following options:");
            Console.WriteLine("1. Display all blogs");
            Console.WriteLine("2. Add blog");
            Console.WriteLine("3. Create post");
            Console.WriteLine("4. Display posts");
            Console.WriteLine("5. Edit Blog");
            Console.WriteLine("6. Edit Post");
            Console.WriteLine("7. Delete Blog");
            Console.WriteLine("8. Delete Post");
            Console.WriteLine("9. Exit");
        }
        
        public void DisplayBlogs(List<Blog> blogList)
        {
            for (var i = 1; i <= blogList.Count; i++)
            {
                Console.WriteLine($"{i}. {blogList[i-1].Name}");
            }
        }

        public void DisplayBlogPosts(Blog blog)
        {
            if (blog.Posts.Count > 0)
            {
                Console.WriteLine("Blog: " + blog.Name);
                foreach (var post in blog.Posts)
                {
                    Console.WriteLine("PostId: " + post.PostId);
                    Console.WriteLine("Title: " + post.Title);
                    Console.WriteLine("Content: " + post.Content);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("0 posts were returned\n");
            }
        }

        public void DisplayBlogPosts(List<Blog> blogList)
        {
            foreach (var blog in blogList)
            {
                if (blogList.Count > 0)
                {
                    DisplayBlogPosts(blog);
                }
            }
        }

        public string GetMenuInput(List<string> validInputs)
        {
            var input = Console.ReadLine();
            while (!validInputs.Contains(input))
            {
                Console.WriteLine("Input not valid. Try again:");
                input = Console.ReadLine();
            }

            return input;
        }

        

        public string GetBlogName()
        {
            Console.Write("Enter a name for a new Blog: ");
            var name = Console.ReadLine();
            while (name == null || name.Equals(""))
            {
                Console.WriteLine("Please enter at least one character for the name:");
                name = Console.ReadLine();
            }

            return name;
        }
        
        public string GetPostTitle()
        {
            Console.Write("Enter a title for the post: ");
            var title = Console.ReadLine();
            while (title == null || title.Equals(""))
            {
                Console.WriteLine("Please enter at least one character for the title:");
                title = Console.ReadLine();
            }

            return title;
        }

        public string GetPostContent()
        {
            Console.WriteLine("Enter the content of the post: ");
            var content = Console.ReadLine();
            while (content == null || content.Equals(""))
            {
                Console.WriteLine("Please enter at least one character for the title:");
                content = Console.ReadLine();
            }

            return content;
        }
        
        public bool GetDeleteConfirmation(Post post)
        {
            Console.WriteLine($"Are you sure you want to delete the Post with title {post.Title}? y/n");
            return GetYesOrNo();
        }
        
        public bool GetDeleteConfirmation(Blog blog)
        {
            Console.WriteLine($"Are you sure you want to delete the Blog with name {blog.Name}? y/n");
            return GetYesOrNo();
        }
        
        private bool GetYesOrNo()
        {
            var userChoice = Console.ReadLine();
            while (userChoice == null || (userChoice.ToUpper() != "Y" && userChoice.ToUpper() != "N"))
            {
                Console.WriteLine("Please enter 'y' for yes or 'n' for no:");
                userChoice = Console.ReadLine();
            }

            return userChoice.ToUpper() == "Y";
        }

        public Blog GetExistingBlogChoice(List<Blog> blogList)
        {
            var validChoices = DetermineValidBlogChoices(blogList);

            Console.WriteLine("Choose a blog:");
            DisplayBlogs(blogList);

            var userChoice = Console.ReadLine();
            while (userChoice == null || !validChoices.ContainsKey(userChoice))
            {
                Console.WriteLine("Please enter a valid blog number:");
                userChoice = Console.ReadLine();
            }

            return blogList.First(b => b.BlogId == validChoices[userChoice]);
        }

        

        public List<Blog> GetExistingBlogListChoice(List<Blog> blogList)
        {
            var validChoices = DetermineValidBlogChoices(blogList);
            validChoices.Add("0", 0); // All blogs

            Console.WriteLine("Select the the blog:");
            Console.WriteLine("0. Posts from all blogs");
            DisplayBlogs(blogList);

            var userChoice = Console.ReadLine();
            while (userChoice == null || !validChoices.ContainsKey(userChoice))
            {
                Console.WriteLine("Please enter a valid blog number:");
                userChoice = Console.ReadLine();
            }

            var returnList = new List<Blog>();

            if (validChoices[userChoice] == 0)
            {
                returnList = blogList;
            }
            else
            {
                returnList.Add(blogList.First(b => b.BlogId == validChoices[userChoice]));
            }

            return returnList;
        }
        
        public int GetExistingPostIdChoice(List<Blog> blogs)
        {
            var chosenBlogs = GetExistingBlogListChoice(blogs).Where(b => b.Posts.Count > 0).ToList();
            if (chosenBlogs.Count == 0)
            {
                showMessage("0 posts were returned");
                return -1;
            }
            DisplayBlogPosts(chosenBlogs);

            var validChoices = DetermineValidPostChoices(chosenBlogs);

            Console.WriteLine("Enter a Post Id:");

            var userChoice = Console.ReadLine();
            int parsedChoice;
            while (userChoice == null || !int.TryParse(userChoice, out parsedChoice) || !validChoices.Contains(parsedChoice))
            {
                Console.WriteLine("Please enter a valid Post ID from the choices above:");
                userChoice = Console.ReadLine();
            }
            
            return parsedChoice;
        }
        
        

        private Dictionary<string, int> DetermineValidBlogChoices(List<Blog> blogList)
        {
            var index = 1;
            var validChoices = new Dictionary<string, int>();
            foreach (var blog in blogList)
            {
                validChoices.Add(index.ToString(), blog.BlogId);
                index++;
            }

            return validChoices;
        }

        private List<int> DetermineValidPostChoices(List<Blog> blogList)
        {
            var validChoices = new List<int>();
            foreach (var blog in blogList)
            {
                foreach (var post in blog.Posts)
                {
                    validChoices.Add(post.PostId);
                }
            }

            return validChoices;
        }
        
        public void showMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}