using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMonitoramento.Utill.Servicos
{
    public static class AppSettingsHelper
    {
        private static IConfiguration _config;

        public static void AppSettingsConfigure(IConfiguration config)
        {
            _config = config;
        }

        public static string Setting(string Key)
        {
            return _config.GetSection(Key).Value;
        }
    }
}
