using System;
using System.Collections.Generic;
using System.IO;
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
        private int vies = 7; //nbr de vie
        private string randomWord;
        

        public MainWindow()
        {
            InitializeComponent();
            setupGame();
        }

        public TextBox HiddenWordTextBox { get; private set; }

        private void setupGame()
        {
            string filePath = "../../Ressouce/mots5.txt"; // Chemin vers le fichier .txt
            List<string> list = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    list.Add(line.Trim().ToLower());
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Le fichier 'mots.txt' n'a pas été trouvé.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
                return;
            }

            Random rnd = new Random();
            int randomIndex = rnd.Next(0, list.Count);
            randomWord = list[randomIndex].ToUpper();
            int wordLength = randomWord.Length;
            string hiddenWord = new string('*', wordLength);
            DisplayHiddenWord(hiddenWord);
            UpdateLifeCounter(); // appel la methode UpdateLifeCounter 
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
                    Damage(); //appel de Damage 
                    UpdateLifeCounter(); // appel de la fonction 
                }

                if (vies == 0)
                {
                    MessageBox.Show("Défaite !");
                    Nope();
                }

                if (Cipher != null && !Cipher.Text.Contains("*"))
                {
                    MessageBox.Show("Victoire !");
                    GG();
                }
            }
            DisplayHiddenWord(Cipher.Text);
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

        private void UpdateLifeCounter()
        {
            // Mettre à jour l'image en fonction des vies restantes
            string imagePath = "Ressouce/Image_Hang/"+(8 - vies)+".png"; // 1.png pour 7 vies, 2.png pour 6 vies, etc.
            Img_Pendu.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }


        private void Damage()
        {
            MediaPlayer playMedia  = new MediaPlayer();
            var uri = new Uri("../../Ressouce/dmg.mp3", UriKind.Relative);
            playMedia.Open(uri);
            playMedia.Play();
        }

        private void GG()
        {
            MediaPlayer playMedia = new MediaPlayer();
            var uri = new Uri("../../Ressouce/GG.mp3", UriKind.Relative);
            playMedia.Open(uri);
            playMedia.Play();
        }

        private void Nope()
        {
            MediaPlayer playMedia = new MediaPlayer();
            var uri = new Uri("../../Ressouce/L.mp3", UriKind.Relative);
            playMedia.Open(uri);
            playMedia.Play();
        }

    }
}
