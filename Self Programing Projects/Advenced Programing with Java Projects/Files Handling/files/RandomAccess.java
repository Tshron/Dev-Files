package files;

import java.io.IOException;
import java.io.RandomAccessFile;

public class RandomAccess {
	
	/**
	 * Treat the file as an array of (unsigned) 8-bit values and sort them 
	 * in-place using a bubble-sort algorithm.
	 * You may not read the whole file into memory! 
	 * @param file
	 */
	public static void sortBytes(RandomAccessFile file) throws IOException {
		int a, b;
		try {
			for (int i = 0; i < file.length() - 1; i++) {
				int count = 0;
				for (int j = 0; j < file.length() - i - 1; j++) {
					file.seek(j);
					if ((a = file.read()) > (b = file.read())) {
						// swap bytes
						file.seek(j);
						file.write(b);
						file.write(a);
						count++;
					}
				}
				// if no swap made on iterate, sorting done.
				if (count == 0) break;
			}
		}catch (IOException e){
			System.err.println("Error: " + e);
		}
	}
	
	/**
	 * Treat the file as an array of unsigned 24-bit values (stored MSB first) and sort
	 * them in-place using a bubble-sort algorithm. 
	 * You may not read the whole file into memory! 
	 * @param file
	 * @throws IOException
	 */
	public static void sortTriBytes(RandomAccessFile file) throws IOException {
		int[] a = new int[3];
		int[] b = new int[3];
		for (int i = 0; i < ((file.length() / 3) ); i++) {
			int count = 0;
			for (int j = 0; j <= (file.length() - 6 - (i * 3)) ; j+=3) {
				// filling each 3 bytes to an array representing the int where MSB first in array
				for (int k = 0; k < 3 ; k++) {
					file.seek(j+k);
					a[k] = file.readUnsignedByte();
					file.seek(j + k + 3);
					b[k] = file.readUnsignedByte();
				}
				boolean flag = false;
				int z = 0;
				// determine which int is greater
				// compare parallel bytes.
				while(z < 3) {
					if(a[z] < b[z]) break;
					else if (a[z] > b[z]) {
							flag = true ;
							break;
					}
					else z++;
				}
				// in case a swap is needed
				if (flag == true) {
					for (int k = 0; k < 3; k++) {
						file.seek(j + k);
						file.write(b[k]);
						file.seek(j + k + 3);
						file.write(a[k]);
						count++;
					}
				}
			}
			// if no swap made on iterate, sorting done.
			if (count == 0) break;
		}
	}
}
