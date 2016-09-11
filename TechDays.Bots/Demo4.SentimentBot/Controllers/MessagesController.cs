using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Configuration;

namespace Demo4.SentimentBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                // Create HTTP client
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["CognitiveServicesTextAnalyticsKey"]);
                string uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
                
                // not sure if this helps with readibility
                JObject sentimentRequest = JObject.FromObject(new { documents = new[] { new { id = Guid.NewGuid().ToString(), text = activity.Text } } });
                
                ByteArrayContent content = new ByteArrayContent(Encoding.UTF8.GetBytes(sentimentRequest.ToString()));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                // Post data to sentiment analysis endpoint
                HttpResponseMessage r = await client.PostAsync(uri, content);

                // Parse the sentiment returned as a value between 0.0 and 1.0
                JObject sentimentResponse = JObject.Parse(await r.Content.ReadAsStringAsync());                
                double score = double.Parse((string)sentimentResponse.SelectToken("documents[0].score"));

                // Respond to user
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity reply = activity.CreateReply(string.Format(@"you are {0} - {1}% happiness", (score > 0.5 ? "happy" : "unhappy"), (score * 100.0)));
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}