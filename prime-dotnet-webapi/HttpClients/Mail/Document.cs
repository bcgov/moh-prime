namespace Prime.HttpClients.Mail
{
    public class Document
    {
        public string Filename { get; set; }
        public byte[] Data { get; set; }
        public string MediaType { get; set; }

        public Document(string filename, byte[] data, string mediaType)
        {
            Filename = filename;
            Data = data;
            MediaType = mediaType;
        }
    }

    public class Pdf : Document
    {
        public Pdf(string filename, byte[] data)
            : base(filename, data, "application/pdf")
        { }
    }
}
