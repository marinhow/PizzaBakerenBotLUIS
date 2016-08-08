using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBakerenBotLUIS
{
    //LUIS MODEL: https://www.luis.ai/application/39d3d817-ef26-423a-8f01-2e0639acc3a8
    [LuisModel("39d3d817-ef26-423a-8f01-2e0639acc3a8", "8e15977e64d64c4884260b3d70d442ae")]
    [Serializable]
    class PizzaOrderDialog : LuisDialog<PizzaOrder>
    {
        private readonly BuildFormDelegate<PizzaOrder> MakePizzaForm;

        internal PizzaOrderDialog(BuildFormDelegate<PizzaOrder> makePizzaForm)
        {
            this.MakePizzaForm = makePizzaForm;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Order")]
        public async Task ProcessOrder(IDialogContext context, LuisResult result)
        {
            var entities = new List<EntityRecommendation>(result.Entities);

            //PizzaSize
            //PizzaName
            //PizzaSauce
            //DrinkOptions




            //if (!entities.Any((entity) => entity.Type == "Kind"))
            //{
            //    //Infer kind
            //    foreach (var entity in result.Entities)
            //    {
            //        string kind = null;
            //        switch (entity.Type)
            //        {
            //            case "Signature": kind = "Signature"; break;
            //            case "GourmetDelite": kind = "Gourmet delite"; break;
            //            case "Stuffed": kind = "stuffed"; break;
            //            default:
            //                if (entity.Type.StartsWith("BYO")) kind = "byo";
            //                break;
            //        }
            //        if (kind != null)
            //        {
            //            entities.Add(new EntityRecommendation(type: "Kind") { Entity = kind });
            //            break;
            //        }
            //    }
            //}
            var order = new PizzaOrder();
            

            foreach (var entity in result.Entities)
            {
              
                switch (entity.Type)
                {
                    case "PizzaName": order.Kind = PizzaOptions.cheese;break;
                    case "PizzaSize": order.Size = SizeOptions.Small;break;
                    case "PizzaDressing": order.Dressing = PizzaDressingOptions.Garlic; break;
                    case "DrinkOptions": order.Drink = DrinkOptions.Beer; break;
                    default:break;
                }

            }

            order.Size = SizeOptions.Large;
            //order.Kind = PizzaOptions.StuffedPizza;
            await context.PostAsync("Your Pizza Order: " + order.ToString());

            var pizzaForm = new FormDialog<PizzaOrder>(order, this.MakePizzaForm, FormOptions.PromptInStart, entities);
            context.Call<PizzaOrder>(pizzaForm, PizzaFormComplete);
        }


        //[LuisIntent("OrderPizza")]
        //[LuisIntent("UseCoupon")]
        //public async Task ProcessPizzaForm(IDialogContext context, LuisResult result)
        //{
        //    var entities = new List<EntityRecommendation>(result.Entities);
        //    if (!entities.Any((entity) => entity.Type == "Kind"))
        //    {
        //         Infer kind
        //        foreach (var entity in result.Entities)
        //        {
        //            string kind = null;
        //            switch (entity.Type)
        //            {
        //                case "Signature": kind = "Signature"; break;
        //                case "GourmetDelite": kind = "Gourmet delite"; break;
        //                case "Stuffed": kind = "stuffed"; break;
        //                default:
        //                    if (entity.Type.StartsWith("BYO")) kind = "byo";
        //                    break;
        //            }
        //            if (kind != null)
        //            {
        //                entities.Add(new EntityRecommendation(type: "Kind") { Entity = kind });
        //                break;
        //            }
        //        }
        //    }

        //    var pizzaForm = new FormDialog<PizzaOrder>(new PizzaOrder(), this.MakePizzaForm, FormOptions.PromptInStart, entities);
        //    context.Call<PizzaOrder>(pizzaForm, PizzaFormComplete);
        //}

        private async Task PizzaFormComplete(IDialogContext context, IAwaitable<PizzaOrder> result)
        {
            PizzaOrder order = null;
            try
            {
                order = await result;
            }
            catch (OperationCanceledException)
            {
                await context.PostAsync("You canceled the form!");
                return;
            }

            if (order != null)
            {
                await context.PostAsync("Your Pizza Order: " + order.ToString());
            }
            else
            {
                await context.PostAsync("Form returned empty response!");
            }

            context.Wait(MessageReceived);
        }

        enum Days { Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };

        //[LuisIntent("StoreHours")]
        //public async Task ProcessStoreHours(IDialogContext context, LuisResult result)
        //{
        //    var days = (IEnumerable<Days>)Enum.GetValues(typeof(Days));

        //    PromptDialog.Choice(context, StoreHoursResult, days, "Which day of the week?");
        //}




        //private async Task StoreHoursResult(IDialogContext context, IAwaitable<Days> day)
        //{
        //    var hours = string.Empty;
        //    switch (await day)
        //    {
        //        case Days.Saturday:
        //        case Days.Sunday:
        //            hours = "5pm to 11pm";
        //            break;
        //        default:
        //            hours = "11am to 10pm";
        //            break;
        //    }

        //    var text = $"Store hours are {hours}";
        //    await context.PostAsync(text);

        //    context.Wait(MessageReceived);
        //}
    }
}