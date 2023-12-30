namespace Api.Models.InputModels.V1.Users
{
    public class AddRoleToUserInput
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
