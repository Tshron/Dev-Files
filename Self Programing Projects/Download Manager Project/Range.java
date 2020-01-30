import java.io.Serializable;

public class Range implements Comparable<Range>, Serializable
{
    private static final long serialVersionUID = 3L;
    public int start;
    public int end;

    public Range(int i_Start, int i_End) 
    {
       this.start = i_Start;
       this.end = i_End; 
    }

    public int compareTo(Range o) 
    {
        return this.start - o.start;
    }
    
}