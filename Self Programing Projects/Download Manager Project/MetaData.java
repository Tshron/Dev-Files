import java.io.*;
import java.util.*;


import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;

public class MetaData implements java.io.Serializable
{
    private static final long serialVersionUID = 1L;
    public HashSet<Range> downloadedTable;
    public long downloadProgress;
    public static String fileName;


    public MetaData(String i_FileName) 
    {
        this.downloadedTable = new HashSet<Range>();    
        fileName = i_FileName;
        this.downloadProgress = 0;

    }
    
    public void writeMetaData()
    {
            try 
            {
                FileOutputStream fos = new FileOutputStream("./" + fileName + ".tmp");
                ObjectOutputStream out = new ObjectOutputStream(fos);
    
                out.writeObject(this);
                out.close();
                fos.close();
                
            }
            catch (Exception e) 
            {
                System.err.println("Error in writing meta data" + e);
            }
    }

    public void replaceMetadataFile()
    {
        Path origin = Paths.get("./" + fileName + ".ser");
        Path temp = Paths.get("./" + fileName + ".tmp");
        try
        {  
            Files.move(temp, origin, StandardCopyOption.ATOMIC_MOVE);
        }
        catch(Exception e)
        {
            // Nothing to do, the move operation faild but we still have the previoues metadata file
        }   
    }

    public static MetaData readMetaData(String i_FileName)
    {
        File originFile = new File("./" + i_FileName + ".ser");
        MetaData mt = null;
            try 
            {
                if(originFile.exists())
                {
                    FileInputStream fin = new FileInputStream(originFile);
                    ObjectInputStream in = new ObjectInputStream(fin);
                    
                    mt = (MetaData)in.readObject();
                    fileName = i_FileName;
                    in.close();
                    fin.close();
                }
                else
                {
                    mt = new MetaData(i_FileName);
                }
            }
            catch(ClassNotFoundException e)
            {
                System.err.println("Can't resolve class MetaData " + e);
            }
            catch (IOException e) 
            {
                System.err.println("Error in reading meta data" + e);
            }
        return mt;
    }

    public void discardMetaData() 
    {
        File file = new File("./" + fileName + ".ser");
        file.delete();
	}



    
}