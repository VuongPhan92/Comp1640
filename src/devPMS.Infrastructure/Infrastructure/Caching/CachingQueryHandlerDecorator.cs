using Infrastructure.Queries;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Web.Caching;

namespace Infrastructure.Caching
{
    // ddo: for all code comment lines below is not used for this time.
    // I decide to use CachePolicyAttribute instead.
    public sealed class CachingQueryHandlerDecorator<TQuery, TResult>
        : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decorated;
        private readonly ObjectCache _cache;
        //public readonly ICachePolicy<TQuery> _policy;

        private static readonly bool _shouldCache;
        private static readonly CachingPolicySettings _policy;

        static CachingQueryHandlerDecorator()
        {
            var attribute = typeof(TQuery).GetCustomAttribute<CachePolicyAttribute>();

            if (attribute != null)
            {
                _shouldCache = true;
                _policy = attribute.Policy;
            }
        }

        public CachingQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decorated,
            //ICachePolicy<TQuery> cachePolicy,
            ObjectCache cache)
        {
            _decorated = decorated;
            _cache = cache;
            //_policy = cachePolicy;
        }

        public TResult Handle(TQuery query)
        {
            //if (_policy.AbsoluteExpiration == Cache.NoAbsoluteExpiration)
            //{
            //    return _decorated.Handle(query);
            //}

            if (!_shouldCache)
            {
                return _decorated.Handle(query);
            }

            var key = query.GetType().ToString();
            var result = (TResult)_cache[key];
            if (result == null)
            {
                //_log.Debug(m => m("No cache entry for {0}", key));
                result = _decorated.Handle(query);
                if (!_cache.Contains(key))
                {
                    //_cache.Add(key, result, new CacheItemPolicy { AbsoluteExpiration = _policy.AbsoluteExpiration });
                    _cache.Add(key, result, _policy.AbsoluteExpiration);
                }
            }
            return result;
        }
    }
}
