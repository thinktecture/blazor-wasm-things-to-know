using System.Runtime.Serialization;

namespace BlazorWasmVirtualization.Shared.DTO
{
    [DataContract]
	public class QueryParameters
	{
		private int _pageSize = 15;

		[DataMember(Order = 1)]
		public int StartIndex { get; set; }

		[DataMember(Order = 2)]
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = value;
			}
		}
	}
}
