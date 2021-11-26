namespace AUA.ProjectName.Common.Consts
{
    public static class AppConsts
    {
        public const string AppName = "AUA Frameworke";
  
        public const string AppDllName = "AUA.ProjectName.WebApi";

        public const string ConnectionStrings = "ConnectionStrings";

        public const string AuthorizationAccessTokenName = "AuthorizationToken";

        public const string EfDbConnection = "EntityFrameWorkConnection";

        public const string DapperDbConnection = "DapperConnection";
        
        public const string AppSettingsFileName = "appsettings.json";

        public static string AppSettingsFilePath => $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

        public const string StringDataTypeName = "string";

        public const string CookieName = ".AUA.Session";
     
        public const int MinimumAccessTokenLength = 200;

        public const int MinimumRefreshTokenLength = 30;

        public const int DefaultPageSize = 10;

        public const int DefaultPageNumber = 0;

        public static IEnumerable<int> NationCodeLength => new[] { 10, 11 };

        public static string[] SplitDateTimeChar => new[] { "/", "\\", ":", "-"," " };

        public static string SeriLogConfigFilePath = Directory.GetCurrentDirectory() + "/ConfigFiles";

        public static string SerilogConfigFileName = "Serilog.json";

        public static string LogSplitter = " >> ";

        public static string ExceptionLogMessage = "Exception Serialize: exceptionId =";


    }
}
