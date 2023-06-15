using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

//using Kendo.DynamicLinq;

namespace Product.Core.Contracts.Entities
{
	public class PaginatedRequest
	{
	    public PaginatedRequest()
	    {
            //Sort = new List<Sort>();
	        Page = 1;
	        PageSize = 10;
	    }
        public PaginatedRequest(int pageSize)
        {
            //Sort = new List<Sort>();
            Page = 1;
            PageSize = pageSize;
        }
		//public int Offset
		//{
		//    get { return (this.PageNumber) * this.PageSize;  }
		//}
		//public int Limit
		//{
		//    get { return this.PageSize; }
		//}
		public int Skip => (Page - 1) * PageSize;

        public int Take => PageSize;

	    private string _sort;

	    public string Sort
	    {
	        get => _sort?.Replace("-", " ");
	        set => _sort = value;
	    }

	    public bool HasSort => _sort != null;

	    public int Page { get; set; }

	    public int PageSize { get; set; }
        //public string SortField { get; set; }

        //public string SortOrder { get; set; }
        //public IList<Sort> Sort { get; set; }
        ////[DataMember(Name = "filter")]
        //public Filter Filter { get; set; }
        public List<DynamicFilter> Filters { get; set; }

        public string FiltersQuery
        {
            get
            {
                if (Filters != null && Filters.Count > 0)
                {
                    var f = Filters.Select(x => x.Filter).Where(x => x != null);
                    return string.Join(" AND ", f);
                }

                return null;
            }
        }
    }

    public class DynamicFilter
    {
        public string Prop { get; set; }
        public string Type { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public string Filter
        {
            get
            {
                if (!Regex.IsMatch(Prop, "^[A-Za-z0-9]$"))
                {
                    return null;
                }

                if (!Regex.IsMatch(Value, "^[A-Za-z0-9,\\s]$"))
                {
                    return null;
                }

                if (Type == "number")
                {

                }
                else if(Type == "string")
                {
                    Value = $"N'{Value}'";
                }
                else
                {
                    Value = $"'{Value}'";
                }
                if (Regex.IsMatch(Operator, "^[><=]+$"))
                {
                    return $"{Prop} {Operator} {Value}";
                }
                if (Operator.Equals("in", StringComparison.CurrentCultureIgnoreCase))
                {
                    return $"{Prop} {Operator} ({Value})";
                }


                return null;

            }
        }
    }
}
