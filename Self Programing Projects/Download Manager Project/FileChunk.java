import java.io.File;

public class FileChunk implements Comparable<FileChunk>
{
    int rangeStart;
    int rangeEnd;
    int numOfDownloadedBytes;
    int downloadStatus;
    byte[] chunkContentByteArray;
    public FileChunk(int i_RangeStart, int i_RangeEnd) 
    {
        rangeStart = i_RangeStart;
        rangeEnd = i_RangeEnd;
        numOfDownloadedBytes = 0;
        // Setting chunk's size to 64KB. 
        chunkContentByteArray = new byte[(int) Math.pow(2, 16)];
    }

    
    @Override
    public int compareTo(FileChunk other) {
       if(this.rangeStart < other.rangeStart) return 0;
       else return 1;
    }
}