using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;

namespace webApp.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Registry { get; set; }

        public List<Students> studentsList()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            var json = File.ReadAllText(filePath);

            var studentsList = JsonConvert.DeserializeObject<List<Students>>(json);

            return studentsList;
        }
    }
}