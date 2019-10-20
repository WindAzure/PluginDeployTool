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
        public Command GenerateKillProcessCommand(Mode mode, Dictionary<string, string> applicationInfo)
        {
            return new KillProcessCommand()
            {
                Mode = mode,
                VenderName = applicationInfo[DeployTarget.VENDER],
                ProductName = applicationInfo[DeployTarget.PRODCUT],
                ClientName = applicationInfo[DeployTarget.CLIENT],
                ApplicationName = applicationInfo[DeployTarget.APPLICATION],
            };
        }

        public GetProjectOutputPathCommand GenerateGetProjectOuputPathCommand(Mode mode)
        {
            return new GetProjectOutputPathCommand()
            {
                Mode = mode,
            };
        }

        public GetApplicationInstallPathCommand GenerateApplicationInstallPathCommand(Mode mode, Dictionary<string, string> applicationInfo)
        {
            return new GetApplicationInstallPathCommand()
            {
                Mode = mode,
                VenderName = applicationInfo[DeployTarget.VENDER],
                ProductName = applicationInfo[DeployTarget.PRODCUT],
                ClientName = applicationInfo[DeployTarget.CLIENT],
                ApplicationName = applicationInfo[DeployTarget.APPLICATION],
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

        public DeployPluginCommand GenerateDeployPluginCommand(Mode mode, Dictionary<string, string> deployTarget)
        {
            return new DeployPluginCommand
            {
                Mode = mode,
                VenderName = deployTarget[DeployTarget.VENDER],
                ProductName = deployTarget[DeployTarget.PRODCUT],
                ClientName = deployTarget[DeployTarget.CLIENT],
                ApplicationName = deployTarget[DeployTarget.APPLICATION],
            };
        }
    }
}
