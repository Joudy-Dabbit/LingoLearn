using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Microsoft.OpenApi.Extensions;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Dashboard.Languages;

public class GetAllAvailableLanguagesQuery
{
    public class Request : IRequest<OperationResponse<List<Response>>>
    {

    }

    public class Response
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}