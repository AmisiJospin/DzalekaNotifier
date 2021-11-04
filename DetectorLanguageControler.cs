using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class DetectorLanguageControler
    {
        public class Result
        {
            public string language_code { get; set; }
            public string language_name { get; set; }
            public double probability { get; set; }
            public double percentage {

                get; set;
            }

            public bool reliable_result { get; set; }
        }

        public class RootObject
        {
            public bool success { get; set; }
            public List<Result> results { get; set; }
        }
    }
}
