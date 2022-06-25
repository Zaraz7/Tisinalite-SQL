using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryPassword
{
    public class PasswordTest
    {
        // Проверка на сложность и доступность пароля
        public static bool PasswordHardCorrect(string password)
        {
            if (password.Length < 8 || password.Length > 20)
                return false;
            if (!password.Any(Char.IsDigit))
                return false;
            // Если нет этих символов проверка также проваливается
            if (password.Intersect("@#$%-_&*").Count() == 0)
                return false;
            return true;
        }
    }
}
