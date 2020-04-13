// -----------------------------------------------------------------------
// <copyright file="QuickFixApplication.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using AR.NetFixGateway.Common;
using AR.NetFixGateway.Engines;
using Ninject;
using Ninject.Modules;

namespace AR.NetFixGateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using QuickFix;
    using QuickFix.FIX42;
    using Message = QuickFix.Message;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public  class QuickFixApplication : IApplication
    {
        public SessionSettings CurrentSessionSettings { get; set; }
        public SessionID CurrentSessionId { get; set; }

        public event Action<SessionID> LogonEvent;
        public event Action<SessionID> LogoutEvent;
        public event Action<MessageHolder> MessageEvent;

        #region Application        
        public void FromAdmin(Message message, SessionID sessionID)
        {
            if (MessageEvent != null)           
               MessageEvent(new MessageHolder(message, true, true));            
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            if (MessageEvent != null)
                MessageEvent(new MessageHolder(message, true));    
        }

        public void OnCreate(SessionID sessionID)
        {
        }

        public void OnLogon(SessionID sessionID)
        {
            this.CurrentSessionId = sessionID;
            //Trace.WriteLine(String.Format("==OnLogon: {0}==", this.ActiveSessionID.ToString()));
            if (LogonEvent != null)
                LogonEvent(sessionID);
        }

        public void OnLogout(SessionID sessionID)
        {
            if (LogoutEvent != null)
                LogoutEvent(sessionID);
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            if (MessageEvent != null)
                MessageEvent(new MessageHolder(message, false, true));    
        }

        public void ToApp(Message message, SessionID sessionId)
        {
            if (MessageEvent != null)
                MessageEvent(new MessageHolder(message, false));    
        }

        #endregion
    }

    public class QuickFixWrapperModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IApplication>().To<QuickFixApplication>();
        }
    }
}
