using ForoApi2.Controllers;
using MVC5IdentitySample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using Foro.Models;
using Lib.Web.Mvc.JQuery.JqGrid;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RestSharp;

namespace MVC5IdentitySample.Views.ForoApi
{
    public class ForoApiController : Controller
    {
        private const string ForoController = "http://localhost:62903/api/Foro/";
        public TopicRepository repository = new TopicRepository();

        [AllowAnonymous]
        public ActionResult Index()
        {

            var restClient = new RestClient(ForoController + "GetListTopics");
            var restRequest = new RestRequest(Method.GET);
            var response = restClient.Execute<List<Topic>>(restRequest);
            return View(response.Data);
        }

        [AllowAnonymous]
        public ActionResult Topic(int Id)
        {
            var restClient = new RestClient(ForoController + "GetTopicById?topicId=" + Id);
            var restRequest = new RestRequest(Method.GET);
            var response = restClient.Execute<Topic>(restRequest);
            if (response.Data.Id == 0)
                return RedirectToAction("Index");
            return View(response.Data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        public ActionResult GetTopicPosts(JqGridRequest request, int topicId)
        {
            int totalRecords = 0;
            var restClient = new RestClient(ForoController + "GetListTopicPosts?topicId=" + topicId);
            var restRequest = new RestRequest(Method.GET);
            var APIresponse = restClient.Execute<List<TopicPost>>(restRequest);

            List<TopicPost> topicPosts = APIresponse.Data;
            totalRecords = topicPosts.Count();
            //Prepare JqGridData instance
            JqGridResponse response = new JqGridResponse()
            {
                //Total pages count
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                //Page number
                PageIndex = request.PageIndex,
                //Total records count
                TotalRecordsCount = totalRecords
            };

            foreach (TopicPost topicPost in topicPosts)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(topicPost.Id), new List<object>()
                {
                    topicPost.Id,
                    topicPost.CreationDate.ToString("yy/MM/dd hh:mm:ss"),
                    topicPost.UserId,
                    topicPost.Content
                }));
            }

            //Return data as json
            return new JqGridJsonResult() { Data = response };
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SeguirPost(int topicId)
        {
            var userId = User.Identity.GetUserId();
            var topic = new TopicSubscription() 
                { 
                    TopicId = topicId,
                    UserId = userId
                };
            var restClient = new RestClient(ForoController + "CreateTopicSubscription");
            var restRequest = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            restRequest.AddBody(topic);
            var response = restClient.Execute(restRequest);
            return Json("Se ha subscrito al topic", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult IngresarPost(string contenido, int topicId)
        {
            var userId = User.Identity.GetUserId();
            var topic = new TopicPost()
                                  {
                                      Content = contenido,
                                      CreationDate = DateTime.Now,
                                      TopicId = topicId ,
                                      UserId = userId
                                  };

            var restClient = new RestClient(ForoController + "CreateTopicPost");
            var restRequest = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            restRequest.AddBody(topic);
            var response = restClient.Execute(restRequest);


            var restClient2 = new RestClient(ForoController + "GetTopicById?topicId=" + topicId);
            var restRequest2 = new RestRequest(Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            var response2 = restClient2.Execute(restRequest2);
            var obj = JsonConvert.DeserializeObject<Topic>(response2.Content);
            //Obtiene usuaios suscritos al topic
            //var userIds = repository.dal_topicSubscription.GetUserIdsSubscribedToTopic(topicId);
            var clientNotifier = GlobalHost.ConnectionManager.GetHubContext<ForoHub>();
            //Envia notificacion a usuarios que estan suscritos al topic especificado
            //clientNotifier.Clients.Users(userIds).addMessage(topic.Tittle);
            clientNotifier.Clients.All.addMessage(obj.Tittle);
            return Json("Post agregado satisfactoriamente", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CrearTopic(Topic topic)
        {
            var mensaje = "Se ha creado un nuevo topic" ;
            var restClient = new RestClient(ForoController + "PostTopic");
            var restRequest = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            topic.CreationDate = DateTime.Now;
            restRequest.AddBody(topic);
            var response = restClient.Execute(restRequest);
             return new JsonResult() { Data = mensaje };
         }
    }
}