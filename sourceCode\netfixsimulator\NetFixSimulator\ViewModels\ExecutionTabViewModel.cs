using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NETFixSimulator.Models;
using Ninject;

namespace AR.NETFixSimulator.ViewModels
{
    public class ExecutionTabViewModel : TabData
    {       
        [Inject]
        public ExecutionTabViewModel(MessagesGroupViewModel messagesGroupViewModel)
        {
            Header = "Executions";
            this.GroupDataCollection.Add(messagesGroupViewModel.groupData);
        }


    }
}
