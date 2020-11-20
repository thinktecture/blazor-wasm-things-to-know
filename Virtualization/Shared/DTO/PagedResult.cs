using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BlazorWasmVirtualization.Shared.DTO
{
	[DataContract]
	public class PagedResult<T>
	{
		[DataMember(Order = 1)]
		public List<T> Items { get; set; }

		[DataMember(Order = 2)]
		public int TotalSize { get; set; }
	}
}
