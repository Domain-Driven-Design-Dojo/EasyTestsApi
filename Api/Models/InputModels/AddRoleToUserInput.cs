using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.InputModels
{
    public class AddRoleToUserInput
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
