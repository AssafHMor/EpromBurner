using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpromBurner
{
    public static class Constants
    {

        public const ushort PID = 0x00DD; // the default PID
        public const ushort VID = 0x04D8; // the default VID
        public const byte SLAVE_READ_ADDRESS = 0xA1; // the slave reading address by the 7 bit method 
        public const byte SLAVE_WRITE_ADDRESS = 0xA0; // the slave writing address by the 7 bit method
        public const byte START_READ_ADDRESS = 0x00; // the first byte address to beging reading fron
        public const int EPROM_SIZE = 250; // the byte size of the EPROM
        public const uint I2C_SPEED = 1000000; // fast mode speed defined by manufacturer 
        


    }
}
