using BlogDemo.DAL;
using BlogDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo
{
    class Program
    {
        // Full Demo @
        // https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                //Create & Save new blog
                Console.Write("Enter a Name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();

                // Display all the blogs from the db
                var allBlogs = db.Blogs.ToList();
                Console.WriteLine($"There are {allBlogs.Count} blogs in the database.");
                foreach (var item in allBlogs)
                    Console.WriteLine($"{item.BlogId} {item.Name}");

                //TODO: 1) Get the user to choose a blog, and then add a post to that blog.
                Console.WriteLine("");
                Console.Write("Choose a Blog to add a post into: ");
                var chosenBlog = int.Parse(Console.ReadLine());
                Console.Write("Post Title: ");
                var postTitle = Console.ReadLine();
                Console.Write("Enter Post Content: ");
                var postContent = Console.ReadLine();

                var addContent = new Post { BlogId = chosenBlog, Title = postTitle, Content = postContent };

                db.Posts.Add(addContent);
                db.SaveChanges();

                Console.Write("Preview Blog? (Y/N): ");
                var YesNo = char.Parse(Console.ReadLine());
                switch (YesNo)
                {
                    case 'Y':
                        Console.Clear();
                        Console.WriteLine($"Post Id: {addContent.PostId}");
                        Console.WriteLine($"Blog Name: {addContent.Title}");
                        Console.WriteLine($"{addContent.Content}");
                        Console.ReadKey();
                        break;
                    case 'N':
                        Console.Clear();
                        Console.WriteLine("Thank you...");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Thank you...");
                        break;
                }


            }
        }
    }
    namespace Entities
    {
        //TODO: 2) Follow up by making changes to entities and trying out Database Migrations
        //              (As discussed in the MSDN demo linked in Program.cs)
        public class Blog
        {
            public int BlogId { get; set; }
            public string Name { get; set; }

            public virtual ICollection<Post> Posts { get; set; }
        }
        public class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public int BlogId { get; set; }

            public virtual Blog Blog { get; set; }
        }

    }// End of Entities
    namespace DAL
    {
        public class BloggingContext : DbContext
        {
            public BloggingContext() : base("name=BlogDb")
            {

            }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

        }

    }
}
