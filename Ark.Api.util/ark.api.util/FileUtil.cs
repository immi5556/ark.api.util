using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ark.net.util
{
    public class FileUtil
    {
        /// <summary>
        /// Wrapper fro saving file, with additional features
        /// 1. creates directory if not exist
        /// 2. appends number sequence to file rather overwrting (ex: filename_1, _2 etc)
        /// </summary>
        /// <param name="path">path to be written</param>
        /// <param name="content">file content</param>
        /// <returns>returns the file path, new if seqience generated otherwise the same as input</returns>
        public static string Save(string path, string content)
        {
            CreateIfNotExistDirectory(System.IO.Path.GetDirectoryName(path));
            var fil = CreateFileSequence(path);
            System.IO.File.WriteAllText(fil, content);
            return fil.ToString();
        }
        public static string Save(string path, byte[] content)
        {
            CreateIfNotExistDirectory(System.IO.Path.GetDirectoryName(path));
            var fil = CreateFileSequence(path);
            System.IO.File.WriteAllBytes(fil, content);
            return fil.ToString();
        }
        public static string CreateIfNotExistDirectory(string dirpath)
        {
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            return dirpath;
        }
        public static string CreateFileSequence(string fullfilepath, int? seq = null)
        {
            string i = seq.HasValue ? "_" + seq.ToString() : "";
            string fpath1 = Path.Combine(Path.GetDirectoryName(fullfilepath), Path.GetFileNameWithoutExtension(fullfilepath) + i.ToString() + Path.GetExtension(fullfilepath));
            if (!File.Exists(fpath1))
            {
                return fpath1;
            }
            else
            {
                seq = seq.HasValue ? seq.Value : 0;
                return CreateFileSequence(fullfilepath, ++seq);
            }
        }
        public static string AppendToFileName(string fullfilepath, string toappend, string extn = null)
        {
            return Path.Combine(Path.GetDirectoryName(fullfilepath), Path.GetFileNameWithoutExtension(fullfilepath) + "_" + toappend + (extn ?? Path.GetExtension(fullfilepath)));
        }
        public static string RemoveInvalidCharsFromFilename(string file_name)
        {
            string finalString = string.Empty;
            if (!string.IsNullOrEmpty(file_name))
            {
                return string.Concat(file_name.Split(Path.GetInvalidFileNameChars()));
            }
            return finalString;
        }
        //return  Path.InvalidPathChars .Combine(Path.GetDirectoryName(fullfilepath), Path.GetFileNameWithoutExtension(fullfilepath) + "_" + toappend + (extn ?? Path.GetExtension(fullfilepath)));
        static readonly Regex removeInvalidChars = new Regex($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]",
            RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
        public static string SanitizeFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return filename;
            return removeInvalidChars.Replace(filename, "_");
        }
    }
}
