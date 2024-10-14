﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Hanged_Man
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string GuessLetter;
        private int vies = 5;
        private string randomWord;

        public MainWindow()
        {
            InitializeComponent();
            setupGame();
        }

        public TextBox HiddenWordTextBox { get; private set; }

        private void setupGame()
        {
            List<string> list = new List<string> { "vache", "chien", "guepe", "koala", "lapin", "zebre", "ecran", "canon", "epave", "pomme" };
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, list.Count);
            randomWord = list[randomIndex].ToUpper();
            int wordLength = randomWord.Length;
            string hiddenWord = new string('*', wordLength);
            DisplayHiddenWord(hiddenWord);
        }

        public void runGame()
        {
            if (GuessLetter != null)
            {
                if (randomWord.Contains(GuessLetter.ToUpper()))
                {
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (randomWord[i].ToString().ToUpper() == GuessLetter.ToUpper())
                        {
                            if (Cipher != null)
                            {
                                Cipher.Text = Cipher.Text.Remove(i, 1).Insert(i, GuessLetter.ToUpper());
                            }
                        }
                    }
                }
                else
                {
                    vies--;
                }

                if (vies == 0)
                {
                    MessageBox.Show("Défaite !");
                }

                if (Cipher != null && !Cipher.Text.Contains("*"))
                {
                    MessageBox.Show("Victoire !");
                }
            }
        }

        private void BTN_alpha_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
            GuessLetter = btn.Content.ToString();
            runGame();
        }

        private void DisplayHiddenWord(string hiddenWord)
        {
            Cipher.Text = hiddenWord;
        }
    }
}
