using Microsoft.AspNetCore.Mvc;
using VoxTenouApp.Interfaces;
using VoxTenouApp.Models;
using VoxTenouApp.Models.Organizer;

namespace VoxTenouApp.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly IHttpServices httpServices;
        public OrganizerController(IHttpServices httpServices)
        {
            this.httpServices = httpServices;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var page = id == null || id.Value == 0 ? 1 : Convert.ToInt16(id);
            var data = await this.GetOrganizer(page);
            if(data is not null)
            return View(data);
            return View();
        }
        private async Task<ResponseApiPagination<GridOrganizer>> GetOrganizer(int page)
        {
            string token = this.HttpContext?.Session?.GetString("Token") ?? string.Empty;
            if (string.IsNullOrEmpty(token))
            {
                return new ResponseApiPagination<GridOrganizer> { };
            }
            return await httpServices.GetListOrganizer(page, token);
        }
        public IActionResult AddNew()
        {
            return View();
        }
        [HttpPost("Organizer/AddNew")]
        public async Task<IActionResult> AddNew(OrganizerFormDto form)
        {
            if (!ModelState.IsValid) return View(form);
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var success = await httpServices.AddNewOrgnizer(token,form);
            if (!success) return View(form);
            return Redirect("/Organizer/Index");
        }
        public async Task<IActionResult> Edit(long Id)
        {
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var data = await httpServices.OrganizerById(token,Id);
            if (data == null) return Redirect("/Organizer/Index");
            return View(data);
        }
        [HttpPost("Organizer/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id,OrganizerFormDto form)
        {
            if (!ModelState.IsValid) return View(form);
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var success = await httpServices.EditOrgnizer(token, form,Id);
            if (!success) return View(form);
            return Redirect("/Organizer/Index");
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var token = this.HttpContext?.Session?.GetString("Token")??string.Empty;
            var success = await httpServices.DeleteOrgnizer(token, Id);
            if (!success) return Json(new { success = false });
            return Json(new { success = true });
        }
    }
}
