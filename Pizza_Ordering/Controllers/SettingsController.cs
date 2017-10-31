using Pizza_Ordering.Services.DTOs;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pizza_Ordering.Controllers
{
    public class SettingsController : ApiController
    {
        private ISettingsBL _service;

        public SettingsController(ISettingsBL service)
        {
            _service = service;
        }

        [HttpGet]
        public SettingsDto Get()
        {
            return _service.GetSettings();
        }

        [HttpPost]
        public void Post(SettingEditDto set)
        {
           _service.UpdateSettings(set);
        }
    }
}
