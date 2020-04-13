using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway.Common;
using Caliburn.Micro;
using QuickFix.Fields;

namespace AR.NETFixSimulator.Models
{
    public class MessageRecord : PropertyChangedBase
    {
        public MessageRecord(MessageHolder messageHolder)
        {
            MsgText = messageHolder.Message.ToString().Replace(QuickFix.Message.SOH, "  ");
            Direction = messageHolder.IsIncoming ? "IN" : "OUT";
            Timestamp = messageHolder.Message.Header.GetDateTime(QuickFix.Fields.Tags.SendingTime);

            MsgType msgType = new MsgType();
            messageHolder.Message.Header.GetField(msgType);
            _msgType = msgType.ToString();

            string name = messageHolder.Message.GetType().ToString();
            int index = name.LastIndexOf('.');
            _msgName = name.Substring(index+1);

        }

        
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                NotifyOfPropertyChange(() => Timestamp);
            }
        }
        private DateTime _timestamp = DateTime.MinValue;
        
        public string MsgText
        {
            get { return _msgText; }
            set
            {
                _msgText = value;
                NotifyOfPropertyChange(() => MsgText);
            }
        }
        private string _msgText;

        public string MessageType
        {
            get { return _msgType; }
            set
            {
                _msgType = value;
                NotifyOfPropertyChange(() => MessageType);
            }
        }
        private string _msgType;

        public string MessageName
        {
            get { return _msgName; }
            set
            {
                _msgType = value;
                NotifyOfPropertyChange(() => MessageName);
            }
        }
        private string _msgName;
        

        public string Direction
        {
            get { return _direction; }
            set { _direction = value;
            NotifyOfPropertyChange(() => Direction);
            }
        }
        private string _direction;

    }
}
