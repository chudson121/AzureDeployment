using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeployment
{
    public class WebAppDto
    {
        public string ResourceGroup { get; set; }
        public string AppName { get; set; }

        public string Url { get; set; }


    }
}
