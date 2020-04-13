// -----------------------------------------------------------------------
// <copyright file="ButtonData.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AR.NETFixSimulator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;

    using Caliburn.Micro;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ButtonData : ControlData
    {
      
        public ButtonData(string label, Expression<Func<IEnumerable<IResult>>> messageTarget, object target)
        {
            this._target = target;

            var lambda = (LambdaExpression)messageTarget;
            MethodCallExpression methodCall = (MethodCallExpression)lambda.Body;

            _message = string.Format(_message, EventName, methodCall.Method.Name);

            Label = label;
        }

        public ButtonData(string label, object target)
        {
            this._target = target;
            Label = label;
        }

        public ButtonData(string label, Expression<System.Action> messageTarget, object target)
        {
            this._target = target;

            var lambda = (LambdaExpression)messageTarget;
            MethodCallExpression methodCall = (MethodCallExpression)lambda.Body;

            _message = string.Format(_message, EventName, methodCall.Method.Name);

            Label = label;
        }

        public ButtonData(string label, string actionName, object target = null)
        {
            this._target = target;

            _message = string.Format(_message, EventName, actionName);

            Label = label;
        }

        public virtual string EventName { get { return "Click"; } }

        public string Message { get { return _message; } }
        public object Target { get { return _target; } }

        private readonly object _target;
        protected string _message = "[Event {0}] = [Action {1}]";

    }
}
