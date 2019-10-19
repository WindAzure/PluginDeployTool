using PluginDeployTool.BasicCommands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDeployTool.DeployCommands
{
    class DeployPluginCommand : DualTypeCommand
    {
        public string ExecuteResultMessage
        {
            get;
            private set;
        }

        public override void Execute()
        {
            var projectOutputFilePath = GetProjectOutputPath(Mode);
            var applicationInstallDirectory = GetApplicationInstallDirectory(Mode);
            var targetFileName = Path.GetFileName(projectOutputFilePath);
            var targetFilePath = System.IO.Path.Combine(applicationInstallDirectory, targetFileName);

            var fileInfo = new FileInfo(targetFilePath);
            var fileInitWriteDateTime = fileInfo.Exists ? fileInfo.LastWriteTimeUtc : new DateTime(1911, 01, 01);

            KillProcess(Mode);
            CopyFile(projectOutputFilePath, targetFilePath);
            fileInfo.Refresh();

            var successMessage = string.Format("Copy {0} Success!", targetFilePath);
            var failMessage = string.Format("Copy {0} Failed! ( File didn't changed )", targetFilePath);
            ExecuteResultMessage = DateTime.Compare(fileInitWriteDateTime, fileInfo.LastWriteTimeUtc) < 0 ? successMessage : failMessage;
        }

        private string GetProjectOutputPath(Mode mode)
        {
            var projectOutputPath = "";

            try
            {
                var getProjectOutputPathCmd = m_commandFactory.GenerateGetProjectOuputPathCommand(mode);
                getProjectOutputPathCmd.Execute();
                projectOutputPath = getProjectOutputPathCmd.FileFullPath;
            }
            catch (Exception e)
            {
                throw new Exception("Get" + ModeText + "PluginOutputPath Failed => " + e.Message);
            }

            return projectOutputPath;
        }

        private string GetApplicationInstallDirectory(Mode mode)
        {
            var applicationInstallDirectory = "";

            try
            {
                var applicationInstallPathCmd = m_commandFactory.GenerateApplicationInstallPathCommand(mode);
                applicationInstallPathCmd.Execute();
                applicationInstallDirectory = applicationInstallPathCmd.ApplicationInstallDirectory;
            }
            catch (Exception e)
            {
                throw new Exception("Get" + ModeText + "InstallPath Failed => " + e.Message);
            }

            return applicationInstallDirectory;
        }

        private void KillProcess(Mode mode)
        {
            m_commandFactory.GenerateKillProcessCommand(mode).Execute();
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

        private CommandFactory m_commandFactory = new CommandFactory();
    }
}
