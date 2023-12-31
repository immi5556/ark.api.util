using System.IO;
using System.Xml.Linq;

namespace ark.net.util
{
    public class FileUtil
    {
        public static void Save(string path, string content)
        {
            CreateIfNotExistDirectory(System.IO.Path.GetDirectoryName(path));
            var fil = CreateFileSequence(path);
            System.IO.File.WriteAllText(fil, content);
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
                return CreateFileSequence(fullfilepath, ++seq);
            }
        }
        public static string AppendToFileName(string fullfilepath, string toappend, string extn = null)
        {
            return Path.Combine(Path.GetDirectoryName(fullfilepath), Path.GetFileNameWithoutExtension(fullfilepath) + "_" + toappend + (extn ?? Path.GetExtension(fullfilepath)));
        }
    }
}
