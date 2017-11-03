using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.BLs
{
    public class SettingsBL : ISettingsBL
    {
        private static SettingsDto _default;

        static SettingsBL()
        {
            _default = new SettingsDto();
        }

        public DTOs.SettingsDto GetSettings()
        {
            return _default;
        }

        public void UpdateSettings(SettingEditDto settings)
        {
            //_default.Capacity = settings.Capacity;
            //_default.EndHour = settings.EndHour;
            //_default.StartHour = settings.StartHour;
            //for (int i = 0; i < settings.IngState.Count; ++i)
            //{
            //    _default.Locked[settings.IngState[i].Id].IsLocked = settings.IngState[i].IsLocked;
            //}
        }
    }
}
