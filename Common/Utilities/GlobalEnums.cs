using System.ComponentModel.DataAnnotations;

namespace Common.Utilities
{
    public static class GlobalEnums
    {
        #region People
        public enum PeopleType
        {
            [Display(Name = "حقیقی")]
            Individual = 1,
            [Display(Name = "حقوقی")]
            Company = 2
        }
        #endregion
    }
}