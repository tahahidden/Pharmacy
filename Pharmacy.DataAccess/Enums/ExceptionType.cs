using System.ComponentModel.DataAnnotations;

namespace Pharmacy.DataAccess.Enums
{
    public enum ExceptionType
    {
        [Display(Name = "item not found!")]
        ItemNotFound = 800,

        [Display(Name = "exception in insert to database")]
        InsertItemToDatabase = 840,

        [Display(Name = "exception in data access layer")]
        UnknownDataAccess = 850,

        [Display(Name = "exception in infra layer")]
        UnknownInfra = 860,
    }
}