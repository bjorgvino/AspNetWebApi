using System;

namespace AspNetWebApi.WebApiExtensions
{
    public static class Extensions
    {
        /// <summary>
        /// Returns true if the <paramref name="potentialType"/> is of the same type or derives from the <paramref name="baseType"/>.
        /// </summary>
        /// <param name="baseType">The base type</param>
        /// <param name="potentialType">The type to compare with</param>
        /// <returns>True of the <paramref name="potentialType"/> is of the same type or derives from the <paramref name="baseType"/></returns>
        public static bool IsSameOrSubclassOf(this Type baseType, Type potentialType)
        {
            return potentialType.IsSubclassOf(baseType) || baseType == potentialType;
        }
    }
}
