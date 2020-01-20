import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) {

        List<String> lines = getLinesFromFile();
        System.out.println("Number of lines found: " + lines.size());
        System.out.println("Starting to process");

        long startTimeWithoutThreads = System.currentTimeMillis();
        workWithoutThreads(lines);
        long elapsedTimeWithoutThreads = (System.currentTimeMillis() - startTimeWithoutThreads);
        System.out.println("Execution time: " + elapsedTimeWithoutThreads);


        long startTimeWithThreads = System.currentTimeMillis();
        workWithThreads(lines);
        long elapsedTimeWithThreads = (System.currentTimeMillis() - startTimeWithThreads);
        System.out.println("Execution time: " + elapsedTimeWithThreads);

    }

    private static void workWithThreads(List<String> lines) {

        int numberOfCoers = Runtime.getRuntime().availableProcessors();
        int numberOfLinesInSublist = lines.size() / numberOfCoers ;
        for (int i = 0; i < numberOfCoers; i++) {
            List<String> SubList = lines.subList(i * numberOfLinesInSublist, (i+1) * numberOfLinesInSublist);
            Worker newWorker = new Worker(SubList);
            Thread workerThread = new Thread(newWorker);

            workerThread.start();
            try
            {
                workerThread.join();
            }
            catch (InterruptedException e)
            {
                e.printStackTrace();
            }
        }
    }

    private static void workWithoutThreads(List<String> lines) {
        Worker worker = new Worker(lines);
        worker.run();
    }

    private static List<String> getLinesFromFile() {

        ArrayList<String> listOfLinesFromTextFile = new ArrayList<>();
        String filePath = "C:\\Windows\\Temp\\Shakespeare.txt";

        try
        {
            BufferedReader bufferForReading = new BufferedReader((new FileReader(filePath)));
            String currentLineOfFile;
            while((currentLineOfFile = bufferForReading.readLine()) != null){
                listOfLinesFromTextFile.add(currentLineOfFile);
            }
        }

        catch (IOException e)
        {
            System.out.println("File not found, or could'nt open, aborting! " + e);
        }

        return listOfLinesFromTextFile ;
    }
}
