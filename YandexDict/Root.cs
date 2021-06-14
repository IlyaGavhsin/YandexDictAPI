using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexDict
{

    public class Head
    {
    }

    public class Syn
    {
        public string text { get; set; }
        public string pos { get; set; }
        public int fr { get; set; }
    }

    public class Tr
    {
        public string text { get; set; }
        public string pos { get; set; }
        public int fr { get; set; }
        public List<Syn> syn { get; set; }
    }

    public class Def
    {
        public string text { get; set; }
        public string pos { get; set; }
        public List<Tr> tr { get; set; }
    }

    public class Root
    {
        public Head head { get; set; }
        public List<Def> def { get; set; }
    }

}
