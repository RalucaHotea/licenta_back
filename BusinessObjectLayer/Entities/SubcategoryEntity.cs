using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer.Entities
{
	[Table("SubCategories")]
	public class SubcategoryEntity
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		[ForeignKey("Categories")]
		public int CategoryId { get; set; }
	}
}
