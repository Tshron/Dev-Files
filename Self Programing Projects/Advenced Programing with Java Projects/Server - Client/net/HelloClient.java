package net;

import java.io.*;
import java.net.Socket;


public class HelloClient implements API.HelloClient {

    Socket clientSocket;

    /**
     * Connect to a remote host using TCP/IP and set {@link #clientSocket} to be the
     * resulting socket object.
     *
     * @param host remote host to connect to.
     * @param port remote port to connect to.
     * @throws IOException
     */
    public void connect(String host, int port) throws IOException {
        try{
            clientSocket = new Socket(host, port);

        }catch (IOException e) {

        }


    }

    /**
     * Perform the following actions {@link #COUNT} times in a row: 1. Connect
     * to the remote server (host:port). 2. Write the string in myname (followed
     * by newline) to the server 3. Read one line of response from the server,
     * write it to sysout (without the trailing newline) 4. Close the socket.
     * <p>
     * Then do the following (only once): 1. send
     * {@link API.HelloServer#BYE_MESSAGE} to the server (followed by newline). 2.
     * Read one line of response from the server, write it to sysout (without
     * the trailing newline)
     * <p>
     * If there are any IO Errors during the execution, output {@link API.HelloServer#ERR_MESSAGE}
     * (followed by newline) to sysout. If the error is inside the loop,
     * continue to the next iteration of the loop. Otherwise exit the method.
     *
     * @param sysout
     * @param host
     * @param port
     * @param myname
     */
    public void run(PrintStream sysout, String host, int port, String myname) {

        BufferedReader reader = null;
        PrintWriter writer = null;
        String response = "";
        for (int i = 0; i < COUNT ; i++) {
            try {
                // connecting to the server
                connect(host,port);
                 writer = new PrintWriter(new OutputStreamWriter(clientSocket.getOutputStream()));
                 reader = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                // writing client's name to server
                writer.println(myname + "\n");
                writer.flush();
                // receiving feedback from the server
                response  = reader.readLine();
                sysout.print(response);
                sysout.flush();
                // terminating the connection
                clientSocket.close();
            }catch (IOException e) {
                sysout.println(API.HelloServer.ERR_MESSAGE );
                sysout.flush();
                continue;
            }
        }
         // doing it one more time
        try{
            connect(host,port);
            writer = new PrintWriter(new OutputStreamWriter(clientSocket.getOutputStream()));
            reader = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            writer.write(API.HelloServer.BYE_MESSAGE + "\n");
            writer.flush();
            response  = reader.readLine();
            sysout.print(response);
            sysout.flush();
            writer.close();
            reader.close();


        }catch (IOException e) {
            sysout.println(API.HelloServer.ERR_MESSAGE);
            sysout.flush();
            return;
        }


    }
}
