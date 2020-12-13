using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using probo_checker.Common.CodeChecker.Models;
using Shell.NET;

namespace probo_checker.Common.CodeChecker.Implementations
{
   public class JavaScriptCodeChecker : BaseCodeChecker
    {
        private const string FindFunctionRegexPattern = @"(?<functionName>\b{0}\b)(\((?<params>.*?)\))";

        public JavaScriptCodeChecker(PostedSubmission submission) : base(submission)
        {
            
        }
        
        protected override SubmissionResult ProcessSubmission()
        {
            SubmissionResult submissionResult = new SubmissionResult
            {
                ProblemName = submission.ProblemName,
                Language = submission.Language,
                Message = "Build succeeded."
            };
            
            InsertNewCode(submission.Code);
            
            submissionResult.Tests = RunTestCases();

            submissionResult.Score = CalculateScore(submissionResult.Tests.Count(t => t.Passed),
                submissionResult.Tests.Count);
            
            return submissionResult;
        }
        
        protected override List<Test> RunTestCases()
        {
            List<Test> testResults = new List<Test>();

            Regex methodsParamsRegex = new Regex(string.Format(FindFunctionRegexPattern, submission.ProblemName));

            Match match = methodsParamsRegex.Match(submission.Code);

            var mainFunctionName = match.Groups["functionName"].Value;
            var mainFunctionParameters = match.Groups["params"].Value;

            if (string.IsNullOrEmpty(mainFunctionName))
            {
                ClearTemplate(submission.Code,submission.ProblemName);
                throw new ArgumentException($"The main method should be equal to the problem's name: {submission.ProblemName}.");
            }
            
            ReplaceStringInTemplate("{functionName}", mainFunctionName);

            foreach (var test in submission.TestCases)
            {
                var parameters = GetParameters(mainFunctionParameters, test.Parameters);
                
                ReplaceStringInTemplate("{parameters}", parameters);
                
                string result = Run(parameters);
                
                testResults.Add(new Test()
                {
                    ExpectedOutput = test.ExpectedOutput,
                    ActualOutput =  result,
                    Passed = test.ExpectedOutput == result,
                    Parameters = new List<Parameter>(test.Parameters)
                });
                
                ReplaceStringInTemplate(parameters,"{parameters}");
            }
            
            ClearTemplate(submission.Code, mainFunctionName);
            
            return testResults;
        }
        
        private string Run(string parameters = null)
        {
            var bash = new Bash();
            var command = bash.Command(BashCommands.NodeExecuteJSFile);

            var exitCode = command.ExitCode;

            if (exitCode != 0)
            {
                ClearTemplate(submission.Code,submission.ProblemName, parameters);
                throw new InvalidOperationException(FormatErrorMessage(command.Output, command.ErrorMsg));
            }
            
            return command.Output.Trim();
        }
        
        private string GetParameters(string methodParameters, ICollection<Parameter> testCaseParameters)
        {
            var parameters = new List<string>();

            foreach (var argument in methodParameters.Split(new string[] {", ", ","}, 
                StringSplitOptions.RemoveEmptyEntries))
            {
                var parameterName = argument;
                
                var testParameter = testCaseParameters
                    .FirstOrDefault(p => p.Name.Equals(parameterName));

                if (testParameter == null)
                {
                    ClearTemplate(submission.Code, submission.ProblemName);
                    throw new ArgumentException($"No parameter with name {parameterName} is found");
                }

                var parameter = testParameter.Value;
                var parameterType = testParameter.Type;

                if (parameterType == "string" || parameterType == "char")
                {
                    parameter = @"'" + parameter + "'";
                }
                
                if (parameterType == "" || parameterType == "char")
                {
                    parameter = @"'" + parameter + "'";
                }

                parameters.Add(parameter);
            }
                
            var inputParameters =  string.Join(", ", parameters.
                Select(p => string.Join(", ", p)));

            return inputParameters;
        }
        
        protected override void InsertNewCode(string code)
        {
            ReplaceStringInTemplate("{newCode}", code);
        }
        
        protected override void ReplaceStringInTemplate(string oldValue, string newValue)
        {
            string text = File.ReadAllText(Resources.JavaScriptTemplateCodeFile);
            text = text.Replace(oldValue, newValue);
            File.WriteAllText(Resources.JavaScriptTemplateCodeFile, text);
        }
        
        protected override void ClearTemplate(string code, string mainMethod, string parameters = null)
        {
            ReplaceStringInTemplate(code, "{newCode}");
            ReplaceStringInTemplate(mainMethod, "{functionName}");

            if (parameters != null)
            {
                ReplaceStringInTemplate(parameters,"{parameters}");
            }
        }
        
        private string FormatErrorMessage(string commandOutput, string commandErrorMsg)
        {
            StringBuilder result = new StringBuilder();
            
            foreach (var line in commandErrorMsg.Split($"{Environment.NewLine}")
                .Distinct().Where(l => !l.Contains(Resources.JavaScriptTemplateCodeFile)))
            {
                
                result.AppendLine(line);
            }

            return result.ToString().Trim();
        }
    }
}
