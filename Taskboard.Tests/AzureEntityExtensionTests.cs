using NUnit.Framework;
using Taskboard.Data.Azure;
using Taskboard.Data.Models;

namespace Taskboard.Tests
{
	public class AzureEntityExtensionTests
	{
		private Issue _entity = new Issue()
		{
			Id = ShortGuid.Get(),
			AssignedTo = "Ted",
			Color = "#00FF00",
			Content = "Content",
			Height = 100,
			Left = 100,
			Top = 100,
			Width = 100,
			WorkFlowState = WorkFlowState.Closed
		};

		[Test]
		public void CanConvertEntityToDynamicTableObject()
		{
			var tableEntity = _entity.ConvertToDynamicTableEntity<Issue>();

			Assert.That(tableEntity, Is.Not.Null);
			Assert.That(tableEntity.RowKey, Is.EqualTo(_entity.Id));
			Assert.That(tableEntity.PartitionKey, Is.EqualTo("Issue"));
			Assert.That(tableEntity.Properties.Count, Is.EqualTo(9));
		}

		[Test]
		public void CanConvertDynamicTableObjectToEntity()
		{
			var tableEntity = _entity.ConvertToDynamicTableEntity<Issue>();
			var entity = tableEntity.ConvertToEntity<Issue>();

			Assert.That(entity.Id, Is.EqualTo(_entity.Id));
			Assert.That(entity.AssignedTo, Is.EqualTo(_entity.AssignedTo));
			Assert.That(entity.Color, Is.EqualTo(_entity.Color));
			Assert.That(entity.Content, Is.EqualTo(_entity.Content));
			Assert.That(entity.Height, Is.EqualTo(_entity.Height));
			Assert.That(entity.Left, Is.EqualTo(_entity.Left));
			Assert.That(entity.Top, Is.EqualTo(_entity.Top));
			Assert.That(entity.Width, Is.EqualTo(_entity.Width));
			Assert.That(entity.WorkFlowState, Is.EqualTo(_entity.WorkFlowState));
		}
	}
}