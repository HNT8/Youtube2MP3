using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;

namespace Youtube2MP3 {
    internal class Program {

        static void Main(string[] args) {
            Console.Title = "Youtube2MP3";

            Console.Write("Youtube Video URL: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string url = Console.ReadLine();
            Console.ResetColor();

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid URL!");
                Console.ResetColor();
                Thread.Sleep(3000);
                Environment.Exit(0);
            }

            Console.WriteLine("Successfully located YouTube video!");
            Console.WriteLine("Converting now...");

            YouTube youtube = YouTube.Default;
            YouTubeVideo video = youtube.GetVideo(url);
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + video.Title;

            File.WriteAllBytes(destination, video.GetBytes());

            MediaFile inputFile = new MediaFile { Filename = destination };
            MediaFile outputFile = new MediaFile { Filename = destination + ".mp3" };

            using (Engine engine = new Engine()) {
                engine.GetMetadata(inputFile);
                engine.Convert(inputFile, outputFile);
            }

            File.Delete(destination);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The video has successfully been converted");
            Console.WriteLine("and the MP3 is now on your desktop!");
            Console.ResetColor();

            Thread.Sleep(3000);
        }
    }
}
