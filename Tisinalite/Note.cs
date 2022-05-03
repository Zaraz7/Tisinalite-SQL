using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisinalite
{
    public class Note
    {
        public string Title { get; set; }
        private string _dir;
        public string Dir
        {
            get { return _dir; }
            set { _dir = value; }
        }
    }
}
