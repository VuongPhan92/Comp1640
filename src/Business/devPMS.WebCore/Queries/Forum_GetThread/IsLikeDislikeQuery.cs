using Infrastructure.Queries;
using SSTVN.PMS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Queries
{
    public class IsLikeDislikeQuery : IQuery<bool>
    {
        public TypeAndLike TypeLike { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
     
    }
}
