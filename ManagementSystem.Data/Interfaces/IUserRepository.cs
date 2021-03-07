using System.Collections.Generic;
using ManagementSystem.Data.DTOs;
using ManagementSystem.Data.Models;

namespace ManagementSystem.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public List<ExportUserDto> GetFilteredDataToExport();
    }
}