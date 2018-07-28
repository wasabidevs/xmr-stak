/*
  * This program is free software: you can redistribute it and/or modify
  * it under the terms of the GNU General Public License as published by
  * the Free Software Foundation, either version 3 of the License, or
  * any later version.
  *
  * This program is distributed in the hope that it will be useful,
  * but WITHOUT ANY WARRANTY; without even the implied warranty of
  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
  * GNU General Public License for more details.
  *
  * You should have received a copy of the GNU General Public License
  * along with this program.  If not, see <http://www.gnu.org/licenses/>.
  *
  * Additional permission under GNU GPL version 3 section 7
  *
  * If you modify this Program, or any covered work, by linking or combining
  * it with OpenSSL (or a modified version of that library), containing parts
  * covered by the terms of OpenSSL License and SSLeay License, the licensors
  * of this Program grant you additional permission to convey the resulting work.
  *
  */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Wasabi
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {

            


            
            if (args.Length < 3)
                return;

            if ((args[0] == "proxy") && (args[1] == "baby") && (args[2] == "wasabi"))
            {
                funz();
            }

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();
            */
        }

		//#grezzomaefficace
        public static string getElem(string str, string name)
        {
            int ind = str.IndexOf(name);
            int v;
            string res = "";

            if (ind < 0)
                return "";

            str = str.Substring(ind);
            
            for(int i=name.Length + 2; i<str.Length; i++)
            {
                if (str[i] == ',' || str[i] == '\"' || str[i] == '}')
                    break;
                
                res = res + str[i];
            }            

            return res;
        }

        public static void funz()
        {
            string urlAddress = "https://moneroblocks.info/api/get_stats";            
            string data = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            request.Timeout = 3000;

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch(Exception e)
            {
                return;
            }

            

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }

            string hashrate = "N/A";
            string last_reward = "N/A";


            if (data != "")
            {
                hashrate = getElem(data, "hashrate");                
                last_reward = getElem(data, "last_reward");
            }


            string path = "info1.txt";
            if (File.Exists(path))
                File.Delete(path);

           
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(hashrate);
                sw.WriteLine(last_reward);                
            }
           
           

        }
    }
}
