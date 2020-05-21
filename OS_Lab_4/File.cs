using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Lab_4
{
    public class File : AbstractFile
    {
        public int Size;

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
