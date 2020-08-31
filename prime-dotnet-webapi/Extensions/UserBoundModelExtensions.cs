using Prime.Models;

namespace Prime
{
    public static class UserBoundModelExtensions
    {
        public static PermissionsRecord PermissionsRecord(this IUserBoundModel model)
        {
            return model == null ? null : new PermissionsRecord { UserId = model.UserId };
        }
    }
}
