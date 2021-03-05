using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres230221
{
    class PostgresAppConfig
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string m_file_name;
        private bool m_init = false;
        private JObject m_configRoot;

        internal static readonly PostgresAppConfig Instance = new PostgresAppConfig();

        // member according to json file
        public string ConnectionString { get; set; }
        public string AppName { get; set; }
        public float Version { get; set; }

        private PostgresAppConfig()
        {

        }

        public void Init(string file_name = null)
        {
            m_file_name = file_name != null ? file_name : "PostgresApp.Config.json";
            m_init = true;

            if (!File.Exists(m_file_name))
            {
                my_logger.Fatal($"File {m_file_name} does not exist!");
                Console.WriteLine($"File {m_file_name} does not exist!");
                Environment.Exit(-1);
            }

            var reader = File.OpenText(m_file_name);
            string json_string = reader.ReadToEnd();

            JObject all = (JObject)JsonConvert.DeserializeObject(json_string);
            m_configRoot = (JObject)all["PostgresApp"];
            ConnectionString = m_configRoot["ConnectionString"].Value<string>();
            AppName = m_configRoot["AppName"].Value<string>();
            Version = m_configRoot["Version"].Value<float>();
        }


    }
}
