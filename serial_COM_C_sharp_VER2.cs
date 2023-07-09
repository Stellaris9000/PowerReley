using System.IO.Ports;
using System.Threading;

class Program
{
    static Dictionary<string, string> boardDict = new Dictionary<string, string> {
        { "BOARD_1", "1" },
        { "BOARD_2", "2" },
        { "BOARD_3", "3" }
    }; //TODO -> Add boards when needs accordingly with C code on Nucleo Board

    static void print_COM_settings(SerialPort serialPort)
    {
        Console.WriteLine("\n- PORT DATA -\nSending and receiving port: " + serialPort.PortName);
        Console.WriteLine("\tBaudRate = " + serialPort.BaudRate);
        Console.WriteLine("\tDataBits = " + serialPort.DataBits);
        Console.WriteLine("\tStopBits = " + serialPort.StopBits);
        Console.WriteLine("\tParity = " + serialPort.Parity);
        Console.WriteLine("\tHandshake = " + serialPort.Handshake);
    }

    static void ser_init(SerialPort serialPort)
    {
        // Parametri di init della comunicazione seriale
        serialPort.PortName = "COM4";
        serialPort.BaudRate = 115200;
        serialPort.DataBits = 8;
        serialPort.Parity = Parity.None;
        serialPort.StopBits = StopBits.One;
        serialPort.Handshake = Handshake.None;
        serialPort.ReadTimeout = 10000;
        serialPort.WriteTimeout = 10000;
    }

    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Board to restart:");
        foreach (KeyValuePair<string, string> kvp in boardDict)
