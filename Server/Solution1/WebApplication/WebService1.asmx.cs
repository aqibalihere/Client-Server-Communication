using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

namespace WebApplication
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate_Marks()
        {
            string subjectsStr = HttpContext.Current.Request.Params["request"];
            List<SubjectModel> subjects = JsonConvert.DeserializeObject<List<SubjectModel>>(subjectsStr);

            double totalMarks = 0;
            double min = subjects[0].obtainedMarks;
            string minSub = subjects[0].name;
            double max = subjects[0].obtainedMarks;
            string maxSub = subjects[0].name;
            for     (int i=0; i<subjects.Count; i++)
            {
                totalMarks += subjects[i].obtainedMarks;
                if(min>subjects[i].obtainedMarks)
                {
                    min = subjects[i].obtainedMarks;
                    minSub = subjects[i].name;
                }
                if (max < subjects[i].obtainedMarks)
                {
                    max = subjects[i].obtainedMarks;
                    maxSub = subjects[i].name;
                }
            }

            double percent = (totalMarks / (subjects.Count * 100)) * 100;

            MarksheetModel marksheetModel = new MarksheetModel();
            marksheetModel.Percentage = percent;
            marksheetModel.MinMarks = min;
            marksheetModel.MaxMarks = max;
            marksheetModel.MinSubjectMarks = minSub;
            marksheetModel.MaxSubjectMarks = maxSub;
            string str = JsonConvert.SerializeObject(marksheetModel);
            return str;
        }

        public class SubjectModel
        {
            public string name { get; set; }
            public double obtainedMarks { get; set; }
        }

        public class MarksheetModel
        {
            public double Percentage { get; set; }
            public double MinMarks { get; set; }
            public double MaxMarks { get; set; }
            public string MinSubjectMarks { get; set; }
            public string MaxSubjectMarks { get; set; }
        }
    }
}
