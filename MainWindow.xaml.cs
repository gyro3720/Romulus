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

namespace Romulus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board = new Board();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SquareClick(object sender, RoutedEventArgs e){
            Button button = (Button)e.Source;
            var bc = new BrushConverter();

            int row = Grid.GetRow(button) - 1;
            int column = Grid.GetColumn(button) - 1;
            int[] position = { row , column };

            if(!(position.SequenceEqual(board.getSelectedPosition()))){//if not clicking the same square
                if(!(board.isATarget(position))){//If the clicked button is not a currently selected target
                    List<int[]> highlight = board.select(position);
                    if (!(highlight.Count == 0)){
                        highlightSquares(highlight);
                    }
                }
                else if(board.isATarget(position)){//If clicked button is an empty square
                    Piece currentPiece = board.getPiece(board.getSelectedPosition());
                    currentPiece.setRow(row); currentPiece.setCol(column);
                    board.updateBoard();
                    string originalCoord = Board.AlphaNumPosition(board.getSelectedPosition()) + "i";
                    string newCoord = Board.AlphaNumPosition(position) + "i";
                    var originalImage = (Image)this.FindName(originalCoord);
                    var newImage = (Image)this.FindName(newCoord);
                    newImage.Source = originalImage.Source;
                    originalImage.Source = null;
                    board.resetSelectedPosition();
                    clearHighlights();
                }
                else{//if clicked button is a target and not empty
                }
                board.output();
            }
            else{
                clearHighlights();
                board.resetSelectedPosition();
            }
        }

        /// <summary>
        /// Highlights a List of int[row, cols] on the board.
        /// </summary>
        /// <param name="positions"></param>
        public void highlightSquares(List<int[]> positions){
            clearHighlights();
            var bc = new BrushConverter();
            for(int i = 0; i < positions.Count; i++){
                int[] location = positions[i];
                String coord = Board.AlphaNumPosition(location);
                var toHighlight = (Button)this.FindName(coord);
                if (location[2] == 0)
                    toHighlight.Background = (SolidColorBrush)bc.ConvertFromString("#cce6ff");
                else if (location[2] == 1)
                    toHighlight.Background = (SolidColorBrush)bc.ConvertFromString("#e8b0b0");
            }
        }

        /// <summary>
        /// Returns all squares on the board to their original colors.
        /// </summary>
        public void clearHighlights(){
            //#cccccc - Light #666666 - Dark
            var bc = new BrushConverter();
            for(int r = 0; r < 8; r++){
                for(int c = 0; c < 8; c++){
                    if((r%2 == 0 && c%2 == 0) || (r%2 == 1 && c%2 == 1)){// even col
                        int[] crd = { r, c };
                        String pos = Board.AlphaNumPosition(crd);
                        var square = (Button)this.FindName(pos);
                        square.Background = (SolidColorBrush)bc.ConvertFromString("#cccccc"); 
                    }
                    else{
                        int[] crd = { r, c };
                        String pos = Board.AlphaNumPosition(crd);
                        var square = (Button)this.FindName(pos);
                        square.Background = (SolidColorBrush)bc.ConvertFromString("#666666");
                    }
                }
            }
        }
    }
}
