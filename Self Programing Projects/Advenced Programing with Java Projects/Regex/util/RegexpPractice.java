package util;


import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * This class is only used to practice regular expressions.
 * @author talm
 *
 */
public class RegexpPractice implements RegexpPracticeInterface {
	/**
	 * Search for the first occurrence of text between single quotes, return the text (without the quotes).
	 * Allow an empty string. If no quoted text is found, return null.
	 * Some examples :
	 * <ul>
	 * <li>On input "this is some 'text' and some 'additional text'" the method should return "text".
	 * <li>On input "this is an empty string '' and another 'string'" it should return "".
	 * </ul>
	 * @param input
	 * @return the first occurrence of text between single quotes
	 */
	public String findSingleQuotedTextSimple(String input) {
		String pattern = "'[^']*'";
		Pattern p  = Pattern.compile(pattern);
		Matcher m = p.matcher(input);
		if(m.find()) { // if there is a match,
			return m.group().replaceAll("'", "");
		}
		else return null ;
	}
	
	
	/**
	 * Search for the first occurrence of text between double quotes, return the text (without the quotes).
	 * (should work exactly like {@link #findSingleQuotedTextSimple(String)}), except with double instead 
	 * of single quotes.
	 * @param input
	 * @return the first occurrence of text between double quotes
	 */
	public String findDoubleQuotedTextSimple(String input) {
		String pattern = "\"[^\"]*\"";
		Pattern p  = Pattern.compile(pattern);
		Matcher m = p.matcher(input);
		if(m.find()) { // if there is a match
			return m.group().replaceAll("\"", "");
		}
		else return null ;
	}
	
	/**
	 * Search for the all occurrences of text between single quotes <i>or</i> double quotes. 
	 * Return a list containing all the quoted text found (without the quotes). Note that a double-quote inside
	 * a single-quoted string counts as a regular character (e.g, on the string [quote '"this"'] ["this"] should be returned).  
	 * Allow empty strings. If no quoted text is found, return an empty list. 
	 * @param input
	 * @return
	 */
	public List<String> findDoubleOrSingleQuoted(String input) {
		ArrayList<String> matchs = new ArrayList<>();
		String pattern = "(\"([^\"]*)\")|('([^']*)')";
		Pattern p  = Pattern.compile(pattern);
		Matcher m = p.matcher(input);
		while (m.find()){
			// if there is a match to a double quote string
			if(m.group(2) != null && m.group(3) == null ) matchs.add(m.group(2));
			// if there is a match to a single quotes string
			if(m.group(4) != null && m.group(1) == null ) matchs.add(m.group(4));
		}
		return matchs;
	}

	/**
	 * Parse a date string with the following general format:<br> 
	 * Wdy, DD-Mon-YYYY HH:MM:SS GMT<br>
	 * Where:
	 * 	 <i>Wdy</i> is the day of the week,
	 * 	 <i>DD</i> is the day of the month, 
	 *   <i>Mon</i> is the month,
	 *   <i>YYYY</i> is the year, <i>HH:MM:SS</i> is the time in 24-hour format,
	 *   and <i>GMT</i> is a the constant timezone string "GMT".
	 * 
	 * You should also accept variants of the format: 
	 * <ul>
	 * <li>a date without the weekday, 
	 * <li>spaces instead of dashes (i.e., "DD Mon YYYY"), 
	 * <li>case-insensitive month (e.g., allow "Jan", "JAN" and "jAn"),
	 * <li>a two-digit year (assume it's between 1970 and 2069 in that case)
	 * <li>a missing timezone
	 * <li>allow multiple spaces wherever a single space is allowed.
	 * </ul> 
	 *     
	 * The method should return a java {@link Calendar} object with fields  
	 * set to the corresponding date and time. Return null if the input is not a valid date string.
	 * @param input
	 * @return
	 */
	public Calendar parseDate(String input) {
		Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("GMT"));
		int date = 0, month = 0, year = 0, hourOfDay = 0, minute = 0, second = 0;
		String pattern = "\\w*,?\\s*(\\d\\d)(-|\\s+)(\\w+)(-|\\s+)(\\d+)\\s*((\\d\\d):(\\d\\d):(\\d\\d))\\s*(\\w+)*";
		Pattern p = Pattern.compile(pattern);
		Matcher dMatch = p.matcher(input);
		String[] months = {"jan", "fab", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"};
		if (dMatch.find()) {
			// parsing the digits who matched into date format numbers
			date = Integer.parseInt(dMatch.group(1));
			String wordMonth = dMatch.group(3).toLowerCase();
			int i = 0;
			// figure out which month number matched.
			while (i < 12) {
				if(months[i].indexOf(wordMonth) == 0) {
					month = i;
					break;
				}
				i++;
			}
			// if no month matched, then the input format is'nt valid
			if (i == 12) return null;
			year = Integer.parseInt(dMatch.group(5));
			if (year >= 70 && year <= 99) year += 1900;
			else if (year >= 0 && year < 70) year += 2000;
			// parsing time format
			hourOfDay = Integer.parseInt(dMatch.group(7));
			minute = Integer.parseInt(dMatch.group(8));
			second = Integer.parseInt(dMatch.group(9));

			// if we matched a not valid time zone
			if((dMatch.group(10) != null) && !dMatch.group(10).equals("GMT")) return null;

			// setting all the calender object fields.
			cal.set(year, month, date, hourOfDay, minute, second);
			return cal;
		}
		else return null;

	}

