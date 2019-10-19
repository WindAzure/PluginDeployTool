using Microsoft.Win32;
using System;

namespace PluginDeployTool.BasicCommands
{
    class GetApplicationInstallPathCommand : DualTypeCommand
    {
        public string ApplicationInstallDirectory
        {
            get;
            private set;
        }

        public override void Execute()
        {
            var applicationDirectory = Registry.GetValue("HKEY_LOCAL_MACHINE\\Software\\Wow6432Node\\VIVOTEK, Inc.\\VAST", "INSTALL_PATH", null) as string;
            CheckDirectoryExist(applicationDirectory);

            var postFixPath = IsServerType() ? @"Server/PluginServer/kernel" : @"Client/VAST2/plugin";
            ApplicationInstallDirectory = System.IO.Path.Combine(applicationDirectory, postFixPath);
        }

        private void CheckDirectoryExist(string applicationDirectory)
        {
            if (applicationDirectory == null || applicationDirectory == "")
            {
                throw new Exception("Registry not found match key!");
            }
        }
    }
}
