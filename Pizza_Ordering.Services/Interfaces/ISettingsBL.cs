using Pizza_Ordering.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.Interfaces
{
    public interface ISettingsBL
    {
        SettingsDto GetSettings();

        void UpdateSettings(SettingEditDto settings);
    }
}
