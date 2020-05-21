using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Lab_4
{
    public class Directory : AbstractFile
    {
        public int Size;
        public List<AbstractFile> Content;

        public Directory(string name)
        {
            Name = name;
            Content = new List<AbstractFile>();
        }
    }
}
