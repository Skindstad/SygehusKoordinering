using SygehusKoordinering.ViewModel;

namespace SygehusKoordinering.Object
{
    // Created By Jakob Skindstad Frederiksen
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
        public Station myStation = null;
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
                    Objects.Play("SoundEffect 2.wav");
                    break;
                case "Haster":
                    Objects.Play("SoundEffect.wav");
                    break;
                case "Livstruende":
                    Objects.Play("SoundEffect 3.wav");
                    break;
                default: break;
            }

            oplysning.Find();
        }
    }
}
