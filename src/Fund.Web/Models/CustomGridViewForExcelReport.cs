using Fund.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;
namespace Fund
{
    public class CustomthisViewForExcelReport : GridView
    {
        public DataTable ExcelData { get; set; }
        public CustomthisViewForExcelReport(DataTable _ExcelData , string ReportName , string SavePath = "~/exports/")
        {
            this.ExcelData = _ExcelData;
            var dt = ExcelData;
          //  HttpContext.Current.Response.ClearContent();
          //  HttpContext.Current.Response.Buffer = true;
          ////  HttpContext.Current.Response.AddHeader("content-disposition" , string.Format("attachment; filename=Pishgaman-{3}-Export-{0}-{1}-{2}.xls" , DateTime.Now.Year , DateTime.Now.Month , DateTime.Now.Day,ReportName));
          //  HttpContext.Current.Response.ContentType = "application/ms-excel";
            
          //  HttpContext.Current.Response.Output.Write(this.RenderExcelForOutputBuffer());
          //  HttpContext.Current.Response.Flush();
          //  HttpContext.Current.Response.End();.
         var utf8WithoutBom = new System.Text.UTF8Encoding(false);
        
            string fileName =  string.Format("Fund-{3}-Export-{0}-{1}-{2}-{4}.xls" 
                , DateTime.Now.Year 
                , DateTime.Now.Month 
                , DateTime.Now.Day 
                , ReportName
                ,DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());

       
            string attachment = "attachment; filename="+ fileName;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                HttpContext.Current.Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            HttpContext.Current.Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                HttpContext.Current.Response.Write("\n");
            }


            HttpContext.Current.Response.End();


       //     TextWriter tw = new StreamWriter(HttpContext.Current.Server.MapPath("~/exports/" +
       //       fileName), false, utf8WithoutBom);

       //     tw.Write(this.RenderExcelForOutputBuffer());

       //     tw.Close();

       ////     new BaseController().Notify(WebSecurity.CurrentUserName,  "گزارش درخواستی شما حاضر است !", "success");
       //   //    HttpContext.Current.Response.ClearContent();
       //      // HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
       //      // HttpContext.Current.Response.ContentEncoding = utf8WithoutBom;
       //    //   HttpContext.Current.Response.Write((this.RenderExcelForOutputBuffer()));
       //      // HttpContext.Current.Response.Flush();
       //       HttpContext.Current.Response.End();

        

        }
        public string RenderExcelForOutputBuffer()
        {
            StringWriter sw = new StringWriter();
               var utf8WithoutBom = new System.Text.UTF8Encoding(false);
              
            HtmlTextWriter htw = new HtmlTextWriter(sw);
 
            this.DataSource = ExcelData;
            this.DataBind();
            this.RenderControl(htw);
            return sw.ToString();
        }
        public override void RenderControl(System.Web.UI.HtmlTextWriter writer)
        {
            this.Font.Name = "Tahoma";
            this.Font.Size = FontUnit.Point(8);
            this.RowStyle.Height = Unit.Pixel(20);
            this.RowStyle.VerticalAlign = VerticalAlign.Top;
            try
            {
            this.HeaderRow.BorderWidth = Unit.Pixel(1);
            this.HeaderRow.BackColor = System.Drawing.Color.Black;
            this.HeaderRow.ForeColor = System.Drawing.Color.White;
            this.HeaderRow.Height = Unit.Pixel(30);
            this.HeaderRow.BorderColor =  System.Drawing.Color.White;
            this.HeaderRow.BorderStyle = BorderStyle.Solid;
            } catch (Exception ex)
            {
                
              
            }
        
           
            for (int i = 0 ; i < this.Rows.Count ; i++)
            {
                if (i % 2 == 0)
                    this.Rows[i].BackColor = System.Drawing.Color.WhiteSmoke;
            }
            base.RenderControl(writer);
        }
    }
}