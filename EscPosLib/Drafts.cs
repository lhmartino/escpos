using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace EscPosLib.Draft
{
    // This class is just to keep some ideas, examples and incomplete code;

    public class EscPos
    {

        static SerialPort porta = new SerialPort("COM4");

        public string conv(int pValor)
        {
            return Convert.ToChar(pValor).ToString();
        }

        public void wEsc(string pComandoEsc)
        {
            porta.Write(pComandoEsc);

        }

        public EscPos()
        {

        }
        ~EscPos()
        {

        }

        public void ImprimeCfe()
        {
            porta.Open();


            wEsc(conv(0xA));

            //''-----> Inicializa a impressora
            wEsc(conv(0x1B) + "@");


            //

            //'////////////////////////////////////////////////////////////////////////////////////////////////

            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(1) + Chr$(&HA)
            wEsc(conv(0x1B) + conv(0x61) + conv(1) + conv(0xA));

            //'-----> Seleciona a fonte de caracteres e o estilo da mesma (negrito, altura, largura e sublinhado) ao mesmo tempo

            //Me.MSComm1.Output = Chr$(27) + Chr$(33) + Chr$(0)
            wEsc(conv(27) + conv(33) + conv(0));

            //'-----> Definição do espaçamento entre linhas (Intervalo 0 <= n <= 255)

            //Me.MSComm1.Output = Chr$(27) + Chr$(51) + Chr$(25)
            wEsc(conv(27) + conv(51) + conv(25));

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //Me.MSComm1.Output = Chr$(27) + Chr$(69) + Chr$(1)
            wEsc(conv(27) + conv(69) + conv(1));

            //'-----> Imprimir Texto


            wEsc(conv(0x1B) + conv(0x1F));

            //Me.MSComm1.Output = " Elgin S/A Relacionamento com Software House" + Chr$(&HA)
            wEsc(" Elgin S/A Relacionamento com Software House" + conv(0xA));

            wEsc(conv(0x1B) + conv(0x12));

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //Me.MSComm1.Output = Chr$(27) + Chr$(69) + Chr$(0)
            wEsc(conv(27) + conv(69) + conv(0));


            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(1) + Chr$(&HA)
            wEsc(conv(0x1B) + conv(0x61) + conv(1) + conv(0xA));

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = " CNPJ: 14.200.166/0001-66 IE:111111111111 " + Chr$(&HA)
            wEsc(" CNPJ: 14.200.166/0001-66 IE:111111111111 " + conv(0xA));

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Rua Barao de Campinas, 305, Centro, Sao Paulo, SP" + Chr$(&HA)
            wEsc("Rua Barao de Campinas, 305, Centro, Sao Paulo, SP" + conv(0xA));

            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(0) + Chr$(&HA)
            wEsc(conv(0x1B) + conv(0x61) + conv(0) + conv(0xA));

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "------------------------------------------------" + Chr$(&HA)
            wEsc("------------------------------------------------" + conv(0xA));


            //'////////////////////////////////////////////////////////////////////////////////////////////////

            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(1) + Chr$(&HA)

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //Me.MSComm1.Output = Chr$(27) + Chr$(69) + Chr$(1)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Extrato No. 876578" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Cupom Fiscal Eletronico - SAT" + Chr$(&HA)

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //Me.MSComm1.Output = Chr$(27) + Chr$(69) + Chr$(0)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "------------------------------------------------" + Chr$(&HA)

            //'////////////////////////////////////////////////////////////////////////////////////////////////

            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(0) + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "CPF/CNPJ do Consumidor: 382.270.218-83" + Chr$(&HA)

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //Me.MSComm1.Output = Chr$(27) + Chr$(69) + Chr$(0)

            //'-----> Impressão e alimentação de n linhas

            //Me.MSComm1.Output = Chr$(27) + Chr$(100) + Chr$(2)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "SUBTOTAL                                R$100,00" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Desconto" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Forma de Pagamento                    Valor Pago" + Chr$(&HA)

            //'-----> Impressão e alimentação de n linhas

            //Me.MSComm1.Output = Chr$(27) + Chr$(100) + Chr$(2)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Dinheiro                                 R$50,00" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Cartao                                   R$50,00" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "------------------------------------------------" + Chr$(&HA)

            //'-----> Seleção do modo impressão tamanho da letra intervalo (0 <= n <= 255)

            //Me.MSComm1.Output = Chr$(27) + Chr$(33) + Chr$(129)

            //'-----> Tira ou coloca o sublinhado: Deixa sublinhado o texto, o comando em decimal ficaria 27 + 45 + n (1 ou 0).

            //Me.MSComm1.Output = Chr$(27) + Chr$(45) + Chr$(0)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Tributos Totais Incidentes (Lei Federal 12.741/2012)  R$5.000,00  " + Chr$(&HA)

            //'-----> Seleciona a fonte de caracteres e o estilo da mesma (negrito, altura, largura e sublinhado) ao mesmo tempo

            //Me.MSComm1.Output = Chr$(27) + Chr$(33) + Chr$(0)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "------------------------------------------------" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "DEST" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Nome do adquirente" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "Endereco do destinario" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "------------------------------------------------" + Chr$(&HA)


            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(1) + Chr$(&HA)

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //Me.MSComm1.Output = Chr$(27) + Chr$(69) + Chr$(1)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "SAT No 000.000.016" + Chr$(&HA)

            //'-----> Imprimir Texto

            //Me.MSComm1.Output = "26/04/2016 - 11:58:42" + Chr$(&HA)

            //'-----> Impressão e alimentação de n linhas

            //Me.MSComm1.Output = Chr$(27) + Chr$(100) + Chr$(5)

            //'////////////////////////////////////////////////////////////////////////////////////////////////

            //'Impressão do CODE 128

            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(1) + Chr$(&HA)

            //'-----> Definir a largura do código de barras

            //MSComm1.Output = Chr$(&H1D) + "w" + Chr$(1)

            //'-----> Seleccione a posição de impressão de caracteres HRI

            //MSComm1.Output = Chr$(&H1D) + "H" + Chr$(2)

            //'-----> Fonte dos Caracteres

            //MSComm1.Output = Chr$(&H1D) + "f" + Chr$(48)

            //'-----> Definir a altura do código de barras

            //MSComm1.Output = Chr$(&H1D) + "h" + Chr$(50)

            //'-----> Imprimir codito de barras com modelo correspondente

            //MSComm1.Output = Chr$(&H1D) + "k" + Chr$(73) + Chr$(50) + Chr$(123) + Chr$(66)

            //MSComm1.Output = "123456789012345678901234567890123456789012345678" + Chr$(&HA)

            //'-----> Deixa o texto em negrito: 1 para negrito e 0 para ficar sem negrito

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H64) + Chr$(1)

            //'-----> Avançar uma linha

            //MSComm1.Output = Chr$(&HA)

            //'////////////////////////////////////////////////////////////////////////////////////////////////

            //'-------> Impressão do QRCODE

            //'Definição de variavel

            //Dim QrCode As String

            //QrCode = "https://www.sefaz.rs.gov.br/NFCE/NFCE-COM.aspx?chNFe=43141006354976000149650540000086781171025455&nVersao=100&tpAmb=2&dhEmi=323031342d31302d33305431353a33303a32302d30323a3030&vNF=0.10&vICMS=0.00&digVal=682f4d6b6b366134416d6f7434346d335a386947354f354b6e50453d&cIdToken=000001&cHashQRCode=771A7CE8C50D01101BDB325611F582B67FFF36D01d56a489a4fs94das"

            //'-----> Center n = 0 Esquerda - n = 1 meio = n = 3 direita

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H61) + Chr$(1) + Chr$(&HA)

            //'------> Define tamanho de cada ponto, no caso 3 dots

            //MSComm1.Output = Chr$(29) + Chr$(40) + Chr$(107) + Chr$(3) + Chr$(0) + Chr$(49) + Chr$(67) + Chr$(5)

            //'------> Define nível de redundância

            //MSComm1.Output = Chr$(29) + Chr$(40) + Chr$(107) + Chr$(3) + Chr$(0) + Chr$(49) + Chr$(69) + Chr$(48)

            //'------> Inicia armazenamento dos dados do código

            //MSComm1.Output = Chr$(29) + Chr$(40) + Chr$(107)

            //'-----> 'Informando o tamanho do dados do QR Code, acrescido de 3 unidades. Neste exemplo, o código tem 508 bytes, portanto o tamanho será 511]
            //'Comando GS + ( + K )) é maior que 256, então tem que dividir utilizando os bytes pH e pL  511 divido por 256 = 1,996094 e sobra (511 - 256) = 255, então fica assim:

            //MSComm1.Output = Chr$(91) + Chr$(1) + Chr$(49) + Chr$(80) + Chr$(48)

            //'-----> Dados do Codigo

            //MSComm1.Output = QrCode

            //'------> Imprime código e finaliza processo

            //MSComm1.Output = Chr$(29) + Chr$(40) + Chr$(107) + Chr$(3) + Chr$(0) + Chr$(49) + Chr$(81) + Chr$(48) + Chr$(12)

            //'-----> Impressão e alimentação de n linhas

            //Me.MSComm1.Output = Chr$(27) + Chr$(100) + Chr$(15)

            //Avança uma linha
            //MSComm1.Output = Chr$(&HA)
            for (int i = 0; i < 2; i++)
                wEsc(conv(0xA));


            //'aciona gilhotina
            //Me.MSComm1.Output = Chr$(27) + Chr$(105)
            wEsc(conv(27) + conv(105));



            //'Página 465 - Abre gaveta

            //MSComm1.Output = Chr$(&H1B) + Chr$(112) + Chr$(48) + Chr$(10) + Chr$(5)

            //'Página 449 - Beep 3 Veses

            //MSComm1.Output = Chr$(&H1B) + Chr$(&H28) + Chr$(&H41) + Chr$(&H5) + Chr$(&H0) + Chr$(&H61) + Chr$(&H64) + Chr$(2) + Chr$(5) + Chr$(3)

            //Next

            porta.Close();
        }



    }




}
