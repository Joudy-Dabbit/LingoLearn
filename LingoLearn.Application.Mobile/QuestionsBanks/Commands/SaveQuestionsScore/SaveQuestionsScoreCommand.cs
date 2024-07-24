using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.QuestionsBanks;

public class SaveQuestionsScoreCommand
{
    public class Request : IRequest<OperationResponse>
    {
        public int Score { get;  set; }
    }
}