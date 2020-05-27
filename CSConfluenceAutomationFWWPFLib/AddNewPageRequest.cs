using Modul.Final.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSConfluenceAutomationFWWPFLib
{
    class AddNewPageRequest : Ac4yServiceRequest
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string URL { get; set; }
        public string SpaceKey { get; set; }
        public string PageTitle { get; set; }
        public string ParentPageTitle { get; set; }
        public string Content { get; set; }
        public int IdLength { get; set; }
    }
}
