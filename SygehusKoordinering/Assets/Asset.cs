using System.Media;


namespace SygehusKoordinering.Assets
{
    public class Asset
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
