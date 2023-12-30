using System;
using Entities.BaseModels;

#nullable disable

namespace Entities.SystemModels
{
    public class SysErrorLog : BaseEntity<long>
    {

        public long FUsersId { get; set; }
        public DateTime CreationDate { get; set; }
        public string MethodName { get; set; }
        public int FSysErrorTypesId { get; set; }
        public string InputJson { get; set; }
        public string CatchedException { get; set; }
    }
}
