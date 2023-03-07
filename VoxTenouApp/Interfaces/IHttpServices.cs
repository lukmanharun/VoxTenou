using VoxTenouApp.Models;
using VoxTenouApp.Models.Organizer;
using VoxTenouApp.Models.SportEvent;
using VoxTenouApp.Models.User;

namespace VoxTenouApp.Interfaces
{
    public interface IHttpServices
    {
        Task<bool> AddNewOrgnizer(string token, OrganizerFormDto form);
        Task<bool> AddNewSportEvent(string token, SubmitSportEventDto form);
        Task<(bool success, string message)> CreateUser(CreateUserDTO userModel);
        Task<bool> DeleteOrgnizer(string token, long Id);
        Task<bool> DeleteSportEvent(string token, long Id);
        Task<bool> EditOrgnizer(string token, OrganizerFormDto form, long Id);
        Task<bool> EditSportEvent(string token, SubmitSportEventDto form, long Id);
        Task<ResponseApiPagination<GridOrganizer>> GetListOrganizer(int page, string token);
        Task<ResponseApiPagination<GridOrganizer>> GetListOrganizer(int page, int pageNumber, string token);
        Task<ResponseApiPagination<GridSportEvent>> GetListSportEvent(int page, string token);
        Task<OrganizerFormSubmitDto> OrganizerById(string token, long Id);
        Task<(bool success, string message, ResponseAuth? auth)> SignIn(SignInDTO userModel);
        Task<GridSportEvent> SportEventById(string token, long Id);
    }
}
