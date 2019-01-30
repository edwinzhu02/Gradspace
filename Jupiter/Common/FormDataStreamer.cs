using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Jupiter.Common
{
    public class FormDataStreamer : MultipartFormDataStreamProvider
    {
        public FormDataStreamer(string rootPath) : base(rootPath) { }
        public FormDataStreamer(string rootPath, int bufferSize) : base(rootPath, bufferSize) { }
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var srcFileName = headers.ContentDisposition.FileName.Replace("\"", "");
            return Guid.NewGuid() + Path.GetExtension(srcFileName);
        }
    }
}