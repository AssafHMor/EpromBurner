/************************************************************************************************
Filename:	Program.cs
Author:		Microchip Technology Inc.
Compiler:	Microsoft C# 2013
Copyright © 2014 Microchip Technology Inc.  All rights reserved.
MCHP License:
	MICROCHIP SOFTWARE NOTICE AND DISCLAIMER:  You may use this software, and any 
	derivatives created by any person or entity by or on your behalf, exclusively 
	with Microchip’s products.  Microchip and its licensors retain all ownership 
	and intellectual property rights in the accompanying software and in all 
	derivatives hereto.  
	This software and any accompanying information is for suggestion only.  It does 
	not modify Microchip’s standard warranty for its products.  You agree that you 
	are solely responsible for testing the software and determining its suitability.  
	Microchip has no obligation to modify, test, certify, or support the software.
	THIS SOFTWARE IS SUPPLIED BY MICROCHIP "AS IS".  NO WARRANTIES, WHETHER EXPRESS, 
	IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED TO, IMPLIED WARRANTIES OF 
	NON-INFRINGEMENT, MERCHANTABILITY, AND FITNESS FOR A PARTICULAR PURPOSE APPLY TO 
	THIS SOFTWARE, ITS INTERACTION WITH MICROCHIP’S PRODUCTS, COMBINATION WITH ANY 
	OTHER PRODUCTS, OR USE IN ANY APPLICATION. 
	IN NO EVENT, WILL MICROCHIP BE LIABLE, WHETHER IN CONTRACT, WARRANTY, TORT 
	(INCLUDING NEGLIGENCE OR BREACH OF STATUTORY DUTY), STRICT LIABILITY, INDEMNITY, 
	CONTRIBUTION, OR OTHERWISE, FOR ANY INDIRECT, SPECIAL, PUNITIVE, EXEMPLARY, 
	INCIDENTAL OR CONSEQUENTIAL LOSS, DAMAGE, FOR COST OR EXPENSE OF ANY KIND WHATSOEVER 
	RELATED TO THE SOFTWARE, HOWSOEVER CAUSED, EVEN IF MICROCHIP HAS BEEN ADVISED OF THE 
	POSSIBILITY OR THE DAMAGES ARE FORESEEABLE.  TO THE FULLEST EXTENT ALLOWABLE BY LAW, 
	MICROCHIP'S TOTAL LIABILITY ON ALL CLAIMS IN ANY WAY RELATED TO THIS SOFTWARE WILL 
	NOT EXCEED THE AMOUNT OF FEES, IF ANY, THAT YOU HAVE PAID DIRECTLY TO MICROCHIP 
	FOR THIS SOFTWARE.
	MICROCHIP PROVIDES THIS SOFTWARE CONDITIONALLY UPON YOUR ACCEPTANCE OF THESE TERMS.
************************************************************************************************/

using System;
using System.Runtime.InteropServices;



namespace MCP2221DLL_UM_CSExampleCode
{ 

    class Program
    {
        //DLL Imports
        [DllImport("..\\..\\MCP2221DLL-UM_x86.dll", EntryPoint = "DllInit", CharSet = CharSet.Unicode)]
        static extern void DllInit();

        [DllImport("..\\..\\MCP2221DLL-UM_x86.dll", EntryPoint = "GetConnectionStatus", CharSet = CharSet.Unicode)]
        static extern bool GetConnectionStatus();

        [DllImport("..\\..\\MCP2221DLL-UM_x86.dll", EntryPoint = "GetDevCount", CharSet = CharSet.Unicode)]
        static extern int GetDevCount();

        [DllImport("..\\..\\MCP2221DLL-UM_x86.dll", EntryPoint = "SelectDev", CharSet = CharSet.Unicode)]
        static extern int SelectDev(int whichDevice);

        [DllImport("..\\..\\MCP2221DLL-UM_x86.dll", EntryPoint = "GetClockPinDividerValue", CharSet = CharSet.Unicode)]
        static extern int GetClockPinDividerValue(int whichSetting);


        static void Main(string[] args)
        {
            //STEP 1:
            //	Initialize the DLL. Since no parameters are provided, the default VID/PID values will be used.
            DllInit();

            //STEP 3:
			//	Check connection status to refresh the internal device list within the DLL
			bool isConnected = GetConnectionStatus();

			//Print the result to the console window
            if (isConnected == true)
            {
                Console.WriteLine("The device is connected.\n");

                // Get total number of devices plugged into PC
                int devCount = GetDevCount();
                Console.WriteLine("There are " + devCount.ToString() + " MCP2221 devices plugged into the PC.\n");

                //Select your desired device (not necessary if only one device is available but this is a good practice nonetheless)
                SelectDev(0);

                // Perform your other desired operations
                int rslt = GetClockPinDividerValue(0);
                if (rslt > 0)	
                {
                    Console.WriteLine("The current value of clock pin divider is: " + (1 << rslt).ToString() + "\n");
                }
                else
                {
                    Console.WriteLine("Encountered error " + rslt.ToString() + " when getting clock pin divider value.");
                }
            }
            else
            {
                Console.WriteLine("The device is NOT connected.\n");
            }	
        }
    }
}
