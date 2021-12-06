namespace Fetch.Chat.Domain
{
    public record Message(Guid Id, string User, string Content);
}