// -----------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using AR.NETFixSimulator.Models;
using AR.NetFixGateway;
using AR.NetFixGateway.Common;
using Caliburn.Micro;
using Ninject;
using Message = QuickFix.Message;

namespace AR.NETFixSimulator.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using AR.NetFixGateway.Engines;

    using QuickFix.Fields;
    using System.ComponentModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class ShellViewModel : PropertyChangedBase, IShellViewModel, IDisposable
    {
        /// <summary>
        /// MessageHandler - any special handling is done here.
        /// </summary>
        /// <param name="messageHandler"></param>
        [Inject]
        public void SetExecutionEngine(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }
        private IMessageHandler _messageHandler;

        [Inject]
        public void SetSession(ISession session)
        {
            _session = session;            
            session.MessageEvent += (message) =>
                                        {
                                            //Add new messages to the queue.  This will be retrieved periodically to display.
                                            _queue.Add(message);
                                            if (message.IsIncoming && !message.IsAdmin)
                                                _messageHandler.CrackMessage(message);
                                        };

            _canceller = new CancellationTokenSource();

            _session.StateChangeEvent += state =>
                                             {
                                                 if (state == SessionState.Started)
                                                     StartListener();
                                                 if(state == SessionState.LoggedOut)
                                                     StopListener();
                                             };

            if (UserSettings.Default.AutoStart)
                _session.Start();
        }

       
        public ShellViewModel(RibbonViewModel ribbonViewModel)
        {
            RibbonVM = ribbonViewModel;
        }

        public RibbonViewModel RibbonVM { get; private set; }

        public IObservableCollection<MessageRecord> Messages
        {
            get { return _messages; }
        }

        //TODO: implement Disposable pattern
        public void Dispose()
        {
            if (_listener != null && _canceller != null)
            {
                StopListener();
                _listener = null;
                _canceller = null;
            }
        }

        private void StopListener()
        {
            _canceller.Cancel();
            _session.Stop();
        }
      
        private void StartListener()
        {
            _listener = Task.Factory.StartNew(
                () =>
                    {
                        while (!_canceller.Token.IsCancellationRequested)
                        {
                            MessageHolder item;
                            if (_queue.TryTake(out item))
                                ProcesssNewMessage(item);

                            Thread.Sleep((int) (UserSettings.Default.Delay*1000));
                        }
                    },
                _canceller.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }


        /// <summary>
        /// Background method responsible for pushing new fix messages to UI
        /// </summary>
        /// <param name="messageHolder"></param>
        private void ProcesssNewMessage(MessageHolder messageHolder)
        {
            var messageRecord = new MessageRecord(messageHolder);
            Application.Current.Dispatcher.BeginInvoke((Action)(() => _messages.Add(messageRecord)));
        }


        #region Fields

        private ISession _session;
        private BlockingCollection<MessageHolder> _queue = new BlockingCollection<MessageHolder>();

        public IObservableCollection<MessageRecord> _messages =
            new BindableCollection<MessageRecord>(new ConcurrentQueue<MessageRecord>());

        private CancellationTokenSource _canceller;
        private Task _listener;

        #endregion

    }

}