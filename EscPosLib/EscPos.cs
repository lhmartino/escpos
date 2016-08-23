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

        const string ESC = "\u001B";
        const string GS = "\u001D";
        const string InitializePrinter = ESC + "@";
        const string BoldOn = ESC + "E" + "\u0001";
        const string BoldOff = ESC + "E" + "\0";
        
        //const string CenterOn = ESC + "E" + "\u0001";
        //const string CenterOff = ESC + "E" + "\0";

        const string DoubleOn = GS + "!" + "\u0011";  // 2x sized text (double-high + double-wide)
        const string DoubleOff = GS + "!" + "\0";

        #endregion

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

        public void Print(string pText)
        {
            Print(pText);
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

    }
}
