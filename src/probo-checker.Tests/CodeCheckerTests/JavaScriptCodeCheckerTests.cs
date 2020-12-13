using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using probo_checker.Common;
using probo_checker.Common.CodeChecker.Implementations;
using probo_checker.Common.CodeChecker.Models;
using Xunit;

namespace probo_checker.Tests.CodeCheckerTests
{
    public class JavaScriptCodeCheckerTests
    {
        public JavaScriptCodeCheckerTests()
        {
            if (!Directory.Exists(Resources.ResourcesDirectory))
            {
                Directory.SetCurrentDirectory(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString(),"..",".."));
            }
        }
        
        [Fact]
        public void ProcessSubmission_WithCorrectInput_ReturnsResults()
        {
            const string expectedOutput = "3";
            const double expectedScore = 100;
            
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

            JavaScriptCodeChecker checker = new JavaScriptCodeChecker(submission);

            var result = checker.Process();
            
            // Assert
            Assert.Equal(expectedOutput,result.Tests.First().ActualOutput);
            Assert.Equal(expectedScore,result.Score);
        }

        [Fact]
        public void ProcessSubmission_WithIncorrectMainMethod_ThrowsException()
        {
            string expectedExceptionMessage =
                $"The main method should be equal to the problem's name: add.";
            
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "JavaScript",
                Code = @"function add1(x, y)
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
            
            JavaScriptCodeChecker checker = new JavaScriptCodeChecker(submission);
            
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => checker.Process());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public void ProcessSubmission_WithIncorrectCode_ThrowsException()
        {
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "JavaScript",
                Code = @"function add(x, y)
                { 
                   return (z+y);
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
            
            JavaScriptCodeChecker checker = new JavaScriptCodeChecker(submission);
            
            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => checker.Process());
        }

        [Fact]
        public void ProcessSubmission_CalculatesScoreCorrectly()
        {
            const double expectedScore = 50;
            
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
                    },
                    new TestCase()
                    {
                        ExpectedOutput = "4",
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

            JavaScriptCodeChecker checker = new JavaScriptCodeChecker(submission);

            var result = checker.Process();
           
            // Assert
            Assert.Equal(expectedScore,result.Score);
        }

        [Fact]
        public void ProcessSubmission_WithIncorrectParameterName_ThrowsException()
        {
            string expectedExceptionMessage =
                $"No parameter with name x is found";
            
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
                                Name = "a",
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
            
            JavaScriptCodeChecker checker = new JavaScriptCodeChecker(submission);
            
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => checker.Process());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}