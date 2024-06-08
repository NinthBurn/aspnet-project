using System.ComponentModel.DataAnnotations;

namespace Lab10.Models
{
	public class AddLogViewModel
	{
		public DateTime LogDate { get; set; }
		public string LogType { get; set; }
		public string LogSeverity { get; set; }
		public string LogContent { get; set; }
	}
}
