using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SSTVN.DDo.Utility.Extension
{
    public static class PropertyHelper<T> where T : class
    {
        /// <summary>
        /// Publics the instance properties equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="to">To.</param>
        /// <param name="ignore">The ignore.</param>
        /// <returns></returns>
        public static bool PublicInstancePropertiesEqual(T self, T to, bool IsIgnoreVirtual = true, params string[] ignore)
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name) && (IsVirtualProperty(pi) != IsIgnoreVirtual))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }

        #region Same code as above but uses LINQ and Extension methods :
        /// <summary>
        /// Publics the instance properties equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="to">To.</param>
        /// <param name="ignore">The ignore.</param>
        /// <returns></returns>
        //public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        //{
        //    if (self != null && to != null)
        //    {
        //        var type = typeof(T);
        //        var ignoreList = new List<string>(ignore);
        //        var unequalProperties =
        //            from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //            where !ignoreList.Contains(pi.Name)
        //            let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
        //            let toValue = type.GetProperty(pi.Name).GetValue(to, null)
        //            where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
        //            select selfValue;
        //        return !unequalProperties.Any();
        //    }
        //    return self == to;
        //}
        #endregion

        public static string PublicInstancePropertiesChanges(T self, T to, bool IsIgnoreVirtual = true, params string[] ignore)
        {
            var sb = new StringBuilder();
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    string xxx = pi.Name;
                    if (!ignoreList.Contains(pi.Name) && (IsVirtualProperty(pi) != IsIgnoreVirtual))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        // GetDisplayName if any
                        string displayName = GetDisplayName(pi);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            var str = string.Format("{0}|{1}: {2} changed to {3}", pi.Name, displayName, selfValue, toValue);
                            sb.AppendLine(str);
                        }
                    }
                }
            }
            return sb.ToString(); ;
        }

        private static bool IsVirtualProperty(PropertyInfo pi)
        {
            var result = pi.GetGetMethod().IsVirtual;
            return result;
        }

        /// <summary>
        /// Gets the property. 
        /// Using PropertyHelper<Foo>.GetProperty(x => x.Bar);
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static PropertyInfo GetProperty<TProperty>(
            Expression<Func<T, TProperty>> selector)
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return (PropertyInfo)((MemberExpression)body).Member;
                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Gets the display attribute name.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string GetDisplayName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            Type type = typeof(T);

            string propertyName = null;
            string[] properties = null;
            IEnumerable<string> propertyList;
            //unless it's a root property the expression NodeType will always be Convert
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expression.Body as UnaryExpression;
                    propertyList = (ue != null ? ue.Operand : null).ToString().Split(".".ToCharArray()).Skip(1); //don't use the root property
                    break;
                default:
                    propertyList = expression.Body.ToString().Split(".".ToCharArray()).Skip(1);
                    break;
            }

            //the propert name is what we're after
            propertyName = propertyList.Last();
            //list of properties - the last property name
            properties = propertyList.Take(propertyList.Count() - 1).ToArray(); //grab all the parent properties

            foreach (string property in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(property);
                type = propertyInfo.PropertyType;
            }

            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            // Look for [MetadataType] attribute in type hierarchy
            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.Name : String.Empty;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="pi">The pi.</param>
        /// <returns></returns>
        public static string GetDisplayName(PropertyInfo pi)
        {
            Type type = typeof(T);

            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(pi.Name).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            // Look for [MetadataType] attribute in type hierarchy
            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(pi.Name);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.Name : String.Empty;
        }

        #region GetPropertyName and GetPropertyNames

        /// <summary>
        /// Gets the property names using expression selector a new dynamic object.
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="separator">The separator.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public static string GetPropertyNames<TProperty>(Expression<Func<T, TProperty>> selector, string separator = ",")
        {
            Expression body = selector;
            if (body is LambdaExpression)
            {
                body = ((LambdaExpression)body).Body;
            }
            if (body.NodeType != ExpressionType.New) return "";

            var members = ((NewExpression)body).Members;

            return members.Count > 0 ? string.Join(separator, members.Select(u => u.Name)) : "";

        }

        /// <summary>
        /// Gets the property names.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertySelectors">The property selectors.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetPropertyNames(params Expression<Func<T, object>>[] propertySelectors)
        {
            var propertyNames = new List<string>();
            foreach (var propertySelector in propertySelectors)
            {
                var tmpName = GetPropertyName(propertySelector);
                if (tmpName != null)
                {
                    propertyNames.Add(tmpName);
                }
            }
            return propertyNames;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns></returns>
        public static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        {
            var propertySelectorExpression = GetMemberExpression(propertySelector);
            if (propertySelectorExpression != null)
            {
                PropertyInfo propertyInfo = propertySelectorExpression.Member as PropertyInfo;
                if (propertyInfo != null)
                {
                    return propertyInfo.Name;
                }
            }
            return null;
        }

        private static MemberExpression GetMemberExpression<TProperty>(Expression<Func<T, TProperty>> exp)
        {
            var member = exp.Body as MemberExpression;
            var unary = exp.Body as UnaryExpression;
            return member ?? (unary != null ? unary.Operand as MemberExpression : null);
        }

        #endregion
    }
}
