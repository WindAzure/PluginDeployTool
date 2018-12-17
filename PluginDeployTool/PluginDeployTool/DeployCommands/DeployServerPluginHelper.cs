using System;
using System.IO;
using PluginDeployTool.BasicCommands;

namespace PluginDeployTool.DeployCommands
{
    class DeployServerPluginHelper
    {
        public string Execute()
        {
            string serverPluginOutputFilePath = GetServerPluginOutputPath();
            string serverInstallFilePath = GetServerInstallPath();

            FileInfo fileInfo = new FileInfo(serverInstallFilePath);
            DateTime fileInitWriteDateTime = fileInfo.Exists ? fileInfo.LastWriteTimeUtc : new DateTime(1911, 01, 01);

            KillProcess();
            CopyFile(serverPluginOutputFilePath, serverInstallFilePath);

            fileInfo.Refresh();
            string successMessage = string.Format("Copy {0} Success!", serverInstallFilePath);
            string failMessage = string.Format("Copy {0} Failed! ( File didn't changed )", serverInstallFilePath);
            return DateTime.Compare(fileInitWriteDateTime, fileInfo.LastWriteTimeUtc) < 0 ? successMessage : failMessage;
        }

        private string GetServerPluginOutputPath()
        {
            GetSourceFilePathCommand getSourceFilePathCmd = null;

            try
            {
                getSourceFilePathCmd = new GetSourceFilePathCommand();
                getSourceFilePathCmd.Execute();
            }
            catch (Exception e)
            {
                throw new Exception("GetServerPluginOutputPath Failed => " + e.Message);
            }

            return getSourceFilePathCmd.FilePath;
        }

        private string GetServerInstallPath()
        {
            GetTargetFilePathCommand getTargetFilePathCmd = null;

            try
            {
                getTargetFilePathCmd = new GetTargetFilePathCommand();
                getTargetFilePathCmd.Execute();
            }
            catch (Exception e)
            {
                throw new Exception("GetServerInstallPath Failed => " + e.Message);
            }

            return getTargetFilePathCmd.FilePath;
        }

        private void KillProcess()
        {
            KillProcessCommand killProcessCmd = new KillProcessCommand();
            killProcessCmd.Execute();
        }

        private void CopyFile(string serverPluginOutputFilePath, string serverInstallFilePath)
        {
            try
            {
                CopyFileCommand copyFileCommand = new CopyFileCommand(serverPluginOutputFilePath, serverInstallFilePath);
                copyFileCommand.Execute();
            }
            catch (Exception e)
            {
                throw new Exception("CopyFile Failed => " + e.Message);
            }
        }
    }
}
