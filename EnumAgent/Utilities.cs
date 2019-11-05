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
		/// <summary>
		/// Get display attribute of given int value for enum
		/// </summary>
		/// <typeparam name="T">Enum identifier</typeparam>
		/// <param name="value">Value of your enum</param>
		/// <returns>string</returns>
		public static string GetDisplayName<T>(int value) where T : struct
		{
			var output = string.Empty;
			var type = typeof(T);
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

		/// <summary>
		/// Get description attribute name of given int value for enum
		/// </summary>
		/// <typeparam name="T">Enum identifier</typeparam>
		/// <param name="value">Value of your enum</param>
		/// <returns>string</returns>
		public static string GetDescription<T>(int value) where T : struct
		{
			var output = string.Empty;
			var type = typeof(T);
			var name = Enum.GetName(type, value);
			var member = type.GetMember(name)[0];
			var attributes = member.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Count() > 0)
			{
				output = ((DescriptionAttribute)attributes[0]).Description;
			}

			return output;
		}

		/// <summary>
		/// Get int value of given enum
		/// </summary>
		/// <param name="_enum">Enum object</param>
		/// <returns>int</returns>
		public static int GetValue(Enum _enum)
		{
			return Convert.ToInt32(_enum, CultureInfo.InvariantCulture.NumberFormat);
		}

		/// <summary>
		/// Parse given string to Enum
		/// </summary>
		/// <typeparam name="T">Enum identifier</typeparam>
		/// <param name="enumtext">Text of your enum</param>
		/// <returns>Enum</returns>
		public static T Parse<T>(string enumtext) where T : struct
		{
			if (IsDefined<T>(enumtext))
			{
				T pm = (T)Enum.Parse(typeof(T), enumtext, true);

				return pm;
			}

			return (T)Enum.ToObject(typeof(T), 0);
		}

		/// <summary>
		/// Checks if given string is defined in your Enum
		/// </summary>
		/// <typeparam name="T">Enum identifier</typeparam>
		/// <param name="enumtext">Text of your enum</param>
		/// <returns>bool</returns>
		public static bool IsDefined<T>(string enumtext) where T : struct
		{
			return Enum.GetNames(typeof(T)).Contains(enumtext);
		}

		/// <summary>
		/// Checks if given int is defined in your Enum
		/// </summary>
		/// <typeparam name="T">Enum identifier</typeparam>
		/// <param name="value">Some integer value</param>
		/// <returns>bool</returns>
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

		/// <summary>
		/// Get display attributes of the given Enum type, takes name if there is no display attribute
		/// </summary>
		/// <param name="type">Enum type</param>
		/// <returns>Dictionary</returns>
		public static Dictionary<int, string> GetNamesWithValues(Type type)
		{
			var list = new Dictionary<int, string>();
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

		/// <summary>
		/// Get description attributes of the given Enum type. Takes name if there is no description attribute
		/// </summary>
		/// <param name="type">Enum type</param>
		/// <returns>Dictionary</returns>
		public static Dictionary<int, string> GetDescriptionWithValues(Type type)
		{
			var list = new Dictionary<int, string>();
			var values = Enum.GetValues(type);

			foreach (var value in values)
			{
				var field = type.GetField(value.ToString());
				var attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);

				if (attrs != null && attrs.Count() > 0)
				{
					foreach (DescriptionAttribute a in attrs)
					{
						list.Add((int)value, a.Description);
					}
				}
				else
				{
					list.Add((int)value, field.Name);
				}
			}

			return list;
		}

		/// <summary>
		/// Converts Enum object to SelectListItem. You can cast return value to MVC SelectListItem
		/// </summary>
		/// <typeparam name="T">Enum identifier</typeparam>
		/// <returns>List</returns>
		public static List<SelectListItem> ConvertToSelectListItem<T>() where T : struct
		{
			var list = new List<SelectListItem>();
			var type = typeof(T);

			foreach (var _enum in Enum.GetValues(type))
			{
				list.Add(new SelectListItem
				{
					Text = GetDescription<T>((int)_enum),
					Value = _enum.ToString()
				});
			}

			return list;
		}
	}
}
