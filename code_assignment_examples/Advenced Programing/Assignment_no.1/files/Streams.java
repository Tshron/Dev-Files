package files;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.Reader;
import java.util.LinkedList;
import java.util.List;

public class Streams {
	/**
	 * Read from an InputStream until a quote character (") is found, then read
	 * until another quote character is found and return the bytes in between the two quotes. 
	 * If no quote character was found return null, if only one, return the bytes from the quote to the end of the stream.
	 * @param in
	 * @return A list containing the bytes between the first occurrence of a quote character and the second.
	 */
	public static List<Byte> getQuoted(InputStream in) throws IOException {
		 List <Byte> res = new LinkedList<>() ;
		 int c ;
		 boolean flag = false;
		try {
			 while ((c = in.read()) != -1) {
				 // read the bytes until you see a qoute.
				 if (c == (int)('"')) {
				 	flag = true;
					 // until you see the second qoute, write the stream's byte to a list
					 while ((c = in.read()) != (int)('"')) {
						 if (c == -1) break;
						 res.add((byte) c);
					 }
					 return res;
				 }
			 }
		 }
		catch (IOException e) {
				 System.err.println("Error : " + e);
		 } finally {
			if (!flag) return null;
		 }
		 return res;
	}
	
	
	/**
	 * Read from the input until a specific string is read, return the string read up to (not including) the endMark.
	 * @param in the Reader to read from
	 * @param endMark the string indicating to stop reading. 
	 * @return The string read up to (not including) the endMark (if the endMark is not found, return up to the end of the stream).
	 */
	public static String readUntil(Reader in, String endMark) throws IOException {
		StringBuilder st = new StringBuilder();
		int c ;
		try {
			while ((c = in.read()) != -1) {
				in.mark(endMark.length()); // mark the position in case we suspect we reached the end mark.
				if (c == endMark.charAt(0)) {
					int i = 0;
					// iterate over the endMark chars and compare them to the read.
					while (i < endMark.length() && c == endMark.charAt(i)) {
						c = in.read();
						i++;
					}
					// if we spot the endMark
					if (i == endMark.length()) return st.toString();
						// if that it is not the endMark, append the chars from the place we marked.
					else {
						// appending the first char of the endMark due to marking after reading it( position moved forward).
						st.append(endMark.charAt(0)); //
						in.reset();
						c = in.read();
						st.append((char) c);
					}
				} else {
					st.append((char) c);
				}
			}
		}catch (IOException e) {
			System.err.println("Error: " + e);
		}finally {
			return st.toString();
		}
	}
	
	/**
	 * Copy bytes from input to output, ignoring all occurrences of badByte.
	 * @param in
	 * @param out
	 * @param badByte
	 */
	public static void filterOut(InputStream in, OutputStream out, byte badByte) throws IOException {
		try {
			int c;
			while ((c = in.read()) != -1) {
				if ((byte) c != badByte) {
					out.write(c);
				}
			}
		} catch (IOException e) {
			System.err.println("Error: " + e);
		}
	}
	
	/**
	 * Read a 48-bit (unsigned) integer from the stream and return it. The number is represented as five bytes, 
	 * with the most-significant byte first. 
	 * If the stream ends before 5 bytes are read, return -1.
	 * @param in
	 * @return the number read from the stream
	 */
	public static long readNumber(InputStream in) throws IOException {
		long res = 0;
		boolean flag = false;
		try {
			for (int i = 4; i >= 0; i--) { // trying to read and sum 5 bytes.
				int temp = in.read();
				if(temp == -1) flag = true; // flag if there is less then 5 bytes to read
				res |= temp;
				 if(i > 0) res <<= 8;
			}
			if (flag) return -1;
		}catch (IOException e) {
			System.err.println("Error: "+ e);
		}finally {
			return res;
		}
	}
}
