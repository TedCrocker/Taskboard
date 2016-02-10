using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using Taskboard.Data.Models;

namespace Taskboard.Data.Azure
{
	public static class EntityExtensions
	{
		public static DynamicTableEntity ConvertToDynamicTableEntity<T>(this Entity entity)
		{
			var propDict = new Dictionary<string, EntityProperty>();
			var type = typeof (T);
			var props = type.GetProperties();

			foreach (var prop in props)
			{
				var entityProperty = GetEntityProperty(prop.Name, prop.GetValue(entity));
				propDict.Add(prop.Name, entityProperty);
			}

			return new DynamicTableEntity(type.Name, entity.Id, "*", propDict);
		}

		private static EntityProperty GetEntityProperty(string key, object value)
		{
			if (value == null) return new EntityProperty((string)null);
			if(value.GetType().IsEnum) return new EntityProperty((int)value);
			if (value.GetType() == typeof(byte[])) return new EntityProperty((byte[])value);
			if (value is bool) return new EntityProperty((bool)value);
			if (value is DateTimeOffset) return new EntityProperty((DateTimeOffset)value);
			if (value is DateTime) return new EntityProperty((DateTime)value);
			if (value is double) return new EntityProperty((double)value);
			if (value is Guid) return new EntityProperty((Guid)value);
			if (value is int) return new EntityProperty((int)value);
			if (value is long) return new EntityProperty((long)value);
			if (value is string) return new EntityProperty((string)value);
			throw new Exception("not supported " + value.GetType() + " for " + key);
		}

		public static T ConvertToEntity<T>(this DynamicTableEntity tableEntity) where T : Entity
		{
			var entity = Activator.CreateInstance<T>();
			var type = typeof (T);

			foreach (var entityProperty in tableEntity.Properties)
			{
				var property = type.GetProperty(entityProperty.Key);
				var value = GetValue(entityProperty.Value);
				if (property.PropertyType.IsNullableEnum())
				{
					var enumType = Nullable.GetUnderlyingType(property.PropertyType);
					var obj = (object)Enum.Parse(enumType, value.ToString());
					property.SetValue(entity, obj);
				}
				else
				{
					property.SetValue(entity, value);
				}
			}

			return entity;
		}

		private static bool IsNullableEnum(this Type t)
		{
			Type u = Nullable.GetUnderlyingType(t);
			return (u != null) && u.IsEnum;
		}

		private static object GetValue(EntityProperty property)
		{
			switch (property.PropertyType)
			{
				case EdmType.Binary:
					return property.BinaryValue;
				case EdmType.Boolean:
					return property.BooleanValue;
				case EdmType.DateTime:
					return property.DateTime;
				case EdmType.Double:
					return property.DoubleValue;
				case EdmType.Guid:
					return property.GuidValue;
				case EdmType.Int32:
					return property.Int32Value;
				case EdmType.Int64:
					return property.Int64Value;
				case EdmType.String:
					return property.StringValue;
				default:
					throw new Exception("not supported " + property.PropertyType);
			}
		}
	}
}