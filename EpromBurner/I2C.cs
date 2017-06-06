using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCP2221;

namespace EpromBurner
{
    
    class I2C
    {

        private ushort PID; // the production USB ID the factory default is: "0x00DD"
        private ushort VID; // the Vendor USB ID the factory default is: "0x04D8"
        public bool connected; // connectivity satuts variable
        public MCP2221.MchpUsbI2c usbI2c; // the instance of the "device"
        

        // init the usb to i2c object
        public I2C (ushort pid, ushort vid)
        {

            PID = pid;
            VID = vid;
            usbI2c = new MchpUsbI2c();
            Connected();

            
        }


        public void Connected()
        {

            connected = usbI2c.Settings.GetConnectionStatus(); // check if the device is connected via USB

            if (connected) // print out weather device is connected or not
            {

                Console.WriteLine("Device is connected");

                // find and print the number of devices connected to the computer
                int numOfConnectedDevices = usbI2c.Management.GetDevCount();

                Console.WriteLine("the number of connected devices is:" + numOfConnectedDevices);

                // if there is more than one device connected, choose the first one, others wont work
                if (numOfConnectedDevices > 0)
                {
                    usbI2c.Management.SelectDev(0); 
                }
                
            
            }

            else
            {

                Console.WriteLine("Device is not connected");

            }

        }
        
    }
}
