using System.Linq.Expressions;
using Application.Dtos.Response;
using FluentAssertions;
using Moq;

namespace Application.Tests.Events
{
    /// <summary>
    /// EventServiceGetByIdTests
    /// </summary>
    internal partial class EventServiceTests
    {
        [Test]
        public async Task GetById_ShouldReturnEventResponseDto_WhenEventExists()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            var eventEntity = new Domain.Entities.Event 
            { 
                Id = eventId, 
                Name = "Test Event", 
                StartsOn = DateTime.UtcNow, 
                EndsOn = DateTime.UtcNow.AddHours(2) 
            };

            var expectedDto = new EventResponseDto 
            { 
                Id = eventId.ToString(), 
                Name = "Test Event DTO" 
            };

            _mockEventRepository.Setup(r => r.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Domain.Entities.Event, bool>>>(),
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IOrderedQueryable<Domain.Entities.Event>>>(),
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>()
            )).ReturnsAsync(eventEntity);

            _mockMapper.Setup(mapper => mapper.Map<EventResponseDto>(eventEntity))
                       .Returns(expectedDto);

            // Act
            var result = await _sut.GetById(eventId.ToString());

            // Assert
            Assert.That(result, Is.Not.Null);
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().Be(expectedDto);
            result.Data.Id.Should().Be(eventId.ToString());

            _mockEventRepository.Verify(r => r.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Domain.Entities.Event, bool>>>(),
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IOrderedQueryable<Domain.Entities.Event>>>(),
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>()), Times.Once());
            _mockMapper.Verify(mapper => mapper.Map<EventResponseDto>(eventEntity), Times.Once());
        }

        [Test]
        public async Task GetById_ShouldReturnNullData_WhenEventDoesNotExist()
        {
            // Arrange
            var eventId = Guid.NewGuid();

            _mockEventRepository.Setup(r => r.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Domain.Entities.Event, bool>>>(),
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IOrderedQueryable<Domain.Entities.Event>>>(),
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>()
            )).ReturnsAsync((Domain.Entities.Event)null);

            _mockMapper.Setup(mapper => mapper.Map<EventResponseDto>(null))
                       .Returns((EventResponseDto)null);

            // Act
            var result = await _sut.GetById(eventId.ToString());

            // Assert
            Assert.That(result, Is.Not.Null); // NUnit Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeNull();

            _mockEventRepository.Verify(r => r.FirstOrDefaultAsync(
                It.IsAny<Expression<Func<Domain.Entities.Event, bool>>>(), 
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IOrderedQueryable<Domain.Entities.Event>>>(), 
                It.IsAny<Func<IQueryable<Domain.Entities.Event>, IQueryable<Domain.Entities.Event>>>()), Times.Once());
            _mockMapper.Verify(mapper => mapper.Map<EventResponseDto>(null), Times.Once());
        }
    }
}
