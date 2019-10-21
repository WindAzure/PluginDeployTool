using System;
using System.IO;

namespace PluginDeployTool.BasicCommands
{
    class CopyFileCommand : BasicCommand
    {
        public string SourceFileFullPath
        {
            get;
            set;
        }

        public string TargetFileFullPath
        {
            get;
            set;
        }

        public override bool Execute()
        {
            try
            {
                File.Copy(SourceFileFullPath, TargetFileFullPath, true);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Permission deny!");
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }
    }
}
