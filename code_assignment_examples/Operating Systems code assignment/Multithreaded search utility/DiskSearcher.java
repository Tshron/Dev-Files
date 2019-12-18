import java.io.File;
import java.util.ArrayList;

public class DiskSearcher {
    public static final int DIRECTORY_QUEUE_CAPACITY = 50;
    public static final int RESULTS_QUEUE_CAPACITY = 50;


    public static void main(String[] args)
    {
        String patternToSearch = args[0];
        String rootDirPath = args[1];
        String destinationDirPath = args[2];
        int numberOfSearchers = Integer.parseInt(args[3]);
        int numberOfCopiers = Integer.parseInt(args[4]);
        int numberOfCopiedFiles = 0;
        ArrayList<String> listOfCopiedFiles = new ArrayList<>();

        File rootDirFile = new File(rootDirPath);
        File destDirFile = new File(destinationDirPath);

        //init the directory and results queues
        SynchronizedQueue directoriesQueue = new SynchronizedQueue(DIRECTORY_QUEUE_CAPACITY);
        SynchronizedQueue resultsQueue = new SynchronizedQueue(RESULTS_QUEUE_CAPACITY);

        // starts the Scouter thread
        Scouter scouter = new Scouter(directoriesQueue, rootDirFile);
        Thread scouterThread = new Thread(scouter);
        scouterThread.start();





        // start @numberOfSearchers Search threads, each will get the directories queue to read from , and the
        // results queue to enqueue the matched files
        for (int i = 0; i < numberOfSearchers ; i++)
        {
            Searcher searchForFilesToCopy = new Searcher(patternToSearch, directoriesQueue, resultsQueue);
            Thread searchThread = new Thread(searchForFilesToCopy);
            try
            {
                searchThread.start();
                searchThread.join();
            }
            catch (InterruptedException e)
            {
                e.printStackTrace();
            }

        }

        // start @numberOfCopiers Copier Threads, each gets the destination file to copy the matched filed to,
        // and the results queue who holds the matched files
        for (int i = 0; i < numberOfCopiers; i++)
        {
            Copier copyThread = new Copier(destDirFile, resultsQueue);
            Thread copyThreadForCopy = new Thread(copyThread);
            try
            {
                copyThreadForCopy.start();
                copyThreadForCopy.join();
            }
            catch (InterruptedException e)
            {
                e.printStackTrace();
            }
            numberOfCopiedFiles += copyThread.getNumberOfFilesCopied();
            listOfCopiedFiles.addAll(copyThread.getListOfCopiedFiels());

        }

        System.out.println("Number of copied files is: " + numberOfCopiedFiles + "\n");
        System.out.println("Files who have been copied :\n");
        for (String name : listOfCopiedFiles)
        {
            System.out.println("- " + name);
        }

    }


}
