using Entities.DatabaseModels.CommonModels.BaseModels;

#nullable disable

namespace Entities.DatabaseModels.SystemModels
{
    public class SysErrorType : BaseEntityWithActors<int>
    {
        public string Title { get; set; }
        public string ErrorDescription { get; set; }
    }
}
