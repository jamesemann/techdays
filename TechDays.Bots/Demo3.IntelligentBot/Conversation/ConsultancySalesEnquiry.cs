using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo3.IntelligentBot.Conversation
{
    [Serializable]
    public class ConsultancySalesEnquiry
    {
        [Prompt("Which Technology would you like Consultancy for?")]
        public string Technology { get; set; }

        [Prompt("What version of {Technology} are you using?")]
        public string Version { get; set; }

        [Prompt("Do you have an Phone number we can contact you on?")]
        public string Telephone { get; set; }

        [Prompt("Before we start, what's your name?")]
        public string Name { get; set; }

        public static IForm<ConsultancySalesEnquiry> BuildForm()
        {
            return new FormBuilder<ConsultancySalesEnquiry>()
                    .Message("Hi, thank you for your message.")
                    .Field("Name")
                    .Field("Technology")
                    .Field("Version")
                    .Confirm("So, you are looking for Consultancy for {Technology} {Version}?")
                    .Message("Great, thanks {Name}, I'll take down some contact details if that's OK and then someone will call you back ASAP")
                    .AddRemainingFields()
                    .OnCompletion(async (ctx, enquiry) =>
                    {
                        var replyToConversation = ctx.MakeMessage();
                        replyToConversation.Text = "";
                        replyToConversation.Type = "message";
                        replyToConversation.Attachments = new List<Attachment>();
                        List<CardImage> cardImages = new List<CardImage>();
                        cardImages.Add(new CardImage(url: "http://www.blackmarble.co.uk/media/1789/amy.png"));
                        List<CardAction> cardButtons = new List<CardAction>();
                        CardAction plButton = new CardAction()
                        {
                            Value = "https://www.blackmarble.co.uk",
                            Type = "openUrl",
                            Title = "Black Marble"
                        };
                        cardButtons.Add(plButton);
                        HeroCard plCard = new HeroCard()
                        {
                            Title = "Amy will be in touch",
                            Subtitle = "While you are waiting, why don't you have a look at some of the services we offer?",
                            Images = cardImages,
                            Buttons = cardButtons
                        };
                        Attachment plAttachment = plCard.ToAttachment();
                        replyToConversation.Attachments.Add(plAttachment);
                        await ctx.PostAsync(replyToConversation);

                        // wire into CRM

                    })
                    .Build();
        }
    }
}