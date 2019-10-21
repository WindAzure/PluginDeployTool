using EnvDTE;
using Microsoft.VisualStudio.Shell;
using PluginDeployTool.OutputWindow;
using System;
using System.Collections.Generic;
using System.IO;

namespace PluginDeployTool.BasicCommands
{
    class GetProjectOutputPathCommand : DualTypeCommand
    {
        public string FileFullPath
        {
            get;
            protected set;
        } = "";

        public override bool Execute()
        {
            var outputFilePaths = GetProjectOutputFilePath();

            outputFilePaths.ForEach(filePath =>
            {
                var fileName = Path.GetFileName(filePath);
                if (IsMatched(fileName))
                {
                    FileFullPath = filePath;
                }
            });

            CheckFilePathExist();
            return true;
        }

        private List<string> GetProjectOutputFilePath()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var outputFilePaths = new List<string>();
            var dTE = Package.GetGlobalService(typeof(DTE)) as DTE;
            foreach (Project project in dTE.Solution.Projects)
            {
                var configManager = project.ConfigurationManager;
                if (configManager == null) continue;

                var config = configManager.ActiveConfiguration;
                foreach (OutputGroup group in config.OutputGroups)
                {
                    for (var i = 0; i < group.FileCount; i++)
                    {
                        var fileName = ((Array)group.FileNames).GetValue(i) as string;
                        if (Path.GetExtension(fileName) == ".dll")
                        {
                            var fileUriString = ((Array)group.FileURLs).GetValue(i) as string;
                            var fileFullPath = fileUriString.Remove(0, 8);
                            outputFilePaths.Add(fileFullPath);
                        }
                    }
                }
            }

            return outputFilePaths;
        }

        private bool IsMatched(string fileName)
        {
            if (fileName.Length < 7) return false;

            var resultExtension = fileName.Substring(fileName.Length - 4, 4);
            var resultPreFix = fileName.Substring(0, 6);
            var resultPostFix = fileName.Substring(fileName.Length - 7, 3);
            if (IsServerType())
            {
                return resultExtension == ".dll" && resultPostFix == "Srv";
            }
            else
            {
                return resultExtension == ".dll" && resultPostFix != "Srv" && resultPreFix == "Plugin";
            }
        }

        private void CheckFilePathExist()
        {
            if (FileFullPath.Length == 0)
            {
                throw new Exception("Project output directory not found!");
            }
        }
    }
}
