namespace Domain.Entities;

public class Question : AggregateRoot
{
    public Question(Guid levelId, int order, string text)
    {
        LevelId = levelId;
        Order = order;
        Text = text;
    }

    public string Text { get; set; }
    public int Order { get; set; }

    public Guid LevelId { get; private set; }
    public Level Level { get; private set; }
    
    
    private readonly List<Answer> _answers = new();
    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();
    
    public void AddAnswer(int order, string text, bool isCorrect)
    {
        _answers.Add(new Answer(Id, order, text, isCorrect));
    }
    
    public void ClearAnswer()
    {
        _answers.Clear();
    }

    public void Modify(int order, string text)
    {
        Order = order;
        Text = text;
    }
}