import java.util.concurrent.PriorityBlockingQueue;

public class MidWayQueue {

    public PriorityBlockingQueue <FileChunk> chunksQueue;

    public MidWayQueue() 
    {
        chunksQueue = new PriorityBlockingQueue<FileChunk>();
    }

    public void PrintSize()
    {
        System.out.println(chunksQueue.size());
    }
}