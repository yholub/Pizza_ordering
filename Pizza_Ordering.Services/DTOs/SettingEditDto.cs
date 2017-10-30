using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.DTOs
{
    public class IngState
    {
        public int Id { get; set; }

        public bool IsLocked { get; set; }
    }

    public class SettingEditDto
    {
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int Capacity { get; set; }

        public List<IngState> IngState { get; set; }

        public SettingEditDto()
        {
            IngState = new List<IngState>();
        }
    }
}
