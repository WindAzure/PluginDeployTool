using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace PluginDeployTool.OutputWindow
{
    sealed class OutputWindowHelper
    {
        private static readonly OutputWindowHelper m_instance = new OutputWindowHelper();

        public static OutputWindowHelper GetInstance()
        {
            return m_instance;
        }

        public void Write(String message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var outputWindow = DeployPluginPackage.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;

            Guid guid = VSConstants.GUID_OutWindowDebugPane;
            outputWindow.GetPane(ref guid, out IVsOutputWindowPane outputPanel);
            outputPanel.OutputString(message + Environment.NewLine);
            outputPanel.Activate();
        }
    }
}
