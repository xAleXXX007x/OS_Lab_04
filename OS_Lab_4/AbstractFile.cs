using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Lab_4
{
    public abstract class AbstractFile
    {
        public int Id;
        public string Name;
        public Cluster Cluster;

        public AbstractFile()
        {

        }
    }
}
