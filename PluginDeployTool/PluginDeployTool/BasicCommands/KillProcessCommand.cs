using System.Collections.Generic;
using System.Linq;

namespace PluginDeployTool.BasicCommands
{
    class KillProcessCommand : Command
    {
        public Mode Mode
        {
            get
            {
                return m_mode;
            }
            set
            {
                m_mode = value;
                if(m_mode == Mode.Server)
                {
                    m_targetProcessNames = new List<string> { "PluginServer", "PLUGIN~1", "notepad" };
                }
                else
                {
                    m_targetProcessNames = new List<string> { "VAST2", "notepad" };
                }
            }
        }

        public override void Execute()
        {
            List<System.Diagnostics.Process> processes = System.Diagnostics.Process.GetProcesses().Where(process =>
            {
                return m_targetProcessNames.Contains(process.ProcessName);
            }).ToList();

            foreach (var process in processes)
            {
                process.Kill();
            }
        }

        private Mode m_mode = Mode.Server;
        private List<string> m_targetProcessNames = new List<string> { "PluginServer", "PLUGIN~1", "notepad" };
    }
}
