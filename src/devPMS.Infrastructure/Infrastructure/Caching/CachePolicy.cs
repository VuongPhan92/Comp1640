using System;
using System.Runtime.Caching;
using System.Web.Caching;

namespace Infrastructure.Caching
{
    //not use this for now
    public class CachePolicy<TQuery> : ICachePolicy<TQuery>
    {
        public CachePolicy()
        {
            AbsoluteExpiration = Cache.NoAbsoluteExpiration;
        }
        public DateTime AbsoluteExpiration { get; set; }
    }

    public class CachePolicyAttribute : Attribute
    {
        private double _absoluteExpirationInSeconds;
        public CachePolicyAttribute(double absoluteExpirationInSeconds)
        {
            _absoluteExpirationInSeconds = absoluteExpirationInSeconds;
        }

        public CachingPolicySettings Policy
        {
            get
            {
                return new CachingPolicySettings(_absoluteExpirationInSeconds);
            }
        }
    }

    public class CachingPolicySettings
    {
        private double _absoluteExpirationInSeconds;
        public CachingPolicySettings(double absoluteExpirationInSeconds)
        {
            _absoluteExpirationInSeconds = absoluteExpirationInSeconds;
        }

        public CacheItemPolicy AbsoluteExpiration
        {
            get
            {
                return new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddSeconds(_absoluteExpirationInSeconds) };
            }
        }
    }
}
