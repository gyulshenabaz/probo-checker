using System;
using System.Collections.Generic;
using probo_checker.Common.CodeChecker.Implementations;
using probo_checker.Common.CodeChecker.Models;
using Xunit;

namespace probo_checker.Tests.CodeCheckerTests
{
    public class BaseCodeCheckerTests
    {
        [Fact]
        public void ProcessSubmission_WithUnsupportedLanguage_ThrowsException()
        {
            string expectedExceptionMessage =
                $"Unsupported language";
            
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "UnsupportedLanguage",
                Code = @"function add(x, y)
                { 
                   return (x+y);
                }",
                TestCases = new List<TestCase>()
                {
                    new TestCase()
                    {
                        ExpectedOutput = "3",
                        Parameters = new List<Parameter>()
                        {
                            new Parameter()
                            {
                                Name = "x",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "y",
                                Value = "2"
                            }
                        }
                    }
                }
            };
            
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => BaseCodeChecker.GetCodeChecker(submission));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public void ProcessSubmission_WithSupportedLanguage_ReturnsProperChecker()
        {
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "JavaScript",
                Code = @"function add(x, y)
                { 
                   return (x+y);
                }",
                TestCases = new List<TestCase>()
                {
                    new TestCase()
                    {
                        ExpectedOutput = "3",
                        Parameters = new List<Parameter>()
                        {
                            new Parameter()
                            {
                                Name = "x",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "y",
                                Value = "2"
                            }
                        }
                    }
                }
            };

            var checker = BaseCodeChecker.GetCodeChecker(submission);
            
            Assert.NotNull(checker);
        }
    }
}