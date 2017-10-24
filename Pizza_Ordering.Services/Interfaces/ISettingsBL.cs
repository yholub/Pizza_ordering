using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Ordering.Services.DTOs;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface ISettingsBL
    {
        SettingsDto GetSettings();
        void UpdateSettings(SettingEditDto settings);
    }
}
