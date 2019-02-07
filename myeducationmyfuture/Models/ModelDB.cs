using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myeducationmyfuture.Models;

namespace myeducationmyfuture.Models
{
    public class ModelDB
    {
        public List<Blog> blogList { get; set; }

        public Blog news { get; set; }
        public int Index { get; set; }

    }
}