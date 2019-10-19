using PluginDeployTool.BasicCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.DeployCommands
{
    class CommandFactory
    {
        public Command GenerateKillProcessCommand(Mode mode)
        {
            return new KillProcessCommand()
            {
                Mode = mode,
            };
        }

        public GetProjectOutputPathCommand GenerateGetProjectOuputPathCommand(Mode mode)
        {
            return new GetProjectOutputPathCommand()
            {
                Mode = mode,
            };
        }

        public GetApplicationInstallPathCommand GenerateApplicationInstallPathCommand(Mode mode)
        {
            return new GetApplicationInstallPathCommand()
            {
                Mode = mode,
            };
        }

        public Command GenerateCopyFileCommand(string sourceFileFullPath, string targetFileFullPath)
        {
            return new CopyFileCommand
            {
                SourceFileFullPath = sourceFileFullPath,
                TargetFileFullPath = targetFileFullPath
            }; ;
        }
    }
}
