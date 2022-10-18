using System;
using Entities.DatabaseModels.CommonModels.BaseModels;

#nullable disable

namespace Entities.DatabaseModels.SystemModels
{
    public class SysLog : BaseEntity<long>
    {
        public long FUsersId { get; set; }
        public int FSystemOperationsId { get; set; }
        public long? EntityId { get; set; }
        public string LogDescription { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
