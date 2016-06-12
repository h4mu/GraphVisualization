namespace Common
{
    public interface IConfigProvider
    {
        string DbConnection { get; }
        string DbPass { get; }
        string DbUser { get; }
    }
}