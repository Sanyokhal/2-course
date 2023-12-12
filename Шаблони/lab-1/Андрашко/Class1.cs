using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Шаблони
{
    public class Post
    {
        public string post_id;
        public string post_date;
        public string post_title;
        public string post_description;
        public string img_url;

        public Post(string date, string title, string description, string url, string post_id)
        {
            post_date = date;
            post_title = title;
            post_description = description;
            img_url = url;
            this.post_id = post_id;
        }
    }
}
