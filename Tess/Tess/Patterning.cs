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
        public Dictionary<char, char> frequentlyMistakenChars;
        public List<string> text;
        
        enum chars
        {
            ALL,

        };

        public Patterning()
        {
            patterns = new Dictionary<string, string>();
            results = new Dictionary<string, string>();
            text = new List<string>();
            this.FillPatterns();
            this.InitiateResults();
        }

        public Patterning(List<string> t)
        {
            patterns = new Dictionary<string, string>();
            results = new Dictionary<string, string>();
            text = t;
            this.FillPatterns();
            this.InitiateResults();
        }

        public void FillPatterns()
        {
           
            this.patterns.Add("kod pocztowy", "( [0-9]{2}\\-[0-9]{3}[^-])|(^[0-9]{2}\\-[0-9]{3}[^-])");
            this.patterns.Add("numer faktury",@"");
           // this.patterns.Add("adres email", "[0-9|a-z|_|-]+@[0-9|a-z|_|-]+\\.[a-z]{2,3}");
            this.patterns.Add("adres email", "[a-z0-9_.-]+@[a-z0-9_.-]+\\.\\w{2,3}");

            this.patterns.Add("NIP", @"((\s)(PL)?[0-9]{10}\s|[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2})|([0-9]{2}-[0-9]{2}-[0-9]{3}-[0-9]{3})|([0-9]{2}-[0-9]{3}-[0-9]{2}-[0-9]{3})|([0-9]{3}-[0-9]{2}-[0-9]{3}-[0-9]{2})|([0-9]{3}-[0-9]{2}-[0-9]{2}-[0-9]{3})");
            this.patterns.Add("numer klienta", "numer klienta[:]?[ ]*([0-9]|-)*");
           // this.patterns.Add("data1","([0-9]{4}-[0-9]{2}-[0-9]{2})|([0-9]{2}/[0-9]{2}/[0-9]{4})|([0-9]{2}.[0-9]{2}.[0-9]{4})");
            //this.patterns.Add("data2", "([0-9]{4}-[0-9]{2}-[0-9]{2})|([0-9]{2}/[0-9]{2}/[0-9]{4})|([0-9]{2}.[0-9]{2}.[0-9]{4})");
            this.patterns.Add("data1", @"([0-9]{4}[-|\.|/]{1}[0-9]{1,2}[-|\.|/]{1}[0-9]{1,2})|([0-9]{1,2}[-|\.|/]{1}[0-9]{1,2}[-|\.|/]{1}[0-9]{4})");
            this.patterns.Add("data2", @"([0-9]{4}[-|\.|/]{1}[0-9]{1,2}[-|\.|/]{1}[0-9]{1,2})|([0-9]{1,2}[-|\.|/]{1}[0-9]{1,2}[-|\.|/]{1}[0-9]{4})");

            this.patterns.Add("numer konta", "([0-9]{26})|([0-9]{2} [0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4} [0-9]{4})");
          //  this.patterns.Add("ulica", "((ul.)|(ul)|(ul )|(ul. )|(uł )|(uł. )|(uł)|(uł.)|(u1 )|(u1. )|(u1)|(u1.)|(uI )|(uI. )|(uI)|(uI.))([a-z]{3,})");
            //this.patterns.Add("numer ulicy", "((ul.)|(ul)|(ul )|(ul. )|(uł )|(uł. )|(uł)|(uł.)|(u1 )|(u1. )|(u1)|(u1.)|(uI )|(uI. )|(uI)|(uI.))([a-z]{3,})([ ]*[0-9]{1,3})");
            this.patterns.Add("numer ulicy", "((ul)|(uł)|(u1)){1}.? ?([a-z]{3,} *)+( )*[0-9]{1,3}");
            this.patterns.Add("strona", @"www\.[^\s]+\.[^\s]+");
            this.patterns.Add("ulica", "((ul)|(uł)|(u1)){1}.? ?([a-z]{3,} *)+");
           // this.patterns.Add("telefon1", @"\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})((\s)|,)");
            this.patterns.Add("telefon2", @"\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-]");
            this.patterns.Add("telefon1", @"\s((\()?([0-9]{1}-?[0-9]{2})(\))?)?(\s)?([0-9]{3}(-|(\s))?[0-9]{2}(-|(\s))?[0-9]{2})[^0-9\-]");
        //    this.patterns.Add("sposob platnosci", "((spos[oó]{1}b p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?).*)|((forma p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?).*)");
            this.patterns.Add("sposob platnosci", "(((spos[oó]{1}b)|(forma) (p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci)|(zap[lłt1]{1}a[1ltł]{1}y):?).*)");

            //   this.
            //  this.patterns.Add("telefon1", "(\\([0-9]{3}\\))? *([0-9]{3}[0-9]{2}[0-9]{2})|([0-9]{7})");
            //this.patterns.Add("telefon1", "(\\([0-9]{3}\\))? *([^-][0-9]{3}-[0-9]{2}-[0-9]{2}[^-])|([0-9]{7})");

        }

        public void InitiateResults()
        {
           //faktury
            this.results.Add("numer faktury", String.Empty);
            this.results.Add("sprzedawca faktura", String.Empty);
            this.results.Add("nabywca faktura", String.Empty);
            this.results.Add("data1", String.Empty);
            this.results.Add("data2", String.Empty);
            this.results.Add("kwota", String.Empty);
            this.results.Add("sposob platnosci", String.Empty);
            this.results.Add("netto faktury", String.Empty);
            this.results.Add("brutto faktury", String.Empty);

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

            //towary
            this.results.Add("nazwa towaru", String.Empty);
            this.results.Add("ilosc towaru", String.Empty);
            this.results.Add("netto towaru", String.Empty);
            this.results.Add("brutto towaru", String.Empty);
        }

        public void FillFrequentlyMistakenChars()
        {
            this.frequentlyMistakenChars.Add('0','o');
        }

        public static List<String> RemoveEmpltyLines(String s)
        {
            string[] l = s.Split('\n');
            List<String> lines = new List<string>();
            foreach (string a in l)
            {
                if (a != String.Empty)
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

        public string IterateFor(string text, string parameter)
        {
            List<string> lines = Patterning.RemoveEmpltyLines(text);
            string orig = text;
            switch(parameter)
            {
                case ("sposob platnosci"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, patterns["sposob platnosci"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = str;
                              //  string pattern = "(sposob p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?)";
                                string pattern = "((spos[oó]{1}b)|(forma)|(metoda) (p[lł1t]{1}a[lłt1]{1}no[śs]{1}ci:?)|(zap[lłt1]{1}a[1ltł]{1}y):?)";
                                temp = Regex.Replace(m.Value.ToString(), pattern, "", RegexOptions.IgnoreCase);
                               
                                
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
                        break;
                    }

                case ("telefon1"):
                case ("telefon2"):
                    {
                        foreach (string str in lines)
                            foreach (Match m in Regex.Matches(str, patterns["telefon1"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                MessageBox.Show("tel");
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
                        break;
                    }
                case ("kod pocztowy"):
                case ("miejscowosc"):
                    {
                        foreach(string str in lines)
                   
                       foreach (Match m in Regex.Matches(str, patterns["kod pocztowy"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                       {
                           string temp = str;
                           
                           
                               
                               
                               results["kod pocztowy"] = m.Value.ToString();
                           
                            
                           temp = Regex.Replace(temp, results["kod pocztowy"], "");
                           temp = Regex.Replace(temp, ":", "");
                           temp = Regex.Replace(temp, " ", "");

                           
                               results["miejscowosc"] = temp;

                           
                       }
                    break;
                    }
                case ("nazwa"):
                    {
                        foreach (string str in lines)
                        {
                            foreach (Match m in Regex.Matches(str, "sprzedawca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = m.Value.ToString();
                                //MessageBox.Show(temp);
                                temp = Regex.Replace(temp, "sprzedawca", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "");

                               
                                results["nazwa"] =  temp;

                            }
                            foreach (Match m in Regex.Matches(str, "nabywca.*", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                            {
                                string temp = m.Value.ToString();
                                
                                temp = Regex.Replace(temp, "nabywca", "", RegexOptions.IgnoreCase);
                                temp = Regex.Replace(temp, ":", "");
                                temp = Regex.Replace(temp, "^ ", "");
                                 results["nazwa"] = temp;

                            }
                            if(results["nazwa"]==String.Empty)
                            {
                                results["nazwa"] = lines[0];
                            }
                        }
                        break;
                    }
                    
                        case("ulica"):
                        {

                            foreach(string str in lines)
                            {
                                foreach (Match m in Regex.Matches(str, patterns["ulica"], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                {
                                    results["ulica"] = m.Value.ToString();
                                    string pattern = "((ul)|(uł)|(u1)){1}";

                                    foreach (Match ma in Regex.Matches(results["ulica"], pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                                    {
                                        
                                            results["ulica"] = Regex.Replace(results["ulica"], ma.Value.ToString(), "");
                                    }

                                    results["ulica"] = Regex.Replace(results["ulica"], @"\.", "");
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

                                    MessageBox.Show(m.Value.ToString());
                                    results["numer ulicy"] = m.Value.ToString();
                                    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], results["ulica"], "");
                                    string pattern = @"((ul)|(uł)|(u1)){1}\.*";
                                    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], pattern, "");
                                    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], @"\.", "");
                                    results["numer ulicy"] = Regex.Replace(results["numer ulicy"], @"\D", "");
                                    MessageBox.Show(results["numer ulicy"]);
                                }

                            }
                            break;
                        }
                case("adres email"):
                    {

                        foreach(string str in lines)
                        {
                                foreach (Match m in Regex.Matches(str, patterns["adres email"]))
                                {
                                results["adres email"] =  m.Value.ToString();
                         
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
                                results["numer konta"] = m.Value.ToString();
                    
                            }



                        }
                        break;
                    }
                   

                        
                    
                default:
                    break;
            }
            if (results.ContainsKey(parameter) && results[parameter]!=string.Empty)
            {
                string s = results[parameter];
                if (s[0] == ' ') s.Substring(1);
                if (s[s.Length-1] == ' ') s.Substring(0,s.Length-1);
                if (s[s.Length - 1] == ',') s.Substring(0, s.Length - 1);
                results[parameter] = s;
                return results[parameter];
            }
            else
            {
                MessageBox.Show("brak: " + parameter);
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

        public void showResults()
        {
            foreach (KeyValuePair<string, string> kvp in results)
                MessageBox.Show(kvp.Key+" "+kvp.Value);
        }
    }
}
