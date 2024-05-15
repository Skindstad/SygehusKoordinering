using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
   public partial class LoginViewModel : ObservableObject
    {
        public static PersonaleRepository personales = [];


        public LoginViewModel() {
        
        }

        [ObservableProperty]
        string mail;

        [ObservableProperty]
        string arbejdeTlf;

        [ObservableProperty]
        string adgangskode;


        [RelayCommand]
        void Login()
        {
           Personale me = personales.Login(Mail, Adgangskode, ArbejdeTlf);

            if(me != null)
            {
                MainViewModel.data.Add(me);
                Main();
            }

        }


        async Task Main()
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    public class LoginData
    {
        private Personale data;
        private List<Display> Displays = new List<Display>();
        public void Add(Personale p) 
        {
        data = p;
        }

        public Personale Getpersonal()
        {
            return data;
        }
        public List<Display> GetDisplays()
        {
            return Displays;
        }
        
        public void AddDisplay(Station location)
        {
            Displays.Add(new Display(location));
        }


        public void AddLocation(string location)
        {
            data.Lokations.Add(location);
        }
        public void ClearLocation()
        {
            Displays.Clear();
            data.Lokations.Clear();
            BundleRepository.removeLocation(data.CPR);
        }

    }

    public class Station : IObservable
    {
        public string Name { get; set; }
        public List<IObserver> Observers = new List<IObserver>();
        public string currentPriority;

        public Station(string name)
        {
            this.Name = name;
        }

        public void Add(IObserver obs)
        {
            Observers.Add(obs);
        }

        public void nodify()
        {
            foreach (IObserver observer in Observers)
            {
                observer.update();
            }
        }
        public void Remove(IObserver obs)
        {
            Observers.Remove(obs);
        }

        public void setPriority(string priority)
        {
            currentPriority = priority;
        }

        public string getPriority()
        {
            return currentPriority;
        }


    }

    public class Display : IObserver
    {
        public Station myStation = null ;
        public static OplysningViewModel oplysning = null;
        public string currentPriority;

        public Display(Station station)
        {
            this.myStation = station;
        }
        
        public void Oplysning(OplysningViewModel oplysningView)
        {
            oplysning = oplysningView;
        }
        public void update()
        {
            currentPriority = myStation.getPriority();

            switch (currentPriority)
            {
                case "Normal":
                    Sound.play("SoundEffect 2.wav");
                    break;
                case "Haster":
                    Sound.play("SoundEffect.wav");
                    break;
                case "Livstruende":
                    Sound.play("SoundEffect 3.wav");
                    break;
            }
           

            oplysning.Find();
        }
    }
    public interface IObservable
    {
        public void Add(IObserver obs);
        public void Remove(IObserver obs);
        public void nodify();
    }

    public interface IObserver
    {
        public void update();
    }

    public class Sound
    {

        public static void play(string filename)
        {
            string currentDirectory = "G:\\bøger\\Programmering\\Code\\SygehusKoordinering\\SygehusKoordinering";


            string soundFilePath = Path.Combine(currentDirectory, "Assets", filename);



            if (File.Exists(soundFilePath))
            {

                SoundPlayer player = new SoundPlayer(soundFilePath);


                player.Play();
            }
            else
            {
                Console.WriteLine("Sound file not found: " + soundFilePath);
            }

        }

    }

}
