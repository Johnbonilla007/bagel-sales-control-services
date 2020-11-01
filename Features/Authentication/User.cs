using System.ComponentModel.DataAnnotations;
using bagel_sales_control.Helpers;

namespace bagel_sales_control.Features.Authentication
{
    public class User : ControlTransactionFields
    {
        [Key]
        public string auth0Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}