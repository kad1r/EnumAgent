using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test.Enums
{
	public enum CacheEnum
	{
		[Display(Name = "Memory Cache")]
		MemCache = 1,

		[Description("Http Cache")]
		HttpCache = 2,

		Session = 3
	}
}
