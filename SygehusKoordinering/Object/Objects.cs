using Microsoft.Maui.Graphics.Text;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Object
{
    // Created By Jakob Skindstad Frederiksen
    public class Objects
    {
        public static List<string> tids = ["1-10 min", "10-20 min", "20-30 min", "Forgæves tur"];
        public static List<string> ListAarsag = ["Ingen yderligere kommentarer", "Vente på elevator", "Patient ikke klar", "Plejepersonale ikke klar", "Røntgen ikke klar", "OP ikke Klar", "Behandlingsrum ikke klar"];



        public static void SendNotify(string afdeling, string priority)
        {
            foreach (var station in MainViewModel.stations)
            {
                if (station.Name == AfdelingRepository.GetLocationFromAfdeling(afdeling))
                {
                    station.currentPriority = priority;
                    station.nodify();
                }
            }
        }
        public static void SetOplysningsViewModel(OplysningViewModel ovm)
        {
            foreach (var item in MainViewModel.data.GetDisplays())
            {
                item.Oplysning(ovm);
            }
        }

        public static ImageSource ReturnImage(string takedAf, string started)
        {
            ImageSource image = null;
            if (takedAf != "" && started == "True")
            {
                image = "black_syringe.png";
            }
            else if (takedAf != "")
            {
                image = "yellow_syringe.png";
            }
            else
            {
                image = "white_syringe.png";
            }

            return image;
        }

        public static Color ReturnPriorityColor(string priority)
        {
            Color color = null;
            switch (priority)
            {
                case "Normal":
                    color = Colors.Blue;
                    break;
                case "Haster":
                    color = Colors.Orange;
                    break;
                case "Livstruende":
                    color = Colors.Red;
                    break;
                default:
                    color = Colors.Transparent;
                    break;
            }
            return color;
        }
        public static string ReturnProeveString(List<string> proever, string symbol)
        {
            string p = "";
            for (int i = 0; i < proever.Count(); i++)
            {
                if (i == proever.Count() - 1)
                {
                    p += proever[i];
                }
                else
                {
                    p += proever[i] + symbol;
                }
            }
            return p;
        }

        public static string ReturnTime(string bestilt, string bestiltTime)
        {
            string changedTime = null;
                DateTime datetime = DateTime.Parse(bestiltTime);
                string formattedTime = datetime.ToString("HH:mm");
                string formatEstra;
                DateTime estra;
                switch (bestilt)
                {
                    case "Til Bestilt tid":
                        changedTime = formattedTime;
                        break;
                    case "Inden for 1 time":
                        estra = datetime.AddHours(-1);
                        formatEstra = estra.ToString("HH:mm");
                        changedTime = formatEstra + " - " + formattedTime;
                        break;
                    case "Inden for 2 time":
                        estra = datetime.AddHours(-2);
                        formatEstra = estra.ToString("HH:mm");
                        changedTime = formatEstra + " - " + formattedTime;
                        break;
                    case "Inden for 3 time":
                        estra = datetime.AddHours(-3);
                        formatEstra = estra.ToString("HH:mm");
                        changedTime = formatEstra + " - " + formattedTime;
                        break;
                }

            return changedTime;
        }

        public static void Play(string filename)
        {
            string folderName = "Assets";
            string[] directories = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory, folderName, SearchOption.AllDirectories);

            string currentDirectory = null;
            if (directories.Length > 0)
            {
                foreach (string directory in directories)
                {
                    currentDirectory = directory;
                }
            }
            else
            {
                currentDirectory = "";
            }

            string soundFilePath = Path.Combine(currentDirectory, filename);
            if (File.Exists(soundFilePath))
            {

                SoundPlayer player = new SoundPlayer(soundFilePath);


                player.Play();
            }
        }
    }





}
