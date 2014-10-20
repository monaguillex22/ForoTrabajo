using MVC5IdentitySample.DAL; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5IdentitySample.Repository
{
    public class TopicRepository
    {
        public DAL_Topic dal_topic = new DAL_Topic();
        public DAL_TopicPost dal_topicPost = new DAL_TopicPost(); 
        public DAL_TopicSubscription dal_topicSubscription = new DAL_TopicSubscription();
    }
}