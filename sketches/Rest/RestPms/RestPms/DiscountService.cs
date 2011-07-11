using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using RestPms.Model;

namespace RestPms
{
    // Start the service and browse to http://<machine_name>:<port>/CurrencyService/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DiscountService
    {
        static List<Discount> _discounts;
        static List<Discount> Discounts
        {
            get
            {
                if (_discounts != null) return _discounts;
                _discounts = new List<Discount>();
                _discounts.Add(new Discount { Id = 1, Name = "10 percent" });
                _discounts.Add(new Discount { Id = 2, Name = "20 percent" });
                return _discounts;
            }
        }

        [WebGet(UriTemplate = "")]
        public List<Discount> GetCollection()
        {
            return Discounts; // new List<Currency> { new Currency { Id = 1, Name = "Hello" } };
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Discount Create(Discount discount)
        {
            var id = Discounts.Max(c => c.Id);
            discount.Id = id;
            Discounts.Add(discount);
            return discount;
        }

        [WebGet(UriTemplate = "{id}")]
        public Discount Get(string id)
        {
            var index = int.Parse(id);
            var query = from c in Discounts where c.Id == index select c;
            var discount = query.FirstOrDefault();
            return discount;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public Discount Update(string id, Discount instance)
        {
            var index = int.Parse(id);
            var query = from c in Discounts where c.Id == index select c;
            var discount = query.FirstOrDefault();
            discount = instance;
            return discount;
        }


        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            // TODO: Remove the instance of SampleItem with the given id from the collection
            throw new NotImplementedException();
        }

    }
}
