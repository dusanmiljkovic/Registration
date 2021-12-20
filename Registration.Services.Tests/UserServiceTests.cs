using Moq;
using NUnit.Framework;
using Registration.Domain.Entities.Companies;
using Registration.Domain.Entities.Users;
using Registration.Domain.Interfaces;
using Registration.Services.Exceptions;
using Registration.Services.Users;
using Registration.Services.Users.Dto.Commands.DeleteUser;
using Registration.Services.Users.Dto.Commands.UpdateUser;
using Registration.Services.Users.Dto.Queries.GetUser;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Registration.Services.Tests
{
    public class UserServiceTests
    {
        private UserService? classUnderTest;
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

            _unitOfWorkMock.Setup(u => u.UserRepository).Returns(_userRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.CompanyRepository).Returns(_companyRepositoryMock.Object);

            _user = new(_username, _password, _email);
            _company = new(_companyName);
            _user.CompanyId = _companyId;
            _user.Id = _userId;
            _company.Id = _companyId;

            classUnderTest = new(_loggerMock.Object, _unitOfWorkMock.Object);
        }


        [Test]
        public void UserServiceConstructor_ShouldThrowException_WhenAnyParameterNull()
        {
            // ACT and ASSERT
            Assert.Throws<ArgumentNullException>(() => new UserService(null, _unitOfWorkMock.Object));
            Assert.Throws<ArgumentNullException>(() => new UserService(_loggerMock.Object, null));
        }

        [Test]
        public void GetUser_ShouldThrowException_WhenCommandNull()
        {
            // ACT and ASSERT
            Assert.ThrowsAsync<ArgumentNullException>(async () => await classUnderTest.GetUserAsync(null));
        }

        [Test]
        public void GetUser_ShouldReturnUserInstance_WhenUserExistsInDatabase()
        {
            // ARRANGE
            GetUserCommand getUserCommand = new() { UserId = _userId };
            _userRepositoryMock.Setup(u => u.GetById(_userId)).Returns(_user);
            _companyRepositoryMock.Setup(c => c.GetById(_user.CompanyId)).Returns(_company);

            // ACT
            var response = classUnderTest.GetUserAsync(getUserCommand);

            // ASSERT
            Assert.IsNotNull(response);
            Assert.AreEqual(_userId, response.Result.UserId);
            Assert.AreEqual(_username, response.Result.Username);
            Assert.AreEqual(_company.Name, response.Result.CompanyName);
            Assert.AreEqual(_email, response.Result.Email);

            VerifyAll();
        }

        [Test]
        public void GetUser_ShouldFail_WhenUserDoesNotExistInDatabase()
        {
            // ARRANGE
            GetUserCommand getUserCommand = new() { UserId = _nonexistentUser };
            _userRepositoryMock.Setup(u => u.GetById(_nonexistentUser)).Returns<User>(null);
            _loggerMock.Setup(u => u.Error(It.IsAny<string>(), It.IsAny<long>()));

            // ACT AND ASSERT
            var ex = Assert.ThrowsAsync<Exceptions.NotFoundException>(() => classUnderTest.GetUserAsync(getUserCommand));
            Assert.That(ex.Message, Is.EqualTo($"User with ID \"{getUserCommand.UserId}\" was not found."));
        }


        [Test]
        public void UpdateUser_ShouldThrowException_WhenCommandNull()
        {
            // ACT and ASSERT
            Assert.ThrowsAsync<ArgumentNullException>(async () => await classUnderTest.UpdateUserAsync(null));
        }

        [Test]
        public void RegisterUser_ShouldThrowException_WhenUserUsernameOrEmailAlreadyExists()
        {
            UpdateUserCommand updateUserCommand = new()
            {
                UserId = _userId,
                Email = _email,
                Password = _password,
                Username = _username
            };
            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { _user });
            _loggerMock.Setup(u => u.Information(It.IsAny<string>()));

            // ACT
            var ex = Assert.ThrowsAsync<UniqueException>(async () => await classUnderTest.UpdateUserAsync(updateUserCommand));

            // ASSERT
            Assert.That(ex.Message, Is.EqualTo("Email and password must be unique."), "Exception message is not correct.");
        }

        [Test]
        public void UpdateUser_ShouldUpdateUser_WhenUserExistsInDatabase()
        {
            // ARRANGE
            UpdateUserCommand updateUserCommand = new()
            {
                UserId = _userId,
                Email = _email,
                Password = _password,
                Username = _username
            };

            _companyRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<Company, bool>>>())).Returns(new List<Company> { });
            _companyRepositoryMock.Setup(u => u.Update(_company)).Returns(_company);
            _companyRepositoryMock.Setup(u => u.GetById(_companyId)).Returns(_company);
            
            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { });
            _userRepositoryMock.Setup(u => u.Update(_user)).Returns(_user);
            _userRepositoryMock.Setup(u => u.GetById(_userId)).Returns(_user);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _loggerMock.Setup(u => u.Information(It.IsAny<string>(), It.IsAny<long>()));
            
            // ACT
            var response = classUnderTest.UpdateUserAsync(updateUserCommand);

            // ASSERT
            Assert.IsNotNull(response);
            Assert.AreEqual(_username, response.Result.Username);
            Assert.AreEqual(_email, response.Result.Email);
        }

        [Test]
        public void UpdateUser_ShouldFail_WhenUserDoesNotExistInDatabase()
        {
            // ARRANGE
            UpdateUserCommand updateUserCommand = new()
            {
                UserId = _nonexistentUser,
                Email = _email,
                Password = _password,
                Username = _username
            };
            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { });
            _userRepositoryMock.Setup(u => u.GetById(_nonexistentUser)).Returns<User>(null);
            _loggerMock.Setup(u => u.Error(It.IsAny<string>(), It.IsAny<long>()));

            // ACT AND ASSERT
            var ex = Assert.ThrowsAsync<Exceptions.NotFoundException>(() => classUnderTest.UpdateUserAsync(updateUserCommand));
            Assert.That(ex.Message, Is.EqualTo($"User with ID \"{updateUserCommand.UserId}\" was not found."));
        }

        [Test]
        public void DeleteUser_ShouldThrowException_WhenCommandNull()
        {
            // ACT and ASSERT
            Assert.ThrowsAsync<ArgumentNullException>(async () => await classUnderTest.DeleteUserAsync(null));
        }

        [Test]
        public void DeleteUser_ShouldDeleteUser_WhenUserExistsInDatabaseAndThereAreOtherUsersInCompany()
        {
            // ARRANGE
            DeleteUserCommand deleteUserCommand = new() { UserId = _userId };
            _userRepositoryMock.Setup(u => u.RemoveById(_userId));
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _loggerMock.Setup(u => u.Information(It.IsAny<string>(), It.IsAny<long>()));
            _userRepositoryMock.Setup(u => u.GetById(_userId)).Returns(_user);
            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { });

            // ACT
            var response = classUnderTest.DeleteUserAsync(deleteUserCommand);

            // ASSERT
            Assert.IsNotNull(response);
        }

        [Test]
        public void DeleteUser_ShouldDeleteUserAndCompany_WhenUserExistsInDatabaseAndThereAreNoOtherUsersInCompany()
        {
            // ARRANGE
            DeleteUserCommand deleteUserCommand = new() { UserId = _userId };
            _userRepositoryMock.Setup(u => u.RemoveById(_userId));
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _loggerMock.Setup(u => u.Information(It.IsAny<string>(), It.IsAny<long>()));
            _userRepositoryMock.Setup(u => u.GetById(_userId)).Returns(_user);
            _userRepositoryMock.Setup(u => u.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User> { _user });
            _unitOfWorkMock.Setup(u => u.CompanyRepository).Returns(_companyRepositoryMock.Object);
            _companyRepositoryMock.Setup(u => u.RemoveById(_user.CompanyId));

            // ACT
            var response = classUnderTest.DeleteUserAsync(deleteUserCommand);

            // ASSERT
            Assert.IsNotNull(response);
        }

        [Test]
        public void DeleteUser_ShouldFail_WhenUserDoesNotExist()
        {
            // ARRANGE
            DeleteUserCommand deleteUserCommand = new() { UserId = _nonexistentUser };
            _userRepositoryMock.Setup(u => u.GetById(_nonexistentUser)).Returns<User>(null);
            _loggerMock.Setup(u => u.Error(It.IsAny<string>(), It.IsAny<long>()));

            // ACT AND ASSERT
            var ex = Assert.ThrowsAsync<Exceptions.NotFoundException>(() => classUnderTest.DeleteUserAsync(deleteUserCommand));
            Assert.That(ex.Message, Is.EqualTo($"User with ID \"{deleteUserCommand.UserId}\" was not found."));
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