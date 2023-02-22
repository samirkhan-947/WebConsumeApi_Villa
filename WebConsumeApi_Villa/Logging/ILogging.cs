namespace WebConsumeApi_Villa.Logging
{
    public interface ILogging
    {
        void Log(string message,string type);
        void LogWithColor(string message, string type);

    }
}
