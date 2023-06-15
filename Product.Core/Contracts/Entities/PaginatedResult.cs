using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Contracts.Entities
{
	public class PaginatedResult<T>
	{
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public IEnumerable<T> Items { get; set; }
        public string[] Errors { get; set; }
	}
}
