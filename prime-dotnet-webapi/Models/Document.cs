using System;

namespace Prime.Models
{
    public class Document
    {
        public string Filename { get; set; }
        public Byte[] Data { get; set; }

        public Document(string filename, Byte[] data)
        {
            Filename = filename;
            Data = data;
        }
    }
}
