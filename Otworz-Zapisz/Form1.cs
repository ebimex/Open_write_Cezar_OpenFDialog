using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Diagnostics;




namespace Otworz_Zapisz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //na początek zablokuj textboxy do wpisywania do czasu wpisania liczby persunięcia tekstu
            if (textBox1.Text == "")
            {
                btnOdczyt.Enabled = false;
                btnZapisz.Enabled = false;
            }
        }



        //zmienna 'pobierz' do ktotej konwertowana jest liczba z okna do wpisywania o ile przesunać tekst
        int pobierz()
        {
            return Convert.ToInt32(textBox1.Text);
        }


        

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxZapis.Text))                       //jeżeli nie ma nic do zapisania z textboxa
            {
                MessageBox.Show("Wprowadź tekst do zapisu.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "InfoFile |*.eb";


            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)            //jeżeli jest coś do zapisania pokaże się okno oraz potwiedzenie OK
            {
                textBoxSciezka.Text = dialog.FileName;                                  // wpisz do textboksa ścieżkę pliku

                string ZmiennaTextDoSzyfrowania = textBoxZapis.Text;                    //z txtboxa do zapisu pobierz tekst do zmiennej string

                var chars = ZmiennaTextDoSzyfrowania.ToCharArray();                     //przekonertyuj ten tekst do tablicy

                string gotowe = "";                                                     //utwórz pustą zmienną 'gotowe'

                for (int clk = 0; clk < chars.Length; clk++)
                {
                    int ascii = Convert.ToInt32(chars[clk]);
                    ascii += pobierz();
                    char c = Convert.ToChar(ascii);
                    gotowe += c;                                                        //po zaszyfrowaniu wrzucaj chary do zmiennej string 'gotowe'
                }

                //File.WriteAllText(dialog.FileName, gotowe);
                File.WriteAllText(dialog.FileName, gotowe, Encoding.Default);

                MessageBox.Show("Tekst zapisany.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                return;
        }




        // wyświetlenie okna „Otwórz” potrzebnna jest kontrolka 'FileDialog' z przybornika
        private void btnOdczyt_Click(object sender, EventArgs e)        
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "InfoFile |*.eb";


            //deszyfrowanie tekstu KODEM CEZARA Z PLIKU TEKSTOWEGO
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxSciezka.Text = dialog.FileName;                                      // wpisz do textboksa ścieżkę pliku

                var tekst =File.ReadAllLines(dialog.FileName);                              //przepisz wszytkie linie z pliku do zmiennej

                textBoxZapis.Text = File.ReadAllText((dialog.FileName), Encoding.Default);

                string ZmiennaTextDoDeszyfrowania = File.ReadAllText((dialog.FileName), Encoding.Default);       //przepisz dalej z zmiennej var do zmiennej string

                var chars = ZmiennaTextDoDeszyfrowania.ToCharArray();                       //dalej przepisz te dane do do tablicy i umieśc je w tablicy o nazwie 'chars'

                string gotowe = "";                                                         //utwórz jeszcze pusty strin o nazwie 'gotowe'
                for (int clk = 0; clk < chars.Length; clk++)                               
                {
                    int ascii = Convert.ToInt32(chars[clk]);
                    ascii -= pobierz();                                                     //przesuń każdy znak w tablicy o tę liczbę znaków aby odkodować
                    char c = Convert.ToChar(ascii);
                    gotowe += c;
                }

                richTextBox1.Text = gotowe;                                                //umieść wynik deszyfrowania w tekstboxie
            }
            else
                return;
        }



        private void btnCzysc_Click(object sender, EventArgs e)
        {
            textBoxSciezka.Text = "";
            richTextBox1.Text = "";
            textBoxZapis.Text = "";
        }




        private void btnZamknij_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  ne pozwala na przecinek 
            if (e.KeyChar == ',' && textBox1.SelectionStart == 0)
                e.Handled = true;

            // pozwala na wprowadzanie tylko liczb
            if (char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
                base.OnKeyPress(e);
            else
                e.Handled = true;

            //po czasie 200ms wykonuje polecenie
            await Task.Delay(200);
            try
            {
                if (this.textBox1.Text != "")
                {
                    btnOdczyt.Enabled = true;
                    btnZapisz.Enabled = true;
                }
                else
                {
                    btnOdczyt.Enabled = false;
                    btnZapisz.Enabled = false;
                }
            }
            catch
            {
                //tu nic nie ma
            }
        }



        private static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                //textBoxZapis.InvokeIfRequired(c => c.Text += e.Data);
            }
        }

        private void buttonSzukaj_Click(object sender, EventArgs e)
        {

            System.String S = "Hello World";

            //textBoxSzukaj.Text = 

        }
    }
}
