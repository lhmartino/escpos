using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace EscPosLib
{

    public enum ControlePorta
    {
        ToOpen, 
        ToClose
    };    

    public class EscPos
    {
        private readonly int[] newline = { 10 };
        private SerialPort porta;// = new SerialPort("COM4");
        
        #region constantes importadas

        //const string ESC = "\u001B";
        //const string GS = "\u001D";
        //const string InitializePrinter = ESC + "@";
        const string BoldOn = ESC + "E" + "\u0001";
        const string BoldOff = ESC + "E" + "\0";
        
        ////const string CenterOn = ESC + "E" + "\u0001";
        ////const string CenterOff = ESC + "E" + "\0";

        const string DoubleOn = GS + "!" + "\u0011";  // 2x sized text (double-high + double-wide)
        const string DoubleOff = GS + "!" + "\0";

        #endregion


        /**
         * ASCII null control character
         */
        const string NUL = "\x00";

        /**
         * ASCII linefeed control character
         */
        const string LF = "\x0a";

        /**
         * ASCII escape control character
         */
        const string ESC = "\x1b";

        /**
         * ASCII form separator control character
         */
        const string FS = "\x1c";

        /**
         * ASCII form feed control character
         */
        const string FF = "\x0c";

        /**
         * ASCII group separator control character
         */
        const string GS = "\x1d";

        /**
         * ASCII data link escape control character
         */
        const string DLE = "\x10";

        /**
         * ASCII end of transmission control character
         */
        const string EOT = "\x04";

        /**
         * Indicates UPC-A barcode when used with Printer::barcode
         */
        const int BARCODE_UPCA = 65;

        /**
         * Indicates UPC-E barcode when used with Printer::barcode
         */
        const int BARCODE_UPCE = 66;

        /**
         * Indicates JAN13 barcode when used with Printer::barcode
         */
        const int BARCODE_JAN13 = 67;

        /**
         * Indicates JAN8 barcode when used with Printer::barcode
         */
        const int BARCODE_JAN8 = 68;

        /**
         * Indicates CODE39 barcode when used with Printer::barcode
         */
        const int BARCODE_CODE39 = 69;

        /**
         * Indicates ITF barcode when used with Printer::barcode
         */
        const int BARCODE_ITF = 70;

        /**
         * Indicates CODABAR barcode when used with Printer::barcode
         */
        const int BARCODE_CODABAR = 71;

        /**
         * Indicates CODE93 barcode when used with Printer::barcode
         */
        const int BARCODE_CODE93 = 72;

        /**
         * Indicates CODE128 barcode when used with Printer::barcode
         */
        const int BARCODE_CODE128 = 73;

        /**
         * Indicates that HRI (human-readable interpretation) text should not be
         * printed, when used with Printer::setBarcodeTextPosition
         */
        const int BARCODE_TEXT_NONE = 0;

        /**
         * Indicates that HRI (human-readable interpretation) text should be printed
         * above a barcode, when used with Printer::setBarcodeTextPosition
         */
        const int BARCODE_TEXT_ABOVE = 1;

        /**
         * Indicates that HRI (human-readable interpretation) text should be printed
         * below a barcode, when used with Printer::setBarcodeTextPosition
         */
        const int BARCODE_TEXT_BELOW = 2;

        /**
         * Use the first color (usually black), when used with Printer::setColor
         */
        const int COLOR_1 = 0;

        /**
         * Use the second color (usually red or blue), when used with Printer::setColor
         */
        const int COLOR_2 = 1;

        /**
         * Make a full cut, when used with Printer::cut
         */
        const int CUT_FULL = 65;

        /**
         * Make a partial cut, when used with Printer::cut
         */
        const int CUT_PARTIAL = 66;

        /**
         * Use Font A, when used with Printer::setFont
         */
        const int FONT_A = 0;

        /**
         * Use Font B, when used with Printer::setFont
         */
        const int FONT_B = 1;

        /**
         * Use Font C, when used with Printer::setFont
         */
        const int FONT_C = 2;

        /**
         * Use default (high density) image size, when used with Printer::graphics,
         * Printer::bitImage or Printer::bitImageColumnFormat
         */
        const int IMG_DEFAULT = 0;

        /**
         * Use lower horizontal density for image printing, when used with Printer::graphics,
         * Printer::bitImage or Printer::bitImageColumnFormat
         */
        const int IMG_DOUBLE_WIDTH = 1;

        /**
         * Use lower vertical density for image printing, when used with Printer::graphics,
         * Printer::bitImage or Printer::bitImageColumnFormat
         */
        const int IMG_DOUBLE_HEIGHT = 2;

        /**
         * Align text to the left, when used with Printer::setJustification
         */
        const int JUSTIFY_LEFT = 0;

        /**
         * Center text, when used with Printer::setJustification
         */
        const int JUSTIFY_CENTER = 1;

        /**
         * Align text to the right, when used with Printer::setJustification
         */
        const int JUSTIFY_RIGHT = 2;

        /**
         * Use Font A, when used with Printer::selectPrintMode
         */
        const int MODE_FONT_A = 0;

        /**
         * Use Font B, when used with Printer::selectPrintMode
         */
        const int MODE_FONT_B = 1;

        /**
         * Use text emphasis, when used with Printer::selectPrintMode
         */
        const int MODE_EMPHASIZED = 8;

        /**
         * Use double height text, when used with Printer::selectPrintMode
         */
        const int MODE_DOUBLE_HEIGHT = 16;

        /**
         * Use double width text, when used with Printer::selectPrintMode
         */
        const int MODE_DOUBLE_WIDTH = 32;

        /**
         * Underline text, when used with Printer::selectPrintMode
         */
        const int MODE_UNDERLINE = 128;

        /**
         * Indicates error correction level L when used with Printer::qrCode
         */
        const int QR_ECLEVEL_L = 0;

        /**
         * Indicates error correction level M when used with Printer::qrCode
         */
        const int QR_ECLEVEL_M = 1;

        /**
         * Indicates error correction level Q when used with Printer::qrCode
         */
        const int QR_ECLEVEL_Q = 2;

        /**
         * Indicates error correction level H when used with Printer::qrCode
         */
        const int QR_ECLEVEL_H = 3;

        /**
         * Indicates QR model 1 when used with Printer::qrCode
         */
        const int QR_MODEL_1 = 1;

        /**
         * Indicates QR model 2 when used with Printer::qrCode
         */
        const int QR_MODEL_2 = 2;

        /**
         * Indicates micro QR code when used with Printer::qrCode
         */
        const int QR_MICRO = 3;

        /**
         * Indicates a request for printer status when used with
         * Printer::getPrinterStatus (experimental)
         */
        const int STATUS_PRINTER = 1;

        /**
         * Indicates a request for printer offline cause when used with
         * Printer::getPrinterStatus (experimental)
         */
        const int STATUS_OFFLINE_CAUSE = 2;

        /**
         * Indicates a request for error cause when used with Printer::getPrinterStatus
         * (experimental)
         */
        const int STATUS_ERROR_CAUSE = 3;

        /**
         * Indicates a request for error cause when used with Printer::getPrinterStatus
         * (experimental)
         */
        const int STATUS_PAPER_ROLL = 4;

        /**
         * Indicates a request for ink A status when used with Printer::getPrinterStatus
         * (experimental)
         */
        const int STATUS_INK_A = 7;

        /**
         * Indicates a request for ink B status when used with Printer::getPrinterStatus
         * (experimental)
         */
        const int STATUS_INK_B = 6;

        /**
         * Indicates a request for peeler status when used with Printer::getPrinterStatus
         * (experimental)
         */
        const int STATUS_PEELER = 8;

        /**
         * Indicates no underline when used with Printer::setUnderline
         */
        const int UNDERLINE_NONE = 0;

        /**
         * Indicates single underline when used with Printer::setUnderline
         */
        const int UNDERLINE_SINGLE = 1;

        /**
         * Indicates double underline when used with Printer::setUnderline
         */
        const int UNDERLINE_DOUBLE = 2;

        public bool ControleAutomaticoDePorta { get; set; }

        public EscPos(string pPorta, bool pControleAutomaticoDePorta = true)
        {
            ControleAutomaticoDePorta = true;
            porta = new SerialPort(pPorta);
        }

        public EscPos(int pPortaCOM, bool pControleAutomaticoDePorta = true)
        {
            ControleAutomaticoDePorta = true;
            porta = new SerialPort("COM" + pPortaCOM.ToString().Trim());
        }
        
        public void AbrePorta()
        {
            if (!this.ControleAutomaticoDePorta)
                this.porta.Open();
        }

        public void FechaPorta()
        {
            if (!this.ControleAutomaticoDePorta)
                this.porta.Close();
        }

        private void GerenciarPorta(ControlePorta pControle)
        {
            //Trata apenas se o controle for automático
            if (this.ControleAutomaticoDePorta)
            {

                switch (pControle)
                {
                    case ControlePorta.ToOpen:
                        {
                            if (!porta.IsOpen) //Verifica se a porta já não está aberta
                                porta.Open();

                            break;
                        }
                    case ControlePorta.ToClose:
                        {
                            if (porta.IsOpen) //Verifica se a porta está aberta
                                porta.Close();

                            break;
                        }
                    default:
                        break;
                }      
          
            }
        }

        public void sendCommand(int[] pCommand)
        {
            //porta.Write(intTobyte(pCommand));
            //string x = BitConverter.ToString(intTobyte(pCommand), 0);            

            this.sendCommand(intArrayToStringCmd(pCommand));            
            
        }

        public void sendCommand(string pCommand)
        {
            GerenciarPorta(ControlePorta.ToOpen);

            porta.Write(pCommand);

            GerenciarPorta(ControlePorta.ToClose);
        }

        public string intArrayToStringCmd(int[] pCommand)
        {
            string vRet = "";


            for (int i = 0; i < pCommand.Length; i++)
                vRet += Convert.ToChar(pCommand[i]).ToString();

            return vRet;
        }
               
        public void PrintLine(string pText)
        {
            Print(pText, true);
        }

        public void PrintDoubleLine(string pText)
        {
            Print(DoubleOn + pText + DoubleOff, true);
        }

        private void Print(string pText, bool pNewLine = false)
        {
            GerenciarPorta(ControlePorta.ToOpen);

            porta.Write(pText);

            if(pNewLine)
                sendCommand(newline);

            GerenciarPorta(ControlePorta.ToClose);
        }
        
        private byte[] intTobyte(int[] data)
        {

            byte[] byteData = data.Select(x => (byte)x).ToArray(); // coonvert int array to byte
            return byteData;
        }

        public void Barcode(string pContent, int pType = BARCODE_CODE128)
        {
            /* Validate input */
            //self::validateInteger($type, 65, 73, __FUNCTION__, "Barcode type");

            int vLen = pContent.Length;

            //switch (pType) {
            //    case BARCODE_UPCA:
            //        self::validateInteger($len, 11, 12, __FUNCTION__, "UPCA barcode content length");
            //        self::validateStringRegex($content, __FUNCTION__, "/^[0-9]{11,12}$/", "UPCA barcode content");
            //        break;
            //    case BARCODE_UPCE:
            //        self::validateIntegerMulti($len, array(array(6, 8), array(11, 12)), __FUNCTION__, "UPCE barcode content length");
            //        self::validateStringRegex($content, __FUNCTION__, "/^([0-9]{6,8}|[0-9]{11,12})$/", "UPCE barcode content");
            //        break;
            //    case BARCODE_JAN13:
            //        self::validateInteger($len, 12, 13, __FUNCTION__, "JAN13 barcode content length");
            //        self::validateStringRegex($content, __FUNCTION__, "/^[0-9]{12,13}$/", "JAN13 barcode content");
            //        break;
            //    case self::BARCODE_JAN8:
            //        self::validateInteger($len, 7, 8, __FUNCTION__, "JAN8 barcode content length");
            //        self::validateStringRegex($content, __FUNCTION__, "/^[0-9]{7,8}$/", "JAN8 barcode content");
            //        break;
            //    case self::BARCODE_CODE39:
            //        self::validateInteger($len, 1, 255, __FUNCTION__, "CODE39 barcode content length"); // 255 is a limitation of the "function b" command, not the barcode format.
            //        self::validateStringRegex($content, __FUNCTION__, "/^([0-9A-Z \$\%\+\-\.\/]+|\*[0-9A-Z \$\%\+\-\.\/]+\*)$/", "CODE39 barcode content");
            //        break;
            //    case self::BARCODE_ITF:
            //        self::validateInteger($len, 2, 255, __FUNCTION__, "ITF barcode content length"); // 255 is a limitation of the "function b" command, not the barcode format.
            //        self::validateStringRegex($content, __FUNCTION__, "/^([0-9]{2})+$/", "ITF barcode content");
            //        break;
            //    case self::BARCODE_CODABAR:
            //        self::validateInteger($len, 1, 255, __FUNCTION__, "Codabar barcode content length"); // 255 is a limitation of the "function b" command, not the barcode format.
            //        self::validateStringRegex($content, __FUNCTION__, "/^[A-Da-d][0-9\$\+\-\.\/\:]+[A-Da-d]$/", "Codabar barcode content");
            //        break;
            //    case self::BARCODE_CODE93:
            //        self::validateInteger($len, 1, 255, __FUNCTION__, "Code93 barcode content length"); // 255 is a limitation of the "function b" command, not the barcode format.
            //        self::validateStringRegex($content, __FUNCTION__, "/^[\\x00-\\x7F]+$/", "Code93 barcode content");
            //        break;
            //    case self::BARCODE_CODE128:
            //        self::validateInteger($len, 1, 255, __FUNCTION__, "Code128 barcode content length"); // 255 is a limitation of the "function b" command, not the barcode format.
            //        // The CODE128 encoder is quite complex, so only a very basic header-check is applied here.
            //        self::validateStringRegex($content, __FUNCTION__, "/^\{[A-C][\\x00-\\x7F]+$/", "Code128 barcode content");
            //        break;
            //}
            //if (!$this -> profile -> getSupportsBarcodeB()) {
            //    // A simpler barcode command which supports fewer codes
            //    self::validateInteger($type, 65, 71, __FUNCTION__);
            //    $this -> connector -> write(self::GS . "k" . chr($type - 65) . $content . self::NUL);
            sendCommand(GS + "k" + Convert.ToChar(pType - 65).ToString() + pContent + NUL);
            //    return;
            //}
            // More advanced function B, used in preference
            //$this -> connector -> write(self::GS . "k" . chr($type) . chr(strlen($content)) . $content);
            //sendCommand(GS + "k" + Convert.ToChar(pType).ToString() + Convert.ToChar(vLen).ToString() + pContent);

            sendCommand(new int[] { 10 });
        }

    }
}
