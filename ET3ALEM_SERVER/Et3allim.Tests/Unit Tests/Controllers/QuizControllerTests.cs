using System;
using System.Collections.Generic;
using BusinessEntities.Models;
using DataServiceLayer;
using ExceptionHandling.CustomExceptions;
using FluentAssertions;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server_Application.Controllers;
using Xunit;

namespace Et3allim.Tests.Unit_Tests.Controllers;

public class QuizControllerTests
{
    private readonly Mock<IQuizDsl> dataServiceStub = new();
    private readonly Random random = new();
    private readonly Mock<IAccountHelper> accountHelperStub = new();

    [Fact]
    public async void GetQuiz_WithUnexistingQuiz_ReturnsNotFound()
    {
        //Arrange
        dataServiceStub.Setup(dsl => dsl.GetQuiz(0))
            .ReturnsAsync((Quiz) null);
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = await controller.GetQuiz(0);

        //Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async void GetQuiz_WithExistingQuiz_ReturnsExpectedQuiz()
    {
        //Arrange
        var expectedQuiz = GenerateRandomQuiz();
        dataServiceStub.Setup(dsl => dsl.GetQuiz(It.IsAny<int>()))
            .ReturnsAsync(expectedQuiz);
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = await controller.GetQuiz(random.Next(1, 100));

        //Assert
        result.Value.Should().BeEquivalentTo(expectedQuiz,
            options => options.ComparingByMembers<Quiz>());
    }

    [Fact]
    public async void GetQuizzes_WithExistingQuiz_ReturnsAllQuizzes()
    {
        //Arrange
        List<Quiz> expectedQuizzes = new List<Quiz>
        {
            GenerateRandomQuiz(), GenerateRandomQuiz(), GenerateRandomQuiz()
        };
        dataServiceStub.Setup(dsl => dsl.GetQuizzes(It.IsAny<string>()))
            .ReturnsAsync(expectedQuizzes);
        accountHelperStub.Setup(helper => helper.GetUserId(null, null))
            .Returns(new Guid().ToString());
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = await controller.GetQuizzes();

        //Assert
        result.Should().BeEquivalentTo(expectedQuizzes,
            options => options.ComparingByMembers<Quiz>());
    }

    [Fact]
    public async void GetQuizTitleFromCode_WithUnexistingCode_ReturnsNoQuizFoundMessage()
    {
        //Arrange
        dataServiceStub.Setup(dsl => dsl.GetQuizTitleFromCode(""))
            .ReturnsAsync((string) null);
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = controller.GetQuizTitleFromCode("");

        //Assert
        var exception = await Assert.ThrowsAsync<CustomExceptionBase>(() => result);
        exception.Message.Should().BeEquivalentTo("No quiz found with this code");
    }

    [Fact]
    public async void GetQuizTitleFromCode_WithValidCode_ReturnsExpectedTitle()
    {
        //Arrange
        var expectedQuiz = GenerateRandomQuiz();
        var code = expectedQuiz.Code;
        var title = expectedQuiz.Name;
        var expectedTitleObject = new
        {
            Value = new
            {
                title
            }
        };

        dataServiceStub.Setup(dsl => dsl.GetQuizTitleFromCode(code))
            .ReturnsAsync(title);
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = await controller.GetQuizTitleFromCode(code);

        //Assert
        result.Result.Should().BeEquivalentTo(expectedTitleObject);
    }

    [Fact]
    public async void PutQuiz_WithNonMatchingId_ReturnsBadRequest()
    {
        //Arrange
        var quiz = GenerateRandomQuiz();
        var id = quiz.Id + 1;
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = await controller.PutQuiz(id, quiz);

        //Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async void PutQuiz_WithQuiz_ShouldCallDSLPutQuiz()
    {
        //Arrange
        var quiz = GenerateRandomQuiz();
        var controller = new QuizController(dataServiceStub.Object,
            accountHelperStub.Object);

        //Act
        var result = await controller.PutQuiz(quiz.Id, quiz);

        //Assert
        dataServiceStub.Verify(dsl => dsl.PutQuiz(quiz), Times.Once);
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async void GetBasicQuizByCode_WithCode_ReturnsQuiz()
    {
        //Arrange
        var quiz = GenerateRandomQuiz();
        dataServiceStub.Setup(dsl => dsl.GetBasicQuizByCode(quiz.Code))
            .ReturnsAsync(quiz);
        var controller = new QuizController(dataServiceStub.Object, accountHelperStub.Object);

        //Act
        var result = await controller.GetBasicQuizByCode(quiz.Code);

        //Assert
        dataServiceStub.Verify(dsl => dsl.GetBasicQuizByCode(quiz.Code), Times.Once);
        result.Should().BeEquivalentTo((ActionResult<Quiz>) quiz);
    }

    [Fact]
    public async void GetFullQuizByCode_WithCode_ReturnsQuiz()
    {
        //Arrange
        var quiz = GenerateRandomQuiz();
        dataServiceStub.Setup(dsl => dsl.GetFullQuizByCode(quiz.Code))
            .ReturnsAsync(quiz);
        var controller = new QuizController(dataServiceStub.Object, accountHelperStub.Object);

        //Act
        var result = await controller.GetFullQuizByCode(quiz.Code);

        //Assert
        dataServiceStub.Verify(dsl => dsl.GetFullQuizByCode(quiz.Code), Times.Once);
        result.Should().BeEquivalentTo((ActionResult<Quiz>) quiz);
    }

    private Quiz GenerateRandomQuiz()
    {
        int id = random.Next(1, 1000);
        return new Quiz
        {
            Id = id,
            Name = "Quiz #" + id,
            Code = "ABCDE3"
        };
    }
}