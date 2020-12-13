using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using probo_checker.Common.CodeChecker.Interfaces;
using probo_checker.Common.CodeChecker.Models;

namespace probo_checker.Common.CodeChecker.Implementations
{
    public abstract class BaseCodeChecker : ICodeChecker
    {
        protected PostedSubmission submission;

        public BaseCodeChecker(PostedSubmission submission)
        {
            this.submission = submission;
        }
        
        public static ICodeChecker GetCodeChecker(PostedSubmission submission)
        {
            switch (submission.Language)
            {
                case "CSharp":
                    return new CSharpCodeChecker(submission);
                case "JavaScript":
                    return new JavaScriptCodeChecker(submission);
                default:
                    throw new ArgumentException("Unsupported language");
            }
        }

        public SubmissionResult Process()
        {
            return ProcessSubmission();
        }

        protected abstract SubmissionResult ProcessSubmission();
        
        protected double CalculateScore(int passedTests, int allTests)
        {
            return Math.Round(passedTests * 100.00 / allTests, 2);
        }

        protected abstract void ClearTemplate(string code, string mainMethod, string parameters = null);

        protected abstract void InsertNewCode(string code);

        protected abstract void ReplaceStringInTemplate(string oldValue, string newValue);
        
        protected abstract List<Test> RunTestCases();
    }
}