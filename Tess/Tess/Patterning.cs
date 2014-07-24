using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;

namespace Tess
{

    class Patterning
    {
        public Dictionary<string, string> patterns;
        public Dictionary<string, string> results;
        public Dictionary<string, string[]> frequentlyMistakenSequences;
        public List<string> text;
        
        enum chars
        {
            ALL,

        };

        public Patterning()
        {
            patterns = new Dictionary<string, string>();
            results = new Dictionary<string, string>();
            frequentlyMistakenSequences = new Dictionary<string, string[]>();
            text = new List<string>();
            this.FillFrequentlyMistakenSequences();
            this.FillPatterns();
            this.InitiateResults();
           // MessageBox.Show(patterns["example"]);
        }

        public Patterning(List<string> t)
        {
            patterns = new Dictionary<string, string>();
            results = new Dictionary<string, string>();
            text = t;
            this.FillFrequentlyMistakenSequences();
            this.FillPatterns();
            this.InitiateResults();
        }

        public void FillPatterns()
        {
           this.patterns.Add("example","płatniości");
        //   patterns["example"]=BuildPattern(patterns["example"]);
           this.patterns.Add("VAT",@".*VAT.*");
           this.patterns.Add("kwota", @"[0-9]+\,[0-9]{2}(\s|$)");
           this.patterns.Add("kod pocztowy", "( [0-9OSI\\|]{2}\\-[0-9OSI\\|]{3}[^-])|(^[0-9OSI\\|]{2}\\-[0-9OSI\\|]{3}[^-])");
            this.patterns.Add("numer faktury",@"");
           // this.patterns.Add("adres email", "[0-9|a-ząęśćźńłóż|_|-]+@[0-9|a-ząęśćźńłóż|_|-]+\\.[a-ząęśćźńłóż]{2,3}");
          //  this.patterns.Add("adres email", "[a-ząęśćźńłóż0-9_.-]+@[a-ząęśćźńłóż0-9_.-]+\\.(\\w)*\\.(\\w{2,4})");
            this.patterns.Add("adres email", @"[0-9|a-ząęśćźńłóż|_|-]*(\.[0-9|a-ząęśćźńłóż|_|-]*)*@[a-ząęśćźńłóż0-9_.-]*\.[a-ząęśćźńłóż]{2,}");

            this.patterns.Add("NIP", @"((\s)(PL)?[0-9OSI\|]{10}\s|[0-9OSI\|]{3}-[0-9OSI\|]{3}-[0-9OSI\|]{2}-[0-9OSI\|]{2})|([0-9OSI\|]{2}-[0-9OSI\|]{2}-[0-9OSI\|]{3}-[0-9OSI\|]{3})|([0-9OSI\|]{2}-[0-9OSI\|]{3}-[0-9OSI\|]{2}-[0-9OSI\|]{3})|([0-9OSI\|]{3}-[0-9OSI\|]{2}-[0-9OSI\|]{3}-[0-9OSI\|]{2})|([0-9OSI\|]{3}-[0-9OSI\|]{2}-[0-9OSI\|]{2}-[0-9OSI\|]{3})");
            this.patterns.Add("numer klienta", "numer klienta[:]?[ ]*([0-9]|-)*");
           // this.patterns.Add("data1","([0-9]{4}-[0-9]{2}-[0-9]{2})|([0-9]{2}/[0-9]{2}/[0-9]{4})|([0-9]{2}.[0-9]{2}.[0-9]{4})");
            //this.patterns.Add("data2", "([0-9]{4}-[0-9]{2}-[0-9]{2})|([0-9]{2}/[0-9]{2}/[0-9]{4})|([0-9]{2}.[0-9]{2}.[0-9]{4})");
            this.patterns.Add("data1", @"([0-9OSI\|]{4}[-|\.|/]{1}[0-9OSI\|]{1,2}[-|\.|/]{1}[0-9OSI\|]{1,2})|([0-9OSI\|]{1,2}[-|\.|/]{1}[0-9OSI\|]{1,2}[-|\.|/]{1}[0-9OSI\|]{4})");
            this.patterns.Add("data2", @"([0-9OSI\|]{4}[-|\.|/]{1}[0-9OSI\|]{1,2}[-|\.|/]{1}[0-9OSI\|]{1,2})|([0-9OSI\|]{1,2}[-|\.|/]{1}[0-9OSI\|]{1,2}[-|\.|/]{1}[0-9OSI\|]{4})");
            this.patterns.Add("data", @"([0-9]{4}[-|\.|/]{1}[0-9]{1,2}[-|\.|/]{1}[0-9]{1,2})|([0-9]{1,2}[-|\.|/]{1}[0-9]{1,2}[-|\.|/]{1}[0-9]{4})");
            this.patterns.Add("REGON", @"(REGON ?:? ?)(((\([0-9]{2}\))([- ]?[0-9]){7})|(([- ]?[0-9]){9}))(\s|$)");
            this.patterns.Add("numer konta", @"([0-9OSI\|]{2} ?[0-9OSI\|]{4} ?[0-9OSI\|]{4} ?[0-9OSI\|]{4} ?[0-9OSI\|]{4} ?[0-9OSI\|]{4} ?[0-9OSI\|]{4})");
          //  this.patterns.Add("ulica", "((ul.)|(ul)|(ul )|(ul. )|(uł )|(uł. )|(uł)|(uł.)|(u1 )|(u1. )|(u1)|(u1.)|(uI )|(uI. )|(uI)|(uI.))([a-ząęśćźńłóż]{3,})");
            //this.patterns.Add("numer ulicy", "((ul.)|(ul)|(ul )|(ul. )|(uł )|(uł. )|(uł)|(uł.)|(u1 )|(u1. )|(u1)|(u1.)|(uI )|(uI. )|(uI)|(uI.))([a-ząęśćźńłóż]{3,})([ ]*[0-9]{1,3})");
            this.patterns.Add("numer ulicy", @"((ul)|(uł)|(u1)|(ui)|(u\|)){1}.? ?([a-ząęśćźńłóż]{3,} *)+( )*[0-9OSI\|]{1,3}[a-ząęśćźńłóża-ząęśćźńłóż]?");
            //this.patterns.Add("strona", @"www\.[^\s]+\.[^\s]+");
            this.patterns.Add("strona", @"www\.[^\s]+\.[^\s]+");
            this.patterns.Add("ulica", "((ul)|(uł)|(u1)|(ui)|(u\\|)){1}.? ?([a-ząęśćźńłóżś]{3,} *)+");
           // this.patterns.Add("ulica", BuildPattern("ul")+"\\.? ?([a-ząęśćźńłóż]{3,} *)+");
           // this.patterns.Add("telefon1", @"\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})((\s)|,)");
            //this.patterns.Add("telefon2", @"\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-]");
            //this.patterns.Add("telefon2", @"(\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-])|([0-9]{3}[\.\- ]?)([0-9]{3}[\.\- ]?[0-9]{3}\W)");
            //this.patterns.Add("telefon2", @"(\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-])|([0-9]{3}[\.\- ]?)([0-9]{3}[\.\- ]?[0-9]{3}\W)");
            this.patterns.Add("telefon2", @"(\s|^)(((\([0-9OSI\|]{2}\))([- ]?[0-9OSI\|]){7})|(([- ]?[0-9OSI\|]){9}))(\s|$)");
            this.patterns.Add("faks", @"(\s|^)(((\([0-9OSI\|]{2}\))([- ]?[0-9OSI\|]){7})|(([- ]?[0-9OSI\|]){9}))(\s|$)");
            this.patterns.Add("telefon1", @"(\s|^)(((\([0-9OSI\|]{2}\))([- ]?[0-9OSI\|]){7})|(([- ]?[0-9OSI\|]){9}))(\s|$)");
            this.patterns.Add("zaplacono", "(zap[lł1I]acono):?.*");
            this.patterns.Add("do zaplaty",@"("+BuildPattern("do zaplaty")+"|"+BuildPattern("należność")+"|"+BuildPattern("naleznosc ogolem")+")");
          //  MessageBox.Show(patterns["do zaplaty"]);
         //   this.patterns.Add("telefon1", @"(\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-])|([0-9]{3}[\.\- ]?)([0-9]{3}[\.\- ]?[0-9]{3})\W");
         //   this.patterns.Add("faks", @"(\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-])|([0-9]{3}[\.\- ]?)([0-9]{3}[\.\- ]?[0-9]{3})\W");
        //    this.patterns.Add("sposob platnosci", "((spos[oó]{1}b p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?).*)|((forma p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?).*)");
           // this.patterns.Add("sposob platnosci", @"((spos[oó]{1}b)|(forma)|(metoda)) ((zap[lł]{1}aty)|(p[lł]{1}atno[sś]{1}ci)).*");
            this.patterns.Add("sposob platnosci", @"((("+BuildPattern("sposób")+")|("+BuildPattern("forma")+")|("+BuildPattern("metoda")+")) ?(("+BuildPattern("zapłaty")+")|("+BuildPattern("płatności")+")))|("+BuildPattern("płatność")+")( |:).*");
         //   ([^0-9]|^)(((\([0-9]{2}\))([- ]?[0-9]){7})|(([- ]?[0-9]){9}))([^0-9]|$)
            //(\s|^)(((\([0-9]{2}\))([- ]?[0-9]){7})|(([- ]?[0-9]){9}))(\s|$)
            //this.patterns.Add("sposob platnosci", "(((spos[oó]{1}b)|(forma)|(metoda)+ (p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci)|(zap[lłt1]{1}a[1ltł]{1}y):?).*)");
          //  this.patterns.Add("razem do zaplaty", "(((spos[oó]{1}b)|(forma) (p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci)|(zap[lłt1]{1}a[1ltł]{1}y):?).*)");

            //   this.
            //  this.patterns.Add("telefon1", "(\\([0-9]{3}\\))? *([0-9]{3}[0-9]{2}[0-9]{2})|([0-9]{7})");
            //this.patterns.Add("telefon1", "(\\([0-9]{3}\\))? *([^-][0-9]{3}-[0-9]{2}-[0-9]{2}[^-])|([0-9]{7})");

        }

