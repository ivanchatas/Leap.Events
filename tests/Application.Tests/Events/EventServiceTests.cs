using Application.Interfaces.Repositories;
using Application.Services;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Events
{
    [TestFixture]
    internal partial class EventServiceTests
    {
        private Mock<IEventRepository> _mockEventRepository;
        private Mock<IMapper> _mockMapper;
        private EventService _sut;

        [SetUp]
        public void Setup()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockMapper = new Mock<IMapper>();
            _sut = new EventService(_mockEventRepository.Object, _mockMapper.Object);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenEventRepositoryIsNull()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new EventService(null, _mockMapper.Object));

            Action act = () => new EventService(null, _mockMapper.Object);
            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("eventRepository");
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMapperIsNull()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new EventService(_mockEventRepository.Object, null));

            Action act = () => new EventService(_mockEventRepository.Object, null);
            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("mapper");
        }
    }
}
