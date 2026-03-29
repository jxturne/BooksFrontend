using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BooksFrontend
{
    class DomainBook
    {
        public class Book
        {
           
            public int ISBN { get; set; }

            public string Name { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }

        }
    }
}

