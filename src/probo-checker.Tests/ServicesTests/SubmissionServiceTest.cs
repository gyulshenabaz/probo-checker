using FluentAssertions;
using Moq;
using probo_checker.DataAccess.Models;
using probo_checker.DataAccess.Repositories.Interfaces;
using probo_checker.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace probo_checker.Tests.ServicesTests
{
    public class SubmissionServiceTest
    {
        readonly SubmissionsService service;
        readonly Mock<IRepository<Submission>> mockRepository;
        
        public SubmissionServiceTest()
        {
            mockRepository = new Mock<IRepository<Submission>>();
            service = new SubmissionsService(mockRepository.Object);
        }

        [Fact]
        public void Submission_GetAll_Test()
        {
            //Arrange
            List<Submission> entities = new List<Submission>()
            {
                new Submission()
                {
                    Id = 2,
                    ProblemName = "Name",
                    Language = "string"
                },
                new Submission()
                {
                    Id = 3,
                    ProblemName = "Name2",
                    Language = "string2"
                }
            };
            mockRepository
               .Setup(x => x.All(null))
               .Returns(entities);

            //Act
            IEnumerable<Submission> result = service.GetAll();

            //Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public void Submission_GetAll_EmptyList_Test()
        {
            //Arrange
            mockRepository
                .Setup(x => x.All(null))
                .Returns(() => new List<Submission>());

            //Act
            IEnumerable<Submission> result = service.GetAll();

            //Assert
            result.Count().Should().Be(0);
        }

        [Fact]

        public void Submission_GetById_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.GetById(entity.Id))
                .Returns(entity);

            //Act
            Submission tEntity = service.GetById(entity.Id);

            //Assert
            Assert.NotNull(entity);
            Assert.Same(entity, tEntity);
        }


        [Fact]
        public void Submission_GetByWrongId_Test()
        {
            //Arrange
            Submission entity = null;
            mockRepository
                .Setup(x => x.GetById(20))
                .Returns(entity);

            //Act
            Submission tEntity = service.GetById(20);
            
            //Assert
            Assert.Null(tEntity);
        }

        [Fact]
        public void Submission_Create_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.Add(entity));
            
            //Act
            bool result = service.Create(entity);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Submission_Create_Excepion_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.Add(entity))
                .Throws(new Exception());

            //Act
            bool result = service.Create(entity);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Submission_Update_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.Update(entity));
            
            //Act
            bool result = service.Update(entity);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Submission_Update_Excepion_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.Update(entity))
                .Throws(new Exception());

            //Act
            bool result = service.Update(entity);
            
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Submission_Delete_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.Remove(entity));
            
            //Act
            bool result = service.Delete(entity);
            
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Submission_Delete_Excepion_Test()
        {
            //Arrange
            Submission entity = new Submission()
            {
                Id = 2,
                ProblemName = "Name",
                Language = "string"
            };
            mockRepository
                .Setup(x => x.Remove(entity))
                .Throws(new Exception());

            //Act
            bool result = service.Delete(entity);
            
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Submission_DeleteRange_Test()
        {
            //Arrange
            List<Submission> entities = new List<Submission>()
            {
                new Submission()
                {
                    Id = 2,
                    ProblemName = "Name",
                    Language = "string"
                },
                new Submission()
                {
                    Id = 3,
                    ProblemName = "Name2",
                    Language = "string2"
                }
            };
            mockRepository
                .Setup(x => x.RemoveRange(entities));
            
            //Act
            bool result = service.DeleteRange(entities);
            
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Submission_DeleteRange_Excepion_Test()
        {
            //Arrange
            List<Submission> entities = new List<Submission>()
            {
                new Submission()
                {
                    Id = 2,
                    ProblemName = "Name",
                    Language = "string"
                },
                new Submission()
                {
                    Id = 3,
                    ProblemName = "Name2",
                    Language = "string2"
                }
            };
            mockRepository
                .Setup(x => x.RemoveRange(entities))
                .Throws(new Exception());

            //Act
            bool result = service.DeleteRange(entities);

            //Assert
            Assert.False(result);
        }        
    }
}
