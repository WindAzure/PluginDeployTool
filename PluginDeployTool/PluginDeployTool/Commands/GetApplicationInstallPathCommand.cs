using Microsoft.Win32;
using System;

namespace PluginDeployTool.BasicCommands
{
    class GetApplicationInstallPathCommand : BrandInfoCommand
    {
        public string ApplicationInstallDirectory
        {
            get;
            private set;
        }

        public override bool Execute()
        {
            if (!(Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Wow6432Node\" + VenderName + @", Inc.\"+ ProductName, "INSTALL_PATH", null) is string applicationDirectory) || 
                applicationDirectory == "")
            {
                return false;
            }

            var postFixPath = IsServerType() ? @"Server/PluginServer/kernel" : @"Client/" + ClientName + "/plugin";
            ApplicationInstallDirectory = System.IO.Path.Combine(applicationDirectory, postFixPath);
            return true;
        }
    }
}
