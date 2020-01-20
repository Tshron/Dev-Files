package net;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.List;
import java.util.ListIterator;

public class HelloServer implements API.HelloServer {
    private ServerSocket serverSocket;

    /**
     * Should return the ServerSocket object set up by the HelloServer.
     *
     * This method may return null if it is called before the {@link #listen()} or {@link #listen(List)} are called.
     * Otherwise, the ServerSocket should be listening on the ports (calling {@link ServerSocket#accept()} should return
     * the next incoming connection).
     * @return
     */
    public ServerSocket getServerSocket() {
        return this.serverSocket ; // construct a new ServerSocket.
    }

    /**
     * Listen on the first available port in a given list.
     * <p>
     * <p>Note: Should not throw exceptions due to ports being unavailable</p>
     *
     * @return The port number chosen, or -1 if none of the ports were available.
     */
    public int listen(List<Integer> portList) throws IOException {
        for(int port : portList) {
            try {
                this.serverSocket = new ServerSocket(port); // trying to listen to port from the list
                return port;
            }catch (IOException e) { // if the port is unavailable try another one

            }
        }
        return -1;
    }


    /**
     * Listen on an available port.
     * Any available port may be chosen.
     *
     * @return The port number chosen.
     */
    public int listen() throws IOException {
        this.serverSocket = new ServerSocket(0);
        return serverSocket.getLocalPort();
    }


    /**
     * 1. Start listening on an open port. Write {@link #LISTEN_MESSAGE} followed by the port number (and a newline) to sysout.
     * If there's an IOException at this stage, exit the method.
     * <p>
     * 2. Run in a loop;
     * in each iteration of the loop, wait for a client to connect,
     * then read a line of text from the client. If the text is {@link #BYE_MESSAGE},
     * send {@link #BYE_MESSAGE} to the client and exit the loop. Otherwise, send {@link #HELLO_MESSAGE}
     * to the client, followed by the string sent by the client (and a newline)
     * After sending the hello message, close the client connection and wait for the next client to connect.
     * <p>
     * If there's an IOException while in the loop, or if the client closes the connection before sending a line of text,
     * send the text {@link #ERR_MESSAGE} to sysout, but continue to the next iteration of the loop.
     * <p>
     * *: in any case, before exiting the method you must close the server socket.
     *
     * @param sysout a {@link PrintStream} to which the console messages are sent.
     */
    public void run(PrintStream sysout) {
        try{
            // trying to listen on an open port, if succeeded print message and the port number
            sysout.println(LISTEN_MESSAGE + listen());
        }catch (IOException e) { // end the run if can't achieved an available port.
            return;
        }

        while(true) {
            try{
                Socket client = serverSocket.accept(); // connecting to a client server
                BufferedReader reader = new BufferedReader( new InputStreamReader(client.getInputStream()));
                PrintWriter writer = new PrintWriter((new OutputStreamWriter(client.getOutputStream())));
                String line = reader.readLine();
                // if the client isn't feeling like talking
                if(line.equals(BYE_MESSAGE)) {
                    writer.println(BYE_MESSAGE);
                    writer.flush();
                    client.close(); // shutting down client side
                    this.serverSocket.close(); // shutting down server.
                    break;
                }
                // if the client decides to talk
                // sey hello to him and close the connection.
                writer.println(HELLO_MESSAGE + line);
                writer.flush();
                client.close(); //
                continue;


            }catch (IOException e){
                sysout.println(ERR_MESSAGE);
                continue;
            }
        }

        try{
            this.serverSocket.close();
        }catch (IOException e){

        }
    }


    /**
     * This is for your own testing.
     * @param args
     */
    public static void main(String args[]) {
        HelloServer server = new HelloServer();

        server.run(System.err);
    }

}
