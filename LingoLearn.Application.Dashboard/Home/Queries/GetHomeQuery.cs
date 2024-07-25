using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard;

public class GetHomeQuery
{
    public class Request: IRequest<OperationResponse<Response>>
    {
        public int Year { get; set; }
    }
    public class Response 
    {
        public int StudentCount { get; set; }
        public int LanguageCount { get; set; }
        public int LessonCount { get; set; }
        public int LevelCount { get; set; }
        public int AdvertisementCount { get; set; }
        public int ChallengeCount { get; set; }
        public int QuestionCount { get; set; }
        public int AnswerCount { get; set; }
        public List<HomeInfoRes> BestLanguages { get; set; }
        public List<HomeInfoRes> BestStudents { get; set; }
        public List<int> StudentCountMonthly { get; set; }
        public List<int> LanguageCountMonthly { get; set; }
        public List<int> LessonCountMonthly { get; set; }
        public List<int> AdvertisementCountMonthly { get; set; }

        public class HomeInfoRes
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}