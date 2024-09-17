using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dashboard.Service
{
    public class FilesAndIO
    {
        // System.IO is used for File operations like CRED.

        public class MainMethods
        {
            public void Method()
            {
                string path = "";

                File.Create(path);
                File.Delete(path);
                File.AppendAllLines(path, new List<string>());
                string s = File.ReadAllText(path);
            }
        }
    }
}
