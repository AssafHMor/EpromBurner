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
Imports System.Runtime.InteropServices

Module Module1

    <DllImport("..\\..\\MCP2221DLL-UM_x86.dll", CharSet:=CharSet.Unicode)>
    Private Sub DllInit()
    End Sub

    <DllImport("..\\..\\MCP2221DLL-UM_x86.dll", CharSet:=CharSet.Unicode)>
    Private Function GetConnectionStatus() As Boolean
    End Function

    <DllImport("..\\..\\MCP2221DLL-UM_x86.dll", CharSet:=CharSet.Unicode)>
    Private Function GetDevCount() As Integer
    End Function

    <DllImport("..\\..\\MCP2221DLL-UM_x86.dll", CharSet:=CharSet.Unicode)>
    Private Function SelectDev(ByVal whichDevice As Integer) As Integer
    End Function


    <DllImport("..\\..\\MCP2221DLL-UM_x86.dll", CharSet:=CharSet.Unicode)>
    Private Function GetClockPinDividerValue(ByVal whichSetting As Integer) As Integer
    End Function


    Sub Main()
        'STEP 1:
        '	Initialize the DLL. Since no parameters are provided, the default VID/PID values will be used.
        DllInit()

        'STEP 3:
        '	Check connection status to refresh the internal device list within the DLL
        Dim isConnected As Boolean
        isConnected = GetConnectionStatus()

        'Print the result to the console window
        If (isConnected = True) Then
            Console.WriteLine("The device is connected." + vbNewLine)

            ' Get total number of devices plugged into PC
            Dim devCount As Integer
            devCount = GetDevCount()
            Console.WriteLine("There are " + devCount.ToString() + " MCP2221 devices plugged into the PC." + vbNewLine)

            'Select your desired device (not necessary if only one device is available but this is a good practice nonetheless)
            SelectDev(0)

            ' Perform your other desired operations
            Dim result As Integer
            result = GetClockPinDividerValue(0)
            If (result > 0) Then
                Console.WriteLine("The current value of clock pin divider is: " + (1 << result).ToString() + vbNewLine)

            Else
                Console.WriteLine("Encountered error " + result.ToString() + " when getting clock pin divider value.")
            End If

        Else
            Console.WriteLine("The device is NOT connected.\n")
        End If
    End Sub
End Module


