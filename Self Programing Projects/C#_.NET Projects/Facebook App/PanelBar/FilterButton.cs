namespace PanelBar
{
    public class FilterButton<T> : ControlButton<T>
    {
        public override void InternalForwardStatus()
        {
            switch (Status)
            {
                case ePanelBarStatus.Off:
                    Status = ePanelBarStatus.On;
                    break;
                case ePanelBarStatus.On:
                    Status = ePanelBarStatus.Off;
                    break;
                default:
                    Status = ePanelBarStatus.Off;
                    break;
            }
        }
    }
}
