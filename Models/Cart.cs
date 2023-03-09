using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DummyProject.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public string UserEmail { get; set; }

        public int Quantity { get; set; }

    }
}
