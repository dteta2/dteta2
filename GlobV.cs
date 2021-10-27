//## init all needed variables
public static class GlobV
{ 
    public static int anfang   = 0;
    public static int anfangf  = 0;
    public static string full;
    public static string[] tmp;

    public static int persCnt     = 0;
    public static int persMax     = 5000;
    public static int famCnt      = 0;
    public static int famMax      = 5000;
    public static int chilCnt     = 0;
    public static int TchilCnt     = 0;
    public static int chilMax     = 50;
    public static string[] person = new string[persMax];
    public static string[] fam = new string[famMax];
    public static string[] chil = new string[chilMax];

    public static int birt = 0;
    public static int deat = 0;
    public static int chr = 0;
    public static int buri = 0;
    public static int occu = 0;
    public static int conf = 0;

    public static string indi = null;
    public static string surn = null;
    public static string givn = null;
    public static string marn = null;
    public static string sex = null;
    public static string birtplac = null;
    public static string birtdate = null;
    public static string deatplac = null;
    public static string deatdate = null;
    public static string chrdate = null;
    public static string chrplac = null;
    public static string buridate = null;
    public static string buriplac = null;
    public static string reli = null;
    public static string occu2 = null;
    public static string occudate = null;
    public static string occuplac = null;
    public static string confdate = null;
    public static string confplac = null;
    public static string note = null;

    public static string famlist = null;
    public static int marr = 0;
    public static string marrdate = null;
    public static string marrplac = null;
    public static string famindi = null;
    public static string husb = null;
    public static string wife = null;
}

public static class FileVariables
{
    public static string Filep    = @"Full Path to GEDCOM File i.e. C:\\GEDCOM.GED";
    public static string hostport = "Host Name i.e localhost";                            // database host
    public static string DataBase  = "DataBase Name i.e tmp_Familytree";
    public static string User     = "User name i.e user";
    public static string Password = "DataBase Password i.e. mypassword";                                     // ascent01
}

public static class Person_st
{
    public static string persID     = null;
    public static string name       = null;
    public static string vorname    = null;
    public static string marname    = null;
    public static string sex        = null;
    public static string birt_date  = null;
    public static string birt_plac  = null;
    public static string burt_sour  = null;
    public static string taufe_date = null;
    public static string taufe_plac = null;
    public static string taufe_sour = null;
    public static string deat_date  = null;
    public static string deat_plac  = null;
    public static string deat_sour  = null;
    public static string buri_date  = null;
    public static string buri_plac  = null;
    public static string buri_sour  = null;
    public static string occupation = null;
    public static string occu_date  = null;
    public static string occu_plac  = null;
    public static string occu_sour  = null;
    public static string religion   = null;
    public static string confi_date = null;
    public static string confi_plac = null;
    public static string confi_sour = null;
    public static string note       = null;
}
public static class Family
{
    public static string famID     = null;
    public static string husband   = null;
    public static string wife      = null;
    public static string marr_date = null;
    public static string marr_plac = null;
    public static string marr_sour = null;
    public static string marb_date = null;
    public static string marb_plac = null;
    public static string marb_sour = null;
}
public static class FamChild
{
    public static string famID = null;
    public static string child = null;
}

public static class WorkA
{
    public static int LineCount   = 0;
    public static int current_row = 0;
    public static string line;
    public static string[] lines;
    public static string[] words  = new string[10];
    public static string[] id     = new string[10];
}

