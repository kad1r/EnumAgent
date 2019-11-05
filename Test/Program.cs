using EnumAgent;
using System;
using Test.Enums;

namespace Test
{
	public class Program
	{
		public static void Main()
		{
			Console.WriteLine("Initialized.");

			var sessionValue = Utilities.GetValue(CacheEnum.Session);
			var isDefined = Utilities.IsDefined<CacheEnum>("HttpCache");
			var cacheNames = Utilities.GetNamesWithValues(typeof(CacheEnum));
			var cacheDescriptions = Utilities.GetDescriptionWithValues(typeof(CacheEnum));
			var parse = Utilities.Parse<CacheEnum>("Session");
			var displayName = Utilities.GetDisplayName<CacheEnum>((int)CacheEnum.MemCache);
			var description = Utilities.GetDescription<CacheEnum>((int)CacheEnum.HttpCache);
			var list = Utilities.ConvertToSelectListItem<CacheEnum>();

			Console.WriteLine("MemCache DisplayName -> " + displayName);
			Console.WriteLine("HttpCache Description -> " + description);

			foreach (var item in list)
			{
				Console.WriteLine(item.Value);
			}

			Console.ReadKey();
		}
	}
}
