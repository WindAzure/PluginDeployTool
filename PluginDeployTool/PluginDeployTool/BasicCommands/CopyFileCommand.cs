using System;
using System.IO;

namespace PluginDeployTool.BasicCommands
{
    class CopyFileCommand : Command
    {
        public CopyFileCommand(string sourceFileFullPath, string targetFileFullPath) : base()
        {
            m_sourceFileFullPath = sourceFileFullPath;
            m_targetFileFullPath = targetFileFullPath;
        }

        public override void Execute()
        {
            try
            {
                File.Copy(m_sourceFileFullPath, m_targetFileFullPath, true);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Permission deny!");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string m_sourceFileFullPath = "";
        private string m_targetFileFullPath = "";
    }
}
