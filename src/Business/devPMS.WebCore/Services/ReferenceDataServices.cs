using devPMS.Data;
using devPMS.WebCore.Queries;
using Infrastructure.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devPMS.WebCore.Services
{
    public sealed class ReferenceDataServices : IService
    {
        private readonly GetBranchQueryHandler _getBranches;
        private readonly GetScopeTypeQueryHandler _getScopeType;

        private List<ScopeType> _scopeTypeList;
        private List<Branch> _brachList;
        private List<Priority> _priorityList;

        public ReferenceDataServices(GetBranchQueryHandler getBranches, GetScopeTypeQueryHandler getScopeType)
        {
            _getBranches = getBranches;
            _getScopeType = getScopeType;
        }

        public List<ScopeType> ScopeTypeList
        {
            get
            {
                if (_scopeTypeList == null)
                {
                    _scopeTypeList = _getScopeType.Handle(new GetScopeTypeQuery()).ToList();
                }
                return _scopeTypeList;
            }
        }

        public List<ScopeType> ScopeTypeListByScopeId(int scopeId)
        {
            return ScopeTypeList.Where(p => p.ScopeId == scopeId)
                    .OrderBy(p => p.ScopeId)
                    .OrderBy(p => p.OrderList)
                    .ToList();
        }

        public List<Branch> BranchList
        {
            get
            {
                if (_brachList == null)
                {
                    _brachList = _getBranches.Handle(new GetBranchQuery()).ToList();
                }
                return _brachList;
            }
        }

        public List<Priority> PriorityList
        {
            get
            {
                if (_priorityList == null)
                {
                    _priorityList = Priority.GetList();
                }
                return _priorityList;
            }
        }
    }

    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityDescription { get; set; }

        public static List<Priority> GetList()
        {
            return new List<Priority>
            {
                new Priority { PriorityId = 0, PriorityDescription = "None" },
                new Priority { PriorityId = 1, PriorityDescription = "Highest" },
                new Priority { PriorityId = 2, PriorityDescription = "High" },
                new Priority { PriorityId = 3, PriorityDescription = "Medium" },
                new Priority { PriorityId = 4, PriorityDescription = "Low" },
                new Priority { PriorityId = 5, PriorityDescription = "Lowest" },
            };
        }
    }
}
