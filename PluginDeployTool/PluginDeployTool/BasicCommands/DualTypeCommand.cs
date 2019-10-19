using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.BasicCommands
{
    public enum Mode { Client, Server }

    abstract class DualTypeCommand : Command
    {
        public Mode Mode
        {
            get;
            set;
        } = Mode.Server;

        public bool IsServerType()
        {
            return Mode == Mode.Server;
        }

        public string ModeText
        {
            get
            {
                return IsServerType() ? "Server" : "Client";
            }
        }
    }
}
