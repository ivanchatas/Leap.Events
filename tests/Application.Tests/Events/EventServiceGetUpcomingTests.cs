using System.Linq.Expressions;
using Application.Dtos.Response;
using Application.Tests.DataDummie;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.Tests.Events
{
    /// <summary>
    /// EventServiceGetUpcomingTests
    /// </summary>
    internal partial class EventServiceTests
    {
        [Test]
        public async Task GetUpcomingNext30Days_ShouldCallGetUpcomingWith30Days()
        {
            // Arrange
            var eventEntities = DataGenerator.GenerateEvents(2);
            var expectedDtos = DataGenerator.GenerateEventDtos(eventEntities.ToList());

            _mockEventRepository.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Event, bool>>>(),
                It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                It.IsAny<Func<IQueryable<Event>, IQueryable<Event>>>()
            )).Returns(Task.FromResult(eventEntities.AsQueryable()));

            _mockMapper.Setup(mapper => mapper.Map<List<EventResponseDto>>(It.IsAny<List<Event>>()))
                       .Returns(expectedDtos);

            // Act
            var result = await _sut.GetUpcomingNext30Days();

            // Assert
            Assert.That(result, Is.Not.Null); // NUnit Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().NotBeNull();

            _mockEventRepository.Verify(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Event, bool>>>(),
                It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                It.IsAny<Func<IQueryable<Event>, IQueryable<Event>>>()),
                Times.Once());
        }

        [Test]
        public async Task GetUpcomingNext60Days_ShouldCallGetUpcomingWith60Days()
        {
            var eventEntities = DataGenerator.GenerateEvents(2);
            var expectedDtos = DataGenerator.GenerateEventDtos(eventEntities.ToList());

            _mockEventRepository.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Event, bool>>>(),
                It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                It.IsAny<Func<IQueryable<Event>, IQueryable<Event>>>()
            )).Returns(Task.FromResult(eventEntities.AsQueryable()));
            
            _mockMapper.Setup(mapper => mapper.Map<List<EventResponseDto>>(It.IsAny<List<Event>>()))
                       .Returns(expectedDtos);

            // Act
            var result = await _sut.GetUpcomingNext60Days();

            // Assert
            Assert.That(result, Is.Not.Null);
            _mockEventRepository.Verify(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Event, bool>>>(),
                It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                It.IsAny<Func<IQueryable<Event>, IQueryable<Event>>>()), Times.Once());
        }

        [Test]
        public async Task GetUpcomingNext180Days_ShouldCallGetUpcomingWith180Days()
        {
            var eventEntities = DataGenerator.GenerateEvents(2);
            var expectedDtos = DataGenerator.GenerateEventDtos(eventEntities.ToList());

            _mockEventRepository.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Event, bool>>>(),
                It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                It.IsAny<Func<IQueryable<Event>, IQueryable<Event>>>()
            )).Returns(Task.FromResult(eventEntities.AsQueryable()));
            
            _mockMapper.Setup(mapper => mapper.Map<List<EventResponseDto>>(It.IsAny<List<Event>>()))
                       .Returns(expectedDtos);

            // Act
            var result = await _sut.GetUpcomingNext180Days();

            // Assert
            Assert.That(result, Is.Not.Null);
            _mockEventRepository.Verify(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Event, bool>>>(),
                It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                It.IsAny<Func<IQueryable<Event>, IQueryable<Event>>>()), Times.Once());
        }
    }
}
