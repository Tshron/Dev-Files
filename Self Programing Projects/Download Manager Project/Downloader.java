import java.io.*;
import java.net.*;
import java.util.ArrayList;

import javax.net.ssl.SSLException;


public class Downloader implements Runnable
{
    String urlToDownload;
    int threadRangeStart;
    int threadRangeEnd;
    int fileSize;
    FileChunk chunk;
    MidWayQueue queue;
    MetaData metaData;
    ArrayList<Range> sortedRangeList;

    public Downloader(String i_UrlToDownload, MidWayQueue i_Queue, int i_ThreadRangeStart, int i_ThreadRangeEnd, MetaData i_MetaData) 
    {
        this.urlToDownload = i_UrlToDownload;
        this.threadRangeStart = i_ThreadRangeStart;
        this.threadRangeEnd = i_ThreadRangeEnd;
        this.queue = i_Queue;
        this.metaData = i_MetaData;
        chunk = new FileChunk(threadRangeStart, threadRangeEnd);
    }
    
    public void run()
    {
        HttpURLConnection connection = null;
        InputStream inStream = null;        

        ArrayList<String> rangesForDownload = splitRange();
        
        if(rangesForDownload.size() > 0)
        {
            System.out.println("\t\t Thread " + Thread.currentThread().getId() + " downloading range: "  + Integer.parseInt(rangesForDownload.get(0).split("-")[0]) + "-" + Integer.parseInt(rangesForDownload.get(rangesForDownload.size() - 1).split("-")[1]) + "\n");
        }

        for (String range : rangesForDownload) 
        {
            try 
            {
                URL url = new URL(urlToDownload);
                connection = (HttpURLConnection)url.openConnection();
                
                connection.setRequestProperty("Range", "bytes=" + range);
                connection.setReadTimeout(10000);
                connection.setConnectTimeout(60000);
                connection.connect();
                
                inStream = connection.getInputStream();
                
                int reader;
                int newRangeStart;
                chunk.rangeStart = Integer.parseInt(range.split("-")[0]);
                
                while((reader = inStream.readNBytes(chunk.chunkContentByteArray, 0, chunk.chunkContentByteArray.length)) != 0)
                {                
                    chunk.numOfDownloadedBytes = reader;
                    chunk.rangeEnd = chunk.rangeStart + chunk.numOfDownloadedBytes - 1;
                    
                    queue.chunksQueue.put(chunk);
                    newRangeStart = nextRangeStart(chunk.rangeEnd);
                    chunk = new FileChunk(newRangeStart, 0);
                }

                inStream.close();
                connection.disconnect();
            }
            //In case of connection failure
            catch(SSLException e)
            {
                connectionFailureSenario(connection);
            }
            catch(SocketTimeoutException e)
            {
                connectionFailureSenario(connection);
            }
            catch(IOException e)
            {
                System.err.println("Error in Downloader " + e);
            }
        }
    }

    private ArrayList<String> splitRange() 
    {
        ArrayList<String> rangesArr = new ArrayList<String>();
        ArrayList<Range> sortedRangeList = new ArrayList<Range>(metaData.downloadedTable);
        sortedRangeList.sort(new RangeComparator());
        
        int currnetRangeStartIndex = threadRangeStart;
        int currentRangeEndIndex = threadRangeEnd;

        for (Range range : sortedRangeList) 
        {
            if(range.start < currnetRangeStartIndex && range.end > currnetRangeStartIndex)
            {
                currnetRangeStartIndex = range.end + 1;
            }
            else if(range.start < currentRangeEndIndex && range.start >= currnetRangeStartIndex)
            {
                if(currnetRangeStartIndex != range.start)
                {
                    rangesArr.add(currnetRangeStartIndex + "-" + (range.start - 1));
                }
                currnetRangeStartIndex = range.end + 1;
            }
            else if(range.start < currentRangeEndIndex && range.end > currentRangeEndIndex)
            {
                currentRangeEndIndex = range.start - 1;
            }
            
        }
        if(currnetRangeStartIndex < threadRangeEnd)
        {
            rangesArr.add(currnetRangeStartIndex + "-" +  threadRangeEnd);
        }

        return rangesArr;
    }

    private int nextRangeStart(int rangeEnd)
    {
        int newRangeStart = rangeEnd + 1;
        if (newRangeStart >= threadRangeEnd)
        {
            newRangeStart = rangeEnd;
        }
        return newRangeStart;
    } 

    private void connectionFailureSenario(HttpURLConnection connection) 
    {
        connection.disconnect();
        synchronized(this)
        {
            System.err.println("**** Connection Failure - Download has stopped ****");
            System.exit(1);
        }
    }
}