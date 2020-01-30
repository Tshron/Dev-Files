import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.LinkedList;
public class DmMain {

    public static void main (String args[])
    {
        try 
        {
            LinkedList<String> urlList = new LinkedList<String>();
            int maxConnections = 1;                          
            if (args.length > 1) 
            {
                try 
                {
                    maxConnections = Integer.parseInt(args[1]); 
                }
                 catch (NumberFormatException e) 
                {
                    printUsageMessage();   
                }
            }    
    
            File inputFile = new File(args[0]);
    
            if (inputFile.exists() && !inputFile.isDirectory()) 
            {
                 urlList = BuildUrlsFromFile(inputFile);
            }
            else
            {
                 urlList.add(args[0]);
            }
    
            String fileUrlName = urlList.get(0);  
            int filesize = GetFileSize(fileUrlName);
            fileUrlName = fileUrlName.substring(fileUrlName.lastIndexOf('/') + 1);
    
            DownloadManager fileDownloadManager = new DownloadManager(urlList, maxConnections, filesize, fileUrlName); 
            fileDownloadManager.startDownloading();
        }
         catch (ArrayIndexOutOfBoundsException e) 
        {
            printUsageMessage();
        }
           
        
    }

    private static void printUsageMessage() 
    {
        System.err.println("Usage:\n\t java IdcDm URL|URL-LIST-FILE [MAX-CONCURRENT-CONNECTIONS]");
    }

    private static int GetFileSize(String fileUrlName) 
    {
        HttpURLConnection conn = null;
        int filesize = 0;
        try 
        {
            URL url = new URL(fileUrlName);
            conn = (HttpURLConnection) url.openConnection();
            conn.connect();
            filesize = conn.getContentLength();
        }
         catch (IOException e)  
        {
            System.err.println("error in main" + e);
        }
        conn.disconnect();
        return filesize;
    }

    private static LinkedList<String> BuildUrlsFromFile(File inputFile) 
    {
        BufferedReader reader = null;
        LinkedList<String> urlList = new LinkedList<String>();
        try
        {
            reader = new BufferedReader(new FileReader(inputFile));
            String line;
            while ((line = reader.readLine()) != null) 
            {
                urlList.add(line);
            }
            reader.close();
        } 
        catch (IOException e) 
        {
            e.printStackTrace();
        }

        return urlList;
    }
}

