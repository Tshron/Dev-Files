import java.io.Serializable;
import java.util.Comparator;

public class RangeComparator implements Comparator<Range>, Serializable
{
    private static final long serialVersionUID = 2L;

    @Override
    public int compare(Range o1, Range o2) 
    {
        return o1.compareTo(o2);
    }
}