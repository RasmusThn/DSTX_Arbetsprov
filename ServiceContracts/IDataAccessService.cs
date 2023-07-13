using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IDataAccessService
    {
        Task SaveFileToDBAsync(Microsoft.AspNetCore.Http.IFormFile file,int id);
    }
}
