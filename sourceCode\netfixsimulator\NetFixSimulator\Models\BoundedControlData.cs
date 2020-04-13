using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.NETFixSimulator.Models
{
    public abstract class BoundedControlData<T> : ControlData
    {
        protected Action<T> valueSetter;
        protected Func<T> valueGetter;

        public BoundedControlData(string actionName, object target)
        {
            this._target = target;

            valueGetter = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>),
                _target, _target.GetType().GetProperty(actionName).GetGetMethod());

            valueSetter = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>),
                _target, _target.GetType().GetProperty(actionName).GetSetMethod());

        }


        public object Target { get { return _target; } }
        private readonly object _target;
    }
}
