using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IControllerService
    {

        Task<TransferTimeReportDto> CreateTimeReportAsync(IFormCollection form);
    }
}
