using LibraryService.Models;
using LibraryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService) =>
            _notificationService = notificationService;

        [HttpGet]
        public async Task<List<NotificationDTO>> Get() =>
           await _notificationService.GetAsync();
    }
}
