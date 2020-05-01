using NLog;
using BlogsConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                var blogManager = new BlogManager();
                blogManager.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
            }
            logger.Info("Program ended");
        }
    }
}
