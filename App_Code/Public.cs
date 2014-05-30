using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;

public static class Public
{
    #region Convertion Methods

    public static short ToShort(object input)
    {
        if (input == null)
        {
            return 0;
        }
        short result = 0;
        short.TryParse(input.ToString(), out result);
        return result;
    }

    public static int ToInt(object input)
    {
        if (input == null)
        {
            return 0;
        }
        int result = 0;
        int.TryParse(input.ToString(), out result);
        return result;
    }

    public static long ToLong(object input)
    {
        if (input == null)
        {
            return 0;
        }
        long result = 0;
        long.TryParse(input.ToString(), out result);
        return result;
    }

    public static decimal ToDecimal(object input)
    {
        if (input == null)
        {
            return 0;
        }
        decimal result = 0;
        decimal.TryParse(input.ToString(), out result);
        return result;
    }

    public static byte ToByte(object input)
    {
        if (input == null)
        {
            return 0;
        }
        byte result = 0;
        byte.TryParse(input.ToString(), out result);
        return result;
    }

    public static bool ToBool(object input)
    {
        bool result = false;
        bool.TryParse(input.ToString(), out result);
        return result;
    }

    public static string ToHex(object input)
    {
        return string.Format("{0:X}", input);
    }

    public static double ToDouble(object input)
    {
        if (input == null)
        {
            return 0;
        }
        double result = 0;
        double.TryParse(input.ToString(), out result);
        return result;
    }

    public static string ToSeparatedPersianDate(object date)
    {
        string dt = date as string;
        if (!string.IsNullOrEmpty(dt))
        {
            return string.Concat(dt.Substring(0, 4), DateTimeFormatInfo.CurrentInfo.DateSeparator, dt.Substring(4, 2), DateTimeFormatInfo.CurrentInfo.DateSeparator, dt.Substring(6, 2));
        }
        return null;
    }

    public static string ToPersianDate(object date)
    {
        string result = null;
        if (date != null)
        {
            DateTime dt = (DateTime)date;
            PersianCalendar objPersianCalendar = new PersianCalendar();
            int year = objPersianCalendar.GetYear(dt);
            int month = objPersianCalendar.GetMonth(dt);
            int day = objPersianCalendar.GetDayOfMonth(dt);
            int hour = objPersianCalendar.GetHour(dt);
            int min = objPersianCalendar.GetMinute(dt);
            int sec = objPersianCalendar.GetSecond(dt);
            result = string.Concat(year.ToString().PadLeft(4, '0'), DateTimeFormatInfo.CurrentInfo.DateSeparator, month.ToString().PadLeft(2, '0'), DateTimeFormatInfo.CurrentInfo.DateSeparator, day.ToString().PadLeft(2, '0'));
        }
        return result;
    }

    public static string ToPersianDateTime(object date)
    {
        string result = null;
        if (date != null)
        {
            DateTime dt = (DateTime)date;
            PersianCalendar objPersianCalendar = new PersianCalendar();
            int year = objPersianCalendar.GetYear(dt);
            int month = objPersianCalendar.GetMonth(dt);
            int day = objPersianCalendar.GetDayOfMonth(dt);
            int hour = objPersianCalendar.GetHour(dt);
            int min = objPersianCalendar.GetMinute(dt);
            int sec = objPersianCalendar.GetSecond(dt);
            result = string.Concat(year.ToString().PadLeft(4, '0'), DateTimeFormatInfo.CurrentInfo.DateSeparator, month.ToString().PadLeft(2, '0'), DateTimeFormatInfo.CurrentInfo.DateSeparator, day.ToString().PadLeft(2, '0'), " ", hour.ToString().PadLeft(2, '0'), DateTimeFormatInfo.CurrentInfo.TimeSeparator, min.ToString().PadLeft(2, '0'));
        }
        return result;
    }

    public static string GetDate(DateTime date)
    {
        DateTime dt = (DateTime)date;
        PersianCalendar objPersianCalendar = new PersianCalendar();
        int year = objPersianCalendar.GetYear(dt);
        int month = objPersianCalendar.GetMonth(dt);
        int day = objPersianCalendar.GetDayOfMonth(dt);
        return string.Concat(year.ToString().PadLeft(4, '0'), month.ToString().PadLeft(2, '0'), day.ToString().PadLeft(2, '0'));
    }

    public static string GetTime(DateTime date)
    {
        return string.Concat(date.Hour.ToString().PadLeft(2, '0'), date.Minute.ToString().PadLeft(2, '0'));
    }

    public static string MoneyToString(object input)
    {
        string result = "0";
        try
        {
            result = Convert.ToInt64(input).ToString();
        }
        catch
        {
        }
        return result;
    }

    #endregion

    #region BMIService

    public static string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
    public static string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
    public static string UserName = ConfigurationManager.AppSettings["UserName"];
    public static string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

    public static string CurrentDateForBMI
    {
        get
        {
            return string.Concat(DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'));
        }
    }

    public static string CurrentTimeForBMI
    {
        get
        {
            return string.Concat(DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
        }
    }

    #endregion

    #region Log

    public static void Log(string log)
    {
        PersianCalendar objPersianCalendar = new PersianCalendar();
        int year = objPersianCalendar.GetYear(DateTime.Now);
        int month = objPersianCalendar.GetMonth(DateTime.Now);
        int day = objPersianCalendar.GetDayOfMonth(DateTime.Now);
        int hour = objPersianCalendar.GetHour(DateTime.Now);
        int min = objPersianCalendar.GetMinute(DateTime.Now);
        StreamWriter objStreamWriter = File.AppendText(HttpContext.Current.Server.MapPath("~/log.txt"));
        lock (objStreamWriter)
        {
            objStreamWriter.WriteLine(string.Concat("Log Date : ", string.Concat(year.ToString(), "/", month.ToString(), "/", day.ToString(), " ", hour.ToString(), ":", min.ToString().PadLeft(2, '0'))));
            objStreamWriter.WriteLine(log);
            objStreamWriter.WriteLine("--------------------------------------------");
            objStreamWriter.Close();
        }
    }

    public static void Log(Exception ex)
    {
        Log(string.Format("Exception Type : {0}\n Message : {1} \n Source : {2} \n InnerException : {3} \n StackTrace : {4} \n TargetSite : {5}", ex.GetType(), ex.Message, ex.Source, ex.InnerException, ex.StackTrace, ex.TargetSite));
    }

    #endregion
}

