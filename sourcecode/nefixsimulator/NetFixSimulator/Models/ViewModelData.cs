// -----------------------------------------------------------------------
// <copyright file="ViewModelData.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Input;

using AR.NETFixSimulator.Infrastructure;

namespace AR.NETFixSimulator.Model
{
    
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ViewModelData
    {
                                
        //public static RibbonData RibbonData
        //{
        //    get
        //    {
        //        if (_data == null)
        //        {
        //            _data = new RibbonData();
        //        }
        //        return _data;
        //    }
        //}

        public static ICommand DefaultCommand
        {
            get
            {
                if (_defaultCommand == null)
                {
                    _defaultCommand = new DelegateCommand(DefaultExecuted, DefaultCanExecute);
                }
                return _defaultCommand;
            }
        }

        private static void DefaultExecuted()
        {
        }

        private static bool DefaultCanExecute()
        {
            return true;
        }

        [ThreadStatic]
        private static RibbonData _data;
        private static ICommand _defaultCommand;
    }
}
