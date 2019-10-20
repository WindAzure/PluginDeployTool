using PluginDeployTool.DeployCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.BasicCommands
{
    abstract class BrandInfoCommand : DualTypeCommand
    {
        public string VenderName
        {
            get;
            set;
        } = DeployTarget.VIVOTEK_INFO[DeployTarget.VENDER];

        public string ProductName
        {
            get;
            set;
        } = DeployTarget.VIVOTEK_INFO[DeployTarget.PRODCUT];
        public string ClientName
        {
            get;
            set;
        } = DeployTarget.VIVOTEK_INFO[DeployTarget.CLIENT];

        public string ApplicationName
        {
            get;
            set;
        } = DeployTarget.VIVOTEK_INFO[DeployTarget.APPLICATION];
    }
}
