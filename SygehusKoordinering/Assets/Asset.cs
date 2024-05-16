using System.Media;


namespace SygehusKoordinering.Assets
{
    public class Asset
    {
        public static void play(string filename)
        {
            
            string folderName = "Assets";
            string[] directories = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory, folderName, SearchOption.AllDirectories);

            string currentDirectory = null;

            if (directories.Length > 0)
            {
                foreach (string directory in directories)
                {
                    Console.WriteLine($"Found folder: {directory}");
                    currentDirectory = directory;
                }
            }
            else
            {
                Console.WriteLine("Folder not found.");
                currentDirectory = "";
            }


            string soundFilePath = Path.Combine(currentDirectory, filename);



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
