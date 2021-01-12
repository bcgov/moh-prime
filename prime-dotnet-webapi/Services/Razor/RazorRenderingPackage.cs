namespace Prime.Services.Razor
{
    public abstract class RazorRenderingPackage<TModel>
    {
        public string ViewPath { get; set; }

        public TModel ViewModel { get; set; }

        public RazorRenderingPackage(string viewPath, TModel viewModel)
        {
            ViewPath = viewPath;
            ViewModel = viewModel;
        }
    }
}
