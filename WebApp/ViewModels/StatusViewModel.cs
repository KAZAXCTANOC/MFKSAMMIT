using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class StatusViewModel
    {
        public StatusViewModel()
        {
            StatusList = new List<Status>()
            {
                new Status{Name = "Завершено"},
                new Status{Name = "Начато"},
                new Status{Name = "Ожидает"},
                new Status{Name = "Просрочено"}
            };
        }
        public List<Status> StatusList { get; set; }
    }
}
