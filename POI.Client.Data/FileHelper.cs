using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace POI.Client.Data
{
    public static class FileHelper
    {
        private const int DefaultBufferSize = 4096;
        
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static Task<string[]> ReadAllLinesAsync(string path)
        {
            return ReadAllLinesAsync(path, Encoding.UTF8);
        }

        public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding)
        {
            var lines = new List<string>();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        public static async Task<string> ReadAllTextAsync(string path, Encoding encoding)
        {
            var sb = new StringBuilder();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    sb.Append(line);
                }
            }
            return sb.ToString();
        }

        public static async Task WriteAllTextAsync(string path, string content, Encoding encoding)
        {
            var buffer = encoding.GetBytes(content);

            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, 
                FileShare.None, buffer.Length, true))
            {
                await fs.WriteAsync(buffer, 0, buffer.Length);
            }

        }
    }
}
