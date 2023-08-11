namespace Application.Common.Abstraction
{
    public interface IDocChangeLogger
    {
        Task Log(Guid DocId, string action);
    }
}
