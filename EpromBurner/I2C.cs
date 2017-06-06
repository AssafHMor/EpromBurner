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

        private ushort PID; // the production USB ID
        private ushort VID; // the Vendor USB ID 
        public bool connected; // connectivity satuts variable
        public MCP2221.MchpUsbI2c usbI2c; // the instance of the "device"
        private int CLOCK_DIVIDER;

        // init the usb to i2c object
        /// <summary>
        /// create the instance of the uUSB to I2C
        /// </summary>
        /// <param name="pid"> the production USB ID </param>
        /// <param name="vid"> the Vendor USB ID </param>
        public I2C (ushort pid, ushort vid)
        {

            PID = pid;
            VID = vid;
            usbI2c = new MchpUsbI2c();
            Connected();

            
        }
        /// <summary>
        /// checking the connection of the device to a USB port on the machine
        /// will also write to the log file the next data:
        /// connected
        /// connection time
        /// PID
        /// VID
        /// descriiptor
        /// DAC
        /// DAC voltage
        /// firmware
        /// flash protection state (0 - unsecured / 1 - password protection enabled / 2 - permanently locked (cannot be undone))
        /// </summary>
        public void Connected()
        {

            connected = usbI2c.Settings.GetConnectionStatus(); // check if the device is connected via USB
            
            if (connected) 
            {

                // the number of devices connected to the current machine
                int numOfConnectedDevices = usbI2c.Management.GetDevCount();

                // if there is more than one device connected, choose the first one, others wont work
                // multiple devices  
                if (numOfConnectedDevices > 0)
                {
                    usbI2c.Management.SelectDev(0); 
                }
                

                Logger.CreateLoggerFile("a.txt");
                Logger.WriteToLog(numOfConnectedDevices + " connected devices"); // add the number of deviced connected to the machine

                for (int i  = 0; i < numOfConnectedDevices; i++)
                {

                    usbI2c.Management.SelectDev(i);
                    Logger.WriteToLog("Connection time"); // add the connection time to the log
                    Logger.WriteToLog("The PID of device # " + i + " is: " + usbI2c.Settings.GetUsbPid()); // add the device Prodution ID
                    Logger.WriteToLog("The VID of device # " + i + " is: " + usbI2c.Settings.GetUsbVid()); // add the device Vendor ID
                    Logger.WriteToLog("The descriptor of device # " + i + " is: " + usbI2c.Settings.GetUsbStringDescriptor()); // add the device descriptor
                    Logger.WriteToLog("The voltage referance of device # " + i + " is: " + usbI2c.Settings.GetAdcVoltageReference()); // add the device voltage referance 
                    Logger.WriteToLog("The DAC value of device # " + i + " is: " + usbI2c.Settings.GetDacValue()); // add the device DAC value
                    Logger.WriteToLog("The DAC voltage referance of device # " + i + " is: " + usbI2c.Settings.GetDacVoltageReference()); // add the device DAC voltage 
                    Logger.WriteToLog("The firmware of device # " + i + " is: " + usbI2c.Settings.GetFirmwareVersion()); // add the device firmware
                    Logger.WriteToLog("The flash protection state of device # " + i + " is: " + usbI2c.Settings.GetFlashProtectionState()); // add the device flash protection state
                    if (usbI2c.Settings.GetClockPinDividerValue(CLOCK_DIVIDER) > 0 && usbI2c.Settings.GetClockPinDividerValue(CLOCK_DIVIDER) < 32)
                    {

                        Logger.WriteToLog("The clock pin divider of device #: " + i + " is: " + usbI2c.Settings.GetClockPinDividerValue(CLOCK_DIVIDER)); // add the device
                        
                    }
                    else
                    {

                        Logger.WriteToLog("AN ERROR OCCURED. The clock pin divider value should be between 1 and 31 which is the exponent of 2^n ");
                    }

                }

            }

            else
            {

                Console.WriteLine("Device is not connected");

            }

        }

        public 


        
    }
}
