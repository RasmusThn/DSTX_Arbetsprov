using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IDataAccessService
    {
        void SaveFileToDB(Microsoft.AspNetCore.Http.IFormFile file,int id);
    }
}
