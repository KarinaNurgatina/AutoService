using System;
using System.ComponentModel.DataAnnotations;

namespace AutoService.Web.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя клиента обязательно")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Тип услуги обязателен")]
        public string ServiceType { get; set; }

        [Required(ErrorMessage = "Статус обязателен")]
        public string Status { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
