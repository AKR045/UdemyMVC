using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyMVC.Models
{
	public class Chapter
	{
		[Key]
		public int ID { get; set; } 
		public string Title { get; set; }
		//[InverseProperty("ChapterID")]
		public List<Topic> Topics { get; set; }
		[ForeignKey("CourseId")]
		public int CourseId { get; set; } // Foreign key
		public Course Course { get; set; } // Navigation property
	}
}
