namespace DataTransferObjects.DataTransferObjects.AccountsDTOs
{
    public class TokenRequestDto
    {
        //[Required]
        public string grant_type { get; set; }
        //[Required]
        public string Username { get; set; }
        //[Required]
        public string Password { get; set; }
    }
}
