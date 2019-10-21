using PluginDeployTool.BasicCommands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.DeployCommands
{
    class DeployPluginCommand : BrandInfoCommand
    {
        public string ExecuteResultMessage
        {
            get;
            private set;
        }

        public override bool Execute()
        {
            (bool status, string applicationInstallDirectory) = GetApplicationInstallDirectory();
            if (!status) return false;

            var projectOutputFilePath = GetProjectOutputPath();
            var targetFileName = Path.GetFileName(projectOutputFilePath);
            var targetFilePath = System.IO.Path.Combine(applicationInstallDirectory, targetFileName);

            var fileInfo = new FileInfo(targetFilePath);
            var fileInitWriteDateTime = fileInfo.Exists ? fileInfo.LastWriteTimeUtc : new DateTime(1911, 01, 01);

            KillProcess();
            CopyFile(projectOutputFilePath, targetFilePath);
            fileInfo.Refresh();

            var successMessage = string.Format("Copy {0} Success!", targetFilePath);
            var failMessage = string.Format("Copy {0} Success! ( File didn't changed )", targetFilePath);
            ExecuteResultMessage = DateTime.Compare(fileInitWriteDateTime, fileInfo.LastWriteTimeUtc) < 0 ? successMessage : failMessage;
            return true;
        }

        private string GetProjectOutputPath()
        {
            var projectOutputPath = "";

            try
            {
                var getProjectOutputPathCmd = m_commandFactory.GenerateGetProjectOuputPathCommand(Mode);
                getProjectOutputPathCmd.Execute();
                projectOutputPath = getProjectOutputPathCmd.FileFullPath;
            }
            catch (Exception e)
            {
                throw new Exception("Get" + ModeText + "PluginOutputPath Failed => " + e.Message);
            }

            return projectOutputPath;
        }

        private (bool status, string applicationInstallDirectory) GetApplicationInstallDirectory()
        {
            var applicationInstallPathCmd = m_commandFactory.GenerateApplicationInstallPathCommand(Mode, PackupApplicationInfo());
            var executeStatus = applicationInstallPathCmd.Execute();
            var installDirectory = applicationInstallPathCmd.ApplicationInstallDirectory;
            return (status: executeStatus, applicationInstallDirectory: installDirectory);
        }

        private void KillProcess()
        {
            m_commandFactory.GenerateKillProcessCommand(Mode, PackupApplicationInfo()).Execute();
        }

        private void CopyFile(string sourceFileFullPath, string destinationFileFullPath)
        {
            try
            {
                m_commandFactory.GenerateCopyFileCommand(sourceFileFullPath, destinationFileFullPath).Execute();
            }
            catch (Exception e)
            {
                throw new Exception("CopyFile Failed => " + e.Message);
            }
        }

        private Dictionary<string, string> PackupApplicationInfo()
        {
            return new Dictionary<string, string> {
                { DeployTarget.VENDER, VenderName },
                { DeployTarget.PRODCUT, ProductName },
                { DeployTarget.CLIENT, ClientName },
                { DeployTarget.APPLICATION, ApplicationName },
            };
        }

        private CommandFactory m_commandFactory = new CommandFactory();
    }
}
