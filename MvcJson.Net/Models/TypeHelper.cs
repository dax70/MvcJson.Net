using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Reflection;

namespace MvcJson.Net.Models
{
	internal static class TypeHelper
	{
		private static readonly Type EnumerableWrapperGenericType = typeof(EnumerableWrapper<>);
		
		private static readonly Type EnumerableInterfaceGenericType = typeof(IEnumerable<>);
		
		private static readonly Type QueryableInterfaceGenericType = typeof(IQueryable<>);

		private static ConcurrentDictionary<Type, Type> delegatingEnumerableCache;

		private static ConcurrentDictionary<Type, ConstructorInfo> delegatingEnumerableConstructorCache;

		static TypeHelper()
		{
			delegatingEnumerableCache = new ConcurrentDictionary<Type, Type>();
			delegatingEnumerableConstructorCache = new ConcurrentDictionary<Type, ConstructorInfo>();
		}

		internal static ConstructorInfo GetTypeRemappingConstructor(Type type)
		{
			ConstructorInfo info = null;
			delegatingEnumerableConstructorCache.TryGetValue(type, out info);
			return info;
		}

		internal static bool TryWrappingForIEnumerableGenericOrSame(ref Type type)
		{
			return TryGetDelegatingType(EnumerableInterfaceGenericType, ref type);
		}

		internal static bool TryWrappingForIQueryableGenericOrSame(ref Type type)
		{
			return TryGetDelegatingType(QueryableInterfaceGenericType, ref type);
		}

		private static bool TryGetDelegatingType(Type interfaceType, ref Type type)
		{
			if (type != null
				&& type.IsGenericType
				&& (type.GetInterface(interfaceType.FullName) != null || type.GetGenericTypeDefinition().Equals(interfaceType)))
			{
				type = GetOrAddDelegatingType(type);
				return true;
			}
			return false;
		}

		private static Type GetOrAddDelegatingType(Type type)
		{
			return delegatingEnumerableCache.GetOrAdd(type, delegate(Type typeToRemap)
			{
				Type elementType;
				if (typeToRemap.GetGenericTypeDefinition().Equals(EnumerableInterfaceGenericType))
				{
					elementType = typeToRemap.GetGenericArguments()[0];
				}
				else
				{
					elementType = typeToRemap.GetInterface(EnumerableInterfaceGenericType.FullName).GetGenericArguments()[0];
				}
				Type typeKey = EnumerableWrapperGenericType.MakeGenericType(new Type[] { elementType });
				ConstructorInfo constructor = typeKey.GetConstructor(new Type[]
				{
					EnumerableInterfaceGenericType.MakeGenericType(new Type[]	{ elementType	})
				});
				delegatingEnumerableConstructorCache.TryAdd(typeKey, constructor);
				return typeKey;
			});
		}
	}
}