namespace DataTransferObjects.BasicDTOs
{
    public class BaseSearchDto
    {
        public int? PageNumber { get; set; }
        public int? RecordsPerPage { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatorId { get; set; }
        public long? ModifierId { get; set; }
        public string FromModificationDate { get; set; }
        public string ToModificationDate { get; set; }
        public string FromCreationDate { get; set; }
        public string ToCreationDate { get; set; }

    }

    
}
