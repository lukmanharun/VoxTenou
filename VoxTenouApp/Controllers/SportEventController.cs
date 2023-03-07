using Microsoft.AspNetCore.Mvc;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Models;
using VoxTenouApp.Models.SportEvent;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VoxTenouApp.Controllers
{
    public class SportEventController : Controller
    {
        private readonly IHttpServices httpServices;
        private readonly IMapper mapper;
        public SportEventController(IHttpServices httpServices, IMapper mapper)
        {
            this.mapper = mapper;
            this.httpServices = httpServices;
        }
        public async Task<IActionResult> Index(int? id)
        {
            id = id ?? 1;
            var data = await this.GetSportEvent(Convert.ToInt16(id));
            return View(data);
        }
        private async Task<ResponseApiPagination<GridSportEvent>> GetSportEvent(int page)
        {
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            return await httpServices.GetListSportEvent(page, token);
        }
        private async Task<IEnumerable<SelectListItem>> GetListOrganizer()
        {
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            if (string.IsNullOrEmpty(token)) return Enumerable.Empty<SelectListItem>();
            var data = await httpServices.GetListOrganizer(1, 1000, token);
            if(data == null) return Enumerable.Empty<SelectListItem>();
            var select = data.data.Select(s => new SelectListItem() { Value = $"{s.id}", Text = s.organizerName });
            var list = new SelectList(select, "Value", "Text");
            return list;
        }
        public async Task<IActionResult> AddNew()
        {
            this.ViewBag.ListOrganizer = await this.GetListOrganizer();
            return View();
        }
        [HttpPost("SportEvent/AddNew")]
        public async Task<IActionResult> AddNew(SubmitSportEventDto form)
        {
            if (!ModelState.IsValid) return View(form);

            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var success = await httpServices.AddNewSportEvent(token, form);
            if (!success) return View(form);
            return Redirect("/SportEvent/Index");
        }
        public async Task<IActionResult> Edit(long Id)
        {
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var data = await httpServices.SportEventById(token, Id);
            if (data == null) return Redirect("/SportEvent/Index");
            data.organizerId = data.organizer == null ? 0 : data.organizer.id;
            if (data == null) return Redirect("/SportEvent/Index");
            var request = mapper.Map<SubmitEditSportEventDto>(data); 
            this.ViewBag.ListOrganizer = await this.GetListOrganizer();
            return View(request);
        }
        [HttpPost("SportEvent/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id, SubmitEditSportEventDto form)
        {
            if (!ModelState.IsValid)
            {
                this.ViewBag.ListOrganizer = await this.GetListOrganizer();
                return View(form);
            }
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var request = mapper.Map<SubmitSportEventDto>(form);
            var success = await httpServices.EditSportEvent(token, request, Id);
            if (!success) return View(form);
            return Redirect("/SportEvent/Index");
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var success = await httpServices.DeleteSportEvent(token, Id);
            if (!success) return Json(new { success = false });
            return Json(new { success = true });
        }
    }
}
