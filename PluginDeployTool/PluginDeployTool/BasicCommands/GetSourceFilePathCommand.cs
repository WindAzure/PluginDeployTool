using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.IO;

namespace PluginDeployTool.BasicCommands
{
    class GetSourceFilePathCommand : Command
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
        public string FileName
        {
            get;
            private set;
        } = "";

        public override void Execute()
        {
            List<string> outputFilePaths = GetProjectOutputFilePath();

            outputFilePaths.ForEach(filePath =>
            {
                string fileName = Path.GetFileName(filePath);
                if (isMatched(fileName))
                {
                    FilePath = filePath;
                    FileName = fileName;
                }
            });

            CheckFilePathExist();
        }

        private List<string> GetProjectOutputFilePath()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            List<string> outputFilePaths = new List<string>();
            DTE dTE = (DTE)Package.GetGlobalService(typeof(DTE));
            foreach (Project project in dTE.Solution.Projects)
            {
                ConfigurationManager configManager = project.ConfigurationManager;
                Configuration config = configManager.ActiveConfiguration;
                foreach (OutputGroup group in config.OutputGroups)
                {
                    if (group.DisplayName == "Primary Output")
                    {
                        foreach (string fileUriString in (Array)group.FileURLs)
                        {
                            string fileFullPath = fileUriString.Remove(0, 8);
                            outputFilePaths.Add(fileFullPath);
                        }
                    }
                }
            }

            return outputFilePaths;
        }
        private bool isMatched(string fileName)
        {
            if (fileName.Length < 7) return false;
            
            string resultExtension = fileName.Substring(fileName.Length - 4, 4);
            string resultPreFix = fileName.Substring(0, 6);
            string resultPostFix = fileName.Substring(fileName.Length - 7, 3);
            if (Mode == Mode.Server)
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
            if (FilePath.Length == 0)
            {
                throw new Exception("Project output directory not found!");
            }
        }
    }
}
