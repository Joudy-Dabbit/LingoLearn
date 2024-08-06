namespace Domain.Entities;

public class ChallengeQuestion : AggregateRoot
{
    public ChallengeQuestion(Guid challengeId, int order,
        string text, bool isMultiChoices)
    {
        ChallengeId = challengeId;
        Order = order;
        Text = text;
        IsMultiChoices = isMultiChoices;
    }

    public string Text { get; set; }
    public int Order { get; set; }
    public bool IsMultiChoices { get; set; }

    public Guid ChallengeId { get; private set; }
    public Challenge Challenge { get; private set; }
    
    
    private readonly List<ChallengeAnswer> _answers = new();
    public IReadOnlyCollection<ChallengeAnswer> Answers => _answers.AsReadOnly();
    
    
    public void AddAnswer(int order, string text, bool isCorrect)
    {
        _answers.Add(new ChallengeAnswer(Id, order, text, isCorrect));
    }
    
    public void ClearAnswer()
    {
        _answers.Clear();
    }

    public void Modify(int order, string text, bool isMultiChoices)
    {
        Order = order;
        Text = text;
        IsMultiChoices = isMultiChoices;
    }
}