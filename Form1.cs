using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace gedcomtodatabase_c
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //RegexExt.PregReplaceA("input", new string[] pattern[], string[] replacements);

            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            WS.ConfigP = appPath + "\\Config.txt";

            if (!Util.ConfigIt()) return;

            textBoxFile.Text = FileVariables.Filep;
            textBoxHost.Text = FileVariables.hostport;
            textBoxDataset.Text = FileVariables.DataBase;
            textBoxUser.Text = FileVariables.User;
            textBoxPassword.Text = FileVariables.Password;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (!ReadGedCom()) { textBoxMsg.Text = WS.MSG; WS.MSG = ""; return; }
            if (!InsertDataBase()) { textBoxMsg.Text = WS.MSG; WS.MSG = ""; return; }

            textBoxMsg.Text = "All done.";
            textBoxMsg.Text += " |--- Lines in: " + WorkA.LineCount;
            textBoxMsg.Text += " |--- Persons: " + GlobV.persCnt;
            textBoxMsg.Text += " |--- Families: " + GlobV.famCnt;
            textBoxMsg.Text += " |---Children: " + GlobV.TchilCnt;

            if (!DBConnect.CloseConnection()) return;
        }
        private bool ReadGedCom()
        {
            FileVariables.Filep = textBoxFile.Text;
            FileVariables.hostport = textBoxHost.Text;             // database host
            FileVariables.DataBase = textBoxDataset.Text;
            FileVariables.User = textBoxUser.Text;
            FileVariables.Password = textBoxPassword.Text;         // ascent01

            // reset counters
            WorkA.LineCount = 0;
            GlobV.persCnt = 0;
            GlobV.famCnt = 0;
            GlobV.chilCnt = 0;
            GlobV.TchilCnt = 0;

            GlobV.person = new string[GlobV.persMax];
            GlobV.fam = new string[GlobV.famMax];
            GlobV.chil = new string[GlobV.chilMax];


            //-------------------------------------------------------------------------------------\\
            //--- Check if File Exists
            if (!File.Exists(FileVariables.Filep))
            {
                textBoxMsg.Text = "File: " + FileVariables.Filep + " NOT FOUND!";
                return false;
            }

            //--- check Database connection
            DBConnect.Initialize();
            if (!DBConnect.OpenConnection()) return false;
            if (!DBConnect.CloseConnection()) return false;


            textBoxMsg.Text = "Start reading GEDCOM file";
            //MessageBox.Show(textBoxMsg.Text);

            //## read file line per line
            WorkA.lines = File.ReadAllLines(FileVariables.Filep);
            WorkA.LineCount = WorkA.lines.Count();

            for (int lc = 0; lc < WorkA.LineCount; lc++)
            {
                if (RegexExt.PregMatch(new Regex(@"\b0\x20\x40(I.*)\x40"), WorkA.lines[lc], out List<string> person))
                { //## if line starts with 0 @I, a new person is found
                    if (GlobV.anfang == 1)
                    { //## if next person entry found, write previous to array
                        GlobV.person[GlobV.persCnt] = GlobV.indi + ";" + GlobV.surn + ";" + GlobV.givn + ";" + GlobV.marn + ";" + GlobV.sex + ";" + GlobV.birtdate + ";" + GlobV.birtplac + ";" + GlobV.chrdate + ";" + GlobV.chrplac + ";" + GlobV.deatdate + ";" + GlobV.deatplac + ";" + GlobV.buridate + ";" + GlobV.buriplac + ";" + GlobV.occu2 + ";" + GlobV.occudate + ";" + GlobV.occuplac + ";" + GlobV.reli + ";" + GlobV.confdate + ";" + GlobV.confplac + ";" + GlobV.note; //# fill person entry data in array
                        GlobV.persCnt++;
                        //## reset variables for to read next entry
                        GlobV.deat = 0;
                        GlobV.chr = 0;
                        GlobV.buri = 0;
                        GlobV.occu = 0;
                        GlobV.conf = 0;
                        GlobV.birt = 0;
                        GlobV.marr = 0;

                        GlobV.indi = null;
                        GlobV.surn = null;
                        GlobV.givn = null;
                        GlobV.marn = null;
                        GlobV.sex = null;
                        GlobV.birtplac = null;
                        GlobV.birtdate = null;
                        GlobV.deatplac = null;
                        GlobV.deatdate = null;
                        GlobV.chrdate = null;
                        GlobV.chrplac = null;
                        GlobV.buridate = null;
                        GlobV.buriplac = null;
                        GlobV.reli = null;
                        GlobV.occu2 = null;
                        GlobV.occudate = null;
                        GlobV.occuplac = null;
                        GlobV.confdate = null;
                        GlobV.confplac = null;
                        GlobV.note = null;
                    }
                    else
                    {
                        textBoxMsg.Text = "Start reading Person data";
                        //MessageBox.Show(textBoxMsg.Text);
                    }
                    GlobV.indi = person[1]; //## set indi variable to new value
                    GlobV.indi = RegexExt.PregReplace(GlobV.indi, "\x27", "\xB4");
                    GlobV.anfang = 1;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20SURN\x20(.*)"), WorkA.lines[lc], out List<string> surnA))
                {
                    GlobV.surn = RegexExt.PregReplace(surnA[1], "\x27", "\xB4");
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20GIVN\x20(.*)"), WorkA.lines[lc], out List<string> givnA))
                {
                    GlobV.givn = RegexExt.PregReplace(givnA[1], "\x27", "\xB4");
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20NAME\x20(.*)"), WorkA.lines[lc], out List<string> nameA))
                {
                    GlobV.full = RegexExt.PregReplace(nameA[1], "\x27", "\xB4");
                    GlobV.tmp = GlobV.full.Split('/');
                    WS.SpltCnt = GlobV.tmp.Count();
                    if (WS.SpltCnt == 2)
                    {
                        GlobV.givn = GlobV.tmp[0];
                        GlobV.surn = GlobV.tmp[1];
                    }
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20_MARNM\x20(.*)"), WorkA.lines[lc], out List<string> marnA))
                {
                    GlobV.marn = RegexExt.PregReplace(marnA[1], "\x27", "\xB4");
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20SEX\x20(.*)"), WorkA.lines[lc], out List<string> sexA))
                {
                    GlobV.sex = sexA[1];
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20BIRT"), WorkA.lines[lc], out List<string> birtA))
                {
                    GlobV.deat = 0;
                    GlobV.chr = 0;
                    GlobV.buri = 0;
                    GlobV.occu = 0;
                    GlobV.conf = 0;
                    GlobV.birt = 1;
                    GlobV.marr = 0;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20DEAT"), WorkA.lines[lc], out List<string> deatA))
                {
                    GlobV.birt = 0;
                    GlobV.chr = 0;
                    GlobV.buri = 0;
                    GlobV.occu = 0;
                    GlobV.conf = 0;
                    GlobV.deat = 1;
                    GlobV.marr = 0;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20CHR"), WorkA.lines[lc], out List<string> chrA))
                {
                    GlobV.birt = 0;
                    GlobV.chr = 1;
                    GlobV.buri = 0;
                    GlobV.occu = 0;
                    GlobV.conf = 0;
                    GlobV.deat = 0;
                    GlobV.marr = 0;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20BURI"), WorkA.lines[lc], out List<string> buriA))
                {
                    GlobV.birt = 0;
                    GlobV.chr = 0;
                    GlobV.buri = 1;
                    GlobV.occu = 0;
                    GlobV.conf = 0;
                    GlobV.deat = 0;
                    GlobV.marr = 0;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20OCCU\x20(.*)"), WorkA.lines[lc], out List<string> occu2A))
                {
                    GlobV.birt = 0;
                    GlobV.chr = 0;
                    GlobV.buri = 0;
                    GlobV.occu = 1;
                    GlobV.conf = 0;
                    GlobV.deat = 0;
                    GlobV.marr = 0;
                    GlobV.occu2 = RegexExt.PregReplace(occu2A[1], "\x27", "\xB4");
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20CONF"), WorkA.lines[lc], out List<string> confA))
                {
                    GlobV.birt = 0;
                    GlobV.chr = 0;
                    GlobV.buri = 0;
                    GlobV.occu = 0;
                    GlobV.conf = 1;
                    GlobV.deat = 0;
                    GlobV.marr = 0;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20MARR"), WorkA.lines[lc], out List<string> marrA))
                {
                    GlobV.marr = 1;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20DATE\x20(.*)"), WorkA.lines[lc], out List<string> givenDate))
                {
                    if (GlobV.birt == 1)
                    {
                        GlobV.birtdate = givenDate[1];                                          //## ...birth
                        GlobV.birtdate = RegexExt.PregReplace(GlobV.birtdate, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.deat == 1)
                    {
                        GlobV.deatdate = givenDate[1];                                          //## ...death
                        GlobV.deatdate = RegexExt.PregReplace(GlobV.deatdate, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.chr == 1)
                    {
                        GlobV.chrdate = givenDate[1];                                           //## ...christening
                        GlobV.chrdate = RegexExt.PregReplace(GlobV.chrdate, "\x27", "\xB4");     //## replace ' with ` because of database relevance
                    }
                    if (GlobV.buri == 1)
                    {
                        GlobV.buridate = givenDate[1];                                          //## ...burial
                        GlobV.buridate = RegexExt.PregReplace(GlobV.buridate, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.occu == 1)
                    {
                        GlobV.occudate = givenDate[1];                                          //## ...occupation
                        GlobV.occudate = RegexExt.PregReplace(GlobV.occudate, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.conf == 1)
                    {
                        GlobV.confdate = givenDate[1];                                          //## ...confirmation
                        GlobV.confdate = RegexExt.PregReplace(GlobV.confdate, "\x27", "\xB4");    //## replace ' with ` because of database relevance
                    }
                    if (GlobV.marr == 1)
                    {
                        GlobV.marrdate = givenDate[1];                                          //## ...marriage
                        GlobV.marrdate = RegexExt.PregReplace(GlobV.marrdate, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20PLAC\x20(.*)"), WorkA.lines[lc], out List<string> givenPlac))
                {
                    if (GlobV.birt == 1)
                    {
                        GlobV.birtplac = givenPlac[1];                                          //## ...birth
                        GlobV.birtplac = RegexExt.PregReplace(GlobV.birtplac, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.deat == 1)
                    {
                        GlobV.deatplac = givenPlac[1];                                          //## ...death
                        GlobV.deatplac = RegexExt.PregReplace(GlobV.deatplac, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.chr == 1)
                    {
                        GlobV.chrplac = givenPlac[1];                                           //## ...christening
                        GlobV.chrplac = RegexExt.PregReplace(GlobV.chrplac, "\x27", "\xB4");     //## replace ' with ` because of database relevance
                    }
                    if (GlobV.buri == 1)
                    {
                        GlobV.buriplac = givenPlac[1];                                          //## ...burial
                        GlobV.buriplac = RegexExt.PregReplace(GlobV.buriplac, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.occu == 1)
                    {
                        GlobV.occuplac = givenPlac[1];                                          //## ...occupation
                        GlobV.occuplac = RegexExt.PregReplace(GlobV.occuplac, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                    if (GlobV.conf == 1)
                    {
                        GlobV.confplac = givenPlac[1];                                          //## ...confirmation
                        GlobV.confplac = RegexExt.PregReplace(GlobV.confplac, "\x27", "\xB4");    //## replace ' with ` because of database relevance
                    }
                    if (GlobV.marr == 1)
                    {
                        GlobV.marrplac = givenPlac[1];                                          //## ...marriage
                        GlobV.marrplac = RegexExt.PregReplace(GlobV.marrplac, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                    }
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20RELI\x20(.*)"), WorkA.lines[lc], out List<string> reliA))
                {
                    GlobV.reli = RegexExt.PregReplace(reliA[1], "\x27", "\xB4");   //## replace ' with ` because of database relevance
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20NOTE\x20(.*)"), WorkA.lines[lc], out List<string> noteA))
                {
                    GlobV.note = RegexExt.PregReplace(noteA[1], "\x27", "\xB4");   //## replace ' with ` because of database relevance
                }
                else if (RegexExt.PregMatch(new Regex(@"\b2\x20CONC\x20(.*)"), WorkA.lines[lc], out List<string> concA))
                {
                    GlobV.note += concA[1];
                    GlobV.note = RegexExt.PregReplace(GlobV.note, "\x27", "\xB4");   //## replace ' with ` because of database relevance
                }
                else if (RegexExt.PregMatch(new Regex(@"\b0\x20\x40(F.*)\x40"), WorkA.lines[lc], out List<string> famindiA))
                {  //## a new family entry is found
                    if (GlobV.anfangf == 1)
                    { //## if next family entry found, write previous to array
                        GlobV.famlist = GlobV.famindi + ";" + GlobV.husb + ";" + GlobV.wife + ";" + GlobV.marrdate + ";" + GlobV.marrplac;
                        foreach (string entry in GlobV.chil)
                        {
                            if (entry != null)
                            {
                                GlobV.famlist += ";" + entry;
                            }
                        }
                        GlobV.fam[GlobV.famCnt] = GlobV.famlist;                                     //## fill family entry data in array

                        GlobV.famlist = null;
                        GlobV.marr = 0;
                        GlobV.marrdate = null;
                        GlobV.marrplac = null;
                        GlobV.famindi = null;
                        GlobV.husb = null;
                        GlobV.wife = null;
                        GlobV.chil = new string[GlobV.chilMax];
                        GlobV.chilCnt = 0;
                        GlobV.famCnt++;
                    }
                    if (GlobV.anfangf == 0)
                    {                                                   //## if first family entry found, write last person entry to array
                        textBoxMsg.Text = "Start reading family data";
                        //MessageBox.Show(textBoxMsg.Text);
                        //array_push(GlobV.person, GlobV.indi + ";" + GlobV.surn + ";" + GlobV.givn + ";" + GlobV.marn + ";" + GlobV.sex + ";" + GlobV.birtdate + ";" + GlobV.birtplac + ";" + GlobV.chrdate + ";" + GlobV.chrplac + ";" + GlobV.deatdate + ";" + GlobV.deatplac + ";" + GlobV.buridate + ";" + GlobV.buriplac + ";" + GlobV.occu2 + ";" + GlobV.occudate + ";" + GlobV.occuplac + ";" + GlobV.reli + ";" + GlobV.confdate + ";" + GlobV.confplac + ";" + GlobV.note);
                        GlobV.person[GlobV.persCnt] = GlobV.indi + ";" + GlobV.surn + ";" + GlobV.givn + ";" + GlobV.marn + ";" + GlobV.sex + ";" + GlobV.birtdate + ";" + GlobV.birtplac + ";" + GlobV.chrdate + ";" + GlobV.chrplac + ";" + GlobV.deatdate + ";" + GlobV.deatplac + ";" + GlobV.buridate + ";" + GlobV.buriplac + ";" + GlobV.occu2 + ";" + GlobV.occudate + ";" + GlobV.occuplac + ";" + GlobV.reli + ";" + GlobV.confdate + ";" + GlobV.confplac + ";" + GlobV.note; //# fill person entry data in array
                        GlobV.persCnt++;

                        //## reset all variables of person, start reading family data
                        GlobV.birt = 0;
                        GlobV.deat = 0;
                        GlobV.chr = 0;
                        GlobV.buri = 0;
                        GlobV.occu = 0;
                        GlobV.conf = 0;

                        GlobV.indi = null;
                        GlobV.surn = null;
                        GlobV.givn = null;
                        GlobV.sex = null;
                        GlobV.birtplac = null;
                        GlobV.birtdate = null;
                        GlobV.deatplac = null;
                        GlobV.deatdate = null;
                        GlobV.chrdate = null;
                        GlobV.chrplac = null;
                        GlobV.buridate = null;
                        GlobV.buriplac = null;
                        GlobV.reli = null;
                        GlobV.occu2 = null;
                        GlobV.occudate = null;
                        GlobV.occuplac = null;
                        GlobV.confdate = null;
                        GlobV.confplac = null;
                        GlobV.note = null;
                        GlobV.anfangf = 1;
                    }
                    GlobV.famindi = famindiA[1];
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20HUSB\x20\x40(.*)\x40"), WorkA.lines[lc], out List<string> husbA))
                { //## husband entry found
                    GlobV.husb = RegexExt.PregReplace(husbA[1], "\x27", "\xB4");                          //## replace ' with ` because of database relevance
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20WIFE\x20\x40(.*)\x40"), WorkA.lines[lc], out List<string> wifeA))
                { //## wife entry found
                    GlobV.wife = RegexExt.PregReplace(wifeA[1], "\x27", "\xB4");                          //## replace ' with ` because of database relevance
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20CHIL\x20\x40(.*)\x40"), WorkA.lines[lc], out List<string> cA))
                { //## child entry found
                    string c = RegexExt.PregReplace(cA[1], "\x27", "\xB4");                                //## replace ' with ` because of database relevance
                                                                                                           //array_push(GlobV.chil, c);
                    GlobV.chil[GlobV.chilCnt] = c;
                    GlobV.chilCnt++;
                    GlobV.TchilCnt++;
                    //##################################################################################################################
                    //##################################################################################################################
                }
                else if (RegexExt.PregMatch(new Regex(@"\b1\x20CHAN"), WorkA.lines[lc], out List<string> chanA))
                { //## End of person entry found, reset controling variables
                    GlobV.deat = 0;
                    GlobV.chr = 0;
                    GlobV.buri = 0;
                    GlobV.occu = 0;
                    GlobV.conf = 0;
                    GlobV.birt = 0;
                    GlobV.marr = 0;
                }
                else if (RegexExt.PregMatch(new Regex(@"\b0\x20TRLR"), WorkA.lines[lc], out List<string> trlrA))
                { //## End of file reached, save last family entry to array
                    GlobV.famlist = GlobV.famindi + ";" + GlobV.husb + ";" + GlobV.wife + ";" + GlobV.marrdate + ";" + GlobV.marrplac;
                    foreach (string entry in GlobV.chil)
                    {
                        if (entry != null)
                        {
                            GlobV.famlist += ";" + entry;
                        }
                    }
                    //array_push(GlobV.fam, GlobV.famlist);
                    GlobV.fam[GlobV.famCnt] = GlobV.famlist;
                    GlobV.famCnt++;
                }
            }
            textBoxMsg.Text = "Finish reading GEDCOM file.\nStart inserting in database";


            return true;
        }
    
        private bool InsertDataBase()
        {
            //--- check Database connection
            DBConnect.Initialize();
            if (!DBConnect.OpenConnection()) return false;

            //## Empty all tables first
            string delete;

            delete = "DELETE FROM person_st;";
            if (!DBConnect.Delete(delete)) return false;

            delete = "DELETE FROM family;";
            if (!DBConnect.Delete(delete)) return false;

            delete = "DELETE FROM famchild;";
            if (!DBConnect.Delete(delete)) return false;

            textBoxMsg.Text ="Start inserting person data";
            //##################################################################################################################
            //##################################################################################################################
            //## fill person_st table from array
            //foreach ($person as $entry)
            for (int i = 0; i < GlobV.persCnt; i++)
            {
                GlobV.tmp = GlobV.person[i].Split('\x3B');
                WS.SpltCnt = GlobV.tmp.Count();
                for (int s = 0; s < WS.SpltCnt; s++)
                {
                    GlobV.tmp[s].Replace("\xB4", "''");
                }
                //## create single insert statement
                string insert = "INSERT INTO person_st (`persID`, `name`, `vorname`, `marname`, `sex`, `birt_date`, `birt_plac`, ";
                insert += "`taufe_date`, `taufe_plac`, `deat_date`, `deat_plac`, `buri_date`, `buri_plac`, ";
                insert += "`occupation`, `occu_date`, `occu_plac`, `religion`, `confi_date`, `confi_plac`, `note`) ";
                insert += "VALUES('" + GlobV.tmp[0] + "', '" + GlobV.tmp[1] + "', '" + GlobV.tmp[2] + "', '" + GlobV.tmp[3] + "', '" + GlobV.tmp[4] + "', '" + GlobV.tmp[5] + "', '" + GlobV.tmp[6] + "', ";
                insert += "'" + GlobV.tmp[7] + "', '" + GlobV.tmp[8] + "', '" + GlobV.tmp[9] + "', '" + GlobV.tmp[10] + "', '" + GlobV.tmp[11] + "', '" + GlobV.tmp[12] + "', '" + GlobV.tmp[13] + "', ";
                insert += "'" + GlobV.tmp[14] + "', '" + GlobV.tmp[15] + "', '" + GlobV.tmp[16] + "', '" + GlobV.tmp[17] + "', '" + GlobV.tmp[18] + "', '" + GlobV.tmp[19] + "')";
                //## replace special characters
                //$insert = replaceSpecialChars($insert);
                //## insert into DB
                //mysqli_query($link, $insert)or die ("SQL-Fehler fill person table ".mysqli_error($link));
                if (!DBConnect.Insert(insert)) { WS.MSG += " Person: " + i + insert; return false; }
            }


            textBoxMsg.Text = "Start inserting family data";
            //##################################################################################################################
            //##################################################################################################################
            //## fill family table
            //foreach($fam as $entry)
            for (int i = 0; i < GlobV.famCnt; i++)
            {
                GlobV.tmp = GlobV.fam[i].Split('\x3B');
                WS.SpltCnt = GlobV.tmp.Count();
                string insert2 = "INSERT INTO family(`famID`, `husband`, `wife`, `marr_date`, `marr_plac`) VALUES(";
                insert2 += "'" + GlobV.tmp[0] + "', '"+ GlobV.tmp[1] + "', '"+ GlobV.tmp[2] + "', '" + GlobV.tmp[3] + "', '" + GlobV.tmp[4] + "');";
                //insert2 = replaceSpecialChars(insert2);
                //mysqli_query($link, $insert2)or die("SQL-Fehler fill family table ".mysqli_error($link));
                if (!DBConnect.Insert(insert2)) return false;

                //## fill famchild table for connection between person and family
                int t = 5;
                while (t < WS.SpltCnt)
                {
                    string insert3 = "INSERT INTO famchild(`famID`, `child`) VALUES('" + GlobV.tmp[0] + "', '" + GlobV.tmp[t] + "');";
                    //insert3 = replaceSpecialChars(insert3);
                    //mysqli_query($link, $insert3)or die("SQL-Fehler fill family childs table ".mysqli_error($link));
                    if (!DBConnect.Insert(insert3)) { 
                        return false; }
                    t++;
                }
            }
            return true;
        }
    }
}
