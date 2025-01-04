using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WytSky.Mobile.Maui.Hadrein.Helpers;

namespace System
{
    public static class ExtensionMethods
    {
        public static string FullMessage(this Exception ex)
        {
            if (ex is AggregateException) return (ex as AggregateException).InnerExceptions.Aggregate("[ ", (total, next) => $"{total}[{next.FullMessage()}] ") + "]";
            var msg = ex.Message.Replace(", see inner exception.", "").Trim();
            var innerMsg = ex.InnerException?.FullMessage();
            if (innerMsg is object && innerMsg != msg) msg = $"{msg} [ {innerMsg} ]";
            return msg;
        }
    }

    public static class ExtensionLogMethods
    {
        public static bool LogExtension(string Text, string Date, string PageName, string MethodName)
        {
            try
            {
                //var res = WytSky.Mobile.Maui.Hadrein.APIs.ServiceExceptionLog.SaveNew(new WytSky.Mobile.Maui.Hadrein.Dtos.ExceptionLogDto()
                //{
                //    Data = Date,
                //    Content = Text,
                //    Platform = Device.RuntimePlatform,
                //    Place = $"Page : {PageName} {Environment.NewLine} Method {MethodName}",
                //});

                //Toast.ShowToastError(Text, $"{PageName}{MethodName}");

                return true;
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(" Error : {0} - {1} ", exc.Message, exc.InnerException != null ? exc.InnerException.FullMessage() : ""));
                return false;
            }
        }
        public static bool LogExtension(Exception ex, string Date, string PageName, string MethodName)
        {
            try
            {
                //var res = WytSky.Mobile.Maui.Hadrein.APIs.ServiceExceptionLog.SaveNew(new WytSky.Mobile.Maui.Hadrein.Dtos.ExceptionLogDto()
                //{
                //    Data = Date,
                //    Content = Text,
                //    Platform = Device.RuntimePlatform,
                //    Place = $"Page : {PageName} {Environment.NewLine} Method {MethodName}",
                //});

                // Toast.ShowToastError(ex.Message, $"{PageName}{MethodName}");

                return true;
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(string.Format(" Error : {0} - {1} ", exc.Message, exc.InnerException != null ? exc.InnerException.FullMessage() : ""));
                return false;
            }
        }
    }
}
