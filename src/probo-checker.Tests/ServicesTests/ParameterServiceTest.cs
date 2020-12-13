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
    public class ParameterServiceTest
    {
        readonly ParameterService service;
        readonly Mock<IRepository<Parameter>> mockRepository;

        public ParameterServiceTest()
        {
            mockRepository = new Mock<IRepository<Parameter>>();
            service = new ParameterService(mockRepository.Object);
        }

        [Fact]
        public void Parameter_GetAll_Test()
        {
            //Arrange
            List<Parameter> entities = new List<Parameter>()
                {
                    new Parameter()
                    {
                        Id = 2,
                        Name = "Name",
                        Type = "string",
                        Value = "33"
                    },
                    new Parameter()
                    {
                        Id = 3,
                        Name = "Another Name",
                        Type = "bool",
                        Value = "3f3"
                    }
                };
            mockRepository
               .Setup(x => x.All(null))
               .Returns(entities);

            //Act
            IEnumerable<Parameter> result = service.GetAll();

            //Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public void Parameter_GetAll_EmptyList_Test()
        {
            //Arrange
            mockRepository
                .Setup(x => x.All(null))
                .Returns(() => new List<Parameter>());

            //Act
            IEnumerable<Parameter> result = service.GetAll();

            //Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public void Parameter_GetById_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
            };
            mockRepository
                .Setup(x => x.GetById(entity.Id))
                .Returns(entity);

            //Act
            Parameter tEntity = service.GetById(entity.Id);

            //Assert
            Assert.NotNull(entity);
            Assert.Same(entity, tEntity);
        }

        [Fact]
        public void Parameter_GetByWrongId_Test()
        {
            //Arrange
            Parameter entity = null;
            mockRepository
                .Setup(x => x.GetById(20))
                .Returns(entity);

            //Act
            Parameter tEntity = service.GetById(20);

            //Assert
            Assert.Null(tEntity);
        }

        [Fact]
        public void Parameter_Create_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
            };
            mockRepository
                .Setup(x => x.Add(entity));

            //Act
            bool result = service.Create(entity);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Parameter_Create_Excepion_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
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
        public void Parameter_Update_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
            };
            mockRepository
                .Setup(x => x.Update(entity));

            //Act
            bool result = service.Update(entity);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Parameter_Update_Excepion_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
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
        public void Parameter_Delete_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
            };
            mockRepository
                .Setup(x => x.Remove(entity));

            //Act
            bool result = service.Delete(entity);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Parameter_Delete_Excepion_Test()
        {
            //Arrange
            Parameter entity = new Parameter()
            {
                Id = 3,
                Name = "Another Name",
                Type = "bool",
                Value = "33"
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
        public void Parameter_DeleteRange_Test()
        {
            //Arrange
            List<Parameter> entities = new List<Parameter>()
                {
                    new Parameter()
                    {
                        Id = 2,
                        Name = "Name",
                        Type = "string",
                        Value = "33"
                    },
                    new Parameter()
                    {
                        Id = 3,
                        Name = "Another Name",
                        Type = "bool",
                        Value = "f33"
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
        public void Parameter_DeleteRange_Excepion_Test()
        {
            //Arrange
            List<Parameter> entities = new List<Parameter>()
                {
                    new Parameter()
                    {
                        Id = 2,
                        Name = "Name",
                        Type = "string",
                        Value = "33"
                    },
                    new Parameter()
                    {
                        Id = 3,
                        Name = "Another Name",
                        Type = "bool",
                        Value = "3f3"
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
