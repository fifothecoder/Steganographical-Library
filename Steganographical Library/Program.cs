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

            byte[] arr = new byte[50000];
            for (int i = 0; i < 50000; i++) { arr[i] = 120; }

            var wav = new WAVConstructor("testing.wav");
            wav.Construct(arr);

            var valid = new WAVValidator("testing.wav");
            bool isValid = valid.Validate();


            Console.WriteLine("Completed!");
            Console.ReadKey(true);
        }

        
    }
}
