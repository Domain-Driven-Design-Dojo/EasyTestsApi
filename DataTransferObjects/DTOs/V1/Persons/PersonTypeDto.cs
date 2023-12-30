using DataTransferObjects.DTOs.BaseDtos;
using Entities.PersonModels;

namespace DataTransferObjects.DTOs.V1.Persons
{
    public class PersonTypeDto : BaseDto<PersonTypeDto, CrePersonType, int>
    {
        public string Title { get; set; }
    }

    public class PersonTypeSelectDto : BaseDto<PersonTypeSelectDto, CrePersonType, int>
    {
        public string Title { get; set; }
        public string CreatorName { get; set; }
        public string LastModifierName { get; set; }
    }
}
