using DataTransferObjects.BasicDTOs;
using Entities.DatabaseModels.HrModels;

namespace DataTransferObjects.DataTransferObjects.HrDTOs
{
    public class PersonTypeDto : BaseDto<PersonTypeDto, CrePeopleType, int>
    {
        public string Title { get; set; }
    }

    public class PersonTypeSelectDto : BaseDto<PersonTypeSelectDto, CrePeopleType, int>
    {
        public string Title { get; set; }
        public string CreatorName { get; set; }
        public string LastModifierName { get; set; }
    }
}
