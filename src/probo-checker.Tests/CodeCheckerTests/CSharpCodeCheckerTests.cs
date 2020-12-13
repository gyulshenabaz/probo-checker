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
    public class CSharpCodeCheckerTests
    {
        public CSharpCodeCheckerTests()
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
                Language = "CSharp",
                Code = @"int add(int param1, int param2) {
                return param1 + param2;
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
                                Name = "param1",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "param2",
                                Value = "2"
                            }
                        }
                    }
                }
            };

            CSharpCodeChecker checker = new CSharpCodeChecker(submission);

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
                Language = "CSharp",
                Code = @"int add1(int param1, int param2) {
                return param1 + param2;
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
                                Name = "param1",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "param2",
                                Value = "2"
                            }
                        }
                    }
                }
            };
            
            CSharpCodeChecker checker = new CSharpCodeChecker(submission);
            
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => checker.Process());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public void ProcessSubmission_WithIncorrectValueParameter_ThrowsException()
        {
            string expectedExceptionMessage =
                $"Input string was not in a correct format.";
            
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "CSharp",
                Code = @"int add(int param1, int param2) {
                return param1 + param2;
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
                                Name = "param1",
                                Value = "some string"
                            },
                            new Parameter()
                            {
                                Name = "param2",
                                Value = "2"
                            }
                        }
                    }
                }
            };
            
            CSharpCodeChecker checker = new CSharpCodeChecker(submission);
            
            var exception = Assert.Throws<InvalidOperationException>(() => checker.Process());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        
        [Fact]
        public void ProcessSubmission_WithIncorrectCode_ThrowsException()
        {
            string expectedExceptionMessage =
                $"Program.cs on line 2: error CS0103: The name 'param3' does not exist in the current context";
            
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "CSharp",
                Code = @"int add(int param1, int param2) {
                return param3;
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
                                Name = "param1",
                                Value = "some string"
                            },
                            new Parameter()
                            {
                                Name = "param2",
                                Value = "2"
                            }
                        }
                    }
                }
            };
            
            CSharpCodeChecker checker = new CSharpCodeChecker(submission);
            
            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => checker.Process());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public void ProcessSubmission_CalculatesScoreCorrectly()
        {
            const double expectedScore = 50;
            
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "CSharp",
                Code = @"int add(int param1, int param2) {
                return param1 + param2;
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
                                Name = "param1",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "param2",
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
                                Name = "param1",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "param2",
                                Value = "2"
                            }
                        }
                    }
                }
            };

            CSharpCodeChecker checker = new CSharpCodeChecker(submission);

            var result = checker.Process();
           
            // Assert
            Assert.Equal(expectedScore,result.Score);
        }

        [Fact]
        public void ProcessSubmission_WithIncorrectParameterName_ThrowsException()
        {
            string expectedExceptionMessage =
                $"No parameter with name param1 is found";
            
            PostedSubmission submission = new PostedSubmission()
            {
                ProblemName = "add",
                Language = "CSharp",
                Code = @"int add(int param1, int param2) {
                return param1+param2;
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
                                Name = "parameter",
                                Value = "1"
                            },
                            new Parameter()
                            {
                                Name = "param2",
                                Value = "2"
                            }
                        }
                    }
                }
            };
            
            CSharpCodeChecker checker = new CSharpCodeChecker(submission);
            
            // Assert
            var exception = Assert.Throws<ArgumentException>(() => checker.Process());
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}