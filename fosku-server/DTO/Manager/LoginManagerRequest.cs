using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Manager;

public record LoginManagerRequest(
    string Email,

    string Password
);
