using Entities.BaseModels;

#nullable disable

namespace Entities.SystemModels
{
    public class SysErrorType : BaseEntityWithActors<int>
    {
        public string Title { get; set; }
        public string ErrorDescription { get; set; }
    }
}
