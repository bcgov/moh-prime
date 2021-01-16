namespace Prime.Services.Razor
{
    public abstract class RazorTemplate<TModel>
    {
        public abstract string ViewPath { get; }
    }
}
