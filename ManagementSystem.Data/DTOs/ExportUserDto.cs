using System.Collections.Generic;

namespace ManagementSystem.Data.DTOs
{
    public class ExportUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public List<ExportAccountDto> Accounts { get; set; }
    }
}