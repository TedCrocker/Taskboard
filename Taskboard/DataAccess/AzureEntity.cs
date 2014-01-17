using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Taskboard.DataAccess
{
	public class AzureEntity : ITableEntity
	{
		public string Id { get; set; }

		public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
		{
			var type = GetType();
			foreach (var keyValue in properties)
			{
				var propInfo = type.GetProperty(keyValue.Key);
				var propertyType = propInfo.PropertyType;
				var nullable = false;

				if (Nullable.GetUnderlyingType(propertyType) != null)
				{
					nullable = true;
					propertyType = Nullable.GetUnderlyingType(propertyType);
				}

				object value = null;
				if (propertyType.IsEnum && !string.IsNullOrEmpty(keyValue.Value.Int32Value.ToString()))
				{
					value = Enum.Parse(propertyType, keyValue.Value.Int32Value.ToString());
				}
				else
				{
					value = GetEntityPropertyValue(keyValue.Value);
				}

				propInfo.SetValue(this, value, null);
			}
		}
		
		public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
		{
			var dict = new Dictionary<string, EntityProperty>();
			foreach(var prop in GetType().GetProperties())
			{
				var propName = prop.Name;
				if (propName != "PartitionKey" && propName != "RowKey" && propName != "Timestamp" && propName != "ETag")
				{
					dict.Add(prop.Name, GetEntityProperty(prop.Name, GetPropertyValue(prop.Name)));
				}

			}
			return dict;
		}

		private object GetPropertyValue(string key)
		{
			var propertyInfo = GetType().GetProperty(key);
			return propertyInfo.GetValue(this, null);
		}

		private EntityProperty GetEntityProperty(string key, object value)
		{
			if (value == null) return new EntityProperty((string)null);
			if (value.GetType() == typeof(byte[])) return new EntityProperty((byte[])value);
			if (value is bool) return new EntityProperty((bool)value);
			if (value is DateTimeOffset) return new EntityProperty((DateTimeOffset)value);
			if (value is DateTime) return new EntityProperty((DateTime)value);
			if (value is double) return new EntityProperty((double)value);
			if (value is Guid) return new EntityProperty((Guid)value);
			if (value is int) return new EntityProperty((int)value);
			if (value is long) return new EntityProperty((long)value);
			if (value is string) return new EntityProperty((string)value);
			if (value is Enum) return new EntityProperty((int) value);
			throw new Exception("not supported " + value.GetType() + " for " + key);
		}

		private object GetEntityPropertyValue(EntityProperty property)
		{
			switch (property.PropertyType)
			{
				case EdmType.Binary:
					return property.BinaryValue;
				case EdmType.Boolean:
					return property.BooleanValue;
				case EdmType.DateTime:
					if (property.DateTimeOffsetValue.HasValue)
					{
						return property.DateTimeOffsetValue.Value.DateTime;
					}
					return null;

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

		private string _partitionKey;
		[JsonIgnore]
		public string PartitionKey
		{
			get
			{
				if (string.IsNullOrEmpty(_partitionKey))
				{
					_partitionKey = GetType().Name;
				}
				return _partitionKey;
			}
			set { _partitionKey = value; }
		}

		[JsonIgnore]
		public string RowKey
		{
			get { return Id; } set
			{
				Id = value;
			}
		}

		[JsonIgnore]
		public DateTimeOffset Timestamp { get; set; }
		[JsonIgnore]
		public string ETag { get; set; }
	}
}