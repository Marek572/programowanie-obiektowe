﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
        record Rate(string Code, string Currency, double Bid, double Ask);
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();
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
                .Select(node => new Rate(
                    node.Element("Code").Value,
                    node.Element("Currency").Value,
                    0,//double.Parse(node.Element("Bid").Value),
                    0//double.Parse(node.Element("Ask").Value)
                    )).ToList();

            foreach(Rate r in list)
            {
                Rates.Add(r.Code, r);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DownloadData();
            //dodawanie do listy
            foreach(string code in Rates.Keys)
            {
                InputCurrencyCode.Items.Add(code);
                ResultCurrencyCode.Items.Add(code);
            }
            //InputCurrencyCode.Items.Add("USD");
            //InputCurrencyCode.Items.Add("PLN");
            //InputCurrencyCode.Items.Add("EUR");
            //ResultCurrencyCode.Items.Add("USD");
            //ResultCurrencyCode.Items.Add("PLN");
            //ResultCurrencyCode.Items.Add("EUR");

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
            MessageBox.Show($"Wybrany kod wejściowy: {inputCode}\rWybrany kod wyjściowy: {resultCode}\nKwota: {amountStr}");
        }

        private void InputValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //spawdzenie czy input to liczba
            if (e.Text.EndsWith(","))
            {
                string ee = e.Text + "0";
                e.Handled = !decimal.TryParse(ee, out decimal value);
            }
            else
            {
                e.Handled = !decimal.TryParse(e.Text, out decimal value);
            }
        }
    }
}