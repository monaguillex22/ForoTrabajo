using Foro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForoApi2.DAL
{
    public class DAL_TopicSubscription
    {
        public List<TopicSubscription> topicSubscriptions { get; set; }

        public DAL_TopicSubscription()
        {
            topicSubscriptions = new List<TopicSubscription>();
            DAL_Topic topics = new DAL_Topic();
            for (int i = 1; i <= topics.GetTopics().Count; i++)
            {
                topicSubscriptions.Add(new TopicSubscription { Id = i, TopicId = i, UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });
            }
        }

        public void AddTopic(TopicSubscription topic)
        {
            topic.Id = topicSubscriptions.LastOrDefault().Id + 1; 
            topicSubscriptions.Add(topic);
        }
        public void DeleteTopic(TopicSubscription topic)
        {
            topicSubscriptions.Remove(topic);
        }
        public List<TopicSubscription> GetTopics()
        { 
            return topicSubscriptions;
        }
        public TopicSubscription GetTopic(int topicId)
        {
            return topicSubscriptions.Where(x => x.Id == topicId).First();
        }

    }
}