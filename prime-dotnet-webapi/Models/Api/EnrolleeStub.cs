using System;

namespace Prime.Models.Api
{
    public class EnrolleeStub : IUserBoundModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        /// <summary>
        /// e.g. "gtcochh2vajdtodkby27kspv554dn4is@bcsc"
        /// </summary>
        public string Username { get; set; }
    }
}
