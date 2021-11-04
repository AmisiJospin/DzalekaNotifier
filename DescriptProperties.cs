using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class DescriptProperties
    {
        public string WhotheyAre { get; set; }
        public string WhatTheyAre { get; set; }
        public string Location { get; set; }

        public DescriptProperties(string whoTheyare, string whatTheyAre, string WhereTheyAre)
        {
            WhotheyAre = whatTheyAre;
            WhatTheyAre = whatTheyAre;
            Location = WhereTheyAre;

        }

        public DescriptProperties()
        {

        }

    }
}
