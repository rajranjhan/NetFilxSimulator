using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AR.NETFixSimulator.Infrastructure;

namespace AR.NETFixSimulator.Models
{
    public class LoginModel
    {
        public string SenderCompID { get; set; }
        public string TargetCompID { get; set; }

        
        public LoginModel(IConfigurationStore configurationStore)
        {
            LoadData();
        }

        public void Save()
        {

        }

        private void LoadData()
        {
            //TODO:  Read  NetFixSimulator.cfg
            File file = File.OpenWrite(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "NetFixSimulator.cfg");
 

        }
    }
}
