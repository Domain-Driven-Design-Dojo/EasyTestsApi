using System.ComponentModel.DataAnnotations;

namespace Common.Utilities
{
    public static class GlobalEnums
    {
        #region Persons
        public enum PersonsType
        {
            [Display(Name = "حقیقی")]
            Individual = 1,
            [Display(Name = "حقوقی")]
            Company = 2
        }
        #endregion
    }
}