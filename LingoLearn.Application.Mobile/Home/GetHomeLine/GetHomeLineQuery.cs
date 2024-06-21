using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Enum;
using Neptunee.BaseCleanArchitecture.OResponse;
using Neptunee.BaseCleanArchitecture.Requests;

namespace LingoLearn.Application.Mobile.Home;

public class GetHomeLineQuery
{
    public class Request : IRequest<OperationResponse<Response>>
    {

    }

    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<LevelsRes> Levels { get; set; }

        public class LevelsRes
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
            public string Description { get; set; }
            public List<LessonsRes> Lessons { get; set; }
        }
        public class LessonsRes
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
            public string Description { get; set; }
            public string? FileUrl { get; set; }
            public LessonType Type { get; set; }
            public bool IsDone { get; set; }
        }

        public static Expression<Func<StudentLanguage, Response>> Selector
            => s => new Response()
            {
                Id = s.Id,
                Name = s.Language.Name.ToString(),
                ImageUrl = s.Language.ImageUrl,
                Levels = s.Language.Levels.Select(v => new LevelsRes()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    Order = v.Order,
                    Lessons =  v.Lessons.Select(le => new LessonsRes()
                    {
                        Id = le.Id,
                        Name = le.Name,
                        Description = le.Description,
                        Order = le.Order,
                        Type = le.Type,
                        FileUrl = le.FileUrl
                    }).ToList()
                }).ToList()
            };
    }
}