using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ark.net.util
{
    public class Logic
    {
        Func<DateTime?, string> GetCurrentTimeStamp = (DateTime? date) => (DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString());
        public List<dynamic> PersistImage(Microsoft.AspNetCore.Http.IFormFileCollection files)
        {
            if (files == null || files.Count == 0) throw new ApplicationException("no files send to upload");
            List<dynamic> lst = new List<dynamic>();
            using (var scope = ServiceActivator.GetScope())
            {
                UploadConfig uplConfig = (UploadConfig)scope.ServiceProvider.GetService(typeof(UploadConfig));
                string dirpath = GetDirectory(System.IO.Path.Combine(uplConfig.FullPath, GetCurrentTimeStamp(null)));
                files.ToList().ForEach(v =>
                {
                    var filepath = GetFile(System.IO.Path.Combine(dirpath, v.FileName));
                    using (var stream = new System.IO.FileStream(filepath, System.IO.FileMode.Create))
                        v.CopyTo(stream);
                    lst.Add(new
                    {
                        FileName = System.IO.Path.GetFileName(filepath),
                        DownloadUrl = ""
                    }); ;
                });
            }
            return lst;
        }
        public string GetDirectory(string dirpath)
        {
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            return dirpath;
        }
        public string GetFile(string fullfilepath, int? seq = null)
        {
            string i = seq.HasValue ? "_" + seq.ToString() : "";
            string fpath1 = Path.Combine(Path.GetDirectoryName(fullfilepath), Path.GetFileNameWithoutExtension(fullfilepath) + i.ToString() + Path.GetExtension(fullfilepath));
            if (!File.Exists(fpath1))
            {
                return fpath1;
            }
            else
            {
                return GetFile(fullfilepath, ++seq);
            }
        }
    }
}
