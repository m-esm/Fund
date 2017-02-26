using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Fund.Web.Models
{
    public class m2s
    {
        public static DateTime shamsi2miladi(string shamsi)
        {
            PersianCalendar p = new PersianCalendar();
            return p.ToDateTime(int.Parse(shamsi.Split('/')[0]), int.Parse(shamsi.Split('/')[1]), int.Parse(shamsi.Split('/')[2]), 0, 0, 0, 0);
        }
        public static int getDaysLeft ( DateTime exp){

            return (exp - DateTime.Now).Days;

        }
        public string miladi2shamsi(DateTime _date)
        {

            PersianCalendar pc = new PersianCalendar();

            StringBuilder sb = new StringBuilder();


            sb.Append(pc.GetYear(_date).ToString("0000"));

            sb.Append("/");

            sb.Append(pc.GetMonth(_date).ToString("00"));

            sb.Append("/");

            sb.Append(pc.GetDayOfMonth(_date).ToString("00"));


            return sb.ToString();

        }

        public static string miladi2shamsiStatic(DateTime _date)
        {

            PersianCalendar pc = new PersianCalendar();

            StringBuilder sb = new StringBuilder();


            sb.Append(pc.GetYear(_date).ToString("0000"));

            sb.Append("/");

            sb.Append(pc.GetMonth(_date).ToString("00"));

            sb.Append("/");

            sb.Append(pc.GetDayOfMonth(_date).ToString("00"));


            return sb.ToString();

        }

        public string GetShamsiMonth(int Month)
        {
            string res = "";
            switch (Month)
            {
                case 1:
                    res = ("فروردین");
                    break;
                case 2:
                    res = ("اردیبهشت");
                    break;
                case 3:
                    res = ("خرداد");
                    break;
                case 4:
                    res = ("تیر");
                    break;
                case 5:
                    res = ("مرداد");
                    break;
                case 6:
                    res = ("شهریور");
                    break;
                case 7:
                    res = ("مهر");
                    break;
                case 8:
                    res = ("آبان");
                    break;
                case 9:
                    res = ("آذر");
                    break;
                case 10:
                    res = ("دی");
                    break;
                case 11:
                    res = ("بهمن");
                    break;
                case 12:
                    res = ("اسفند");
                    break;
            }

            return res;


        }

        public string m2sJustMonth(DateTime _date)
        {
            PersianCalendar pc = new PersianCalendar();
            return GetShamsiMonth(pc.GetMonth(_date));
        }

        public string m2sYearAndMonthNumeric(DateTime _date)
        {
            PersianCalendar pc = new PersianCalendar();
            string month = pc.GetMonth(_date).ToString();
            if (month.Length == 1)
                month = 0 + month;
            return pc.GetYear(_date) + "/" + month;
        }

        public string m2sMM(DateTime _date)
        {
            PersianCalendar pc = new PersianCalendar();
            StringBuilder sb = new StringBuilder();
            sb.Append(pc.GetDayOfMonth(_date).ToString("00"));
            sb.Append(' ');
            switch (pc.GetMonth(_date))
            {
                case 1:
                    sb.Append("فروردین");
                    break;
                case 2:
                    sb.Append("اردیبهشت");
                    break;
                case 3:
                    sb.Append("خرداد");
                    break;
                case 4:
                    sb.Append("تیر");
                    break;
                case 5:
                    sb.Append("مرداد");
                    break;
                case 6:
                    sb.Append("شهریور");
                    break;
                case 7:
                    sb.Append("مهر");
                    break;
                case 8:
                    sb.Append("آبان");
                    break;
                case 9:
                    sb.Append("آذر");
                    break;
                case 10:
                    sb.Append("دی");
                    break;
                case 11:
                    sb.Append("بهمن");
                    break;
                case 12:
                    sb.Append("اسفند");
                    break;
            }
            sb.Append(' ');
            sb.Append(pc.GetYear(_date).ToString("0000"));
            return sb.ToString();

        }
        public string m2sMMWithHours(DateTime _date)
        {
            PersianCalendar pc = new PersianCalendar();
            StringBuilder sb = new StringBuilder();
            sb.Append(pc.GetDayOfMonth(_date).ToString("00"));
            sb.Append(' ');
            switch (pc.GetMonth(_date))
            {
                case 1:
                    sb.Append("فروردین");
                    break;
                case 2:
                    sb.Append("اردیبهشت");
                    break;
                case 3:
                    sb.Append("خرداد");
                    break;
                case 4:
                    sb.Append("تیر");
                    break;
                case 5:
                    sb.Append("مرداد");
                    break;
                case 6:
                    sb.Append("شهریور");
                    break;
                case 7:
                    sb.Append("مهر");
                    break;
                case 8:
                    sb.Append("آبان");
                    break;
                case 9:
                    sb.Append("آذر");
                    break;
                case 10:
                    sb.Append("دی");
                    break;
                case 11:
                    sb.Append("بهمن");
                    break;
                case 12:
                    sb.Append("اسفند");
                    break;
            }
            sb.Append(' ');
            sb.Append(pc.GetYear(_date).ToString("0000"));
            sb.Append(" ساعت ");
            sb.Append(pc.GetHour(_date) + ":" + pc.GetMinute(_date));
            return sb.ToString();

        }
    }
}