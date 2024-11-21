using fosku_server.DTO;
using fosku_server.DTO.Manager;
using fosku_server.Models;
using fosku_server.Services.Auth;
using fosku_server.Services.Managers;
using Microsoft.AspNetCore.Mvc;

namespace fosku_server.Controllers
{
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService managerService;
        private readonly IAuthService authService;
        public ManagerController(IManagerService managerService, IAuthService authService)
        {
            this.managerService = managerService;
            this.authService = authService;
        }

        [HttpPost]
        [Route("/manager/login")]
        public ActionResult LoginManager([FromBody] LoginManagerRequest request)
        {
            Manager? manager = managerService.GetManager(request.Email);
            if (manager == null)
            {
                return NotFound("User not found!");
            }
            Manager? validatedUser = (Manager?)authService.AuthenticatePerson(request.Password, manager);
            if (validatedUser == null)
            {
                return Forbid();
            }

            var response = new LoginResponse(Token: authService.GenerateToken(validatedUser));
            return Ok(response);
        }
    }
}
