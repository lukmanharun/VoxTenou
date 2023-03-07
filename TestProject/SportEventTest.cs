
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Models.User;
using VoxTenouApp;
using VoxTenouApp.Models;
using VoxTenouApp.Models.Organizer;
using Microsoft.AspNetCore.Http;
using VoxTenouApp.Models.SportEvent;

namespace TestProject
{
    public class SportEventTest
    {
        private readonly Mock<HttpContext> httpContext;
        private readonly IMapper mockMapper;
        private readonly Mock<IHttpServices> mockHttpService;
        private readonly SportEventController controller;
        public SportEventTest()
        {
            httpContext = new Mock<HttpContext>();
            this.mockHttpService = new Mock<IHttpServices>();
            var mappingProfile = new MapperProfile();
            var conifg = new MapperConfiguration(mappingProfile);
            mockMapper = new Mapper(conifg);
            controller = new SportEventController(this.mockHttpService.Object,mockMapper);
        }
        [Fact]
        public async Task Index_List()
        {
            int? page = 1;
            var result = await controller.Index(page);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Index_List_Notfound()
        {
            var result = await controller.Index(null);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Index_Error()
        {
            mockHttpService.Setup(s => s.GetListOrganizer(1, "")).Throws<Exception>().Verifiable();
            var result = await controller.Index(null);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AddNew_Success()
        {
            var result = await controller.AddNew(new VoxTenouApp.Models.SportEvent.SubmitSportEventDto());
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AddNew_SuccessView()
        {
            var result = await controller.AddNew(new SubmitSportEventDto());
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AddNew_ModelState()
        {
            controller.ModelState.AddModelError("key", "error message");
            var result = await controller.AddNew(null);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_Success()
        {
            var result = await controller.Edit(1);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_ModelState()
        {
            controller.ModelState.AddModelError("key", "error message");
            var result = await controller.Edit(0);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task EditPOST_Success()
        {
            var result = await controller.Edit(1, new SubmitEditSportEventDto());
            Assert.NotNull(result);
        }
        [Fact]
        public async Task EditPOST_ModelState()
        {
            controller.ModelState.AddModelError("key", "error message");
            var result = await controller.Edit(0, new SubmitEditSportEventDto());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var result = await controller.Delete(1);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_ModelState()
        {
            controller.ModelState.AddModelError("key", "error message");
            var result = await controller.Delete(0);
            Assert.NotNull(result);
        }

    }
}