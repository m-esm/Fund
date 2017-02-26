using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{

    public class SharjHelper
    {
        DB db = new DB();
        public Dictionary<float, float> GetAll()
        {

            var sharjs = db.Sharjs.AsEnumerable();
            var costs = db.Costs.AsEnumerable();
            var partners = db.Partners.AsEnumerable();

            Dictionary<float, float> PartnersTotalSharj = new Dictionary<float, float>();


            foreach (var p in partners)
                PartnersTotalSharj.Add(p.ID, 0);

            foreach (var sharj in sharjs)
                PartnersTotalSharj[sharj.PartnerID] += sharj.Amount;

            foreach (var c in costs)
                foreach (var p in c.Percents)
                {
                    float cash = ((c.Amount * p.Percent) / 100);
                    PartnersTotalSharj[p.PartnerID] -= cash;
                }

            return PartnersTotalSharj;  


        }


    }
}