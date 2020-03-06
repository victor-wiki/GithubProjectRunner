﻿using System;

namespace GithubProjectHandler
{
    public class ExceptionHelper
    {
        public static string GetExceptionDetails(Exception ex)
        {
            while (ex.InnerException != null)
            {
                return GetExceptionDetails(ex.InnerException);
            }
            return ex.Message + Environment.NewLine + ex.StackTrace;
        }
    }
}
