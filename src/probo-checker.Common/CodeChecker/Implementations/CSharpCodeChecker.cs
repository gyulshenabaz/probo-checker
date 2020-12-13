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
    public class CSharpCodeChecker : BaseCodeChecker
    {
        private const string FindMethodRegexPattern = @"(?<attributes>[\w\s]*)(?<methodName>\b{0}\b)(\((?<params>.*?)\))";
        private const int NewCodeLine = 73; //The row where new code is inserted

        public CSharpCodeChecker(PostedSubmission submission) : base(submission)
        {
            
        }

        protected override SubmissionResult ProcessSubmission()
        {
            SubmissionResult submissionResult = new SubmissionResult
            {
                ProblemName = submission.ProblemName,
                Language = submission.Language
            };
            
            InsertNewCode(submission.Code);
            
            if (Build())
            {
                submissionResult.Message = "Build succeeded.";
            }
            
            submissionResult.Tests = RunTestCases();

            submissionResult.Score = CalculateScore(submissionResult.Tests.Count(t => t.Passed),
                submissionResult.Tests.Count);
            
            return submissionResult;
        }
        
        private bool Build()
        {
            var bash = new Bash();
            var command = bash.Command(BashCommands.DotNetBuild);

            var exitCode = command.ExitCode;
            
            if (exitCode != 0)
            {
                ClearTemplate(submission.Code, submission.ProblemName);
                throw new InvalidOperationException(FormatErrorMessage(command.Output,command.ErrorMsg));
            }

            return true;
        }

        private string Run(string parameters = null)
        {
            var bash = new Bash();
            var command = bash.Command(BashCommands.DotNetRun);

            var exitCode = command.ExitCode;

            if (exitCode != 0)
            {
                ClearTemplate(submission.Code,submission.ProblemName, parameters);
                throw new InvalidOperationException(FormatErrorMessage(command.Output,command.ErrorMsg));
            }
            
            return command.Output.Trim();
        }

        protected override List<Test> RunTestCases()
        {
            List<Test> testResults = new List<Test>();

            Regex methodsParamsRegex = new Regex(string.Format(FindMethodRegexPattern, submission.ProblemName));

            Match match = methodsParamsRegex.Match(submission.Code);

            var mainMethodName = match.Groups["methodName"].Value;
            var mainMethodParameters = match.Groups["params"].Value;

            if (string.IsNullOrEmpty(mainMethodName))
            {
                ClearTemplate(submission.Code,submission.ProblemName);
                throw new ArgumentException($"The main method should be equal to the problem's name: {submission.ProblemName}.");
            }
            
            ReplaceStringInTemplate("{MethodName}", mainMethodName);

            foreach (var test in submission.TestCases)
            {
                var parameters = GetParameters(mainMethodParameters,test.Parameters);
                
                ReplaceStringInTemplate("\"userInput\"", parameters);
                
                string result = Run(parameters);
                
                testResults.Add(new Test()
                {
                    ExpectedOutput = test.ExpectedOutput,
                    ActualOutput =  result,
                    Passed = test.ExpectedOutput == result,
                    Parameters = new List<Parameter>(test.Parameters)
                });
                
                ReplaceStringInTemplate(parameters,"\"userInput\"");
            }
            
            ClearTemplate(submission.Code, mainMethodName);
            
            return testResults;
        }
        
        private string GetParameters(string methodParameters, ICollection<Parameter> testCaseParameters)
        {
            var parameters = new List<string>();

            foreach (var arguments in methodParameters.Split(new string[] {", ", ","},
                StringSplitOptions.RemoveEmptyEntries))
            {
                var methodParameter = arguments.Split(' ').ToList();

                var parameterName = methodParameter[1];
                
                var testParameter = testCaseParameters
                    .FirstOrDefault(p => p.Name.Equals(parameterName));

                if (testParameter == null)
                {
                    ClearTemplate(submission.Code, submission.ProblemName);
                    throw new ArgumentException($"No parameter with name {parameterName} is found");
                }
                
                var parameter = testParameter.Value;
                
                parameters.Add(parameter);
            }
                
            var inputParameters =  string.Join(", ", parameters.
                Select(p => string.Join(", ", $@"""{p}""")));

            return inputParameters;
        }

        protected override void ClearTemplate(string code, string mainMethod, string parameters = null)
        {
            ReplaceStringInTemplate(code, "{newCode}");
            ReplaceStringInTemplate(mainMethod, "{MethodName}");

            if (parameters != null)
            {
                ReplaceStringInTemplate(parameters,"\"userInput\"");
            }
        }
        
        protected override void InsertNewCode(string code)
        {
            ReplaceStringInTemplate("{newCode}", code);
        }

        private string FormatErrorMessage(string commandOutput, string commandErrorMsg)
        {
            commandOutput = Regex.Replace(commandOutput, @"\[.*?\]", "");
            
            StringBuilder result = new StringBuilder();

            result.AppendLine(commandErrorMsg
                .Contains("System.FormatException") ? "Input string was not in a correct format." : commandErrorMsg);
            
            foreach (var line in commandOutput.Split($"{Environment.NewLine}")
                .Distinct().Where(l => l.Contains("Program.cs")))
            {
                Regex findRowCount = new Regex(@"\d+");
                var actualRowCount = int.Parse(findRowCount.Match(line).Value);

                var rowCount = actualRowCount - NewCodeLine < 0 ? actualRowCount : actualRowCount - NewCodeLine;
                
                var newLine = Regex.Replace(line, @"\(.*?\)", $" on line {rowCount.ToString()}");

                result.AppendLine(newLine);
            }

            return result.ToString().Trim();
        }

        protected override void ReplaceStringInTemplate(string oldValue, string newValue)
        {
            string text = File.ReadAllText(Resources.DotNetTemplateCodeFile);
            text = text.Replace(oldValue, newValue);
            File.WriteAllText(Resources.DotNetTemplateCodeFile, text);
        }
    }
}