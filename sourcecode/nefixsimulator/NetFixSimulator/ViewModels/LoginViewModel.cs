using System.Windows;
using AR.NETFixSimulator.Models;
using AR.NetFixGateway.Common;

using Caliburn.Micro;

using Ninject;

namespace AR.NETFixSimulator.ViewModels
{

    public class LoginViewModel : Screen, ILoginViewModel 
    {

        [Inject]
        public LoginViewModel(IConfigurationStore configurationStore)
        {                    
            _configurationStore = configurationStore;
            LoadData();
        }
        private IConfigurationStore _configurationStore;

        public bool IsLoginValid {  get { return true; } }

        private void LoadData()
        {
            FixVersion = _configurationStore.GetValue("BeginString").ToString();
            _senderCompID = _configurationStore.GetValue("SenderCompID").ToString();
            _targetCompID = _configurationStore.GetValue("TargetCompID").ToString();
            _port = _configurationStore.GetValue("SocketAcceptPort").ToString();
            AutoStart = UserSettings.Default.AutoStart;
        }
    #region Porpeties
        public bool AutoStart { get
        {
            return _autoStart;
        } 
            set
            {
                if (_autoStart != value)
                {
                    _autoStart = value;
                    UserSettings.Default.AutoStart = _autoStart;
                }

            }
        }

        private bool _autoStart = false;
        
        public string FixVersion { get; private set; }
        public string Port { get
        {
            return _port;
        }
            set
            {
                if (!value.Equals(_port))
                {
                    _port = value;
                    _configurationStore.SetValue("SocketAcceptPort", _port);
                }
            }
        }       
        private string _port;
        
        public string SenderCompID
        {
            get
            {
                return _senderCompID;
            }
            set
            {
                if (!value.Equals(_senderCompID))
                {
                    _senderCompID = value;
                    _configurationStore.SetValue("SenderCompID", _senderCompID);
                }
            }
        }       
        private string _senderCompID;

        public string TargetCompID
        {
            get
            {
                return _targetCompID;
            }
            set
            {
                if (!value.Equals(_targetCompID))
                {
                    _targetCompID = value;
                    _configurationStore.SetValue("TargetCompID", _targetCompID);
                }
            }
        }       
        private string _targetCompID;
    #endregion

        /// <summary>
        /// OnOk Called - Save the settings
        /// </summary>
        public void Login()
        { 
            _configurationStore.Save();            
            UserSettings.Default.Save();
            TryClose(true);            
        }

        public void Cancel()
        {
            Application.Current.Shutdown();
        }
        
    }
}
