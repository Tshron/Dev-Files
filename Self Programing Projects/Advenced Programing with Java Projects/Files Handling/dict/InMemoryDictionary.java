package dict;

import java.io.*;
import java.util.TreeMap;
import java.util.*;


/**
 * Implements a persistent dictionary that can be held entirely in memory.
 * When flushed, it writes the entire dictionary back to a file.
 * 
 * The file format has one keyword per line:
 * <pre>word:def1:def2:def3,...</pre>
 * 
 * Note that an empty definition list is allowed (in which case the entry would have the form: <pre>word:</pre> 
 * 
 * @author talm
 *
 */
public class InMemoryDictionary extends TreeMap<String,String> implements PersistentDictionary  {
	private static final long serialVersionUID = 1L; // (because we're extending a serializable class)

	private File dictionary;

	public InMemoryDictionary(File dictFile) {
		this.dictionary = dictFile;
	}

	@Override
	public void open() throws IOException {
		if(!this.dictionary.exists()) this.dictionary.createNewFile();//if a new file hasn't been created,will create it now
		try{
			Reader reader = new FileReader(this.dictionary);
			BufferedReader buff_reader = new BufferedReader(reader);
			String line = buff_reader.readLine();//current line will hold the value of each line
			while(line != null) { //while not reached the end of the file
				String key = line.substring(0, line.indexOf(":"));// setting the key value to whatever before the ":"
				String def = ""; // in case it's not an empty definition line
				if(line.length() > line.indexOf(":")) {
					def = line.substring(line.indexOf(":") + 1); // setting the def value to whatever after the ":"
				}
				put(key, def); // inserting the key and definition to the TreeMap
				line = buff_reader.readLine(); // advance to the next line in the file.
			}
			buff_reader.close();
			reader.close();
		}catch (Exception e) {
			System.out.println("Error: " + e);
		}
	}


	@Override
	public void close() throws IOException {
		FileWriter writer;
		try{
			// setting fileWriter and Buffer to write to the text
			writer = new FileWriter(this.dictionary);
			BufferedWriter buff = new BufferedWriter(writer);
			Set<String> keys = this.keySet(); // a set holding the keys from the map
			for (String k : keys){//go through all of the keys
				buff.write(k + ":" + get(k)); // write each line as a --key : def -- format
				buff.newLine(); // go down one line
			}
			buff.close();
			writer.close();
		}catch (Exception e) {
			System.out.println("Error: " + e);
		}

	}
}
