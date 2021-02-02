namespace Prime.Models.Documents
{
    /// <summary>
    /// An abstraction of a document in memory, not specific to a document upload
    /// </summary>
    public class File
    {
        public string Filename { get; set; }
        public byte[] Data { get; set; }
        public string MediaType { get; set; }

        public File(string filename, byte[] data, string mediaType = null)
        {
            Filename = filename;
            Data = data;
            MediaType = mediaType;
        }
    }

    public class Pdf : File
    {
        public Pdf(string filename, byte[] data)
            : base(filename, data, "application/pdf")
        { }
    }
}
