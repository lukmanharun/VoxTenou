using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VoxTenouApp;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Models.User;

namespace TestProject
{
    public class UserTest
    {
        private readonly IMapper mockMapper;
        private readonly Mock<IHttpServices> mockHttpService;
        private readonly UserController controller;
        public UserTest()
        {
            this.mockHttpService = new Mock<IHttpServices>();
            var mappingProfile = new MapperProfile();
            var conifg = new MapperConfiguration(mappingProfile);
            mockMapper = new Mapper(conifg);
            controller = new UserController(this.mockHttpService.Object);
            mockHttpService.Setup(s => s.SignIn(new SignInDTO()
            {
                Email = "email@gmail.com",
                Password = "P@ssw0rd"
            })).ReturnsAsync((true,"Login Success",new ResponseAuth { email = "email@gmail.com" ,id = 1,token = "testtoken"})).Verifiable();
        }
        [Fact]
        public async Task SignUser_ShouldSuccess()
        {
            var result = await controller.SignIn(new SignInDTO
            {
                Email = "email@gmail.com",
                Password = "P@ssw0rd"
            });
            Assert.NotNull(result);
        }
        [Fact]
        public async Task SignUser_Error500()
        {
            mockHttpService.Setup(s => s.SignIn(new SignInDTO())).Throws<Exception>().Verifiable();
            var result = await controller.SignIn(new SignInDTO
            {
                Email = "email@gmail.com",
                Password = "P@ssw0rd"
            });
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SignUser_ModelState()
        {
            controller.ModelState.AddModelError("key", "error message");
            var result = await controller.SignIn(null);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Register_Success()
        {
            var result = await controller.Register(new CreateUserDTO
            {
                email = "email@g.com",
                firstName = "test",
                lastName = "test",
                password = "passW0rd",
                repeatPassword = "passW0rd"
            });
            var oor = result as RedirectResult;
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Register_ModelState()
        {
            controller.ModelState.AddModelError("key", "error message");
            var result = await controller.Register(null);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Register_Error500()
        {
            mockHttpService.Setup(s => s.CreateUser(new CreateUserDTO())).Throws<Exception>().Verifiable();
            var result = await controller.Register(new CreateUserDTO());
            Assert.NotNull(result);
        }


    }
}
