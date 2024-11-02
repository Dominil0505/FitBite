using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserRoles<T>
    {
        Task<List<T>> GetAllRoles();
    }
}
