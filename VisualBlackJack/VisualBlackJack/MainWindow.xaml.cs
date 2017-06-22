using CardLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.SqlClient;
using System.Data;

namespace VisualBlackJack {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        BlackJackGame game;
         string connectionString = @"Data Source=WIN-9SASTSSN504\SQLEXPRESS;Initial Catalog=BlackJack;Integrated Security=True";
        //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Statistics;Integrated Security=True;Pooling=False";
        SqlConnection connection;

        string sql_select_all = "use [BlackJack] select * from [Statistics]";

        DataTable dt;

        public MainWindow() {
            
            InitializeComponent();
            game = new BlackJackGame();

            statBar_lbl.Content = "User drawing cards";
            UpdateValues();
            game.GameFinished += OnGameFinished;

            connection = new SqlConnection(connectionString);
            dt = new DataTable();
            connection.Open();
            
            // Load the database on the datatable and bind to datagrid
            SqlCommand command = new SqlCommand(sql_select_all, connection);
            dt.Load(command.ExecuteReader());
            blackJackDataGrid.DataContext = dt.DefaultView;
            
        }

        private void UpdateValues() {
            player_hand_lv.ItemsSource = game.GetPlayerHand();
            player_score_txt.Text = game.GetPlayerScore().ToString();
            player_cards_drawn_txt.Text = game.GetPlayerHand().Count().ToString();

            computer_hand_lv.ItemsSource = game.GetHouseHand();
            computer_score_txt.Text = game.GetHouseScore().ToString();
            computer_cards_drawn_txt.Text = game.GetHouseHand().Count().ToString();

        }

        /// <summary>
        /// Handles the ending of a game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnGameFinished(object sender, GameFinishedArgs e) {
            hit_btn.IsEnabled = false;
            stay_btn.IsEnabled = false;

            if (e.Winner.Equals("player")) {
                winner_txt.Text = "User";
                winner_txt.Background = Brushes.Green;
                statBar_lbl.Content = "Game over - User Won";
            }
            else {
                winner_txt.Text = "Computer";
                winner_txt.Background = Brushes.Red;
                statBar_lbl.Content = "Game over - Computer Won";
            }

            // Insert Data into database at the end of the game
            string sql = $"use [BlackJack] Insert into [Statistics] (Winner, UserScore, UserCardsDrawn, ComputerScore, ComputerCardsDrawn) " + 
                $"values('{winner_txt.Text}', {game.GetPlayerScore()}, {game.GetPlayerHand().Count}, {game.GetHouseScore()}, {game.GetHouseHand().Count}); ";

            SqlCommand command = new SqlCommand(sql, connection);
            int rowsAffected = command.ExecuteNonQuery();

            // Reload the data from the database
            dt.Clear();
            dt.Load(new SqlCommand(sql_select_all, connection).ExecuteReader());
            


        }

        private void hit_btn_Click(object sender, RoutedEventArgs e) {
            game.Hit();
            UpdateValues();
        }

        private void stay_btn_Click(object sender, RoutedEventArgs e) {
            game.Stay();
            UpdateValues();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            game.NewGame();
            statBar_lbl.Content = "User drawing cards";
            UpdateValues();
            hit_btn.IsEnabled = true;
            stay_btn.IsEnabled = true;
            winner_txt.Text = "";
            winner_txt.Background = Brushes.White;
        }

        /// <summary>
        /// Exit Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            connection.Close();
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            MessageBox.Show("BlackJack \nVersion 3.0 \nWritten by Edwins Ventura");
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (statistics_tab.IsSelected) {
                //connection.Open();
                //string sql = "select * from GameStatistics";
                //SqlCommand command = new SqlCommand(sql, connection);
                //SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                //blackJackDataGrid.ItemsSource = reader;
                ////reader.Close();
            }
        }
    }


}
