using System;

namespace Prime.Models.Api
{
    public class EnrolleeStub : IUserBoundModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }
}
