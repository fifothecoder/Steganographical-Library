using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StegoLib.Abstract_Classes;

namespace StegoLib
{
    sealed class WAVHider : WAVFile {

        private readonly byte SMALL_BIT_CHANGE_AMOUNT;

        public WAVHider(string target, byte bitChange) : base(target) {
            SMALL_BIT_CHANGE_AMOUNT = bitChange;
            _data = File.ReadAllBytes(_targetPath).ToList();
        }

        public void Hide(byte[] data) {

            

            string bigString = "";

            foreach (var d in data) bigString += Convert.ToString(d, 2).PadLeft(8, '0');

            for (int i = 0; i < bigString.Length;) {
                bool[] field = new bool[SMALL_BIT_CHANGE_AMOUNT];
                for (int j = 0; j < SMALL_BIT_CHANGE_AMOUNT; j++, i++) { field[j] = bigString[i] == '1'; }

                for (int j = 0; j < field.Length; j++) {
                    byte power = (byte)Math.Pow(2, j);

                    //Turn off specific bit
                }


            }

            
        }

        public void Hide(string message) {
            //Cast message to byte array
            byte[] data = new byte[message.Length];
            for (int i = 0; i < message.Length; i++) { data[i] = (byte)message[i]; }

            Hide(data);
        }
    }
}
