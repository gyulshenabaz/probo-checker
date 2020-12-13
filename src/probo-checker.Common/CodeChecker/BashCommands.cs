namespace probo_checker.Common.CodeChecker
{
    public static class BashCommands
    {
        #region DotNet
        
        public static string DotNetBuild => $"dotnet build {Resources.DotNetTemplateProject}";
        
        public static string DotNetRun => $"dotnet run --project {Resources.DotNetTemplateDirectory}";
        
        #endregion

        #region JavaScript

        public static string NodeExecuteJSFile => $"node {Resources.JavaScriptTemplateCodeFile}";
        
        #endregion
    }
}