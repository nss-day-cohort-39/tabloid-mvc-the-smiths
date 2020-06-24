using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TabloidMVC.Models
{
    public class PostTag
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        public int TagId { get; set; }


    }
}