        public string BuildPattern( string  pattern)
    {


             string orig = pattern;
            string copy = pattern;
        string nowy= string.Empty;
 
            List<string> sek = new List<string>();
            List<int> ind=new List<int>();
            List<int> len=new List<int>();
            List<KeyValuePair<int, string>> indeksy = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> indeksy2 = new List<KeyValuePair<int, string>>();
            
        
   
            
                foreach (String k in frequentlyMistakenSequences.Keys)
                {
                    foreach (Match m in Regex.Matches(pattern, k))
                    {
                     
                        indeksy.Add(new KeyValuePair<int, string>(m.Index, k));

               
                    }
                }
                indeksy.Sort(
                   delegate(KeyValuePair<int, string> first, KeyValuePair<int, string> second)
                   {
                       return first.Key.CompareTo(second.Key);
                   }
                    );
               // foreach (KeyValuePair<int, string> kvp in indeksy)
               //     MessageBox.Show(kvp.Key + ":" + kvp.Value);
                
                int j = 0;
             int i = 0;
             bool skip = false;
             for ( i = 0; i < indeksy.Count;i++ )
             {
                 //MessageBox.Show(indeksy[i].Key+":"+indeksy[i].Value);
                 if(i<indeksy.Count-1)
                 if(indeksy[i].Key==indeksy[i+1].Key)
                 {
                    // MessageBox.Show(indeksy[i].Key + " ten sam " + indeksy[i+1].Value);
                     if (indeksy[i].Value.Length > indeksy[i + 1].Value.Length)
                     {
                         indeksy.RemoveAt(i + 1);
                         indeksy.RemoveAt(i + 1);
                     }
                     else
                     {
                         indeksy.RemoveAt(i);
                         if(indeksy.Count>i+1)indeksy.RemoveAt(i + 1);
                     }
                 }
               //  if(ind)
             }
             //   foreach (KeyValuePair<int, string> kvp in indeksy)
             //       MessageBox.Show(kvp.Key + "::" + kvp.Value);
               j = 0;
               i = 0;
                 
            foreach (KeyValuePair<int, string> kvp in indeksy)
                 {
             
                     string str = string.Empty;
                     str += "(";
                   
                         str += orig.Substring(kvp.Key, kvp.Value.Length);
                     


                     foreach (string s in frequentlyMistakenSequences[kvp.Value])
                     {

                         str += "|" + s;
                         i += s.Length + 1;


                     }
                     Regex rgx = new Regex(kvp.Value);


                     str += ")";
                     i += 2 + kvp.Value.Length;
                     string copy2 = copy.Substring(j, copy.Length - j);
                     copy2 = rgx.Replace(copy2, str, 1);
                     copy = copy.Substring(0, j) + copy2;
                     j = i;

                 }
               
                return copy;

            /*********************************************/
            /*
         //   foreach()
       // return "";
        string orig = pattern;
            string copy = pattern;
        string nowy= string.Empty;
      //  int i = 0;
      //  int j = 0;
        List<string> sek = new List<string>();
            List<int> ind=new List<int>();
            List<int> len=new List<int>();
            List<KeyValuePair<int, string>> indeksy = new List<KeyValuePair<int, string>>();
            List<KeyValuePair<int, string>> indeksy2 = new List<KeyValuePair<int, string>>();
            
           // while(i<copy.Length)
          //  {
   
            
                foreach (String k in frequentlyMistakenSequences.Keys)
                {
                    foreach (Match m in Regex.Matches(pattern, k))
                    {
                       // if(k.Length==1)
                        indeksy.Add(new KeyValuePair<int, string>(m.Index, k));

                   //     ind.Add(m.Index);
                    //    len.Add(m.Length);
                    //    sek.Add(k);
                    }
                }
                indeksy.Sort(
                   delegate(KeyValuePair<int, string> first, KeyValuePair<int, string> second)
                   {
                       return first.Key.CompareTo(second.Key);
                   }
                    );
                
            //    foreach (KeyValuePair<int, string> kvp in indeksy)
              //      MessageBox.Show(kvp.Key + ":" + kvp.Value);
                int j = 0;
             int i = 0;
             bool skip = false;
            foreach(KeyValuePair<int, string> kvp in indeksy)
            {
                if(skip==true)
                {
                    skip = false;
                    continue;
                }
                string str=string.Empty;
                string dododania = string.Empty;
                str+="([";
               // str+=orig[kvp.Key];
                if(kvp.Value.Length==1)
                str += orig.Substring(kvp.Key, kvp.Value.Length);
                else
                {
                    skip = true;
                    //str +="("+ orig.Substring(kvp.Key, kvp.Value.Length)+")";
                    if (dododania == string.Empty)
                    {
                        dododania += "(" + orig.Substring(kvp.Key, kvp.Value.Length) + "){1}";
                        i = i + orig.Substring(kvp.Key, kvp.Value.Length).Length + 5;
                    }
                    else
                    {
                        dododania += "|(" + orig.Substring(kvp.Key, kvp.Value.Length) + "){1}";
                        i = i + orig.Substring(kvp.Key, kvp.Value.Length).Length + 6;
                    }
                }
            //    dododania += "(";
                foreach(string s in frequentlyMistakenSequences[kvp.Value])
                {
                    if (s.Length == 1)
                    {
                        str += s;
                        i++;
                    }
                    else
                    {
                        //str += "(" + s + ")";
                       // i = i + s.Length+2;
                        if (dododania == string.Empty)
                        {
                            dododania += "(" + s + "){1}";
                            i = i + s.Length + 5;
                        }
                        else
                        {
                            dododania += "|(" + s + "){1}";
                            i = i + s.Length + 6;
                        }
                    }
                }
               // dododania += ")";
                Regex rgx = new Regex(kvp.Value);
                if (dododania != string.Empty)
                {
                    str += "]{1})|(" + dododania + ")";
                    i += 4;
                }
                else
                    str += "]{1})";
                i += 7 + kvp.Value.Length;
                string copy2 = copy.Substring(j, copy.Length - j);
               // copy2 = Regex.Replace(copy2, kvp.Value, str);
                copy2 = rgx.Replace(copy2, str, 1);
                copy = copy.Substring(0, j) + copy2;
                //foreach (KeyValuePair<int, string> kv in indeksy)
                 //   indeksy2.Add(new KeyValuePair<int, string>(kv.Key+i, kv.Value));
                j = i;
                dododania = string.Empty;
            }
                //for (int j = 0; j < ind.Count; j++)
                //    MessageBox.Show(ind[j] + " " + len[j] + " " + sek[j]);
            /*
                while (i < orig.Length)
                {
                    if(ind.Contains(i))
                    {
                        if (i != 0)
                            nowy += nowy + orig.Substring(0, j);
                        str += str + "[";
                        j++;
                        foreach(string val in frequentlyMistakenSequences[sek[i]])
                        {
                            str += "(" + val + ")";
                            j = j + 2 + len[i];
                        }
                        str += "]{1}";
                        j += 4;
                       // MessageBox.Show(i.ToString());

                    }
                    j++;
                    i++;
                }
                

                    //  i++;

                    // }
                    /*
                    foreach (String k in frequentlyMistakenSequences.Keys)
                    {
                        if (Regex.IsMatch(pattern, k))
                        {
                            string s = "[";

                            foreach (string str in frequentlyMistakenSequences[k])
                            {

                                s = s + "(" + str + ")";

                            }
                            s = s + "]{1}";
                            copy = Regex.Replace(pattern, k, s);
                        }
                      //  MessageBox.Show(pattern);
                    }
                return copy;
            */
    }

        public void InitiateResults()
        {
           //faktury
            this.results.Add("numer faktury", String.Empty);
            this.results.Add("sprzedawca faktura", String.Empty);
            this.results.Add("nabywca faktura", String.Empty);
            this.results.Add("data1", String.Empty);
            this.results.Add("data2", String.Empty);
            this.results.Add("termin zaplaty", String.Empty);
            this.results.Add("data wystawienia", String.Empty);
            this.results.Add("data sprzedazy", String.Empty); //wykonania uslug

            this.results.Add("kwota", String.Empty);
            this.results.Add("sposob platnosci", String.Empty);
            this.results.Add("netto faktury", String.Empty);
            this.results.Add("do zaplaty", String.Empty);
            this.results.Add("zapłacono", String.Empty);

            //firmy
            this.results.Add("adres email",String.Empty);
            this.results.Add("nazwa", String.Empty);
            this.results.Add("kod pocztowy", String.Empty);
            this.results.Add("NIP",String.Empty);
            this.results.Add("numer klienta",String.Empty);        
            this.results.Add("numer konta",String.Empty);
            this.results.Add("miejscowosc", String.Empty);
            this.results.Add("numer ulicy", String.Empty);
            this.results.Add("ulica", String.Empty);
            this.results.Add("strona", String.Empty);
            this.results.Add("telefon1", String.Empty);
            this.results.Add("telefon2", String.Empty);
            this.results.Add("faks", String.Empty);

            //towary
            this.results.Add("nazwa towaru", String.Empty);
            this.results.Add("ilosc towaru", String.Empty);
            this.results.Add("netto towaru", String.Empty);
            this.results.Add("brutto towaru", String.Empty);
        }

