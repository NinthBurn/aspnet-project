using Microsoft.Identity.Client;

namespace Lab10.Models
{
	public class Pager
	{
		public int TotalItems {  get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int StartPage { get; set; }
		public int EndPage { get; set; }

		public Pager() { }

		public Pager(int totalItems, int currentPage, int pageSize = 4)
		{
			TotalItems = totalItems;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

			StartPage = CurrentPage - 2;
			EndPage = CurrentPage + 2;

			if (EndPage > TotalPages)
			{
				EndPage = TotalPages;
				StartPage = EndPage - 4;
			}

			if (StartPage <= 0)
			{
				StartPage = 1;
			}
		}
	}
}
