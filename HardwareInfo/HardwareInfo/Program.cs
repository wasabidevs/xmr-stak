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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Management;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor.Hardware;

namespace HardwareInfo
{
    public class OHData
    {
        string path = @"info.txt";
        string fconf = @"HardwareInfoConfig.txt";
        bool celsius = true;


        // DATA ACCESSOR
        public List<OHMitem> DataList
        {
            get
            {
                return ReportItems;
            }
            set
            {

            }

        }

        // UPDATE METHOD
        public void Update()
        {
            if (File.Exists(path))
                File.Delete(path);

            if( File.Exists(fconf) )
            {
                string cf = System.IO.File.ReadAllText(fconf).ToLower();

                string cs;

                //cerco i gradi
                cs = "degreesincelsius=";             
                celsius = (cf.Substring(cf.IndexOf(cs) + cs.Length, 1) == "1");


            }

            UpdateOHM();
        }

        // for report compilation
        public class OHMitem
        {
            public OHMitem()
            {
            }
            public string name
            {
                get
                {
                    return Name;
                }
                set
                {
                    Name = value;
                }
            }
            public string type
            {
                get
                {
                    return OHMType;
                }
                set
                {
                    OHMType = value;
                }
            }
            public string reading
            {
                get
                {
                    return OHMValue;
                }
                set
                {
                    OHMValue = value;
                }
            }

            private string Name = String.Empty;
            private string OHMType = String.Empty;
            private string OHMValue = String.Empty;

        }
        // for report compilation
        private List<OHMitem> ReportItems = new List<OHMitem>();

