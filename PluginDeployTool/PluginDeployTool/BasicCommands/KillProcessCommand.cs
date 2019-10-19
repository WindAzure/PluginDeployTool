using System.Collections.Generic;
using System.Linq;

namespace PluginDeployTool.BasicCommands
{
    class KillProcessCommand : DualTypeCommand
    {
        public override void Execute()
        {
            var targetProcessNameList = IsServerType() ? new List<string> { "PluginServer", "PLUGIN~1" } : new List<string> { "VAST2" };
            var processes = System.Diagnostics.Process.GetProcesses().Where(process =>
            {
                return targetProcessNameList.Contains(process.ProcessName);
            }).ToList();

            foreach (var process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }
    }
}
