using PluginDeployTool.BasicCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.DeployCommands
{
    class DeployPluginHelper
    {
        public List<string> Run(Mode mode)
        {
            var commandFactory = new CommandFactory();
            var executeResultMessageList = new List<string>();
            foreach (var deployTarget in DeployTarget.List)
            {
                var deployPluginCommand = commandFactory.GenerateDeployPluginCommand(mode, deployTarget);
                if (deployPluginCommand.Execute())
                {
                    executeResultMessageList.Add(deployPluginCommand.ExecuteResultMessage);
                }
            }
            return executeResultMessageList;
        }
    }
}
