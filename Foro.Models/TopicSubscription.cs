using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foro.Models
{
    public class TopicSubscription
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string UserId { get; set; }
    }
}
