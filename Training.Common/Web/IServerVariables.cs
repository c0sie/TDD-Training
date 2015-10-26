namespace Training.Common.Web
{
    public interface IServerVariables
    {
        string ClientIP { get; }

        string Port { get; }

        string Protocol { get; }

        string ServerName { get; }

        string Host { get; }
    }
}
