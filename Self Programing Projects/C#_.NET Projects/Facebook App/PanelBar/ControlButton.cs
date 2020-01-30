using System;

namespace PanelBar
{
    public abstract class ControlButton<T> : IControlButton<T>
    {
        public ICommand<T> Command { get; set; }

        public ePanelBarStatus Status { get; set; }
        
        public void ForwardStatus()
        {
            InternalForwardStatus();
        }

        public abstract void InternalForwardStatus();
    }
}
