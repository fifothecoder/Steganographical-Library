using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steganographical_Library.Abstracts;
using StegoLib.Abstract_Classes;

namespace Steganographical_Library.Validators
{
    sealed class WAVValidator : WAVFile
    {
        public WAVValidator(string target) : base(target) {
            _data = File.ReadAllBytes(_targetPath).ToList();
        }

        public bool Validate() {

            //RIFF, WAVE and fmt header checking
            if (_data[0] != 82) return false;
            if (_data[1] != 73) return false;
            if (_data[2] != 70) return false;
            if (_data[3] != 70) return false;

            if (_data[8] != 87) return false;
            if (_data[9] != 65) return false;
            if (_data[10] != 86) return false;
            if (_data[11] != 69) return false;

            if (_data[12] != 102) return false;
            if (_data[13] != 109) return false;
            if (_data[14] != 116) return false;
            if (_data[15] != 32) return false;

            //Check datasize
            long dataSize = _data[4] + _data[5] * 256 + _data[6] * 65536 + _data[7] * 16777216;
            dataSize /= 8;
            if (_data.Count - 8 != dataSize) return false;

            //Check File Size
            long fileSize = _data[40] + _data[41] * 256 + _data[42] * 65536 + _data[43] * 16777216;
            fileSize /= 8;
            if (_data.Count - 44 != fileSize) return false;

            return true;
        }

    }
}