        // ADDS ITEMS TO REPORT
        private void AddReportItem(string ARIName, string ARIType, string ARIValue)
        {
            if ((ARIType == "Data" && ARIValue == "" && !ARIName.Contains("Memory")) || (ARIType == "Level" && ARIValue == ""))
            {
                return;
            }

            // END REV

            OHMitem ARItem = new OHMitem();
            ARItem.name = ARIName;
            ARItem.type = ARIType;
            ARItem.reading = ARIValue;
            if (ARIType == "GpuAti")
            {
                ARItem.type = "Graphics Card";
            }

            if (ARIType == "Temperature")
            {
                try
                {
                    double temp = Convert.ToDouble(ARIValue);
                    // 01-26-2017 ARItem.reading = ((((9.0 / 5.0) * temp) + 32).ToString("000.0") + " F");

                    if (celsius)
                        ARItem.reading = temp.ToString() + " C";
                    else
                        ARItem.reading = ((((9.0 / 5.0) * temp) + 32).ToString("F1") + " F");
                }
                catch
                {

                    return;
                }
            }
            if (ARIType == "Clock")
            {
                try
                {
                    double temp = Convert.ToDouble(ARIValue);
                    if (temp < 1000)
                    {
                        ARItem.reading = (temp.ToString("F1") + " MHZ");
                    }
                    else
                    {
                        temp = temp / 1000;
                        ARItem.reading = (temp.ToString("F1") + " GHZ");
                    }
                }
                catch
                {
                    return;
                }

            }
            if (ARIType == "Control" || ARIType == "Load")
            {
                try
                {
                    double temp = Convert.ToDouble(ARIValue);
                    ARItem.name = ARIName;
                    ARItem.reading = (temp.ToString("F1") + " %"); // REV 10-30-2017 F0->F1
                }
                catch
                {
                    return;
                }
            }
            if (ARIType == "Voltage")
            {
                try
                {
                    double temp = Convert.ToDouble(ARIValue);
                    ARItem.name = ARIName;
                    ARItem.reading = (temp.ToString("F1") + " V");
                }
                catch
                {
                    return;
                }
            }
            // 07-28-2016 Added This item
            if (ARIType == "Fan")
            {
                try
                {
                    double rpm = Convert.ToDouble(ARIValue);
                    ARItem.name = ARIName;
                    ARItem.reading = (rpm.ToString("F0") + " RPM");
                }
                catch
                {
                    return;
                }
            }
            // 01-27-2016 Added Item
            if (ARIType == "Power")
            {
                try
                {
                    double watts = Convert.ToDouble(ARIValue);
                    ARItem.name = ARIName;
                    ARItem.reading = (watts.ToString("F1") + " W");
                }
                catch
                {
                    return;
                }
            }


            if (ARItem.name == "")
                ARItem.name = "N/A";

            if (ARItem.type == "")
                ARItem.type = "N/A";

            if (ARItem.reading == "")
            {
                ARItem.reading = "N/A";
                ARItem.name = "$" + ARItem.name;
            }

            
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(ARItem.name);
                    sw.WriteLine(ARItem.type);
                    sw.WriteLine(ARItem.reading);
                }
            }
            else
            {             
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(ARItem.name);
                    sw.WriteLine(ARItem.type);
                    sw.WriteLine(ARItem.reading);
                }
            }


        }
        // LOCAL INSTANCE OHM
        private OpenHardwareMonitor.Hardware.Computer computerHardware = new OpenHardwareMonitor.Hardware.Computer();
        // UPDATE OHM DATA
        private void UpdateOHM()
        {

            string s = String.Empty;
            string name = string.Empty;
            string type = string.Empty;
            string value = string.Empty;
            int x, y, z, yy, zz;
            int hardwareCount;
            int subcount;
            int sensorcount;
            int moresubhardwarecount;
            int moresensorcount;
            ReportItems.Clear();
            computerHardware.MainboardEnabled = true;
            computerHardware.FanControllerEnabled = true;
            computerHardware.CPUEnabled = true;
            computerHardware.GPUEnabled = true;
            computerHardware.RAMEnabled = true;
            computerHardware.HDDEnabled = true;
            computerHardware.Open();
            hardwareCount = computerHardware.Hardware.Count();
            for (x = 0; x < hardwareCount; x++)
            {
                name = computerHardware.Hardware[x].Name;
                type = computerHardware.Hardware[x].HardwareType.ToString();
                value = ""; // no value for non-sensors;
                AddReportItem(name, type, value);
                subcount = computerHardware.Hardware[x].SubHardware.Count();

                // ADDED 07-28-2016
                // NEED Update to view Subhardware
                for (y = 0; y < subcount; y++)
                {
                    computerHardware.Hardware[x].SubHardware[y].Update();
                    if (computerHardware.Hardware[x].SubHardware[y].SubHardware.Count() > 0)
                    {
                        yy = computerHardware.Hardware[x].SubHardware[y].SubHardware.Count();
                        for (zz = 0; zz < yy; zz++)
                        {
                            computerHardware.Hardware[x].SubHardware[y].SubHardware[zz].Update();

                        }
                    }

                }

                if (subcount > 0)
                {
                    for (y = 0; y < subcount; y++)
                    {
                        sensorcount = computerHardware.Hardware[x].SubHardware[y].Sensors.Count();
                        // REV 08-06-2016
                        moresubhardwarecount = computerHardware.Hardware[x].SubHardware[y].SubHardware.Count();
                        // END REV
                        name = computerHardware.Hardware[x].SubHardware[y].Name;
                        type = computerHardware.Hardware[x].SubHardware[y].HardwareType.ToString();
                        value = "";
                        AddReportItem(name, type, value);

                        if (sensorcount > 0)
                        {

                            for (z = 0; z < sensorcount; z++)
                            {

                                name = computerHardware.Hardware[x].SubHardware[y].Sensors[z].Name;
                                type = computerHardware.Hardware[x].SubHardware[y].Sensors[z].SensorType.ToString();
                                value = computerHardware.Hardware[x].SubHardware[y].Sensors[z].Value.ToString();
                                AddReportItem(name, type, value);

                            }
                        }
                        // REV 08-06-2016
                        for (yy = 0; yy < moresubhardwarecount; yy++)
                        {
                            computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].Update();
                            moresensorcount = computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].Sensors.Count();
                            name = computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].Name;
                            type = computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].HardwareType.ToString();
                            value = "";
                            AddReportItem(name, type, value);

                            if (sensorcount > 0)
                            {

                                for (zz = 0; zz < sensorcount; zz++)
                                {

                                    name = computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].Sensors[zz].Name;
                                    type = computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].Sensors[zz].SensorType.ToString();
                                    value = computerHardware.Hardware[x].SubHardware[y].SubHardware[yy].Sensors[zz].Value.ToString();
                                    AddReportItem(name, type, value);

                                }
                            }

                        }
                        // END REV
                    }

                }

                sensorcount = computerHardware.Hardware[x].Sensors.Count();

                if (sensorcount > 0)
                {
                    for (z = 0; z < sensorcount; z++)
                    {

                        name = computerHardware.Hardware[x].Sensors[z].Name;
                        type = computerHardware.Hardware[x].Sensors[z].SensorType.ToString();
                        value = computerHardware.Hardware[x].Sensors[z].Value.ToString();
                        AddReportItem(name, type, value);

                    }
                }

            }

            computerHardware.Close();
        }
    }

    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args.Length < 3)
                return;

            if ((args[0] == "proxy") && (args[1] == "baby") && (args[2] == "wasabi"))
            {
                OHData data = new OHData();
                data.Update();
            }

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();
            */
        }



        
    }

    
}
