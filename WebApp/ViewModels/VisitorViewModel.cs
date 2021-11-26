using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class VisitorViewModel
    {
        public List<SelectListItem> Visitors { get; set; }
        public int[] VisitorsId { get; set; }
        public int EventId { get; set; }
    }
}
