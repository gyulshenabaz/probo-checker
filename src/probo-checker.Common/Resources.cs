using System;
using System.IO;

namespace probo_checker.Common
{
    public static class Resources
    {
        public static string MainDirectory => Path.Combine("..",Directory.GetParent(Environment.CurrentDirectory).ToString());
        
        public static string ResourcesDirectory => Path.Combine(MainDirectory, "Resources");
        
        #region CSharp
        
        public static string DotNetTemplateDirectory => Path.Combine(ResourcesDirectory, "DotNet");

        public static string DotNetTemplateCodeFile => Path.Combine(MainDirectory, "Resources","DotNet","Program.cs");
        
        public static string DotNetTemplateProject => Path.Combine(DotNetTemplateDirectory, "TemplateConsoleApp.csproj");
        
        #endregion
        
        #region JavaScript
        
        public static string JavaScriptTemplateCodeFile => Path.Combine(ResourcesDirectory, "JavaScript","Template.js");
        
        #endregion
    }
}