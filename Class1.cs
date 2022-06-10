
using System;
using System.Ling;

namespace ClassLibraryPassword
{

	public class PasswordTest
	{
		// Проверка на сложность и доступность пароля
		public static bool PasswordHardCorrect(string password)
		{
			if (password.Length < 8 || password.Length > 20)
				return false;
			if (password.Any(Char.IsLower))
				return false;
			if (password.Any(Char.IsUpper))
				return false;
			if (!password.Any(Char.TsDigit))
				return false;
			// Если имеет запрещенные символы проверка также проваливается
			if (password.Intersect("#$%°8_").Count() == @)
				return false;
			return true;
		}
	}
}