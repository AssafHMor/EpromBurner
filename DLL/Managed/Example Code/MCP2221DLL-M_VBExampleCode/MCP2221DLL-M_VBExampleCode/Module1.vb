'/************************************************************************************************
'Filename:	Module1.vb
'Author:	Microchip Technology Inc.
'Compiler:	Microsoft Visual Basic 2010
'Copyright © 2014 released Microchip Technology Inc.  All rights reserved.
'MCHP License:
'	MICROCHIP SOFTWARE NOTICE AND DISCLAIMER:  You may use this software, and any 
'	derivatives created by any person or entity by or on your behalf, exclusively 
'	with Microchip’s products.  Microchip and its licensors retain all ownership 
'	and intellectual property rights in the accompanying software and in all 
'	derivatives hereto.  
'	This software and any accompanying information is for suggestion only.  It does 
'	not modify Microchip’s standard warranty for its products.  You agree that you 
'	are solely responsible for testing the software and determining its suitability.  
'	Microchip has no obligation to modify, test, certify, or support the software.
'	THIS SOFTWARE IS SUPPLIED BY MICROCHIP "AS IS".  NO WARRANTIES, WHETHER EXPRESS, 
'	IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED TO, IMPLIED WARRANTIES OF 
'	NON-INFRINGEMENT, MERCHANTABILITY, AND FITNESS FOR A PARTICULAR PURPOSE APPLY TO 
'	THIS SOFTWARE, ITS INTERACTION WITH MICROCHIP’S PRODUCTS, COMBINATION WITH ANY 
'	OTHER PRODUCTS, OR USE IN ANY APPLICATION. 
'	IN NO EVENT, WILL MICROCHIP BE LIABLE, WHETHER IN CONTRACT, WARRANTY, TORT 
'	(INCLUDING NEGLIGENCE OR BREACH OF STATUTORY DUTY), STRICT LIABILITY, INDEMNITY, 
'	CONTRIBUTION, OR OTHERWISE, FOR ANY INDIRECT, SPECIAL, PUNITIVE, EXEMPLARY, 
'	INCIDENTAL OR CONSEQUENTIAL LOSS, DAMAGE, FOR COST OR EXPENSE OF ANY KIND WHATSOEVER 
'	RELATED TO THE SOFTWARE, HOWSOEVER CAUSED, EVEN IF MICROCHIP HAS BEEN ADVISED OF THE 
'	POSSIBILITY OR THE DAMAGES ARE FORESEEABLE.  TO THE FULLEST EXTENT ALLOWABLE BY LAW, 
'	MICROCHIP'S TOTAL LIABILITY ON ALL CLAIMS IN ANY WAY RELATED TO THIS SOFTWARE WILL 
'	NOT EXCEED THE AMOUNT OF FEES, IF ANY, THAT YOU HAVE PAID DIRECTLY TO MICROCHIP 
'	FOR THIS SOFTWARE.
'	MICROCHIP PROVIDES THIS SOFTWARE CONDITIONALLY UPON YOUR ACCEPTANCE OF THESE TERMS.
'Other License's:
'	none
'************************************************************************************************/

Imports System
'//STEP 1: 
'//   Add the DLL as a reference to your project through "Project" -> "Add Reference" menu item within Visual Studio
Imports MCP2221     '//<---- Need to include this namespace


Module Module1

    Sub Main()
        '//STEP 2:
        '//	Make an instance of the MCP2221.MchpUsbI2c class. If using custom VID/PID, use VID and PID as arguments to the constructor.
        Dim UsbI2c As MCP2221.MchpUsbI2c = New MchpUsbI2c()

        '//STEP 3:
        '//	Navigate the DLL classes to find your desired function. Other examples are shown below.
        Dim isConnected As Boolean = UsbI2c.Settings.GetConnectionStatus()

        '//Print the result to the console window
        If (isConnected = True) Then
            Console.WriteLine("The device is connected." + vbNewLine)

            '//
            '//  Ex. Check for the total number of devices connected. Select first one.
            '//
            '// Get total number of devices plugged into PC
            Dim devCount As Integer
            devCount = UsbI2c.Management.GetDevCount()
            Console.WriteLine("There are " + devCount.ToString() + " MCP2221 devices plugged into the PC." + vbNewLine)
            UsbI2c.Management.SelectDev(0)

            '//
            '//  Ex. Get USB descriptor string
            '//
            Dim usbDescriptor As String = UsbI2c.Settings.GetUsbStringDescriptor()
            Console.WriteLine("The USB descriptor string is: " + usbDescriptor + vbNewLine)

            '//
            '//  Get the current (SRAM) setting of the clock pin divider value
            '//
            Dim rslt As Integer = UsbI2c.Settings.GetClockPinDividerValue(DllConstants.CURRENT_SETTINGS_ONLY)
            If (rslt > 0) Then
                Console.WriteLine("The current value of clock pin divider is: " + (1 << rslt).ToString() + vbNewLine)
            Else
                Console.WriteLine("Encountered error " + rslt.ToString() + " when getting clock pin divider value.")
            End If
        Else
            Console.WriteLine("The device is NOT connected." + vbNewLine)
        End If
    End Sub
End Module


