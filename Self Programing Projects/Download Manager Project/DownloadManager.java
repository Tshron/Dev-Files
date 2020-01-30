import java.io.IOException;
import java.net.SocketTimeoutException;
import java.util.LinkedList;
import java.util.Random;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.LinkedBlockingDeque;

public class DownloadManager
{
    public static final int DOWNLOADING = 0;
    public static final int PAUSED = 1;
    public static final int COMPLETED = 2;
    public static final int CANCELLED = 3;
    public static final int ERROR = 4;

    LinkedList<String> urlsList;
    int MaxNumOfConcurretConnection;
    int fileSize;
    int downloadState;
    String fileName;
    MidWayQueue blockingQueue;
    ExecutorService threadPool;
    MetaData metaData;
    int downloadProgressPrint;
    long prevDownloadProgress;
    long currentDownloadProgress;

 
    public DownloadManager(LinkedList<String> i_ListOfUrls, int i_MaxNumOfConcurretConnection, int i_FileSize, String i_FileName) 
    {
        this.urlsList = i_ListOfUrls;
        this.MaxNumOfConcurretConnection = i_MaxNumOfConcurretConnection;
        this.fileSize = i_FileSize;
        this.fileName = i_FileName;
        this.blockingQueue = new MidWayQueue();
        this.threadPool = Executors.newFixedThreadPool(this.MaxNumOfConcurretConnection);
        this.metaData = MetaData.readMetaData(fileName);
        this.downloadProgressPrint = -1;
        this.prevDownloadProgress = metaData.downloadProgress;
        this.currentDownloadProgress = 0;
    }


    public void startDownloading() 
    {
        //downloadPart
        printDownloadState("started");
        downloadState = (int)(prevDownloadProgress / fileSize);
        download(MaxNumOfConcurretConnection);

        //write to disk part
        DiskWriter diskWriter = new DiskWriter(fileName, blockingQueue, fileSize, this.metaData);

        while(blockingQueue.chunksQueue.size() > 0 || downloadState != 1)
        {
            diskWriter.WriteChunk();
            
            OverrideMetaData();
            
            currentDownloadProgress = diskWriter.numOfWritedBytes;
            downloadProgressPrint = printDownloadProgress(downloadProgressPrint);
            if(currentDownloadProgress == fileSize)
            {
                diskWriter.CloseFile();
                downloadState = 1;
            }
        }
        metaData.discardMetaData();
        printDownloadState("succeeded");
        
        threadPool.shutdown();
        
	}
    
    
    public void download(int MaxNumOfConcurretConnection) 
    {      
        int rangeSize = fileSize / MaxNumOfConcurretConnection;  
        double remainder = 0;
        int threadtRangeStart = 0;
        int threadRangeEnd = threadtRangeStart + rangeSize - 1;
        int randomLineNum;
        
        for (int i = 0; i < MaxNumOfConcurretConnection; i++) 
        {
            if(i == (MaxNumOfConcurretConnection - 2))
            {
                remainder = fileSize % MaxNumOfConcurretConnection;
            }
            
            if(urlsList.size() == 1)
            {
                BeginDownload(urlsList.getFirst(), threadtRangeStart, threadRangeEnd);
                threadtRangeStart += rangeSize;
                threadRangeEnd = threadtRangeStart + rangeSize + (int)remainder - 1;
            }
            else
            {
                randomLineNum = randomURL();
                 BeginDownload(urlsList.get(randomLineNum), threadtRangeStart, threadRangeEnd);                threadtRangeStart += rangeSize;
                threadRangeEnd = threadtRangeStart  + rangeSize + (int)remainder - 1;
                urlsList.remove(randomLineNum);
            }   
        }
    }
    
    public void BeginDownload(String i_UrlToDownload, int i_StartRange, int i_EndRange)
    {
        Downloader downloader = new Downloader(i_UrlToDownload, blockingQueue, i_StartRange , i_EndRange, metaData);
        threadPool.execute(downloader);
    }
    
    private void OverrideMetaData() 
    {
        metaData.replaceMetadataFile();
    }
    
    private int printDownloadProgress(int i_CurrnetState) 
    {
        int newDownloadState = ((int)(currentDownloadProgress * 100 / fileSize));
        if(newDownloadState != i_CurrnetState)
        {
            System.out.println("downloading " + newDownloadState + "%");
        }
        return newDownloadState;
    }
    
    private void printDownloadState(String string) 
    {
        System.out.println("**** Download " + string + " ****\n");
    }
    
    public int randomURL() 
    {  
        Random random = new Random(); 
        int randomLine = random.nextInt(urlsList.size() - 1);
        return randomLine;
    }

}
    