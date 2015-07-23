using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyWcfRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class RestServ : IRestServ
    {       
        public List<string> GetNames()
        {
            List<string> lst = new List<string>() { "Alex Costea", "Georgian Costea", "John Doe" };
            return lst;            
        }

        public List<string> GetNamesByLastName(string lastname)
        {
            List<string> lst = new List<string>() { "Alex Costea", "Georgian Costea", "John Doe" };
            return lst.Where(n=>n.Contains(lastname)).ToList(); 
        }

        public bool SaveNames(List<string> lstRecords)
        {
            //Save the list of names somewhere
            return true;
        }
    }
}
