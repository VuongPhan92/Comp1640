using System;

namespace Infrastructure.Caching
{
    // ddo: for more information read this 
    // http://stackoverflow.com/questions/19803555/how-to-inject-cacheitempolicy-using-simple-injector
    public interface ICachePolicy<TQuery>
    {
        DateTime AbsoluteExpiration { get; }
    }
}
