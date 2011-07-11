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
    public class CurrencyService
    {
        static List<Currency> _currencies;
        static List<Currency> Currencies
        {
            get
            {
                if (_currencies != null) return _currencies;
                _currencies = new List<Currency>();
                _currencies.Add(new Currency { Id = 1, Name = "Euro" });
                _currencies.Add(new Currency { Id = 2, Name = "Dollar" });
                return _currencies;
            }
        }

        [WebGet(UriTemplate = "")]
        public List<Currency> GetCollection()
        {
            return Currencies; // new List<Currency> { new Currency { Id = 1, Name = "Hello" } };
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public Currency Create(Currency currency)
        {
            var id = Currencies.Max(c => c.Id);
            currency.Id = id;
            Currencies.Add(currency);
            return currency;
        }

        [WebGet(UriTemplate = "{id}")]
        public Currency Get(string id)
        {
            var index = int.Parse(id);
            var query = from c in Currencies where c.Id == index select c;
            var currency = query.FirstOrDefault();
            return currency;
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public Currency Update(string id, Currency instance)
        {
            var index = int.Parse(id);
            var query = from c in Currencies where c.Id == index select c;
            var currency = query.FirstOrDefault();
            currency = instance;
            return currency;
        }


        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            // TODO: Remove the instance of SampleItem with the given id from the collection
            throw new NotImplementedException();
        }

    }
}
