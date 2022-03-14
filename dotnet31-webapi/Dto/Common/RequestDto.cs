using cmes_webapi.Api.Dto;
using System.ComponentModel.DataAnnotations;

namespace cmes_webapi.Api.Dto
{
    public class RequestDto<T>
    {
        [Required(ErrorMessage = "{0} es un dato obligatorio.")]
        public BGBAHeaderDto BGBAHeader { get; set; }

        [Required(ErrorMessage = "Datos es un dato obligatorio.")]
        public T Datos { get; set; }
    }
}
