using System;
using System.IO.Ports;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string[] boardDictKeys = { "BOARD_1", "BOARD_2", "BOARD_3" };
        string[] boardDictValues = { "1", "2", "3" };
        // TODO -> Add boards when needs accordingly with C code on Nucleo Board

        SerialPort serialPort = new SerialPort();
        SerInit(serialPort);

        Console.Clear();
        Console.WriteLine("Board to restart:");
        for (int i = 0; i < boardDictKeys.Length; i++)
        {
            Console.WriteLine($"\t\t{boardDictKeys[i],-4} print -> {boardDictValues[i]}");
        }

        string serialCmd = "";
        while (true)
        {
            Console.Write("\nINSERT BOARD TO RESTART: ");
            serialCmd = Console.ReadLine().ToUpper();
            if (Array.IndexOf(boardDictKeys, serialCmd) != -1)
            {
                break;
            }
            Console.WriteLine("Selected board doesn't exist!");
        }

        if (!serialPort.IsOpen)
        {
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening COM port: " + ex.Message);
                Thread.Sleep(3000);
                return;
            }
        }

        PrintComSettings(serialPort);
        serialPort.Write(serialCmd);
        int timeout = 10;
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < timeout)
        {
            byte[] buffer = new byte[32];
            int bytesRead = serialPort.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                Console.Write(System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead));
            }
        }
        serialPort.Close();
        Thread.Sleep(3000);
    }

    static void PrintComSettings(SerialPort serialPort)
    {
        Console.WriteLine("\n- PORT DATA -");
        Console.WriteLine("Sending and receiving port: " + serialPort.PortName);
        Console.WriteLine("\
