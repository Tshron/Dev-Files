import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.*;

public class DiskWriter
{
    public String destintionPath; 
    public MidWayQueue queueOfDownloadedChunks;
    public int numOfWritedChunks;
    public long numOfWritedBytes;
    public RandomAccessFile randomAccessFile;
    public FileChunk chunk;
    public MetaData metaData;
    public Range range;


    public DiskWriter(String i_FilenamePath, MidWayQueue i_Queue, int i_NumOfBytesInFile, MetaData i_MetaData) 
    {
        this.destintionPath = i_FilenamePath;
        this.queueOfDownloadedChunks = i_Queue;
        this.metaData = i_MetaData;
        this.numOfWritedBytes = i_MetaData.downloadProgress;
        this.numOfWritedChunks = 0;
        try
        {
            this.randomAccessFile = new RandomAccessFile("./" + destintionPath, "rw");
        }
        catch(FileNotFoundException e)
        {
            System.err.println("Cannot access destination file localy" + e);
        }
       
    }

    public void WriteChunk()
    {
        try
        {
            // take one chunk out of the queue
            chunk = queueOfDownloadedChunks.chunksQueue.take();

            // start writing the bytes to the right position of file
            randomAccessFile.seek(chunk.rangeStart);
            randomAccessFile.write(chunk.chunkContentByteArray, 0, chunk.numOfDownloadedBytes);
            
            //updating the user about download progress
            numOfWritedBytes += chunk.numOfDownloadedBytes;

            //update metadata file
            UpdateMetaData();
        }
        catch(Exception e)
        {
            System.err.println("error in diskwriter " + e);
        }
        
    }

    private void UpdateMetaData() 
    {
        range = new Range(chunk.rangeStart, chunk.rangeEnd);
        
        metaData.downloadedTable.add(range);
        
        metaData.downloadProgress = numOfWritedBytes ;
        metaData.writeMetaData();
    }

    public void CloseFile() 
    {
        try
        {
            randomAccessFile.close();
        }
        catch(IOException e)
        {
            System.err.println("Error while closing the randomAccessFile");
        }
	}


}