using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookingApp
{
  internal class ValidationClass
  {
    public static bool CheckLoginExist(string login)
    {
      return SourceCore.MyBase.CLIENTS.FirstOrDefault(lg => lg.LOGIN == login) == null;
    }

    public static bool CheckNumberPhone(string phone)
    {
      string pattern = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";
      Regex regex = new Regex(pattern);
      return regex.IsMatch(phone);
    }

    public static bool CheckPassword(string password)
    {
      string pattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,24}$";
      Regex regex = new Regex(pattern);
      return regex.IsMatch(password);
    }

  }
}
