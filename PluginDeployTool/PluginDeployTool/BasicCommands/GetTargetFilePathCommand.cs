using Microsoft.Win32;
using System;

namespace PluginDeployTool.BasicCommands
{
    class GetTargetFilePathCommand : Command
    {
        public Mode Mode
        {
            get;
            set;
        } = Mode.Server;
        public string FilePath
        {
            get;
            private set;
        } = "";
        
        public override void Execute()
        {
            string targetDirectory = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\VIVOTEK, Inc.\\VAST", "INSTALL_PATH", null);
            CheckTargetDirectoryExist(targetDirectory);

            GetSourceFilePathCommand getSourceFilePathCmd = new GetSourceFilePathCommand
            {
                Mode = Mode
            };
            getSourceFilePathCmd.Execute();

            string targetFileName = getSourceFilePathCmd.FileName;
            if (Mode == Mode.Server)
            {
                string postFixPath = @"Server/PluginServer/kernel";
                FilePath = System.IO.Path.Combine(targetDirectory, postFixPath, targetFileName);
            }
            else
            {
                string postFixPath = @"Client/VAST2/plugin";
                FilePath = System.IO.Path.Combine(targetDirectory, postFixPath, targetFileName);
            }
        }

        private void CheckTargetDirectoryExist(string targetDirectory)
        {
            if (targetDirectory == null || targetDirectory == "")
            {
                throw new Exception("Registry not found match key!");
            }
        }
    }
}
