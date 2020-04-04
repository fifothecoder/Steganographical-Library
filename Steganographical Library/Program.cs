using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Steganographical_Library.Abstracts;
using Steganographical_Library.Validators;

namespace StegoLib
{
    class Program
    {
        [STAThread]
        static void Main(string[] args) {


            var wav = new WAVConstructor("testing.wav");
            wav.Construct(Helpers.LoadDataFile("wav", "mp3").ToArray());

            /*var valid = new WAVValidator("testing.wav");
            bool isValid = valid.Validate();*/


            Console.WriteLine("Completed!");
            Console.ReadKey(true);
        }

        
    }
}
