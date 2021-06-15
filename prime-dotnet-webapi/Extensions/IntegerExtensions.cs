namespace Prime.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsInvalidId(this int id)
        {
            return id == -1;
        }
    }
}
