using System;
using System.Collections.Generic;
using BlogsConsole.Models;

namespace BlogsConsole
{
    public class UserInterface
    {
        public void ShowMenu()
        {
            Console.WriteLine("Enter the number of one of the following options:");
            Console.WriteLine("1) Display all blogs");
            Console.WriteLine("2) Add blog");
            Console.WriteLine("3) Create post");
            Console.WriteLine("4) Exit");
        }

        public string GetMenuInput()
        {
            var validInputs = new List<string> { "1", "2", "3", "4"};
            var input = Console.ReadLine();
            while (!validInputs.Contains(input))
            {
                Console.WriteLine("Input not valid. Try again:");
                input = Console.ReadLine();
            }

            return input;
        }

        public void ShowAllBlogs(List<Blog> blogList)
        {
            foreach (var blog in blogList)
            {
                Console.WriteLine($"{blog.BlogId}. {blog.Name}");
            }
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

        public string GetBlogChoice(List<Blog> blogList, Dictionary<string, string> validChoices)
        {
            Console.WriteLine("Choose which blog to post to:");
            ShowAllBlogs(blogList);

            var userChoice = Console.ReadLine();
            while (userChoice!= null && !validChoices.ContainsKey(userChoice))
            {
                userChoice = Console.ReadLine();
            }

            return userChoice;
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
    }
}