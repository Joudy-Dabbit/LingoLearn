using System.Linq.Expressions;
using Domain.Entities;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Lessons;

public class FinishLessonCommand
{
    public class Request : IRequest<OperationResponse<Response>>  
    {
        public Guid LessonId { get; set; }
    }   
    
    public class Response 
    {
        public bool EarnedLevelCertificate { get; set; }
        public bool EarnedLanguageCertificate { get; set; }
    }
}
