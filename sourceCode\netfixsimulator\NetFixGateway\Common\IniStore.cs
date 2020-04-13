using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AR.NetFixGateway.Common
{
    public class IniStore : IConfigurationStore
    {       

        public  IniStore(string path)
        {
            _path = path;            
            this.Open();
        }

        private readonly string _path;
        private List<string> _fileContents;

        public object GetValue(string key)
        {
            Contract.Requires(_fileContents != null);
            var foundLine = _fileContents.Where(x => x.StartsWith(key)).Select(line => line.Split('='));

            if (foundLine.Any())
            {
                return foundLine.First()[1];
            }

            return string.Empty;
        }

        public void SetValue(string key, object value)
        {
            var findIndex =  _fileContents.FindIndex(0, s => s.StartsWith(key));
            if (findIndex != -1)
            {
                _fileContents[findIndex] = key + "=" + value;
            }            
        }
        
        public void Open()
        {
            Contract.Requires(File.Exists(_path), String.Format("{0} configuration file does not exist", _path));            
            using (var sr = new StreamReader(_path))
            {
                //This allows you to do one Read operation.
                char[] delimiters = new char[] { '\r', '\n' };
                _fileContents = sr.ReadToEnd().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }

        public void Save()
        {
            Contract.Requires(_fileContents != null); 
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }

            using (var sw = new StreamWriter(_path))
            {
                _fileContents.ForEach(line => sw.WriteLine(line));
                _fileContents = null;
            }            
        }


        //TODO: Use Disposable pattern
        public void Dispose()
        {
            _fileContents = null;
        }
    }
}
