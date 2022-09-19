using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabiTravel.Shared
{
    public class DateOperation
    {
        public static CultureInfo culture;
        public static string yourCulture = null;


        #region Méthode Yourculture (determiner la region où on se trouve) 
        public static void YourCulture()
        {
            if (yourCulture == null)
            {
                culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            }
            else
            {
                culture = new CultureInfo(yourCulture, false);
            }
        }
        #endregion


        #region Afficher le nom du jour
        public static string GetDayNameWithDate(DateTime date1)
        {
            try
            {
                //YourCulture();
                var cultureInfo = CultureInfo.GetCultureInfo("en-GB");
                var day = cultureInfo.DateTimeFormat.GetDayName(date1.DayOfWeek);
                return day;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw new Exception(err.ToString());
            }

        }
        #endregion


    }

}

