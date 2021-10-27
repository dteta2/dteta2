using System;
using System.IO;

namespace gedcomtodatabase_c
{
    public static class Util
    {
        //--- Check if File Exists
        public static bool IfFileExists(string FileName)
        {
            bool rv = false;
            if (!File.Exists(FileName))
            {
                WS.MSG = "--- NOT FOUND ---!";
                rv = false;
            }
            else
            {
                rv = true;
            }
            return rv;
        }

        public static bool OpenReader(string FileName)
        {
            bool rv = false;

            try
            {
                WS.reader = new StreamReader(FileName);
                rv = true;
            }
            catch (Exception exp)
            {
                WS.MSG = exp.Message;
                rv = false;
            }
            return rv;
        }

        public static bool ReadFile(string FilePath)
        {
            bool rv = false;

            try
            {
                WS.lines = File.ReadAllLines(FilePath);
                rv = true;
            }
            catch (Exception exp)
            {
                WS.MSG = exp.Message;
                rv = false;
            }
            return rv;
        }

        public static bool ReadLine()
        {
            try
            {
                WS.line = WS.reader.ReadLine();
                if (WS.line != null) 
                {
                    WS.RlCnt++;
                    return true;
                }
                else
                {
                    return false;  // end of file
                }
            }
            catch (Exception exp)
            {
                WS.MSG = exp.Message;
                return false;
            }
        }

        public static bool ConfigIt()
        {
            if (!Util.IfFileExists(WS.ConfigP))
            {
                 return false;
            }
            if (!Util.OpenReader(WS.ConfigP))
            {
                return false;
            }
            int c = 0;
            while ((WS.line = WS.reader.ReadLine()) != null)
            {
                WS.Splt = WS.line.Split('=');

                switch (WS.Splt[0])
                {
                    case "Filep":
                        FileVariables.Filep = WS.Splt[1];
                        c++;
                        break;
                    case "hostport":
                        FileVariables.hostport = WS.Splt[1];
                        c++;
                        break;
                    case "DataBase":
                        FileVariables.DataBase = WS.Splt[1];
                        c++;
                        break;
                    case "User":
                        FileVariables.User = WS.Splt[1];
                        c++;
                        break;
                    case "Password":
                        FileVariables.Password = WS.Splt[1];
                        c++;
                        break;
                    default:
                        break;
                }
            }
            if (c == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}