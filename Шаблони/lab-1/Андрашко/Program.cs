using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Шаблони;
using System.IO;

namespace Шаблони
{
    public class WebPage
    {
        public List<string> strings= new List<string>();
 
        public WebPage() {
        }

        public void ShowPageStructure(List<Post> posts,string header = "",bool anounce = false,string author_name = "",string author_phone = "")
        {
            strings.Clear();    
            if (header != "")
            {
                    strings.Add($"<header>{header}</header>");
            }

            if (anounce)
            {
                if(posts.Count > 0)
                {
                    strings.Add("<ul>");
                    foreach (var item in posts)
                    {
                        strings.Add($"<li><a href='#{item.post_id}'>{item.post_title}</a></li>");
                    }
                    strings.Add("</ul>");
                    strings.Add("<br/>");
                }
                
            }

            if(posts.Count > 0)
            {
                foreach(var post in posts)
                {
                    strings.Add($"<div class='post' id='{post.post_id}'>");
                    strings.Add($"<h3>{post.post_title}  Дата публікації: {post.post_date}</h3>");
                    strings.Add($"<p>{post.post_description}</p>");
                    strings.Add($"<img src='{post.img_url}' />");
                    strings.Add("</div>");
                    strings.Add("<br/>");
                }
            }
            else
            {
                strings.Add("<h3>Новин нема</h3>");
            }

            if (author_name != "" && author_phone != "")
            {
                strings.Add("<footer>");
                strings.Add($"<h4>Ім'я автора - {author_name}</h4>");
                strings.Add($"<h4>Номер телефону - {author_phone}</h4>");
                strings.Add("</footer>");
            }

            foreach(var str in strings)
            {
                Console.WriteLine(str);
            }
            strings = new List<string>();
        }
        public void addPost(List<Post> posts,string post_date, string post_title, string post_description, string img_url, string id)
        {
            posts.Add(new Post(post_date, post_title, post_description, img_url, id));
        }

        public void WriteFile()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "index.html");
            File.WriteAllLines(filePath, strings);
        }
    }
}