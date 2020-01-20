import java.io.File;
import java.io.FileFilter;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Searcher implements Runnable
{
    private SynchronizedQueue<File> directoryQueue;
    private SynchronizedQueue<File> resultsQueue;
    public String patternToSearch;

    public Searcher(String pattern, SynchronizedQueue directoryQueue, SynchronizedQueue resultsQueue)
    {
        this.directoryQueue = directoryQueue;
        this.resultsQueue = resultsQueue;
        this.patternToSearch = pattern;
        
    }

    public void run()
    {
        // register the results queue as a producer
        resultsQueue.registerProducer();

        //while there are still directories in the queue, dequeue one and make the search part on it
        while (!directoryQueue.isEmpty())
        {
            File currentDirectoryToRun = directoryQueue.dequeue();
            File[] filesFromInsideCurrentDir = currentDirectoryToRun.listFiles();


            for (File fileFromArr : filesFromInsideCurrentDir)
            {
                Pattern patternToMatch = Pattern.compile(patternToSearch);
                Matcher m = patternToMatch.matcher(fileFromArr.getName());
                // insert to the results arr only files but not dirs
                if(fileFromArr.isFile())
                {
                    if (m.find())
                    {
                        resultsQueue.enqueue(fileFromArr);
                    }
                }
            }
        }
        // finally, unregister the thread from the producers list.
        resultsQueue.unregisterProducer();
    }
}
