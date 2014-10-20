using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foro.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string KeyWords { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
    }
}
