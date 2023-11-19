﻿using Aminos.Models.AllNet.Requests;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Aminos.Models
{
	public abstract class QueryPathSerializeBase
	{
		private readonly static Dictionary<Type, IDictionary<string, Action<string, object>>> cachedSetterMap = new();
		private readonly static Dictionary<Type, IDictionary<string, Func<object, object>>> cachedGetterMap = new();

		public QueryPathSerializeBase() { }
		public QueryPathSerializeBase(string queryStr) : this()
		{
			ParseQueryPath(queryStr);
		}

		public string GenerateQueryPath()
		{
			var type = GetType();
			if (!cachedGetterMap.TryGetValue(type, out var map))
			{
				cachedGetterMap[type] = map = type.GetProperties().ToDictionary(
					x => x.Name,
					x => new Func<object, object>((obj) => x.GetValue(obj))).ToImmutableDictionary();
			}

			var queryBuilder = new StringBuilder();

			foreach (var pair in map)
				queryBuilder.Append($"{pair.Key}={pair.Value(this)}&");

			if (queryBuilder.Length > 0)
				queryBuilder.Length -= 1;

			return queryBuilder.ToString();
		}

		public void ParseQueryPath(string queryString)
		{
			var type = GetType();
			if (!cachedSetterMap.TryGetValue(type, out var map))
			{
				cachedSetterMap[type] = map = type.GetProperties().ToDictionary(
					x => x.Name,
					x => new Action<string, object>((val, obj) => x.SetValue(obj, val))).ToImmutableDictionary();
			}

			foreach (var pair in queryString.Split("&"))
			{
				var split = pair.Split("=", StringSplitOptions.RemoveEmptyEntries);
				var name = split.ElementAtOrDefault(0);
				var value = split.ElementAtOrDefault(1);

				if (map.TryGetValue(name, out var setter))
					setter(value, this);
			}
		}

		public X ParseQueryPath<X>(string queryString) where X : QueryPathSerializeBase, new()
		{
			var o = new X();
			o.ParseQueryPath(queryString);
			return o;
		}

	}
}