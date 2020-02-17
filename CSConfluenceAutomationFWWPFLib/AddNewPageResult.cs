using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSConfluenceAutomationFWWPFLib
{
    public class AddNewPageResult
    {
        public NewPageErrorResponse FailedResponse { get; set; }
        public NewPageSuccessResponse SuccessResponse { get; set; }
    }
}
