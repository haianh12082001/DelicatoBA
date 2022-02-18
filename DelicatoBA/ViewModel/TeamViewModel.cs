using DelicatoBA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelicatoBA.ViewModel
{
    public class ListTeamViewModel
    {
        public PagedList.IPagedList<Team> Teams { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}