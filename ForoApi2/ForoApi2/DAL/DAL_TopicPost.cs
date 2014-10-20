using Foro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForoApi2.DAL
{
    public class DAL_TopicPost
    {
        public List<TopicPost> topicPosts { get; set; }

        public DAL_TopicPost()
        {
            topicPosts = new List<TopicPost>();
            DAL_Topic topics = new DAL_Topic();
            for (int i = 1; i <= topics.GetTopics().Count; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    topicPosts.Add(new TopicPost { TopicId = i, Id = 3 * (i - 1) + j, Content = "Topic Post" + (3 * (i - 1) + j), CreationDate = DateTime.Now, UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });
                }
            } 
        }

        public void AddTopic(TopicPost topic)
        {
            topic.Id = topicPosts.LastOrDefault().Id + 1;
            topic.CreationDate = DateTime.Now;
            topicPosts.Add(topic);
        }
        public void DeleteTopic(TopicPost topic)
        {
            topicPosts.Remove(topic);
        }
        public List<TopicPost> GetTopics()
        {
            return topicPosts;
        }
        public TopicPost GetTopic(int topicId)
        {
            return topicPosts.Where(x => x.Id == topicId).First();
        }

        public List<TopicPost> GetTopicsByTopicId(int topicId)
        {
            if (topicPosts == null)
            {
                topicPosts = new List<TopicPost>();
                var topics = new DAL_Topic();
                for (int i = 1; i <= topics.GetTopics().Count; i++)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        topicPosts.Add(new TopicPost { TopicId = i, Id = 3 * (i - 1) + j, Content = "Topic Post" + (3 * (i - 1) + j), CreationDate = DateTime.Now, UserId = "16c8bfd4-dbd5-40b0-b862-61119943b39d" });
                    }
                }
            }
            return topicPosts.Where(x => x.TopicId == topicId).ToList();
        }
    }
}