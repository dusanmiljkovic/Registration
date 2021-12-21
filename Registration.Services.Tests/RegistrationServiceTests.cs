using Moq;
using NUnit.Framework;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Exceptions;
using Registration.Services.Registration;
using Registration.Services.Registration.Dto.Commands.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Registration.Services.Tests
{
    public class RegistrationServiceTests
    {
        private RegistrationService? classUnderTest;
        private Mock<Serilog.ILogger>? _loggerMock;
        private Mock<IUnitOfWork>? _unitOfWorkMock;
        private Mock<IUserRepository>? _userRepositoryMock;
        private Mock<ICompanyRepository>? _companyRepositoryMock;

        private readonly string _username = "user1";
        private readonly string _password = "password1";
        private readonly string _email = "user1@gmail.com";
        private readonly string _companyName = "company1";
        private readonly long _companyId = 1;
        private readonly long _userId = 1;
        private readonly long _nonexistentUser = 2;
        private User? _user;
        private Company? _company;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<Serilog.ILogger>(MockBehavior.Strict);
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            _companyRepositoryMock = new Mock<ICompanyRepository>(MockBehavior.Strict);


            _user = new(_username, _password, _email);
            _company = new(_companyName);
            _user.CompanyId = _companyId;
            _user.Id = _userId;

            classUnderTest = new(_loggerMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void RegisterUser_ShouldThrowException_WhenCommandNull()
        {
            // ACT and ASSERT
            Assert.ThrowsAsync<ArgumentNullException>(async () => await classUnderTest.RegisterUserAsync(null));

            VerifyAll();
        }

        [Test]
        public void RegistrationServiceConstructor_ShouldThrowException_WhenAnyParameterNull()
        {
            // ACT and ASSERT
            Assert.Throws<ArgumentNullException>(() => new RegistrationService(null, _unitOfWorkMock.Object));
            Assert.Throws<ArgumentNullException>(() => new RegistrationService(_loggerMock.Object, null));

            VerifyAll();
        }

        [Test]
        public void RegisterUser_ShouldThrowException_WhenUserUsernameOrEmailAlreadyExists()
        {
            RegisterUserCommand registerUserCommand = new()
            {
                CompanyName = _companyName,
                Username = _username,
                Password = _password,
                Email = _email
            };
            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { _user });
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            //_loggerMock.Setup(u => u.Information(It.IsAny<string>()));

            // ACT
            var ex = Assert.ThrowsAsync<UniqueException>(async () => await classUnderTest.RegisterUserAsync(registerUserCommand));

            // ASSERT
            Assert.That(ex.Message, Is.EqualTo("Email and username must be unique."), "Exception message is not correct.");

            VerifyAll();
        }

        [Test]
        public void RegisterUser_ShouldCreateCompany_WhenCompanyDoesNotExist()
        {
            // ARRANGE
            RegisterUserCommand registerUserCommand = new()
            {
                CompanyName = _companyName,
                Username = _username,
                Password = _password,
                Email = _email
            };

            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { });
            _companyRepositoryMock.Setup(u => u.Add(It.IsAny<Company>())).Returns(_company);
            _companyRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<Company, bool>>>())).Returns(new List<Company> { });
            _loggerMock.Setup(u => u.Information(It.IsAny<string>(), It.IsAny<string>()));
            _loggerMock.Setup(u => u.Information(It.IsAny<string>(), It.IsAny<long>()));
            _unitOfWorkMock.Setup(u => u.CompanyRepository).Returns(_companyRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // ACT
            RegisterUserCommandResponse response = classUnderTest.RegisterUserAsync(registerUserCommand).Result;

            // ASSERT
            Assert.IsNotNull(response);
            Assert.AreEqual(_username, response.Username);
            Assert.AreEqual(_companyName, response.CompanyName);
            Assert.AreEqual(_email, response.Email);

            VerifyAll();
        }

        [Test]
        public void RegisterUser_ShouldRegsterUser_WhenCompanyExists()
        {
            // ARRANGE
            RegisterUserCommand registerUserCommand = new()
            {
                CompanyName = _companyName,
                Username = _username,
                Password = _password,
                Email = _email
            };

            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { });
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            _userRepositoryMock.Setup(u => u.Add(It.IsAny<User>())).Returns(_user);
            _companyRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<Company, bool>>>())).Returns(new List<Company> { _company });
            _loggerMock.Setup(u => u.Information(It.IsAny<string>(), It.IsAny<long>()));
            _unitOfWorkMock.Setup(u => u.CompanyRepository).Returns(_companyRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // ACT
            RegisterUserCommandResponse response = classUnderTest.RegisterUserAsync(registerUserCommand).Result;

            // ASSERT
            Assert.IsNotNull(response);
            Assert.AreEqual(_username, response.Username);
            Assert.AreEqual(_companyName, response.CompanyName);
            Assert.AreEqual(_email, response.Email);

            VerifyAll();
        }

        private void VerifyAll()
        {
            _loggerMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
            _userRepositoryMock.VerifyAll();
            _companyRepositoryMock.VerifyAll();
        }
    }
}
