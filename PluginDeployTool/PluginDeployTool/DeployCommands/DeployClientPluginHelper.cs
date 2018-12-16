using PluginDeployTool.BasicCommands;
using System;
using System.IO;

namespace PluginDeployTool.DeployCommands
{
    class DeployClientPluginHelper
    {
        public string Execute()
        {
            string clientPluginOutputFilePath = GetClientPluginOutputPath();
            string clientInstallFilePath = GetClientInstallPath();

            FileInfo fileInfo = new FileInfo(clientInstallFilePath);
            DateTime fileInitWriteDateTime = fileInfo.Exists ? fileInfo.LastWriteTimeUtc : new DateTime(1911, 01, 01);

            KillProcess();
            CopyFile(clientPluginOutputFilePath, clientInstallFilePath);

            fileInfo.Refresh();
            return DateTime.Compare(fileInitWriteDateTime, fileInfo.LastWriteTimeUtc) < 0 ? "Success!!" : "Failed!! ( File didn't changed )";
        }
        private string GetClientPluginOutputPath()
        {
            GetSourceFilePathCommand getSourceFilePathCmd = null;

            try
            {
                getSourceFilePathCmd = new GetSourceFilePathCommand
                {
                    Mode = Mode.Client
                };

                getSourceFilePathCmd.Execute();
            }
            catch (Exception e)
            {
                throw new Exception("GetServerPluginOutputPath Failed => " + e.Message);
            }

            return getSourceFilePathCmd.FilePath;
        }

        private string GetClientInstallPath()
        {
            GetTargetFilePathCommand getTargetFilePathCmd = null;

            try
            {
                getTargetFilePathCmd = new GetTargetFilePathCommand
                {
                    Mode = Mode.Client
                };

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
            KillProcessCommand killProcessCmd = new KillProcessCommand
            {
                Mode = Mode.Client
            };

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
