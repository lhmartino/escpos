using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscPosLib.Elgin
{
    public class EscPos
    {

        //if you want to use decimal values in your methods.
        public byte[] intTobyte(int[] data)
        {

            byte[] byteData = data.Select(x => (byte)x).ToArray(); // coonvert int array to byte
            return byteData;
        }



        //initialize printer

        //ESC @
        public void initializePrinter(String szPrinterName)
        {

            int[] command = { 27, 64 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(command));
        }


        //print text

        public bool printText(String szPrinterName, String data)
        {
            //for more character sets: http://www.ascii-codes.com/

            //for another charsets: 
            //Encoding ascii = Encoding.GetEncoding("ascii");
            //Encoding windows = Encoding.GetEncoding("Windows-1252");

            //you must use this encoding  for  brazilian portuguese special characters: ^,~,ç,á...
            Encoding brazilian = Encoding.GetEncoding("IBM860");
            byte[] byteData = brazilian.GetBytes(data);

            RawPrinterHelper.SendBytesToPrinter(szPrinterName, byteData);

            return true;
        }

        // Print Position Commands
        //ESC a n
        public bool SelectJustification(string szPrinterName, int justification_code)
        {

            //0= default 
            //48 left
            //1,49 centering
            //2,50 right

            int[] align = { 27, 97, justification_code };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(align));

            return true;
        }





        //Character Control Commands

        //use this mode to cancel another mode.
        public bool normalModeText(string szPrinterName)
        {
            int[] normal = { 27, 33, 0 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(normal));

            return true;
        }

        //Character font A (12 × 24) selected.
        public bool charFontAText(String szPrinterName)
        {

            int[] fontA = { 27, 33, 0 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(fontA));


            return true;
        }

        //Character font B (9 × 17) selected.
        public bool charFontBText(String szPrinterName)
        {
            int[] fontB = { 27, 33, 1 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(fontB));


            return true;

        }

        //Emphasized mode is turned on
        public bool emphasizedModeText(string szPrinterName)
        {
            int[] mode = { 27, 33, 8 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(mode));
            return true;
        }

        //Double-height selected.
        public bool doubleHeightText(string szPrinterName)
        {
            int[] height = { 27, 33, 16 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(height));

            return true;
        }
        //Double-width selected.
        public bool DoubleWidthText(string szPrinterName)
        {
            int[] width = { 27, 33, 32 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(width));
            return true;
        }


        //Underline mode is turned on
        public bool UnderlineModeText(string szPrinterName)
        {
            int[] underline = { 27, 33, 128 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(underline));
            return true;
        }


        //print and Line feed
        public bool lineFeed(String szPrinterName, int numLines)
        {
            // fucntion LF 
            int[] lf = { 10 };
            for (int i = 1; i <= numLines; i++)
            {
                RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(lf));
            }
            return true;
        }

        //Generate pulse in Real Time
        public bool drawerKick(String szPrinterName)
        {
            // function DLE DC4 fn m t （fn=1）


            int[] pulse = { 27, 112, 0, 100, 200 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(pulse));


            return true;
        }

        //execute test print
        public bool testPrint(String szPrinterName)
        {
            //function GS ( A pL pH n m

            int[] test = { 29, 40, 65, 2, 0, 0, 2 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(test));

            return true;
        }


        //Select an international character set
        public bool charSet(String szPrinterName, int language)
        {
            //function ESC R n
            //0-USA
            //12-Latin America
            //
            int[] char_set = { 27, 82, language };

            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(char_set));

            return true;

        }


        //select character code table
        public bool codeTable(String szPrinterName, int language)
        {
            //function Esc t n
            // 0 - PC437 (USA: Standard Europe)]
            // 40 [ISO8859-15 (Latin9)]
            // 3 [PC860 (Portuguese)]

            int[] code = { 27, 116, language };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(code));


            return true;
        }

        //Select cut mode and cut paper
        public bool CutPaper(String szPrinterName)
        {
            //hex 1D 56 m, m =0,1,48,49

            int[] cut = { 29, 86, 0 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(cut));

            return true;
        }


        //activate printer buzzer
        public bool buzzer(String szPrinterName)
        {



            //hex data = "1b 28 41 05 00 61 64 03 0a 0a";
            int[] buzzer = { 27, 40, 65, 5, 0, 97, 100, 3, 10, 10 };

            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(buzzer));
            // RawPrinterHelper.SendASCiiToPrinter(szPrinterName, data);
            return true;

        }

        //*************************** barcode  commands **********************************

        //GS h n - sets bar the height of bar code to n dots.
        public bool barcode_height(string szPrinterName, int range = 162) // default = 162
        {

            //range 1 ≤ n ≤ 255
            int[] height = { 29, 104, range };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(height));
            return true;
        }

        // GS w n Set bar code width
        public bool barcode_width(string szPrinterName, int range = 3)
        {
            //range = 2 ≤ n ≤ 6
            int[] width = { 29, 119, range };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(width));
            return true;
        }

        //GS f n Select a font for the HRI characters when printing a bar code.
        public bool barcodeHRI_chars(string szPrinterName, int font_code = 0)
        { //default 0 

            //[Range] n = 0, 1, 48, 49
            int[] hri = { 29, 102, font_code };


            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(hri));
            return true;
        }


        //GS H n Select print position of HRI characters
        public bool barcodeHRIPostion(string szPrinterName, int position_code = 1)
        { //default = 0

            //[Range] 0 ≤ n ≤ 3, 48 ≤ n ≤ 51 

            int[] print_position = { 29, 72, position_code };

            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(print_position));
            return true;


        }

        //GS k Print barcode
        //<Function A>
        public bool printBarcode(string szPrinterName, string data, int type = 2)
        { //for this example 2 = JAN/EAN13
            int[] barcode = { 29, 107, type };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(barcode));

            RawPrinterHelper.SendStringToPrinter(szPrinterName, data);
            int[] nul = { 0 }; // null char at the end.
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(nul));
            return true;
        }

        //GS k Print Barcode
        // <Function B>
        public bool printBarcodeB(String szPrinterName, string data, int type = 73)
        { //for this example 73 = CODE128

            int size = (int)data.Length; //  the number of bytes of bar code data
            int[] barcode = { 29, 107, type, size };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(barcode));
            RawPrinterHelper.SendStringToPrinter(szPrinterName, data);


            return true;
        }

        //*************************** barcode  commands **********************************



        //function to print Qrcode
        public bool printQrcode(String Strdata, String szPrinterName)
        {





            int length = Strdata.Length + 3; //  string size  + 3
            //int length = Strdata.Length; 
            byte length_low_byte = 0, length_high_byte = 0;
            length_low_byte = (byte)(length & 0xff);//low byte used in function 180 
            length_high_byte = (byte)((length >> 8) & 0xff);//high byte in function 180 


            //if you don't want to use shift operator:
            //int length_low_byte = length % 256;
            //int length_high_byte = length / 256;


            initializePrinter(szPrinterName);

            //<Function ESC a n> Select justification  
            int[] escAn = { 27, 97, 0 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(escAn));



            //<Function GS L> Set left margin
            int[] fGsl = { 29, 76, 0, 0 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(fGsl));

            //<Function 165> GS ( k p L p H cn fn n (cn = 49,fn = 65)  QR Code: Select the model
            int[] f165 = { 29, 40, 107, 4, 0, 49, 65, 50, 0 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(f165));


            //<Function 167> GS ( k pL pH cn fn n (cn = 49, fn = 67) QR Code: Set the size of module
            int[] f167 = { 29, 40, 107, 3, 0, 49, 67, 4 }; //  size of qrcode:  1-16
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(f167));



            //<Function 169> GS ( k pL pH cn fn n (cn = 49, fn = 69) QR Code: Select the error correction level
            int[] f169 = { 29, 40, 107, 3, 0, 49, 69, 48 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(f169));


            //<Function 180> GS ( k pL pH cn fn m d1…dk (cn = 49, fn = 80) QR Code: Store the data in the symbol storage area
            //pL and pH are the low- and high-order bytes of a 16-bit integer value that specifies the length in bytes of the following data  

            int[] f180 = { 29, 40, 107, length_low_byte, length_high_byte, 49, 80, 48 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(f180));



            //send string/url to printer
            //RawPrinterHelper.SendASCiiToPrinter(szPrinterName, Strdata);
            RawPrinterHelper.SendStringToPrinter(szPrinterName, Strdata);

            //<Function 181> GS ( k pL pH cn fn m (cn = 49, fn = 81) QR Code: Print the symbol data in the symbol storage area
            int[] f181 = { 29, 40, 107, 3, 0, 49, 81, 48 };
            RawPrinterHelper.SendBytesToPrinter(szPrinterName, intTobyte(f181));

            //


            return true;
        }

    }
}
