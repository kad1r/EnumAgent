namespace EnumAgent
{
	public static class Helper
	{
		public static string replaceTurkishChars(string text)
		{
			var oldValue = new char[] { 'ö', 'Ö', 'ü', 'Ü', 'ç', 'Ç', 'İ', 'ı', 'Ğ', 'ğ', 'Ş', 'ş' };
			var newValue = new char[] { 'o', 'O', 'u', 'U', 'c', 'C', 'I', 'i', 'G', 'g', 'S', 's' };

			for (int i = 0; i < oldValue.Length; i++)
			{
				text = text.Replace(oldValue[i], newValue[i]);
				text = text.Replace(" ", "");
			}

			var a = text.Substring(0, 1);
			text = a.ToUpper() + text.Substring(1, text.Length - 1).ToLower();

			return text.Trim();
		}
	}
}
