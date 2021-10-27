using System.IO;

//--- Working Storage
public static class WS
{ 
    public static string Work;
    public static string MSG;
    public static string EOF = null;

    public static int RlCnt = 0;
    public static string[] Splt;
    public static int SpltCnt = 0;

    //// --- line variables
    //public static string[] LnData;
    //public static string LnDataType;
    //public static int LnDataCnt = 0;
    //public static string LnType;

    //// --- hold variables
    //public static string HldType;
    //public static string HldDataType;


    public static StreamReader reader;
    public static string ConfigP = @"C:\gedcomtodatabase-c\Config.txt";

    public static string line;
    public static string[] lines;
}



