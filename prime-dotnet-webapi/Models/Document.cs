using System;

namespace Prime.Models
{
    public class Document
    {
        public Document(string filename, Byte[] data)
        {
            Filename = filename;
            Data = data;
        }
        public string Filename { get; set; }
        public Byte[] Data { get; set; }
    }
}
