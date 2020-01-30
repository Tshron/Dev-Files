using System;
using System.Collections.Generic;

namespace PanelBar
{
    public interface IControlButton<T>
    {
        ICommand<T> Command { get; }

        ePanelBarStatus Status { get; }

        void ForwardStatus();
    }
}
