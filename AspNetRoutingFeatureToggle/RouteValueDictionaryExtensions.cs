namespace AspNetRoutingFeatureToggle
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Routing;

    /// <summary>
    /// Set of extension methods on object.
    /// </summary>
    internal static class RouteValueDictionaryExtensions
    {
        /// <summary>
        /// Converts an object in a <see cref="RouteValueDictionary"/>.
        /// </summary>
        /// <param name="values">The object values.</param>
        /// <returns>Null if values is null otherwise builds an appropriate RouteValueDictionary.</returns>
        internal static RouteValueDictionary ToRouteValueDictionary(this object values)
        {
            if (values == null)
            {
                return null;
            }
            IDictionary<string, object> dictionary = values as IDictionary<string, object>;
            if (dictionary != null)
            {
                return new RouteValueDictionary(dictionary);
            }
            var dictionaryProjectionValues = values.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(p => p.GetIndexParameters().Length == 0 && p.GetMethod != null)
                                      .ToDictionary(p => p.Name, p => p.GetValue(values));
            return new RouteValueDictionary(dictionaryProjectionValues);
        }
    }
}
