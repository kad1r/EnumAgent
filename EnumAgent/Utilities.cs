using EnumAgent.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace EnumAgent
{
	public static class Utilities
	{
		public static string GetDisplayName(Type type, int value)
		{
			var output = string.Empty;
			var name = Enum.GetName(type, value);
			var member = type.GetMember(name)[0];
			var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);

			if (attributes != null && attributes.Count() > 0)
			{
				output = ((DisplayAttribute)attributes[0]).Name;

				if (((DisplayAttribute)attributes[0]).ResourceType != null)
				{
					output = ((DisplayAttribute)attributes[0]).GetName();
				}
			}

			return output;
		}

		public static string GetDescription(Type type, int value)
		{
			var output = string.Empty;
			var name = Enum.GetName(type, value);
			var member = type.GetMember(name)[0];
			var attributes = member.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Count() > 0)
			{
				output = ((DescriptionAttribute)attributes[0]).Description;
			}

			return output;
		}

		public static int GetValue(Enum _enum)
		{
			return Convert.ToInt32(_enum, CultureInfo.InvariantCulture.NumberFormat);
		}

		public static T Parse<T>(string enumtext) where T : struct
		{
			if (IsDefined<T>(enumtext))
			{
				T pm = (T)Enum.Parse(typeof(T), enumtext, true);

				return pm;
			}

			return (T)Enum.ToObject(typeof(T), 0);
		}

		public static bool IsDefined<T>(string enumtext) where T : struct
		{
			return Enum.GetNames(typeof(T)).Contains(enumtext);
		}

		public static bool IsDefined<T>(int value)
		{
			var vals = (int[])Enum.GetValues(typeof(T));

			foreach (int i in vals)
			{
				if (value == i)
				{
					return true;
				}
			}

			return false;
		}

		public static Dictionary<int, string> GetNamesWithValues(Enum _enum)
		{
			var list = new Dictionary<int, string>();
			var type = _enum.GetType();
			var values = Enum.GetValues(type);

			foreach (var value in values)
			{
				var field = type.GetField(value.ToString());
				var attrs = field.GetCustomAttributes(typeof(DisplayAttribute), true);

				if (attrs != null && attrs.Count() > 0)
				{
					foreach (DisplayAttribute a in attrs)
					{
						list.Add((int)value, a.Name);
					}
				}
				else
				{
					list.Add((int)value, field.Name);
				}
			}

			return list;
		}

		public static Dictionary<int, string> GetDescriptionWithValues(Type type)
		{
			var list = new Dictionary<int, string>();
			var values = Enum.GetValues(type);

			foreach (var value in values)
			{
				var field = type.GetField(value.ToString());
				var f = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

				foreach (DescriptionAttribute a in f)
				{
					list.Add((int)value, a.Description);
				}
			}

			return list;
		}

		public static List<SelectListItem> ConvertToSelectListItem(Type type)
		{
			var list = new List<SelectListItem>();

			foreach (var _enum in Enum.GetValues(type))
			{
				list.Add(new SelectListItem
				{
					Text = GetDescription(type, (int)_enum),
					Value = _enum.ToString()
				});
			}

			return list;
		}
	}
}
