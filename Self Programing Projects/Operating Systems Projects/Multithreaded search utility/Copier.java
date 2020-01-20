import java.io.*;
import java.util.ArrayList;
import java.util.List;

public class Copier implements Runnable
{

    public static final int COPY_BUFFER_SIZE = 4096;
    private SynchronizedQueue<File> resultsQueue;
    private File destination;
    private int numberOfCopiedFiles;
    private ArrayList<String> copiedFilesNames;

    public Copier(File destination, SynchronizedQueue<File> resultsQueue)
    {
        this.resultsQueue = resultsQueue;
        this.destination = destination;
        this.numberOfCopiedFiles = 0;
        this.copiedFilesNames = new ArrayList<>();
    }

    public int getNumberOfFilesCopied()
    {
        return this.numberOfCopiedFiles;
    }

    public ArrayList<String> getListOfCopiedFiels()
    {
        return this.copiedFilesNames ;
    }


    @Override
    // this method copy the files from the @resultsQueue one by one to the destination file
    public void run()
    {
        InputStream inStream = null;
        OutputStream outStream = null;
        while(!resultsQueue.isEmpty())
        {
           File currentFileToCopy = resultsQueue.dequeue();
           String fileNameToCopy = currentFileToCopy.getName();
           File newLocationOfTheFile = new File(destination, fileNameToCopy);
           try
           {
               inStream = new FileInputStream(currentFileToCopy);
               outStream = new FileOutputStream(newLocationOfTheFile);
               byte[] bufferForCopy = new byte[COPY_BUFFER_SIZE];
               int reader;
               while((reader = inStream.read(bufferForCopy)) != -1)
               {
                   outStream.write(bufferForCopy, 0, reader);
               }
               outStream.flush();
           }
           catch (IOException e)
           {
               e.printStackTrace();
           }
           finally
           {
               try
               {
                   inStream.close();
                   outStream.close();
                   this.numberOfCopiedFiles++;
                   this.copiedFilesNames.add(fileNameToCopy);
               }
               catch (IOException e)
               {
                   e.printStackTrace();
               }
           }

        }
    }
}
