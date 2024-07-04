namespace Domain.Entities;

public class Answer: AggregateRoot
{
    public Answer(Guid questionId, int order, string text, bool isCorrect)
    {
        QuestionId = questionId;
        Order = order;
        Text = text;
        IsCorrect = isCorrect;
    }

    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int Order { get; set; }

    public Guid QuestionId { get; private set; }
    public Question Question { get; private set; }
    
    public void Modify(int order, string text, bool isCorrect)
    {
        Order = order;
        Text = text;
        IsCorrect = isCorrect;
    }
}