using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Shadowsocks.Model
{
    [Serializable]
    public class IEProxyConfig
    {
        public int useProxy;
        public string proxyServer;
        public string pacUrl;
        private static string CONFIG_FILE = "IE_proxy_config.json";

        public IEProxyConfig()
        {
            useProxy = 0;
            proxyServer = "";
            pacUrl = "";
        }
        public static IEProxyConfig Load()
        {
            try
            {
                string configContent = File.ReadAllText(CONFIG_FILE);
                IEProxyConfig config = JsonConvert.DeserializeObject<IEProxyConfig>(configContent);
                if(config.pacUrl == null)
                {
                    config.pacUrl = "";
                }
                if(config.proxyServer == null)
                {
                    config.proxyServer = "";
                }

                return config;
            }
            catch (Exception e)
            {
                if (!(e is FileNotFoundException))
                    Controller.Logging.LogUsefulException(e);
                return new IEProxyConfig();
 
            }
        }

        public static void Save(IEProxyConfig config)
        {
           try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(CONFIG_FILE, FileMode.Create)))
                {
                    string jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
                    sw.Write(jsonString);
                    sw.Flush();
                }
            }
            catch (IOException e)
            {
                Controller.Logging.LogUsefulException(e);
            }
        }
    }
}
