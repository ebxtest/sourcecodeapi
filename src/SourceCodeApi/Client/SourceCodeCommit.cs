namespace SourceCodeApi.Client;

public class SourceCodeCommit
{
    public string Id { get; set; }
    public string Message { get; set; }
    public string AuthorName { get; set; }
    public DateTime TimeStamp { get; set; }
}