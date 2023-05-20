using System.Threading;
using System.Threading.Tasks;
using Application.DomainEvent.Events;
using Application.Ports.Repository;
using Application.UseCase.CreatePersonCommand;
using AutoFixture;
using Domain.AggregateModels.PersonModel;
using Domain.Common.Abstract;
using Domain.Common.Models;
using MapsterMapper;
using MediatR;
using Moq;
using NUnit.Framework;

namespace HexagonalArchitecture.Test.UseCase;

[TestFixture]
public class CreatePersonCommandHandlerTest
{
    private Fixture _fixture;

    private Mock<IPersonRepository> _personRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IMediator> _mediatorMock;
    
    private CreatePersonCommandHandler _sut;
    
    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _personRepositoryMock = new Mock<IPersonRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _mediatorMock = new Mock<IMediator>();
        
        _sut = new CreatePersonCommandHandler(_personRepositoryMock.Object, _mapperMock.Object, _unitOfWorkMock.Object, _mediatorMock.Object);
    }
    
    [Test]
    public async Task CreatePersonCommandHandler_TrueStory()
    {
        //Arrange
        var createPersonCommand = _fixture.Create<CreatePersonCommand>();
        var person = _fixture.Create<Person>();
        _mapperMock.Setup(x => x.Map<Person>(createPersonCommand))
            .Returns(person);


        //Act
        var result = await _sut.Handle(createPersonCommand, CancellationToken.None);
        var serviceResult = result as ServiceResult<CreatePersonCommandViewModel>;

        //Assert
        _personRepositoryMock.Verify(x => x.Add(person),Times.Once);
        _mediatorMock.Verify(x => x.Publish(It.IsAny<PersonCreatedDomainEvent>(), CancellationToken.None),Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(),Times.Once);
        Assert.NotNull(result);
        Assert.IsTrue(result.Succeeded);
        Assert.That(person.Id, Is.EqualTo(serviceResult?.Data.PersonId));
    }
}