using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        //Hold the current result of cells in the active game
        private MarkType[] mResults;
        //True if it's 1st player turn (X);
        private bool mPlayerOneTurn;
        //True if game gas ended
        private bool mGameEnded;

        #endregion
        #region Constructor

        //Default constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion
        //Starts a new game and sets values to default
        public void NewGame()
        {
            //New blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Player One starts the game
            mPlayerOneTurn = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change foreground, background and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //game is not finished at the beggining
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //STarts a new game on the click after if finished 
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button) sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value baes on players turn
            mResults[index] = mPlayerOneTurn ? MarkType.Cross : MarkType.Nought;

            //Set the cell text for the result
            button.Content = mPlayerOneTurn ? "X" : "O";

            //Change noughts to green
            if(mPlayerOneTurn)
                button.Foreground = Brushes.Red;

            //Toggle players turn
            mPlayerOneTurn ^= true;

            //Check for a winner
            CheckForWinner();


          }
        /// <summary>
        /// Checks if there is a winner
        /// </summary>
        private void CheckForWinner()
        {

            #region HorizontalWins
            //Check for horizontal wins
            //
            //
            //
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[5] & mResults[6]) == mResults[3])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            else if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion
            #region VerticalWins
            //Check for vertical wins
            //
            //
            //
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion
            #region DiagonalWins
            //Checks diagonal wins
            //
            //
            //
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            else if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            //Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {

                //Game ended
                mGameEnded = true;

                //Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {                                    
                    button.Background = Brushes.Orange;
                });
            }
        }
    }


}