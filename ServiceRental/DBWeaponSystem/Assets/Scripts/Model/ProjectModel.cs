using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class ProjectModel
    {
        public string pno;
        public string pname;
        public string pcontent;
        public string ptime;
        public string ptype;
        public float pmoney;
        public int pfind;
        public string cno;

        int number = 8;
        string strread;
        public ProjectModel(string Str)
        {
            foreach (char str in Str)
            {
                if (str != ' ')
                {
                    strread += str;
                }
                else if (strread != "")
                {
                    switch (number)
                    {
                        case 8:
                            pno = strread;
                            break;
                        case 7:
                            pname = strread;
                            break;
                        case 6:
                            pcontent = strread;
                            break;
                        case 5:
                            ptime = strread;
                            break;
                        case 4:
                            ptype = strread;
                            break;
                        case 3:
                            pmoney = float.Parse(strread);
                            break;
                        case 2:
                            pfind = int.Parse(strread);
                            break;
                        case 1:
                            cno = strread;
                            break;

                    }
                    number--;
                    strread = "";
                }
            }
        }
    }
}
