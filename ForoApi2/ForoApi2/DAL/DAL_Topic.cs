using Foro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForoApi2.DAL
{
    public class DAL_Topic
    {
        public static List<Topic> topics { get; set; }

        public DAL_Topic()
        {
            topics = new List<Topic>();
            topics.Add(new Topic { Id = 1, CreationDate = DateTime.Now, Tittle = "Tittle 1", Content = "Content 1", KeyWords = "Lotso, Jesus, Caceres", UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });
            topics.Add(new Topic { Id = 2, CreationDate = DateTime.Now, Tittle = "Tittle 2", Content = "Content 2", KeyWords = "Sheldo, Bryan, Gaona", UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });
            topics.Add(new Topic { Id = 3, CreationDate = DateTime.Now, Tittle = "Tittle 3", Content = "Content 3", KeyWords = "Gato, Roy, Rojas", UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });

            for (int i = 4; i <= 6; i++)
            {
                topics.Add(new Topic { Id = i, CreationDate = DateTime.Now, Tittle = "Tittle " + i, Content = "Content " + i, KeyWords = i + "Key", UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });
            }
        }

        public void AddTopic(Topic topic)
        {
            topic.Id = topics.LastOrDefault().Id + 1;
            topic.CreationDate = DateTime.Now;
            topics.Add(topic);
        }
        public void DeleteTopic(Topic topic)
        {
            topics.Remove(topic);
        }
        public List<Topic> GetTopics()
        {
            return topics;
        }
        public Topic GetTopic(int topicId)
        {
            return topics.Where(x => x.Id == topicId).First();
        }

    }
}