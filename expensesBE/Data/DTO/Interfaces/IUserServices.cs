// IUserService.cs
using System.Security.Claims;
using System.Threading.Tasks;
using expensesBE.Data.DTO;

namespace expensesBE.Data.DTO.Interfaces
{
    public interface IUserServices
    {
        string GetUsername(ClaimsPrincipal user);

        Task<string?> GetUserIdAsync(ClaimsPrincipal user);
    }
}

