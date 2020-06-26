using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDeatailsViewModel
    {
        public Post Post { get; set; }
     
        public List<Tag> Tags { get; set; }
    }
}
