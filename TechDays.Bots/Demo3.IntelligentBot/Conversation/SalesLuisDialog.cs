using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo3.IntelligentBot.Conversation
{
    [LuisModel("<Your LUIS Model ID>", "<Your LUIS Subscription Key>")]
    [Serializable]
    public class SalesLuisDialog : LuisDialog<object>
    {
        private Func<IForm<object>> buildForm;

        public SalesLuisDialog(Func<IForm<object>> buildForm)
        {
            this.buildForm = buildForm;
            var form = buildForm();
        }

        [LuisIntent("Consultancy")]
        public async Task IntentConsultancySales(IDialogContext context, LuisResult result)
        {
            var technologyEntity = (from entity in result.Entities where entity.Type == "Technology" select entity).FirstOrDefault();

            var form = new FormDialog<ConsultancySalesEnquiry>(
                new ConsultancySalesEnquiry() { Technology = technologyEntity != null ? technologyEntity.Entity : null },
                ConsultancySalesEnquiry.BuildForm,
                FormOptions.PromptInStart
            );

            context.Call<ConsultancySalesEnquiry>(form, async (formContext, formResult) =>
            {
                var order = await formResult;
                formContext.Wait(MessageReceived);
            });
        }

        [LuisIntent("Technical Leadership")]
        public async Task IntentTechLeadSales(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Technical Leadership sales");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Custom Development")]
        public async Task IntentCustomDevelopmentSales(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Custom Development sales");
            context.Wait(MessageReceived);
        }

        [LuisIntent("General")]
        public async Task IntentGeneralSales(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Custom Development sales");
            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task IntentNone(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("don't know");
            context.Wait(MessageReceived);
        }
    }
}
