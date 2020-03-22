using System.ComponentModel.DataAnnotations;

namespace Hilfswerk.Api.Services.Authentication
{
    public class TokenRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