        public void FillFrequentlyMistakenSequences()
        {
            this.frequentlyMistakenSequences.Add("0", new string[] { "o", "O" });
            this.frequentlyMistakenSequences.Add("o", new string[] { "0", "O" });
            this.frequentlyMistakenSequences.Add("O", new string[] { "o", "0" });
            this.frequentlyMistakenSequences.Add("l", new string[] { "I", "i", "ł", "1","\\|" });
            this.frequentlyMistakenSequences.Add("i", new string[] { "I", "l", "ł", "1", "\\|" });
            this.frequentlyMistakenSequences.Add("I", new string[] { "i", "l", "ł", "1", "\\|" });
            this.frequentlyMistakenSequences.Add("ł", new string[] { "i", "l", "I", "1", "\\|" });
            this.frequentlyMistakenSequences.Add("1", new string[] { "i", "l", "ł", "I", "\\|" });
            this.frequentlyMistakenSequences.Add("}", new string[] { "j", "l", "ł", "1", "I" });
            this.frequentlyMistakenSequences.Add("{", new string[] { "j", "l", "ł", "1", "I" });
            this.frequentlyMistakenSequences.Add("m", new string[] { "in", "nr", "rn", "ni","n" });
            this.frequentlyMistakenSequences.Add("y", new string[] { "j" });
            this.frequentlyMistakenSequences.Add("5", new string[] { "3" });
            this.frequentlyMistakenSequences.Add("3", new string[] { "5" });
            this.frequentlyMistakenSequences.Add("p", new string[] { "g" });
            this.frequentlyMistakenSequences.Add("g", new string[] { "p" });
            this.frequentlyMistakenSequences.Add("W", new string[] { "VV", "Vv", "vV" });
            this.frequentlyMistakenSequences.Add("w", new string[] { "vv", "Vv", "vV"});
            this.frequentlyMistakenSequences.Add("s", new string[] { "ś" });
            this.frequentlyMistakenSequences.Add("ś", new string[] { "s" });
            this.frequentlyMistakenSequences.Add("ć", new string[] { "c" });
            this.frequentlyMistakenSequences.Add("z", new string[] { "ż" });
            this.frequentlyMistakenSequences.Add("ż", new string[] { "z" });

            this.frequentlyMistakenSequences.Add("c", new string[] { "ć" });
            this.frequentlyMistakenSequences.Add("n", new string[] { "m" });
            this.frequentlyMistakenSequences.Add("a", new string[] { "u" });
            this.frequentlyMistakenSequences.Add("u", new string[] { "a" });


            //this.frequentlyMistakenSequences.Add("in", new string[] { "m","in" });
           // this.frequentlyMistakenSequences.Add("rn", new string[] { "m", });
           // this.frequentlyMistakenSequences.Add("nr", new string[] { "m", });
          //  this.frequentlyMistakenSequences.Add("ni", new string[] { "m", });

            List<string> lista = new List<string>();
            var array = lista.ToArray(); ;
            {
                //il
                lista = new List<string>();// (frequentlyMistakenSequences["l"].ToList());
                lista.Add("H");
                if (frequentlyMistakenSequences.ContainsKey("l")) foreach (string s in frequentlyMistakenSequences["l"])
                    {
                        lista.Add("i" + s);

                    }
                if (frequentlyMistakenSequences.ContainsKey("i")) foreach (string s in frequentlyMistakenSequences["i"])
                    {
                        lista.Add(s + "l");

                    }
                array = lista.ToArray();
                this.frequentlyMistakenSequences.Add("il", array);
            }
            {
                //li
                lista = new List<string>();// (frequentlyMistakenSequences["l"].ToList());
                lista.Add("h");
                lista.Add("H");

                if (frequentlyMistakenSequences.ContainsKey("l")) foreach (string s in frequentlyMistakenSequences["l"])
                    {
                        lista.Add(s+"i");
                        // lista.Add(s + "i");

                    }
                if (frequentlyMistakenSequences.ContainsKey("i")) foreach (string s in frequentlyMistakenSequences["i"])
                    {
                        lista.Add("l"+s);
                        // lista.Add(s + "i");

                    }
                array = lista.ToArray();
                this.frequentlyMistakenSequences.Add("li", array);
            }
            {
                //in
                lista = new List<string>();// (frequentlyMistakenSequences["l"].ToList());
                lista.Add("m");
                // lista.Add("h");
                if (frequentlyMistakenSequences.ContainsKey("n")) foreach (string s in frequentlyMistakenSequences["n"])
                    {
                        lista.Add("i"+ s);
                        

                    }
                if (frequentlyMistakenSequences.ContainsKey("i")) foreach (string s in frequentlyMistakenSequences["i"])
                    {
                        lista.Add(s+"n" );


                    }
                array = lista.ToArray();
                this.frequentlyMistakenSequences.Add("in", array);
            }

            {
                //ni
                lista = new List<string>();// (frequentlyMistakenSequences["l"].ToList());
                lista.Add("m");
                // lista.Add("h");
                if (frequentlyMistakenSequences.ContainsKey("n")) foreach (string s in frequentlyMistakenSequences["n"])
                    {
                        lista.Add(s+"i" );


                    }
                if (frequentlyMistakenSequences.ContainsKey("i")) foreach (string s in frequentlyMistakenSequences["i"])
                    {
                        lista.Add( "n"+s);


                    }
                array = lista.ToArray();
                this.frequentlyMistakenSequences.Add("ni", array);
            }

            {
                //nr
                lista = new List<string>();// (frequentlyMistakenSequences["l"].ToList());
                lista.Add("m");
                // lista.Add("h");
                if (frequentlyMistakenSequences.ContainsKey("n")) foreach (string s in frequentlyMistakenSequences["n"])
                    {
                        lista.Add(s + "r");


                    }
                if (frequentlyMistakenSequences.ContainsKey("r")) foreach (string s in frequentlyMistakenSequences["r"])
                    {
                        lista.Add("n" + s);


                    }
                array = lista.ToArray();
                this.frequentlyMistakenSequences.Add("nr", array);
            }

            {
                //rn
                lista = new List<string>();// (frequentlyMistakenSequences["l"].ToList());
                lista.Add("m");
                // lista.Add("h");
                if (frequentlyMistakenSequences.ContainsKey("n")) foreach (string s in frequentlyMistakenSequences["n"])
                    {
                        lista.Add("r"+s);


                    }
                if (frequentlyMistakenSequences.ContainsKey("r")) foreach (string s in frequentlyMistakenSequences["r"])
                    {
                        lista.Add(  s+"n");


                    }
                array = lista.ToArray();
                this.frequentlyMistakenSequences.Add("rn", array);
            }

            /*
            string pas = string.Empty;
            foreach(KeyValuePair<string, string[]> kvp in frequentlyMistakenSequences)
            {
                pas += kvp.Key+":";
                foreach (string s in frequentlyMistakenSequences[kvp.Key])
                {
                    pas += s +" ";
                }
                MessageBox.Show(pas);
                pas = string.Empty;
            }
             * */
          //  foreach (string s in frequentlyMistakenSequences["in"])
               

        }

        public static List<String> RemoveEmpltyLines(String s)
        {
            string[] l = s.Split('\n');
            List<String> lines = new List<string>();
            foreach (string a in l)
            {
                if (!String.IsNullOrWhiteSpace(a))
                    lines.Add(a);
            }
            return lines;
        }
 
