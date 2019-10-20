using PluginDeployTool.DeployCommands;
using System.Collections.Generic;
using System.Linq;

namespace PluginDeployTool.BasicCommands
{
    class KillProcessCommand : BrandInfoCommand
    {
        public override bool Execute()
        {
            var targetProcessNameList = IsServerType() ? new List<string> { "PluginServer", "PLUGIN~1" } : new List<string> { ApplicationName };
            var processes = System.Diagnostics.Process.GetProcesses().Where(process =>
            {
                return targetProcessNameList.Contains(process.ProcessName);
            }).ToList();

            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
            return true;
        }
    }
}
