using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Godot.IcsRunner.Core
{
    public class JobReader
    {
        const string TagOrder = "order";
        const string TagQuantity = "count";
        const string TagCostcenter = "costcenter";
        const string TagSalesItem = "plu";

        public static List<RecipeJob> ResolveJobs(string fileName)
        {
            try
            {
                var xmlRoot = XElement.Load(fileName);
                return ResolveJobs(xmlRoot);
            }
            catch (XmlException e)
            {
                System.Diagnostics.Debug.WriteLine("Could not load spool job: {0}", e);
                return null;
            }
        }

        public static List<RecipeJob> ResolveJobs(XElement rootElement)
        {
            var result = new List<RecipeJob>();
            var orderElements = rootElement.Elements(TagOrder);
            foreach(var orderElement in orderElements)
            {
                var quantityTag = orderElement.Element(TagQuantity);
                if (quantityTag == null )
                    continue;
                var quantity = Decimal.Parse(quantityTag.Value, System.Globalization.CultureInfo.InvariantCulture);
                if (quantity == 0.0m) continue;

                var salesItemTag = orderElement.Element(TagSalesItem);
                if (salesItemTag == null )
                    continue;
                var salesItem = Convert.ToInt32(salesItemTag.Value);
                if (salesItem == 0) continue;

                var recipeJob = new RecipeJob
                    {Quantity = quantity, SalesItem = salesItem};

                var costcenter = orderElement.Element(TagCostcenter);
                if( costcenter!=null )
                    recipeJob.Costcenter = Convert.ToInt32(costcenter.Value);
                /*var price = orderElement.Element(TagPrice);
                if( price!=null )
                    recipeJob.Price = Convert.ToInt32(price.Value);*/
                result.Add(recipeJob);
            }

            return result;
        }
    }
}