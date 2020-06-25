using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostTagViewModel
    {
        public Post Post { get; set; }
        public Tag Tag { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PostTag> PostTags { get; set; }
        //public Boolean PostTagExists()
        //{

        //   //foreach(Tag tag in Tags)
        //   // {
        //   //     return PostTags.Exists((pT) => pT.PostId == Post.Id && pT.TagId == tag.Id);
        //   // }
        //  //return PostTags.Exists((pT) => pT.PostId == Post.Id && pT.TagId == Tag.Id);
        //  //  @if(Model.PostTagExists() == false)


        //}

    }
}
