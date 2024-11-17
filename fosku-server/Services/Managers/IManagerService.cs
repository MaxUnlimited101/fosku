using fosku_server.Models;
using fosku_server.Data;
using fosku_server.Helpers;

namespace fosku_server.Services.Managers;

public interface IManagerService
{
    public void CreateManager(Manager manager);
    public Manager? GetManager(int id);
    public Manager? GetManager(string Email);
    public void UpdateManager(Manager manager);
}
