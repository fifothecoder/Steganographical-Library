using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using StegoLib.Abstract_Classes;
using StegoLib.Interfaces;

namespace StegoLib
{
    sealed class WAVConstructor : WAVFile, IConstructor
    {

        public WAVConstructor(string target) : base(target) {
            //Nothing here yet
        }

        public void Construct(byte[] dataBytes = null)  {
            
            if(_data.Count != 0) throw new DataException();

            //RIFF Header   - Big Endian
            _data.Add(82);
            _data.Add(73);
            _data.Add(70);
            _data.Add(70);

            //Filesize      - Little Endian
            _data.Add(0);
            _data.Add(0);
            _data.Add(0);
            _data.Add(0);

            //WAVE Header   - Big Endian
            _data.Add(87);
            _data.Add(65);
            _data.Add(86);
            _data.Add(69);

            //fmt Header    - Big Endian
            _data.Add(102);
            _data.Add(109);
            _data.Add(116);
            _data.Add(32);

            //Subchunk1Size - Little Endian
            _data.Add(16);
            _data.Add(0);
            _data.Add(0);
            _data.Add(0);

            _data.Add(1);   //AudioFormat (1)
            _data.Add(0);
            _data.Add(2);   //NumChannels (2)
            _data.Add(0);

            _data.Add(34);  //SampleRate (22050)
            _data.Add(86);
            _data.Add(0);   
            _data.Add(0);

            _data.Add(136); //Byte Rate (88200)
            _data.Add(88);
            _data.Add(1);   
            _data.Add(0);

            _data.Add(4);   //Block Align (4)
            _data.Add(0);
            _data.Add(16);  //Bits per sample (16)
            _data.Add(0);

            //DATA Header
            _data.Add(100);
            _data.Add(97);
            _data.Add(116);
            _data.Add(97);

            //Subchunk2Size
            _data.Add(0);
            _data.Add(0);
            _data.Add(0);
            _data.Add(0);

            byte[] bytes;

            if (dataBytes != null) {
                foreach (var d in dataBytes) _data.Add(d); //Add custom data

                //Setup right subchunk size
                string chunkHex = ((_data.Count - 44) * 8).ToString("X");
                if (chunkHex.Length % 2 == 1) chunkHex = "0" + chunkHex; 
                bytes = StringToByteArray(chunkHex).Reverse().ToArray();
                for (int i = 0; i < bytes.Length; i++) { _data[i + 40] = bytes[i]; }
            }


            //Setup right file size
            string fileHex = ((_data.Count - 8) * 8).ToString("X");
            if (fileHex.Length % 2 == 1) fileHex = "0" + fileHex; 
            bytes = StringToByteArray(fileHex).Reverse().ToArray();
            for (int i = 0; i < bytes.Length; i++) { _data[i + 4] = bytes[i]; }

            File.WriteAllBytes(_targetPath, _data.ToArray());

        }

    }
}
