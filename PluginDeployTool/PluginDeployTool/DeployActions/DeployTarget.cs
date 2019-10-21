using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.DeployCommands
{
    class DeployTarget
    {
        public static readonly string VENDER = "VENDER";
        public static readonly string PRODCUT = "PRODCUT";
        public static readonly string CLIENT = "CLIENT";
        public static readonly string APPLICATION = "APPLICATION";

        public static readonly Dictionary<string, string> VIVOTEK_INFO = new Dictionary<string, string>
        {
            { VENDER, "VIVOTEK" },
            { PRODCUT, "VAST" },
            { CLIENT, "VAST2" },
            { APPLICATION, "VAST2" },
        };

        public static readonly Dictionary<string, string> HONEYWELL_INFO = new Dictionary<string, string>
        {
            { VENDER, "Honeywell" },
            { PRODCUT, "NVR Viewer" },
            { CLIENT, "NVR Viewer" },
            { APPLICATION, "Honeywell NVR Viewer" },
        };

        public static readonly List<Dictionary<string, string>> List = new List<Dictionary<string, string>>{
            VIVOTEK_INFO,
            HONEYWELL_INFO,
        };
    }
}
