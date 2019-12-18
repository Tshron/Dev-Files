import java.io.File;

public class Scouter implements Runnable
{
    private SynchronizedQueue<File> directoryQueue;
    private File root;


    public Scouter(SynchronizedQueue<File> directoryQueue, File root)
    {
       this.directoryQueue = directoryQueue;
       this.root = root;
    }

    @Override
    public void run() {
        if (root.isDirectory())
        {
            // registering the thread / scouter as a producer
            directoryQueue.registerProducer();
            subDirectoriesEnqueueHandler(root);
            directoryQueue.unregisterProducer();
        }
    }

    public void subDirectoriesEnqueueHandler(File root)
    {
        directoryQueue.enqueue((root));
        File[] subDirectories = root.listFiles();
        for (File subDir : subDirectories)
        {
            if(subDir.isDirectory())
            {
                subDirectoriesEnqueueHandler(subDir);
            }
        }

    }
}
