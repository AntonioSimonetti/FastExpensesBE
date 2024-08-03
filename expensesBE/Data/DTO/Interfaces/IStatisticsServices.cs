using System.Collections.Generic;
using System.Threading.Tasks;

namespace expensesBE.Data.DTO.Interfaces
{
    public interface IStatisticsServices
    {
        Task<IEnumerable<KeyValuePair<string, decimal>>> GetExpenseAmountPerCategory();
    }
}

