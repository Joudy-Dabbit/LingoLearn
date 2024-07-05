namespace Domain.Entities;

public class Reply: AggregateRoot

{
    public Reply(string text, Guid studentId, Guid commentId)
    {
        Text = text;
        StudentId = studentId;
        CommentId = commentId;
    }

    public string Text { get; set; }
    
    public Guid CommentId { get; private set; }
    public Comment Comment { get; private set; }    
    
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; }    
}