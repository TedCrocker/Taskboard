using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Taskboard.DataAccess
{
	public class AzureEntity : ITableEntity
	{
		public int Id { get; set; }

		public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
		{
			foreach (var keyValue in properties)
			{
				//SetValue(keyValue.Key, keyValue.Value);
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
					return property.DateTimeOffsetValue;
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

		public string RowKey
		{
			get { return Id.ToString(); } set
			{
				Id = Int32.Parse(value);
				//Id = (T)Convert.ChangeType(value, typeof(T));
			}
		}

		public DateTimeOffset Timestamp { get; set; }
		public string ETag { get; set; }
	}
}