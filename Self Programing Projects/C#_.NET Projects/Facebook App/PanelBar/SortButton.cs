namespace PanelBar
{
    public class SortButton<T> : ControlButton<T>
    {
        public override void InternalForwardStatus()
        {
            switch (Status)
            {
                case ePanelBarStatus.Off:
                    Status = ePanelBarStatus.Up;
                    break;
                case ePanelBarStatus.Up:
                    Status = ePanelBarStatus.Down;
                    break;
                default:
                    Status = ePanelBarStatus.Off;
                    break;
            }
        }
    }
}
