using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace App_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        record Rate()
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            [JsonPropertyName("currency")]
            public string Currency { get; set; }
            [JsonPropertyName("bid")]
            public decimal Bid { get; set; }
            [JsonPropertyName("ask")]
            public decimal Ask { get; set; }
        };
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();
        class RateTable
        {
            [JsonPropertyName("table")]
            public string Table { get; set; }
            [JsonPropertyName("no")]
            public string Number { get; set; }
            [JsonPropertyName("tradingDate")]
            public DateTime tradingDate { get; set; }
            [JsonPropertyName("effectiveDate")]
            public DateTime efectiveDate { get; set; }
            [JsonPropertyName("rates")]
            public List<Rate> Rates { get; set; }
        }

        private void DownloadDataJson()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            string json = client.DownloadString("https://api.nbp.pl/api/exchangerates/tables/C");
            RateTable ratesTable = JsonSerializer.Deserialize<List<RateTable>>(json)[0];
            ratesTable.Rates.Add(new Rate() { Code = "PLN", Currency = "złoty", Ask = 1, Bid = 1 });


            foreach (Rate r in ratesTable.Rates)
            {
                Rates.Add(r.Code,r);
            }
        }

        private void DownloadData()
        {
            //stworzyc kolekcje rates
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xml = client.DownloadString("https://api.nbp.pl/api/exchangerates/tables/C");
            XDocument doc = XDocument.Parse(xml);
            List<Rate> list = doc
                .Elements("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(node => new Rate() { 
                    Code = node.Element("Code").Value,
                    Currency = node.Element("Currency").Value,
                    Bid = decimal.Parse(node.Element("Bid").Value),
                    Ask = decimal.Parse(node.Element("Ask").Value)
                    }
                ).ToList();

            foreach(Rate r in list)
            {
                Rates.Add(r.Code, r);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DownloadDataJson();

            UpdateGUI();
        }

        private void UpdateGUI()
        {
            InputCurrencyCode.Items.Clear();
            ResultCurrencyCode.Items.Clear();
            foreach (string code in Rates.Keys)
            {
                InputCurrencyCode.Items.Add(code);
                ResultCurrencyCode.Items.Add(code);
            }

            //ustawienie wartosci domyslnej
            ResultCurrencyCode.SelectedIndex = 0;
            InputCurrencyCode.SelectedIndex = 1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {
            //kod reagujacy na klikniecie
            //obliczenie kwoty dla ResultValue
            string inputCode = (string)InputCurrencyCode.SelectedItem;
            string resultCode = (string)ResultCurrencyCode.SelectedItem;
            string amountStr = InputValue.Text;
            if(decimal.TryParse(amountStr, out decimal amount))
            {
                ResultValue.Text = (amount * Rates[inputCode].Bid / Rates[resultCode].Bid).ToString("N2");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Wybierz plik z notowania walut";
            dialog.Filter = "Pliki tekstowe (*.txt)|*.txt";
            dialog.DefaultExt = "*.txt";

            if(dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                if (File.Exists(path))
                {
                    string[] lines = File.ReadAllLines(path);
                    Rates.Clear();
                    foreach(string line in lines)
                    {
                        string[] tokens = line.Split("\t");
                        //cos sie wywala
                        Rate rate = new Rate()
                        {
                            Code = tokens[0],
                            Currency = tokens[1],
                            Ask = decimal.Parse(tokens[2]),
                            Bid = decimal.Parse(tokens[3])
                        };
                        Rates.Add(rate.Code, rate);
                    }
                    UpdateGUI();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if(dialog.ShowDialog() == true)
            {
                string jsonText = JsonSerializer.Serialize <Dictionary<string, Rate>>(Rates);
                File.WriteAllText(dialog.FileName, jsonText);
            }
        }

        private void InputValue_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InputValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //spawdzenie czy input to liczba
            e.Handled = !decimal.TryParse(InputValue.Text + e.Text, out decimal value);
        }
    }
}
