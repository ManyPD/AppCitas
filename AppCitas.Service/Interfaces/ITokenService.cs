using AppCitas.Service.Entities;

namespace AppCitas.Service.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
