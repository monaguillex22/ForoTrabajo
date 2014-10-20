using Foro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using ForoApi2.DAL;


namespace ForoApi2.Controllers
{
    public class ForoController : ApiController
    {
        private static DAL_Topic dal_topic = new DAL_Topic();
        private static DAL_TopicPost dal_topicPost = new DAL_TopicPost();
        private static DAL_TopicSubscription dal_topicSubscription = new DAL_TopicSubscription();

        [HttpPost]
        public virtual HttpResponseMessage PostTopic([FromBody] Topic topic)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
             
            try
            {
                dal_topic.AddTopic(topic);
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
            return response;
        }

        [HttpPost]
        public virtual HttpResponseMessage CreateTopicPost([FromBody] TopicPost topicPost)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK); 
            try
            {
                dal_topicPost.AddTopic(topicPost);
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
            return response;
        }
        [HttpGet] 
        public List<Topic> GetListTopics()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK); 
            var topicList = new List<Topic>();
            try
            {
               topicList= dal_topic.GetTopics(); 
            }
            catch (Exception ex) 
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
            return topicList;
        }
        [HttpGet] 
        public Topic GetTopicById(int topicId)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var topic = new Topic();
            try
            {
                topic = dal_topic.GetTopic(topicId);
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
            return topic;
        }
        [HttpGet] 
        public List<TopicPost> GetListTopicPosts(int topicId)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK); 
            var topicPostsList = new List<TopicPost>();
            try
            {
                topicPostsList = dal_topicPost.GetTopicsByTopicId(topicId);
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
            return topicPostsList;
        }

        [HttpPost]
        public virtual HttpResponseMessage CreateTopicSubscription([FromBody] TopicSubscription topic)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                dal_topicSubscription.AddTopic(topic);
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
            return response;
        }
	}
}