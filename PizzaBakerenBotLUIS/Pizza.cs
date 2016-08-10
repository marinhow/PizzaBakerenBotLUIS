using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Text;
#pragma warning disable 649

namespace PizzaBakerenBotLUIS
{
    public enum SizeOptions
    {
        // 0 value in enums is reserved for unknown values.  Either you can supply an explicit one or start enumeration at 1.
        Unknown,
        [Terms(new string[] { "small" })]
        Small,
        [Terms(new string[] { "med", "medium" })]
        Medium,
        [Terms(new string[] { "big", "large" })]
        Large,
        [Terms(new string[] { "family", "extra large" })]
        Family
    };
    public enum PizzaOptions
    {
        [Terms(new string[] { "pepperoni", "peperoni" })]
        Pepperoni = 1,
        [Terms(new string[] { "cheese"})]
        Cheese,
        [Terms(new string[] { "chicken", "kylling" })]
        Chicken
    };




    [Serializable]
    class PizzaOrder
    {
        [Prompt("What kind of pizza do you want? {||}")]
        [Template(TemplateUsage.NotUnderstood, "What does \"{0}\" mean???")]
        [Describe("Kind of pizza")]
        public PizzaOptions PizzaName;
        public SizeOptions Size;

        

        public string PhoneNumber;
        public string Address;

        //[Optional]
        //public CouponOptions Coupon;

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("PizzaOrder({0}, ", Size);
            //switch (Kind)
            //{
            //    case PizzaOptions.BYOPizza:
            //        builder.AppendFormat("{0}, {1}, {2}, [", Kind, BYO.Crust, BYO.Sauce);
            //        foreach (var topping in BYO.Toppings)
            //        {
            //            builder.AppendFormat("{0} ", topping);
            //        }
            //        builder.AppendFormat("]");
            //        break;
            //    case PizzaOptions.GourmetDelitePizza:
            //        builder.AppendFormat("{0}, {1}", Kind, GourmetDelite);
            //        break;
            //    case PizzaOptions.SignaturePizza:
            //        builder.AppendFormat("{0}, {1}", Kind, Signature);
            //        break;
            //    case PizzaOptions.StuffedPizza:
            //        builder.AppendFormat("{0}, {1}", Kind, Stuffed);
            //        break;
            //}
            builder.AppendFormat(", {0}, {1})", Address, PhoneNumber);
            return builder.ToString();
        }
    };
}