        public Dictionary<string, string> IterateAll()
        {

            foreach (string s in text)
            {
                foreach (KeyValuePair<string, string> kvp in patterns)
                {
                  //  MessageBox.Show(kvp.Key);
                    foreach (Match m in Regex.Matches(s, kvp.Value, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        
                        string temp;
                        if (kvp.Key == "numer klienta")
                        {
                            temp = Regex.Replace(m.Value.ToString(), "numer klienta", "", RegexOptions.IgnoreCase);
                            temp = Regex.Replace(temp, ":", "");
                        }
                        else
                            if(kvp.Key=="data2" && results["data2"]!=String.Empty)
                            {
                                temp = results["data2"];
                            }
                            else
                            {
                            temp = m.Value.ToString();
                            }

                        results[kvp.Key]=temp;
                    }
                }
            }

            return results;
        }

        public string ValidateAmount(string amount)
        {
            if (!Regex.IsMatch(amount, "\\,") && (!Regex.IsMatch(results["brutto towaru"], "\\.")))
            {
                //MessageBox.Show("nie ma kropki ani przecinka");
                foreach (Match m in Regex.Matches(amount, "[0-9]+ [0-9]{2}"))
                {
                    //  MessageBox.Show("spacja");
                    amount = Regex.Replace(m.Value.ToString(), " ", ",");

                }
                foreach (Match m in Regex.Matches(amount, "( |^)[0-9]{3,}( |$)"))
                {
                    // MessageBox.Show("brak spacji");

                    Match mm = Regex.Match(m.Value.ToString(), "[0-9]{3,}");
                    //if(mm.Value.ToString().Length>2)
                    if (mm.Success)
                    {
                        amount = String.Concat(mm.Value.ToString().Substring(0, mm.Value.ToString().Length - 2), "," + mm.Value.ToString().Substring(mm.Value.ToString().Length - 2));
                    }
                    //results["brutto towaru"] = Regex.Replace(m.Value.ToString(), " ", ",");

                }
            }
            return amount;
        }

        public string IterateTable(List<string> section, int type)
        {
            //List<string> lines = Patterning.RemoveEmpltyLines(text);
           // string orig = text;
            int v = section.Count;
            List<List<string>> cells = new List<List<string>>();
          //  List<string> sectionLines = section.
            //String[][] cells = new String[][] { };
           // String[,] cells = new String[,] { };
            string[] row = new string[] { };
            for (int i = 0; i < section.Count; i++)
            {
                row = section[i].Split('@');
                
                cells.Add(row.ToList<string>());
                cells[i].RemoveAt(0);
                cells[i].RemoveAt(cells[i].Count-1);
               // cells[i, 0] = new string(row);
                //foreach (string s in row)
                //    MessageBox.Show(s + " ");
               // for(int j=0;j<row.Length;j++)
                //cells[i, j] = row[j];
            }
            string wynik=string.Empty;
            
           /* foreach(List<String> sRow in cells)
            {
                foreach(string sCell in sRow )
                {
                    wynik += sCell + "\t\t\t";
                }
                wynik += "\n";
            }*/
           // MessageBox.Show(BuildPattern("nazwa"));
          //  MessageBox.Show(BuildPattern("ilość"));
          //  MessageBox.Show(BuildPattern("wartość brutto"));
          //  MessageBox.Show(BuildPattern("wartość netto"));


            for(int i=0;i<cells[0].Count;i++)
            {
                if (Regex.IsMatch(cells[0][i], BuildPattern("nazwa"), RegexOptions.IgnoreCase))
                {
                    cells[0][i]="nazwa";
                }
                if (Regex.IsMatch(cells[0][i], BuildPattern("ilość"), RegexOptions.IgnoreCase))
                {
                    cells[0][i]="ilość";
                }
                else if (Regex.IsMatch(cells[0][i], BuildPattern("il."), RegexOptions.IgnoreCase))
                    cells[0][i] = "ilość";
                if (Regex.IsMatch(cells[0][i], BuildPattern("wartość brutto"), RegexOptions.IgnoreCase))
                {
                    cells[0][i]="wartość brutto";
                }
                string pattern = BuildPattern(patterns["VAT"]);
               // pattern=
                if(Regex.IsMatch(cells[0][i], pattern, RegexOptions.IgnoreCase))
              //  if (m!=null)
                {
                    if (!Regex.IsMatch(Regex.Match(cells[0][i], pattern, RegexOptions.IgnoreCase).ToString(), BuildPattern("wartość"), RegexOptions.IgnoreCase))
                    cells[0][i] = "VAT";
                }
                if (Regex.IsMatch(cells[0][i], BuildPattern("wartość netto"), RegexOptions.IgnoreCase))
                {
                    cells[0][i]="wartość netto";
                }
                else
                    if (Regex.IsMatch(cells[0][i], BuildPattern("cena.*bez podatku"), RegexOptions.IgnoreCase))
                    {
                        cells[0][i] = "wartość netto";
                    }
            }
            
          // foreach(string s in cells[0])
            for (int j = 1; j < cells.Count; j++)
            {
                for (int i = 0; i < cells[0].Count; i++)
                {
                    string s = cells[0][i];
                    switch (s)
                    {
                        case ("ilość"):
                            if (cells[1][i] != "") results["ilosc towaru"] = cells[j][i];
                            break;
                        case ("nazwa"):
                            if (cells[1][i] != "") results["nazwa towaru"] = cells[j][i];
                            break;
                        case ("wartość brutto"):
                            if (cells[1][i] != "") results["brutto towaru"] = cells[j][i];

                            if (!Regex.IsMatch(results["brutto towaru"], "\\,") && (!Regex.IsMatch(results["brutto towaru"], "\\.")  ))
                            {
                                //MessageBox.Show("nie ma kropki ani przecinka");
                              foreach(Match m in Regex.Matches(results["brutto towaru"],"[0-9]+ [0-9]{2}"))
                              {
                                //  MessageBox.Show("spacja");
                                  results["brutto towaru"] = Regex.Replace(m.Value.ToString(), " ",",");

                              }
                              foreach (Match m in Regex.Matches(results["brutto towaru"], "( |^)[0-9]{3,}( |$)"))
                              {
                                 // MessageBox.Show("brak spacji");

                                  Match mm = Regex.Match(m.Value.ToString(), "[0-9]{3,}");
                                  //if(mm.Value.ToString().Length>2)
                                  if(mm.Success)

                                  {
                                      results["brutto towaru"] = String.Concat(mm.Value.ToString().Substring(0, mm.Value.ToString().Length - 2), "," + mm.Value.ToString().Substring(mm.Value.ToString().Length - 2));
                                  }
                                  //results["brutto towaru"] = Regex.Replace(m.Value.ToString(), " ", ",");

                              }
                            }
                            

                            break;
                        case ("wartość netto"):
                            if (cells[1][i] != "") results["netto towaru"] = cells[j][i];

                            if (!Regex.IsMatch(results["netto towaru"], "\\,") && (!Regex.IsMatch(results["netto towaru"], "\\.")))
                            {
                                //MessageBox.Show("nie ma kropki ani przecinka");
                                foreach (Match m in Regex.Matches(results["netto towaru"], "[0-9]+ [0-9]{2}"))
                                {
                                    //  MessageBox.Show("spacja");
                                    results["netto towaru"] = Regex.Replace(m.Value.ToString(), " ", ",");

                                }
                                foreach (Match m in Regex.Matches(results["netto towaru"], "( |^)[0-9]{3,}( |$)"))
                                {
                                    // MessageBox.Show("brak spacji");

                                    Match mm = Regex.Match(m.Value.ToString(), "[0-9]{3,}");
                                  //  if (mm.Value.ToString().Length > 2)
                                    if(mm.Success)
                                    {
                                        results["netto towaru"] = String.Concat(mm.Value.ToString().Substring(0, mm.Value.ToString().Length - 2), "," + mm.Value.ToString().Substring(mm.Value.ToString().Length - 2));
                                    }
                                    //results["brutto towaru"] = Regex.Replace(m.Value.ToString(), " ", ",");

                                }
                            }

                            break;
                        case ("VAT"):
                            if (cells[1][i] != "")
                            {
                                if (Regex.IsMatch(cells[j][i],"[0-9]+"))
                                results["VAT"] = cells[j][i];
                            }
                            break;
                    }
                }
                MessageBox.Show("ilosc:"+results["ilosc towaru"]+"nazwa:"+results["nazwa towaru"]+"brutto:"+results["brutto towaru"] +"netto:"+results["netto towaru"] );
                
            }
          

                MessageBox.Show(wynik);
            return "";
        }

        public string IterateFor(string text, string parameter)
        {
            List<string> lines = Patterning.RemoveEmpltyLines(text);
            string orig = text;
            bool miejscowoscCutted = false;
            switch(parameter)
            {
                
                case ("do zaplaty"):
                    {
                       // string pattern = BuildPattern("do zaplaty");
                        string pattern = patterns["do zaplaty"];
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                //MessageBox.Show("match");
                                string temp = str;
                                //  string pattern = "(sposob p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?)";
                                //string pattern = "do zap[lł]{1}aty";
                               // string pattern = BuildPattern("do zaplaty");
                             //   MessageBox.Show(ValidateAmount("192 66"));
                                temp = Regex.Replace(temp, pattern, "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, "z[łlI1](otych)?", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, "gr(oszy)?", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, BuildPattern("pozostaje"), "", RegexOptions.IgnoreCase);
                                Match mat = Regex.Match(temp, patterns["kwota"], RegexOptions.IgnoreCase);
                               if(mat.Success) results["do zaplaty"] = mat.Value.ToString();
                                
                            }
                        if(!Regex.IsMatch(results["do zaplaty"],","))
                                {
                                    Match m = Regex.Match(results["do zaplaty"], @"[0-9]+ [0-9]{2}");
                                    results["do zaplaty"] = Regex.Replace(m.Value.ToString(), " ", ",");
                                }
                        if (results["do zaplaty"] == String.Empty)
                            MessageBox.Show("nie ma zaplaty");
                        break;
                    }

                case ("zaplacono"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, "zap[lł]{1}acono", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                //MessageBox.Show("match");
                                string temp = str;
                                //  string pattern = "(sposob p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?)";
                                string pattern = "zap[lł]{1}acono";
                                temp = Regex.Replace(temp, pattern, "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, "z[łlI1](otych)?", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, "gr(oszy)?", "", RegexOptions.IgnoreCase);

                                results["zaplacono"] = temp;
                            }
                       
                        break;
                    }
                   
                case ("sposob platnosci"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, patterns["sposob platnosci"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                        //        MessageBox.Show("sposob:"+m.Value.ToString());
                                string temp = str;
                                string pattern = @"((spos[oó]{1}b)|(forma)|(metoda)) ((zap[lł]{1}aty)|(p[lł]{1}atno[sś]{1}ci))";
                               // string pattern = "((spos[oó]{1}b)|(forma)|(metoda) (p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?)|(zap[lłt1]{1}a[1ltł]{1}y):?)";
                                temp = Regex.Replace(temp, pattern, "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "", RegexOptions.IgnoreCase);
                               
                                
                                results["sposob platnosci"] = temp;
                            }
                        break;
                    }

                case("numer faktury"):
                    {
                        if (lines.Count == 1)
                        {
                            string pattern = "orygina[lł1]{1}";
                            string copy = text;
                            copy = Regex.Replace(copy, "VAT", "", RegexOptions.IgnoreCase);
                            copy = Regex.Replace(copy, "faktura", "", RegexOptions.IgnoreCase);
                            copy = Regex.Replace(copy, "kopia", "", RegexOptions.IgnoreCase);
                            copy = Regex.Replace(copy, pattern, "", RegexOptions.IgnoreCase);
                            copy = Regex.Replace(copy, "nr", "", RegexOptions.IgnoreCase);
                            copy = Regex.Replace(copy, ":", "");
                            copy = Regex.Replace(copy, "#", "");
                            copy = Regex.Replace(copy, "\n", "");
                            results["numer faktury"] = copy;
                            
                           // return copy;
                        }
                        else 
                        { 
                            for(int i=0;i<lines.Count;i++)
                            {
                                if(Regex.IsMatch(lines[i],"nr",RegexOptions.IgnoreCase))
                                {
                                    string pattern = "orygina[lł1]{1}";
                                    string copy = lines[i];
                                    copy = Regex.Replace(copy, "VAT", "", RegexOptions.IgnoreCase);
                                    copy = Regex.Replace(copy, "faktura", "", RegexOptions.IgnoreCase);
                                    copy = Regex.Replace(copy, "kopia", "", RegexOptions.IgnoreCase);
                                    copy = Regex.Replace(copy, pattern, "", RegexOptions.IgnoreCase);
                                    copy = Regex.Replace(copy, "nr", "", RegexOptions.IgnoreCase);
                                    copy = Regex.Replace(copy, ":", "");
                                    copy = Regex.Replace(copy, "#", "");
                                    results["numer faktury"] = copy;
                                 //   return copy;
                                }
                                else
                                    if (Regex.IsMatch(lines[i], "faktura", RegexOptions.IgnoreCase))
                                    {
                                        MessageBox.Show("faktura match");
                                        string pattern = "orygina[lł1]{1}";
                                        string copy = lines[i];
                                        copy = Regex.Replace(copy, "VAT", "", RegexOptions.IgnoreCase);
                                        copy = Regex.Replace(copy, "faktura", "", RegexOptions.IgnoreCase);
                                        copy = Regex.Replace(copy, "kopia", "", RegexOptions.IgnoreCase);
                                        copy = Regex.Replace(copy, pattern, "", RegexOptions.IgnoreCase);
                                        copy = Regex.Replace(copy, "nr", "", RegexOptions.IgnoreCase);
                                        copy = Regex.Replace(copy, ":", "");
                                        copy = Regex.Replace(copy, "#", "");
                                      //  MessageBox.Show(copy);
                                        results["numer faktury"] = copy;
                                        //return copy;
                                    }
                            }
                        //faktura korygujaca ?
                        }
                        break;
                    }
                case ("numer klienta"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, patterns["numer klienta"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = str;
                                temp = Regex.Replace(m.Value.ToString(), "numer klienta", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "");
                                //  if (!results.ContainsKey("NIP")) results.Add("NIP", m.Value.ToString());
                                results["numer klienta"] = temp;
                            }
                        break;
                    }
                case ("data1"): case("data2"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, patterns["data1"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                             
                                //  if (!results.ContainsKey("NIP")) results.Add("NIP", m.Value.ToString());
                                if (results["data1"] != string.Empty && results["data1"] != m.Value.ToString())
                                {
                                    results["data2"] = m.Value.ToString();
                                }
                                else if (results["data1"] != string.Empty && results["data1"] == m.Value.ToString())
                                {
                                    if(results["data2"]==string.Empty)
                                    {
                                    results["data2"]=m.Value.ToString();
                                    }
                                }
                                else if (results["data1"] == string.Empty)
                                {
                                    results["data1"] = m.Value.ToString();
                                }
                                else if (results["data1"] != string.Empty && results["data2"] != string.Empty)
                                {
                                    if (results["data1"] == results["data2"] && results["data1"]!=m.Value.ToString())
                                    {
                                        results["data1"] = m.Value.ToString();
                                    }
                                }
                            }
                        results["data1"] = Regex.Replace(results["data1"], "O", "0", RegexOptions.IgnoreCase);
                        results["data1"] = Regex.Replace(results["data1"], "S", "5", RegexOptions.IgnoreCase);
                        results["data1"] = Regex.Replace(results["data1"], "I", "1", RegexOptions.IgnoreCase);
                        results["data1"] = Regex.Replace(results["data1"], @"\|", "1", RegexOptions.IgnoreCase);
                        break;
                    }

                case ("termin zaplaty"):
                    {
                        string pattern = BuildPattern("termin zapłaty");
                        pattern = "(" + pattern + ")|(" + BuildPattern("termin płatności") + ")|("+BuildPattern("termin")+")";
                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, pattern, RegexOptions.IgnoreCase))
                            {
                                if (Regex.IsMatch(str, patterns["data"]))
                                {
                                    foreach (Match mat in Regex.Matches(str, patterns["data"]))
                                        results["termin zaplaty"] = mat.Value.ToString();
                                    //  results["termin zaplaty"] = Regex.Replace(str, pattern, "");
                                    // results["termin zaplaty"] = Regex.Replace(results["termin zaplaty"], ":", "");
                                }
                                else
                                {
                                    string pattern2 = BuildPattern("data wystawienia");
                                    string pattern3 = BuildPattern("data sprzedazy");
                                    int index = lines.IndexOf(str);
                                    if (!Regex.IsMatch(lines[index + 1], pattern2, RegexOptions.IgnoreCase) && !Regex.IsMatch(lines[index + 1], pattern3, RegexOptions.IgnoreCase))
                                        foreach (Match mm in Regex.Matches(lines[index + 1], patterns["data"]))
                                        {
                                            results["termin zaplaty"] = mm.Value.ToString();
                                            results["termin zaplaty"] = Regex.Replace(results["termin zaplaty"], ":", "");

                                        }
                                }
                            }

                        }
                        if (results["termin zaplaty"].Length > 3)
                        {
                            results["termin zaplaty"] = Regex.Replace(results["termin zaplaty"], "O", "0", RegexOptions.IgnoreCase);
                            results["termin zaplaty"] = Regex.Replace(results["termin zaplaty"], "S", "5", RegexOptions.IgnoreCase);
                            results["termin zaplaty"] = Regex.Replace(results["termin zaplaty"], "I", "1", RegexOptions.IgnoreCase);
                            results["termin zaplaty"] = Regex.Replace(results["termin zaplaty"], @"\|", "1", RegexOptions.IgnoreCase);
                        }
                      
                    }
                    break;

                case ("data wystawienia"):
                    {
                        string pattern = BuildPattern("data wystawienia");
                      //  pattern = "(" + pattern + ")|(" + BuildPattern("termin płatności") + ")";
                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, pattern, RegexOptions.IgnoreCase))
                            {
                                if (Regex.IsMatch(str, patterns["data"]))
                                {
                                    foreach (Match mat in Regex.Matches(str, patterns["data"]))
                                        results["data wystawienia"] = mat.Value.ToString();
                                  //  results["data wystawienia"] = Regex.Replace(str, pattern, "");
                                  //  results["data wystawienia"] = Regex.Replace(results["data wystawienia"], "faktury", "");
                                  //  results["data wystawienia"] = Regex.Replace(results["data wystawienia"], ":", "");
                                }
                                else
                                {
                                    string pattern2 = BuildPattern("termin zapłaty");
                                    pattern2 = "(" + pattern2 + ")|(" + BuildPattern("termin płatności") + ")";
                                    string pattern3 = BuildPattern("data sprzedazy");
                                    int index = lines.IndexOf(str);
                                    if (!Regex.IsMatch(lines[index + 1], pattern2, RegexOptions.IgnoreCase) && !Regex.IsMatch(lines[index + 1], pattern3, RegexOptions.IgnoreCase))
                                        foreach (Match mm in Regex.Matches(lines[index + 1], patterns["data"]))
                                        {
                                            results["data wystawienia"] = mm.Value.ToString();

                                        }
                                }
                            }
                           
                        }
                        results["data wystawienia"] = Regex.Replace(results["data wystawienia"], "O", "0", RegexOptions.IgnoreCase);
                        results["data wystawienia"] = Regex.Replace(results["data wystawienia"], "S", "5", RegexOptions.IgnoreCase);
                        results["data wystawienia"] = Regex.Replace(results["data wystawienia"], "I", "1", RegexOptions.IgnoreCase);
                        results["data wystawienia"] = Regex.Replace(results["data wystawienia"], @"\|", "1", RegexOptions.IgnoreCase);
                    }

                    break;

                case ("data sprzedazy"):
                    {
                        string pattern = BuildPattern("data sprzedaży");
                          pattern = "(" + pattern + ")|(" + BuildPattern("data wykonania") + ")|("+BuildPattern("data wykonania uslugi")+")|("+BuildPattern("data dostawy")+")";
                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, pattern, RegexOptions.IgnoreCase))
                            {
                                if (Regex.IsMatch(str, patterns["data"]))
                                {
                                    foreach (Match mat in Regex.Matches(str, patterns["data"]))
                                        results["data sprzedazy"] = mat.Value.ToString();
                                   // results["data sprzedazy"] = Regex.Replace(str, pattern, "");
                                   // results["data sprzedazy"] = Regex.Replace(results["data sprzedazy"], "towaru", "");
                                   // results["data sprzedazy"] = Regex.Replace(results["data sprzedazy"], ":", "");
                                }
                                else
                                {
                                    string pattern2 = BuildPattern("termin zapłaty");
                                    pattern2 = "(" + pattern2 + ")|(" + BuildPattern("termin płatności") + ")";
                                    string pattern3 = BuildPattern("data wystawienia");
                                    int index = lines.IndexOf(str);
                                    if (!Regex.IsMatch(lines[index + 1], pattern2, RegexOptions.IgnoreCase) && !Regex.IsMatch(lines[index + 1], pattern3, RegexOptions.IgnoreCase))
                                        foreach (Match mm in Regex.Matches(lines[index + 1], patterns["data"]))
                                        {
                                            results["data sprzedazy"] = mm.Value.ToString();

                                        }
                                }
                            }

                        }
                        results["data sprzedazy"] = Regex.Replace(results["data sprzedazy"], "O", "0", RegexOptions.IgnoreCase);
                        results["data sprzedazy"] = Regex.Replace(results["data sprzedazy"], "S", "5", RegexOptions.IgnoreCase);
                        results["data sprzedazy"] = Regex.Replace(results["data sprzedazy"], "I", "1", RegexOptions.IgnoreCase);
                        results["data sprzedazy"] = Regex.Replace(results["data sprzedazy"], @"\|", "1", RegexOptions.IgnoreCase);
                    }

                    break;

                case ("telefon1"):
                case ("telefon2"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, patterns["telefon1"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                if (Regex.IsMatch(str, BuildPattern("faks") + " ?:? ?" + m, RegexOptions.IgnoreCase))
                                {
                                    results["faks"] = m.Value.ToString();
                                }
                                else
                                {
                                    Match mat=Regex.Match(str,patterns["REGON"],RegexOptions.IgnoreCase);
                                    if (Regex.IsMatch(mat.Value.ToString(), m.Value.ToString(), RegexOptions.IgnoreCase))
                                    {
                                        results["REGON"] = m.Value.ToString();
                                    }
                                    else
                                    {

                                        //    MessageBox.Show("tel");
                                        //  if (!results.ContainsKey("NIP")) results.Add("NIP", m.Value.ToString());
                                        if (results["telefon1"] != string.Empty && results["telefon1"] != m.Value.ToString())
                                        {
                                            results["telefon2"] = m.Value.ToString();
                                        }
                                        else if (results["telefon1"] != string.Empty && results["telefon1"] == m.Value.ToString())
                                        {
                                            if (results["telefon2"] == string.Empty)
                                            {
                                                results["telefon2"] = m.Value.ToString();
                                            }
                                        }
                                        else if (results["telefon1"] == string.Empty)
                                        {
                                            results["telefon1"] = m.Value.ToString();
                                        }
                                        else if (results["telefon1"] != string.Empty && results["telefon2"] != string.Empty)
                                        {
                                            if (results["telefon1"] == results["telefon2"] && results["telefon1"] != m.Value.ToString())
                                            {
                                                results["telefon1"] = m.Value.ToString();
                                            }
                                        }

                                    }
                                }
                                results["telefon1"] = Regex.Replace(results["telefon1"], "O", "0", RegexOptions.IgnoreCase);
                                results["telefon1"] = Regex.Replace(results["telefon1"], "S", "5", RegexOptions.IgnoreCase);
                                results["telefon1"] = Regex.Replace(results["telefon1"], "I", "1", RegexOptions.IgnoreCase);
                                results["telefon1"] = Regex.Replace(results["telefon1"], @"\|", "1", RegexOptions.IgnoreCase);

                                results["telefon2"] = Regex.Replace(results["telefon2"], "O", "0", RegexOptions.IgnoreCase);
                                results["telefon2"] = Regex.Replace(results["telefon2"], "S", "5", RegexOptions.IgnoreCase);
                                results["telefon2"] = Regex.Replace(results["telefon2"], "I", "1", RegexOptions.IgnoreCase);
                                results["telefon2"] = Regex.Replace(results["telefon2"], @"\|", "1", RegexOptions.IgnoreCase);
                            }
                        break;
                    }

                case ("faks"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, BuildPattern("faks")+".*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {

                                Match mat = Regex.Match(m.Value.ToString(),patterns["faks"],RegexOptions.IgnoreCase);
                                results["faks"] = mat.Value.ToString();
                                MessageBox.Show("fax:" + results["faks"]);
                            }
                        break;
                    }

                case("NIP"):
                    {
                        foreach (string str in lines)
                        foreach (Match m in Regex.Matches(str, patterns["NIP"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                          //  if (!results.ContainsKey("NIP")) results.Add("NIP", m.Value.ToString());
                             results["NIP"] =  m.Value.ToString();
                        }
                        results["NIP"] = Regex.Replace(results["NIP"], "PL", "", RegexOptions.IgnoreCase);
                        results["NIP"] = Regex.Replace(results["NIP"], "O", "0", RegexOptions.IgnoreCase);
                        results["NIP"] = Regex.Replace(results["NIP"], "S", "5", RegexOptions.IgnoreCase);
                        results["NIP"] = Regex.Replace(results["NIP"], "I", "1", RegexOptions.IgnoreCase);
                        results["NIP"] = Regex.Replace(results["NIP"], @"\|", "1", RegexOptions.IgnoreCase);
                        break;
                    }
                case ("kod pocztowy"):
                case ("miejscowosc"):
                    {
                        foreach(string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, patterns["kod pocztowy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = str;




                                results["kod pocztowy"] = m.Value.ToString();


                                temp = Regex.Replace(temp, results["kod pocztowy"], "");
                                temp = Regex.Replace(temp, ":", "");
                                temp = Regex.Replace(temp, "^,", "");
                              //  temp = Regex.Replace(temp, " ", "");
                                temp = Regex.Replace(temp, ",.*", "");

                                results["miejscowosc"] = temp;

                            }

                  
                       }
                        results["kod pocztowy"] = Regex.Replace(results["kod pocztowy"], "O", "0", RegexOptions.IgnoreCase);
                        results["kod pocztowy"] = Regex.Replace(results["kod pocztowy"], "S", "5", RegexOptions.IgnoreCase);
                        results["kod pocztowy"] = Regex.Replace(results["kod pocztowy"], "I", "1", RegexOptions.IgnoreCase);
                        results["kod pocztowy"] = Regex.Replace(results["kod pocztowy"], @"\|", "1", RegexOptions.IgnoreCase);
                        if (results["miejscowosc"].Length < 3)
                        {
                            foreach(string str in lines)
                            {
                            string pattern = BuildPattern("miejsce wystawienia");
                            foreach (Match m in Regex.Matches(str, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                             //   MessageBox.Show("mijsce:"+m.Value.ToString());
                                results["miejscowosc"] = Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase);
                                 pattern = BuildPattern("dokumentu");
                                 results["miejscowosc"] = Regex.Replace(results["miejscowosc"], pattern, "", RegexOptions.IgnoreCase);
                                 results["miejscowosc"] = Regex.Replace(results["miejscowosc"], ":", "", RegexOptions.IgnoreCase);
                            //    MessageBox.Show("result:"+results["miejscowosc"]);
                                if (results["miejscowosc"].Length < 3)
                                {
                                    int index = lines.IndexOf(str);
                                    results["miejscowosc"] = lines[index+1];
                                }


                            }
                        }
                    }
                    break;
                    }
                case ("nazwa"):
                    {
                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str,BuildPattern("sprzedawca")+".*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = m.Value.ToString();
                                //MessageBox.Show(temp);
                                temp = Regex.Replace(temp, BuildPattern("sprzedawca"), "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, BuildPattern("podatnik"), "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "");

                                int index = lines.IndexOf(str);

                                while (!Regex.IsMatch(temp, "[a-ząęśćźńłóż]{3,}"))
                                {
                                    index++;
                                    temp = lines[index];
                                }
                                results["nazwa"] = temp;

                              //  if (temp != String.Empty)
                             //       results["nazwa"] = temp;
                             //   else results["nazwa"] = lines[1];
                            }
                            foreach (Match m in Regex.Matches(str, @"nabywca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = m.Value.ToString();
                                //MessageBox.Show(temp);
                                temp = Regex.Replace(temp, BuildPattern("nabywca i płatnik"), "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, BuildPattern("nabywca"), "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, BuildPattern("płatnik"), "", RegexOptions.IgnoreCase);

                                temp = Regex.Replace(temp, ":", "");

                                int index = lines.IndexOf(str);

                               // results["nazwa"] = temp;
                                while(!Regex.IsMatch(temp, "[a-ząęśćźńłóż]{3,}",RegexOptions.IgnoreCase))
                                {
                                    index++;
                                    temp = lines[index];
                                }
                                //if (temp != String.Empty)
                                    results["nazwa"] = temp;
                               // else results["nazwa"] = lines[1];
                            }
                        }
                        
                        break;
                    }
                    {/*
                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, @"sprzedawca\S*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = m.Value.ToString();
                                //MessageBox.Show(temp);
                                temp = Regex.Replace(temp, "sprzedawca", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "");

                               
                                results["nazwa"] =  temp;
                              //  MessageBox.Show("nazwa:" + temp);
                            }
                            foreach (Match m in Regex.Matches(str, @"nabywca\S*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                             //   System.Windows.MessageBox.Show("nabywca");
                                string temp = m.Value.ToString();
                                string pattern = "nabywca i płatnik";
                                temp = Regex.Replace(temp, pattern, "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, "nabywca", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "");
                                temp = Regex.Replace(temp, "^ ", "");
                                temp = Regex.Replace(temp, " ", "");

                                if (temp.Length > 2)
                                    results["nazwa"] = temp;
                                else results["nazwa"] = lines[1];
                                
                            }
                           // if()
                         
                        }
                        break;
                        */
                    }
                    
                        case("ulica"):
                        {

                            foreach(string str in lines)
                            {
                                foreach (Match m in Regex.Matches(str, patterns["ulica"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                {
                                   // System.Windows.MessageBox.Show("ulica: " + m.Value.ToString());
                                    results["ulica"] = m.Value.ToString();
                                   // string pattern = "((ul)|(uł)|(u1)|(ui)){1}";
                                    string pattern = BuildPattern("ul");
                               //     MessageBox.Show("wzor na ulice:"+pattern);
                                    foreach (Match ma in Regex.Matches(results["ulica"], pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                    {
                                        
                                           // results["ulica"] = Regex.Replace(results["ulica"], ma.Value.ToString(), "");
                                         results["ulica"] = Regex.Replace(results["ulica"], pattern, "");

                                    }

                                    results["ulica"] = Regex.Replace(results["ulica"], @"\W", "");
                                    results["ulica"] = Regex.Replace(results["ulica"], @"_", "");
                                    results["ulica"] = Regex.Replace(results["ulica"], @"[0-9]", "");
                                }
                            }
                        break;
                        }

                        case ("numer ulicy"):
                        
                            {

                            foreach (string str in lines)
                            {
                                foreach (Match m in Regex.Matches(str, patterns["numer ulicy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                {

                                   // MessageBox.Show(m.Value.ToString());
                                    results["numer ulicy"] = m.Value.ToString();
                                    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], results["ulica"], "");
                                    //string pattern = @"((ul)|(uł)|(u1)|(u\\|)){1}\.*";
                                    string pattern = BuildPattern("ul")+@"\.?";
                                    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], pattern, "");
                                //    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], @"\.", "");
                                  //  results["numer ulicy"] = Regex.Replace(results["numer ulicy"], @"\D", "");
                                 //   MessageBox.Show(results["numer ulicy"]);
                                }

                            }

                           
                            results["numer ulicy"] = Regex.Replace(results["numer ulicy"], "O", "0", RegexOptions.IgnoreCase);
                            results["numer ulicy"] = Regex.Replace(results["numer ulicy"], "S", "5", RegexOptions.IgnoreCase);
                            results["numer ulicy"] = Regex.Replace(results["numer ulicy"], "I", "1", RegexOptions.IgnoreCase);
                            results["numer ulicy"] = Regex.Replace(results["numer ulicy"], @"\|", "1", RegexOptions.IgnoreCase);
                            break;
                        }
                case("adres email"):
                    {

                        foreach(string str in lines)
                        {
                                foreach (Match m in Regex.Matches(str, patterns["adres email"], RegexOptions.IgnoreCase))
                                {
                                results["adres email"] =  m.Value.ToString();
                                results["adres email"] = Regex.Replace(results["adres email"], "pl$", ".pl");
                                }
                        }
                        break;
                    }
                    case("strona"):
                    {

                        foreach(string str in lines)
                        {
                                foreach (Match m in Regex.Matches(str, patterns["strona"]))
                                {
                                results["strona"] =  m.Value.ToString();
                         
                                }
                        }
                        break;
                    }
                
                    case ("numer konta"):
                    {

                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, patterns["numer konta"]))
                            {
                            //    MessageBox.Show(m.Value.ToString());
                                results["numer konta"] = m.Value.ToString();
                    
                            }



                        }
                        results["numer konta"] = Regex.Replace(results["numer konta"], "O", "0", RegexOptions.IgnoreCase);
                        results["numer konta"] = Regex.Replace(results["numer konta"], "S", "5", RegexOptions.IgnoreCase);
                        results["numer konta"] = Regex.Replace(results["numer konta"], "I", "1", RegexOptions.IgnoreCase);
                        results["numer konta"] = Regex.Replace(results["numer konta"], @"\|", "1", RegexOptions.IgnoreCase);
                        break;
                    }
                   

                        
                    
                default:
                    MessageBox.Show("nie ma takiego paramatru: " + parameter);
                    break;
            }
            if (results["miejscowosc"] != String.Empty && results["ulica"] != String.Empty && results["numer ulicy"]!=string.Empty && !miejscowoscCutted)
            {
                miejscowoscCutted = true;
                results["miejscowosc"] = Regex.Replace(results["miejscowosc"], BuildPattern("adres"), "",RegexOptions.IgnoreCase);
                results["miejscowosc"] = Regex.Replace(results["miejscowosc"], results["ulica"], "");
                results["miejscowosc"] = Regex.Replace(results["miejscowosc"], results["numer ulicy"], "");
                results["miejscowosc"] = Regex.Replace(results["miejscowosc"], BuildPattern("ul") + ".? ?", "",RegexOptions.IgnoreCase);
            }
            if (results["telefon1"] == String.Empty && results["telefon2"] == String.Empty && results["faks"] != String.Empty)
            {
                results["telefon1"] = results["faks"];
                results["telefon2"] = results["faks"]; 

            }
            /*
            if(parameter=="termin zaplaty")
            if (results["termin zaplaty"].Length < 4)
            {
                int count = 0;
                string[] data = new string[3];
 
                foreach(string s in lines)
                foreach(Match m in Regex.Matches(s,patterns["data1"],RegexOptions.IgnoreCase))
                {
                    data[count] = m.Value.ToString();
                    count++;
                }
                foreach(string ss in data)
                {

                }
            }
            */
            if (results.ContainsKey(parameter) && results[parameter]!=string.Empty)
            {
                string s = results[parameter];
                if (s[0] == ' ') s.Substring(1);
                if (s[s.Length-1] == ' ') s.Substring(0,s.Length-1);
                if (s[s.Length - 1] == ',') s.Substring(0, s.Length - 1);
                results[parameter] = s;
                return results[parameter];
                if(results["numer faktury"]!=string.Empty)
                {
                    results["numer faktury"]=Regex.Replace(results["numer faktury"],"\"","");
                    results["numer faktury"] = Regex.Replace(results["numer faktury"], "`", "");
                }
            }
            else
            {
               // MessageBox.Show("brak: " + parameter);
                return "";
            }
        }

        public  Dictionary<string, string> IterateSection(string sectionName, string s)
        {
            List<string> lines = Patterning.RemoveEmpltyLines(s);
            string copy = s;
          
            
           Dictionary<string,string> result=new Dictionary<string,string>();
           switch(sectionName)
           {
               case("nabywca"):
                  // Dictionary<string, string> nabywca = new Dictionary<string, string>();
                   foreach(string str in lines)
                   {
                       foreach (Match m in Regex.Matches(str, patterns["kod pocztowy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = str;
                           if (!result.ContainsKey("kod pocztowy")) result.Add("kod pocztowy", m.Value.ToString());
                           else
                           {
                               
                               temp = Regex.Replace(temp, result["kod pocztowy"], "");
                               result["kod pocztowy"] += "|" + m.Value.ToString();
                           }
                            
                           temp = Regex.Replace(temp, result["kod pocztowy"], "");
                           temp = Regex.Replace(temp, ":", "");
                           temp = Regex.Replace(temp, " ", "");

                           if (!result.ContainsKey("miejscowosc")) result.Add("miejscowosc", temp);
                           else
                           {
                               result["miejscowosc"] += "|" + temp;

                           }

                          // MessageBox.Show(result["miejscowosc"]);
                           copy = Regex.Replace(copy, result["miejscowosc"], "");
                           copy = Regex.Replace(copy, result["kod pocztowy"], "");

                       }
                       foreach (Match m in Regex.Matches(str, patterns["NIP"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           if (!result.ContainsKey("NIP")) result.Add("NIP", m.Value.ToString());
                           else result["NIP"] += "|" + m.Value.ToString();
                           copy = Regex.Replace(copy, result["NIP"], "");
                       }
                       foreach (Match m in Regex.Matches(str, "sprzedawca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = m.Value.ToString();
                           //MessageBox.Show(temp);
                           temp = Regex.Replace(temp, "sprzedawca", "", RegexOptions.IgnoreCase);
                           temp = Regex.Replace(temp, ":", "");
                           
                           if (!result.ContainsKey("nazwa")) result.Add("nazwa",temp);
                           else result["nazwa"] += "|" + temp;
                           copy = Regex.Replace(copy, result["nazwa"], "");
                       }
                       foreach (Match m in Regex.Matches(str, "nabywca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = m.Value.ToString();
                          // MessageBox.Show(temp);
                           temp = Regex.Replace(temp, "nabywca", "",RegexOptions.IgnoreCase);
                           temp = Regex.Replace(temp, ":", "");
                           temp = Regex.Replace(temp, "^ ", "");
                           if (!result.ContainsKey("nazwa")) result.Add("nazwa", temp);
                           else result["nazwa"] += "|" + temp;
                           copy = Regex.Replace(copy, result["nazwa"], "");
                       }
                        foreach (Match m in Regex.Matches(str, patterns["ulica"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            if (!result.ContainsKey("ulica")) result.Add("ulica", m.Value.ToString());
                            else result["ulica"] += "|" + m.Value.ToString();
                            copy = Regex.Replace(copy, result["ulica"], "");
                           // string pattern = @"((ul)|(uł)|(u1)){1}.?(\s)?";
                            string pattern = "((ul)|(uł)|(u1)){1}";

                        foreach (Match ma in Regex.Matches(result["ulica"], pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                           // MessageBox.Show(ma.Value.ToString());
                            if (result.ContainsKey("ulica"))
                            result["ulica"] = Regex.Replace(result["ulica"],ma.Value.ToString(),"");
                        }
                            
                        result["ulica"] = Regex.Replace(result["ulica"], @"\.", "");
                        }
                        foreach (Match m in Regex.Matches(str, patterns["numer ulicy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                     //       MessageBox.Show("a:"+str);

                    //        MessageBox.Show("numer: "+m.Value.ToString());
                            if (!result.ContainsKey("numer ulicy")) result.Add("numer ulicy", m.Value.ToString());
                            else result["numer ulicy"] += "|" + m.Value.ToString();
                            copy = Regex.Replace(copy, result["numer ulicy"], "");
                            result["numer ulicy"] = Regex.Replace(result["numer ulicy"], result["ulica"], "");
                            string pattern = @"((ul)|(uł)|(u1)){1}\.*";
                            result["numer ulicy"] = Regex.Replace(result["numer ulicy"], pattern, "");
                        }

                      // if(result.ContainsKey("ulica"))
                        

                   }

                   if (!result.ContainsKey("ulica") && !result.ContainsKey("numer ulicy"))
                   {
                       copy = Regex.Replace(copy, "N.P", "",RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, "nabywca", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, "sprzedawca", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, ":", "");
                       copy = Regex.Replace(copy, "\n", "");
                       result["ulica"] = Regex.Replace(copy, "[0-9]{1,3}", "");
                      // MessageBox.Show("ulica po: " + result["ulica"]);
                       result["numer ulicy"] = Regex.Replace(copy, @"\D*", "");
                   }
                   //MessageBox.Show("left: " + copy);
                   break;
               case("sprzedawca"):
                  // Dictionary<string, string> nabywca = new Dictionary<string, string>();
                   foreach(string str in lines)
                   {
                       foreach (Match m in Regex.Matches(str, patterns["kod pocztowy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = str;
                           if (!result.ContainsKey("kod pocztowy")) result.Add("kod pocztowy", m.Value.ToString());
                           else
                           {
                               
                               temp = Regex.Replace(temp, result["kod pocztowy"], "");
                               result["kod pocztowy"] += "|" + m.Value.ToString();
                           }
                            
                           temp = Regex.Replace(temp, result["kod pocztowy"], "");
                           temp = Regex.Replace(temp, ":", "");
                           temp = Regex.Replace(temp, " ", "");

                           if (!result.ContainsKey("miejscowosc")) result.Add("miejscowosc", temp);
                           else
                           {
                               result["miejscowosc"] += "|" + temp;

                           }

                          // MessageBox.Show(result["miejscowosc"]);
                           copy = Regex.Replace(copy, str, "");
                           copy = Regex.Replace(copy, result["miejscowosc"], "");
                           copy = Regex.Replace(copy, result["kod pocztowy"], "");

                       }
                       foreach (Match m in Regex.Matches(str, patterns["NIP"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           if (!result.ContainsKey("NIP")) result.Add("NIP", m.Value.ToString());
                           else result["NIP"] += "|" + m.Value.ToString();
                           copy = Regex.Replace(copy, result["NIP"], "");
                       }
                       foreach (Match m in Regex.Matches(str, "sprzedawca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = m.Value.ToString();
                           //MessageBox.Show(temp);
                           temp = Regex.Replace(temp, "sprzedawca", "", RegexOptions.IgnoreCase);
                           temp = Regex.Replace(temp, ":", "");
                           
                           if (!result.ContainsKey("nazwa")) result.Add("nazwa",temp);
                           else result["nazwa"] += "|" + temp;
                           copy = Regex.Replace(copy, str, "");
                          // copy = Regex.Replace(copy, result["nazwa"], "");
                       }
                       foreach (Match m in Regex.Matches(str, "nabywca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = m.Value.ToString();
                          // MessageBox.Show(temp);
                           temp = Regex.Replace(temp, "nabywca", "",RegexOptions.IgnoreCase);
                           temp = Regex.Replace(temp, ":", "");
                           temp = Regex.Replace(temp, "^ ", "");
                           if (!result.ContainsKey("nazwa")) result.Add("nazwa", temp);
                           else result["nazwa"] += "|" + temp;
                         //  copy = Regex.Replace(copy, result["nazwa"], "");
                           copy = Regex.Replace(copy, str, "");
                       }
                        foreach (Match m in Regex.Matches(str, patterns["ulica"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            if (!result.ContainsKey("ulica")) result.Add("ulica", m.Value.ToString());
                            else result["ulica"] += "|" + m.Value.ToString();
                            copy = Regex.Replace(copy, result["ulica"], "");
                           // string pattern = @"((ul)|(uł)|(u1)){1}.?(\s)?";
                            string pattern = "((ul)|(uł)|(u1)){1}";

                        foreach (Match ma in Regex.Matches(result["ulica"], pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                           // MessageBox.Show(ma.Value.ToString());
                            if (result.ContainsKey("ulica"))
                            result["ulica"] = Regex.Replace(result["ulica"],ma.Value.ToString(),"");
                        }
                            
                        result["ulica"] = Regex.Replace(result["ulica"], @"\.", "");
                        }
                        foreach (Match m in Regex.Matches(str, patterns["numer ulicy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                          //  MessageBox.Show("a:"+str);

                       //     MessageBox.Show("numer: "+m.Value.ToString());
                            if (!result.ContainsKey("numer ulicy")) result.Add("numer ulicy", m.Value.ToString());
                            else result["numer ulicy"] += "|" + m.Value.ToString();
                            copy = Regex.Replace(copy, result["numer ulicy"], "");
                            result["numer ulicy"] = Regex.Replace(result["numer ulicy"], result["ulica"], "");
                            string pattern = @"((ul)|(uł)|(u1)){1}\.*";
                            result["numer ulicy"] = Regex.Replace(result["numer ulicy"], pattern, "");
                        }

                      // if(result.ContainsKey("ulica"))
                        

                   }

                   if (!result.ContainsKey("ulica") && !result.ContainsKey("numer ulicy"))
                   {
                       copy = Regex.Replace(copy, "N.P", "",RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, "nabywca", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, "sprzedawca", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, ":", "");
                       copy = Regex.Replace(copy, "\n", "");
                       result["ulica"] = Regex.Replace(copy, "[0-9]{1,3}", "");
                      // MessageBox.Show("ulica po: " + result["ulica"]);
                       result["numer ulicy"] = Regex.Replace(copy, @"\D*", "");
                   }
                   //MessageBox.Show("left: " + copy);
                   break;
               case("naglowek"):
                   
                   result["nazwa"]=lines[0];
                   foreach (string str in lines)
                   {
                       foreach (Match m in Regex.Matches(str, patterns["kod pocztowy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = str;
                           if (!result.ContainsKey("kod pocztowy")) result.Add("kod pocztowy", m.Value.ToString());
                           else
                           {

                               temp = Regex.Replace(temp, result["kod pocztowy"], "");
                               result["kod pocztowy"] += "|" + m.Value.ToString();
                           }

                           temp = Regex.Replace(temp, result["kod pocztowy"], "");
                           temp = Regex.Replace(temp, ":", "");
                           temp = Regex.Replace(temp, " ", "");

                           if (!result.ContainsKey("miejscowosc")) result.Add("miejscowosc", temp);
                           else
                           {
                               result["miejscowosc"] += "|" + temp;

                           }

                           // MessageBox.Show(result["miejscowosc"]);
                           copy = Regex.Replace(copy, result["miejscowosc"], "");
                           copy = Regex.Replace(copy, result["kod pocztowy"], "");
                           copy = Regex.Replace(copy, str, "");

                       }
                       foreach (Match m in Regex.Matches(str, patterns["NIP"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           if (!result.ContainsKey("NIP")) result.Add("NIP", m.Value.ToString());
                           else result["NIP"] += "|" + m.Value.ToString();
                           copy = Regex.Replace(copy, result["NIP"], "");
                           copy = Regex.Replace(copy, "N.P:*", "",RegexOptions.IgnoreCase);
                       }
                   
                 
                       foreach (Match m in Regex.Matches(str, patterns["ulica"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           if (!result.ContainsKey("ulica")) result.Add("ulica", m.Value.ToString());
                           else result["ulica"] += "|" + m.Value.ToString();
                           copy = Regex.Replace(copy, result["ulica"], "");
                           // string pattern = @"((ul)|(uł)|(u1)){1}.?(\s)?";
                           string pattern = "((ul)|(uł)|(u1)){1}";

                           foreach (Match ma in Regex.Matches(result["ulica"], pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                           {
                               // MessageBox.Show(ma.Value.ToString());
                               if (result.ContainsKey("ulica"))
                                   result["ulica"] = Regex.Replace(result["ulica"], ma.Value.ToString(), "");
                           }

                           result["ulica"] = Regex.Replace(result["ulica"], @"\.", "");
                       }
                       foreach (Match m in Regex.Matches(str, patterns["numer ulicy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                          // MessageBox.Show("a:" + str);

                           MessageBox.Show("numer: " + m.Value.ToString());
                           if (!result.ContainsKey("numer ulicy")) result.Add("numer ulicy", m.Value.ToString());
                           else result["numer ulicy"] += "|" + m.Value.ToString();
                           copy = Regex.Replace(copy, result["numer ulicy"], "");
                           result["numer ulicy"] = Regex.Replace(result["numer ulicy"], result["ulica"], "");
                           string pattern = @"((ul)|(uł)|(u1)){1}\.*";
                           result["numer ulicy"] = Regex.Replace(result["numer ulicy"], pattern, "");
                       }

                       foreach (Match m in Regex.Matches(str, patterns["numer konta"]))
                       {
                           if (!result.ContainsKey("numer konta")) result.Add("numer konta", m.Value.ToString());
                           else result["numer konta"] += "|" + m.Value.ToString();
                          // copy = Regex.Replace(copy, result["numer konta"], "");
                           copy = Regex.Replace(copy, str, "",RegexOptions.IgnoreCase);
                       }
                       foreach (Match m in Regex.Matches(str, patterns["adres email"]))
                       {
                           if (!result.ContainsKey("adres email")) result.Add("adres email", m.Value.ToString());
                           else result["adres email"] += "|" + m.Value.ToString();
                           // copy = Regex.Replace(copy, result["numer konta"], "");
                           copy = Regex.Replace(copy, str, "", RegexOptions.IgnoreCase);
                       }
                       foreach (Match m in Regex.Matches(str, patterns["strona"]))
                       {
                           if (!result.ContainsKey("strona")) result.Add("strona", m.Value.ToString());
                           else result["strona"] += "|" + m.Value.ToString();
                           // copy = Regex.Replace(copy, result["numer konta"], "");
                           copy = Regex.Replace(copy, str, "", RegexOptions.IgnoreCase);
                       }
                       foreach (Match m in Regex.Matches(str, patterns["telefon"]))
                       {
                           if (!result.ContainsKey("telefon")) result.Add("telefon", m.Value.ToString());
                           else result["telefon"] += "|" + m.Value.ToString();
                           // copy = Regex.Replace(copy, result["numer konta"], "");
                           copy = Regex.Replace(copy, str, "", RegexOptions.IgnoreCase);
                           result["telefon"] = Regex.Replace(result["telefon"], ":", "");
                           result["telefon"] = Regex.Replace(result["telefon"], ",", "");
                       }
                   

                   }

                   if (!result.ContainsKey("ulica") && !result.ContainsKey("numer ulicy"))
                   {
                       copy = Regex.Replace(copy, "N.P", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, "nabywca", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, "sprzedawca", "", RegexOptions.IgnoreCase);
                       copy = Regex.Replace(copy, ":", "");
                       copy = Regex.Replace(copy, "\n", "");
                       result["ulica"] = Regex.Replace(copy, "[0-9]{1,3}", "");
                       MessageBox.Show("ulica po: " + result["ulica"]);
                       result["numer ulicy"] = Regex.Replace(copy, @"\D*", "");
                   }
                   break;
               default:
                   break;
           }
            return result;
        }

        public static List<string> GetTextWithoutHeaders(string  text)
        {
            text=Regex.Replace(text,@"~.*\~","^");
            List<string> sections = new List<string>(text.Split('^'));
            sections.RemoveAt(0);
            return sections;
        }

        public static List<List<string>> GetHeaders(string text)
        {
            
            List<string> lines = Patterning.RemoveEmpltyLines(text);
            string orig = text;
            List<string> headers = new List<string>();
            List<List<string>> result = new List<List<string>>();
            foreach (string s in lines)
            {
                foreach (Match m in Regex.Matches(s, @"~.*~"))
                {
               
                    string str = m.Value.ToString().Substring(1, m.Value.ToString().Length - 2);
                 //   if (str.Contains(";"))
                        result.Add(new List<string>(str.Split(';')));
                  //  else
                  //  {
                  //      result.Add(new List<string>());
                  //      result[result.Count - 1].Add(str);
                  //  }
                    result[result.Count - 1].RemoveAt(result[result.Count - 1].Count - 1);
              
                }
            }
          
           
            return result;
        }

        public void showResults()
        {
            foreach (KeyValuePair<string, string> kvp in results)
                MessageBox.Show(kvp.Key+" "+kvp.Value);
        }
    }
}
