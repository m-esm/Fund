using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    public class CostsByMonthsChartModel
    {
        public string Partner { get; set; }
        public string Date { get; set; }
        public double TotalCost { get; set; }

        public static string getPartnerColor(string partnerName)
        {
            string _pn = partnerName.ToLower().Trim();
            switch (_pn)
            {
                case "it":
                    return "#ff6a00";
                case "گرافیک":
                    return "#fffa0f";
                case "معماری":
                    return "#428bca";
                default:
                    return "#000";
            }
        }
    }
}