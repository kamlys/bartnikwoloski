using bartnikwolski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bartnikwolski.ViewModels.Admin
{
    public class IndexViewModel
    {
        public IList<Page> PageList { get; set; }
    }
}