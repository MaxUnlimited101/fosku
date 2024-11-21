using fosku_server.Models;
using fosku_server.Data;
using fosku_server.Helpers;

namespace fosku_server.Services.Managers;

public class ManagerService : IManagerService
{
    private readonly AppDbContext _context;

    public ManagerService(AppDbContext context)
    {
        _context = context;
    }

    public void CreateManager(Manager manager)
    {
        byte[] saltBytes = HashingHelper.GenerateSalt();
        string hashPassword = HashingHelper.HashPassword(manager.PasswordHash, saltBytes);
        manager.SaltString = Convert.ToBase64String(saltBytes);
        manager.PasswordHash = hashPassword;
        _context.Managers.Add(manager);
        _context.SaveChanges();
    }


    public Manager? GetManager(int id)
    {
        var query = from managers in _context.Managers
                             where managers.Id == id
                             select managers;
        Manager? manager = query.FirstOrDefault();
        return manager;
    }
    public Manager? GetManager(string Email)
    {
        var query = from managers in _context.Managers
                             where managers.Email == Email
                             select managers;
        Manager? manager = query.FirstOrDefault();
        return manager;
    }
    public void UpdateManager(Manager manager)
    {
        _context.Managers.Update(manager);
        _context.SaveChangesAsync();
    }
}
