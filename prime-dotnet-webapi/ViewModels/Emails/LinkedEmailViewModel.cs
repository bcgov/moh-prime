namespace Prime.ViewModels.Emails
{
    public class LinkedEmailViewModel
    {
        public string Url { get; set; }

        public LinkedEmailViewModel(string url)
        {
            Url = url;
        }
    }
}
