using SSTVN.PMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Business
{
    public class LikeDislikeCommand
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public TypeAndLike TypeLike { get; set; }
       // public bool dislike { get; set; }
    }
}
