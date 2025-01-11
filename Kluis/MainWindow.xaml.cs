using System;
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

namespace Kluis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] numberPlayer = new char[6];
        char[] code = new char[6];

        int indexOfArray = 0;
        int maxAttempts = 3;
        int attempt = 1;

        Random rnd = new Random();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            randomCodeLabel.Content = "";
            MessageBox.Show(
                "You have 3 attempts to open the vault. Insert number one-by-one until you have the 6-number code.",
                "Open the vault!",
                MessageBoxButton.OK);

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = char.Parse(rnd.Next(0, 9).ToString());
                randomCodeLabel.Content += "*";
            }
            this.Title = string.Join("", code);
            //randomCodeLabel.Content = string.Join("", code);
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int buttonContent = int.Parse(button.Content.ToString());
            ChosenNumber(buttonContent);
        }

        private void ChosenNumber(int numberButton)
        {
            // Update numberPlayer at the current index
            numberPlayer[indexOfArray] = char.Parse(numberButton.ToString());

            // Check if the player's number matches the code at the current index
            if (numberPlayer[indexOfArray] == code[indexOfArray])
            {
                // Convert the current content of the label to a character array
                char[] contentArray = randomCodeLabel.Content.ToString().ToCharArray();

                // Replace the asterisk at the current index with the correct number
                contentArray[indexOfArray] = code[indexOfArray];

                // Update the label's content with the modified string
                randomCodeLabel.Content = string.Join("", contentArray);
            }

            // Increment the index
            indexOfArray++;

            // Check if the player has finished entering all numbers
            if (indexOfArray == numberPlayer.Length)
            {
                if (numberPlayer.SequenceEqual(code))
                {
                    MessageBoxResult result = MessageBox.Show("You've unlocked the vault. Try Again?", "Success", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        RestartGame();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else if (attempt < maxAttempts)
                {
                    MessageBox.Show($"You've failed to open the vault. {attempt} attempts / {maxAttempts} left.", "Fail", MessageBoxButton.OK);
                    attempt++;
                    indexOfArray = 0;
                    Array.Clear(numberPlayer, 0, numberPlayer.Length);

                    // Reset the label to all asterisks
                    randomCodeLabel.Content = new string('*', code.Length);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("You've failed to open the vault and used all attempts. Try Again?", "Fail", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        RestartGame();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void RestartGame()
        {
            numberPlayer = new char[6];
            code = new char[6];

            indexOfArray = 0;
            maxAttempts = 3;
            attempt = 1;

            Grid_Loaded(null, null);
        }

        private void Numpad_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad0:
                    ChosenNumber(0);
                    break;
                case Key.NumPad1:
                    ChosenNumber(1);
                    break;
                case Key.NumPad2:
                    ChosenNumber(2);
                    break;
                case Key.NumPad3:
                    ChosenNumber(3);
                    break;
                case Key.NumPad4:
                    ChosenNumber(4);
                    break;
                case Key.NumPad5:
                    ChosenNumber(5);
                    break;
                case Key.NumPad6:
                    ChosenNumber(6);
                    break;
                case Key.NumPad7:
                    ChosenNumber(7);
                    break;
                case Key.NumPad8:
                    ChosenNumber(8);
                    break;
                case Key.NumPad9:
                    ChosenNumber(9);
                    break;
            }
        }
    }
}