	/**
	 * Separate the input into <i>tokens</i> and return them in a list.
	 * A token is any mixture of consecutive word characters and single-quoted strings (single quoted strings
	 * may contain any character except a single quote).
	 * The returned tokens should not contain the quote characters. 
	 * A pair of single quotes is considered an empty token (the empty string).
	 * 
	 * For example, the input "this-string 'has only three tokens'" should return the list
	 * {"this", "string", "has only three tokens"}. 
	 * The input "this*string'has only two@tokens'" should return the list
	 * {"this", "stringhas only two@tokens"}
	 * 
	 * @param input
	 * @return
	 */

	public List<String> wordTokenize(String input) {
		ArrayList<String> matchs = new ArrayList();
		String pattern = "(('[^']*'|\\w+))+" ;
		Pattern p  = Pattern.compile(pattern);
		Matcher m = p.matcher(input);
		while(m.find()) {
			if (m.group(0) != null) matchs.add(m.group(0).replaceAll("'", ""));
		}
		return matchs;
	}
	

	/**
	 * Search for the all occurrences of text between single quotes, but treating "escaped" quotes ("\'") as
	 * normal characters. Return a list containing all the quoted text found (without the quotes, and with the quoted escapes
	 * replaced). 
	 * Allow empty strings. If no quoted text is found, return an empty list. 
	 * Some examples :
	 * <ul>
	 * <li>On input "'This is not wrong' and 'this is isn\'t either", the method should return a list containing 
	 * 		("This is not wrong" and "This isn't either").
	 * <li>On input "No quoted \'text\' here" the method should return an empty list.
	 * </ul>
	 * @param input
	 * @return all occurrences of text between single quotes, taking escaped quotes into account.
	 */
	public List<String> findSingleQuotedTextWithEscapes(String input) {
		ArrayList<String> list = new ArrayList<>();
		Pattern p  = Pattern.compile("(\\\\')|('')|('(((\\\\')|[^'])*[^\\\\])')");
		Matcher m = p.matcher(input);
		while(m.find()) {
		    if(m.group(2) == null) {
				// replace all ** \' ** to ** ' **
                if(m.group(1) == null) list.add(m.group(4).replaceAll("\\\\'", "'"));
            }
			// in case we matched an empty quoted string
            else list.add("");
        }
		return list;
	}

	/**
	 * Search for the all occurrences of text between single quotes, but treating "escaped" quotes ("\'") as
	 * normal characters. Return a list containing all the quoted text found (without the quotes, and with the quoted escapes
	 * replaced). 
	 * Allow empty strings. If no quoted text is found, return an empty list. 
	 * Some examples :
	 * <ul>
	 * <li>On input "'This is not wrong' and 'this is isn\'t either", the method should return a list containing 
	 * 		("This is not wrong" and "This isn't either").
	 * <li>On input "No quoted \'text\' here" the method should return an empty list.
	 * </ul>
	 * @param input
	 * @return all occurrences of text between single quotes, taking escaped quotes into account.
	 */
	public List<String> findDoubleQuotedTextWithEscapes(String input){
    ArrayList<String> list = new ArrayList<>();
    Pattern p  = Pattern.compile("(\\\\\")|(\"\")|(\"(((\\\\\")|[^\"])*[^\\\\])\")");
    Matcher m = p.matcher(input);
		while(m.find()) {
        if(m.group(2) == null) {
        	// replace all ** \" ** to ** " **
            if(m.group(1) == null) list.add(m.group(4).replaceAll("\\\\\"", "\""));
        }
        // in case we matched an empty quoted string
        else list.add("");
    }
		return list;
}

    /**
	 * Parse the input into a list of attribute-value pairs.
	 * The input should be a valid attribute-value pair list: attr=value; attr=value; attr; attr=value...
	 * If a value exists, it must be either an HTTP token (see {@link AVPair}) or a double-quoted string.
	 * 
	 * @param input
	 * @return
	 */
	public List<AVPair> parseAvPairs(String input) {
        // TODO: Implement
        return null;
	}


    /**
     * Parse the input into a list of attribute-value pairs, with input checking.
     * The input should be a valid attribute-value pair list: attr=value; attr=value; attr; attr=value...
     * If a value exists, it must be either an HTTP token (see {@link AVPair}) or a double-quoted string.
     *
     * This  method should return null if the input is not a list of attribute-value pairs with the format
     * specified above.
     * @param input
     * @return
     */
    @Override
    public List<AVPair> parseAvPairs2(String input) {
        // TODO: Implement
        return null;
    }
}
