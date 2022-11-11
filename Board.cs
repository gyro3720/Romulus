using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romulus
{
    class Board
    {
        private int turn = 0;
        private bool checkmate;
        private List<Piece> captured_b;
        private List<Piece> captured_w;
        private Piece[,] board = new Piece[8,8];
        List<int[]> possibleMoves;
        int[] selectedPosition = { -1, -1};

        /// <summary>
        /// Constructor; No parameters
        /// </summary>
        public Board(){
            captured_b = new List<Piece>();
            captured_w = new List<Piece>();
            checkmate = false;
            possibleMoves = new List<int[]>();
            initializeBoard();
        }

        /// <summary>
        /// Returns a List<Piece> of all captured black pieces
        /// </summary>
        /// <returns>List<Piece> captured_b</returns>
        public List<Piece> getCapturedBlack(){
            return captured_b;
        }

        /// <summary>
        /// Returns a List<Piece> of all captured white pieces
        /// </summary>
        /// <returns>List<Piece> captured_w</Piece></returns>
        public List<Piece> getCapturedWhite(){
            return captured_w;
        }

        /// <summary>
        /// Checks if checkmate is reached
        /// </summary>
        /// <returns></returns>
        public bool isCheckMate(){
            return checkmate;
        }


        /// <summary>
        /// Returns the current turn.
        /// </summary>
        /// <returns></returns>
        public int getTurn(){
            return turn;
        }

        /// <summary>
        /// Returns an int[row, col] corresponding to selectedPosition[].
        /// </summary>
        /// <returns>int[row, col] selectedPosition[]</returns>
        public int[] getSelectedPosition(){
            return selectedPosition;
        }

        /// <summary>
        /// Returns the piece at a given int[row, column] on the current board. Throws an error if null.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Piece getPiece(int[] position){
            Console.WriteLine(position[0]);
            Console.WriteLine(position[1]);
            if (!(board[position[0], position[1]] == null)){
                return board[position[0], position[1]];
            }
            throw new Exception("No piece at requested square!");
        }

        /// <summary>
        /// Creates Piece objects and inserts them in board[,] in the proper position. 
        /// </summary>
        private void initializeBoard(){
            for(int row = 0; row < 4; row++){
                switch (row)
                {
                    case 0: //black crit pieces
                        for(int col = 0; col < 8; col++){
                            if(col == 0 || col == 7){//rooks
                                Piece rook = new Piece(1, row, col, false);
                                board[row, col] = rook;
                            }
                            else if(col == 1 || col == 6){//knights
                                Piece knight = new Piece(2, row, col, false);
                                board[row, col] = knight;
                            }
                            else if(col == 2 || col == 5){//bishops
                                Piece bishop = new Piece(3, row, col, false);
                                board[row, col] = bishop;
                            }
                            else if(col == 3){//queen
                                Piece queen = new Piece(4, row, col, false);
                                board[row, col] = queen;
                            }
                            else if(col == 4){//king
                                Piece king = new Piece(5, row, col, false);
                                board[row, col] = king;
                            }
                        }
                        break;
                    case 1: //black pawns
                        for(int col = 0; col < 8; col++){
                            Piece pawn = new Piece(0, row, col, false);
                            board[row, col] = pawn;
                        }
                        break;
                    case 2: //white pawns
                        for(int col = 0; col < 8; col++){
                            Piece pawn = new Piece(0, 6, col);
                            board[6, col] = pawn;
                        }
                        break;
                    case 3:
                        for (int col = 0; col < 8; col++)
                        {
                            if (col == 0 || col == 7)
                            {//rooks
                                Piece rook = new Piece(1, 7, col);
                                board[7, col] = rook;
                            }
                            else if (col == 1 || col == 6)
                            {//knights
                                Piece knight = new Piece(2, 7, col);
                                board[7, col] = knight;
                            }
                            else if (col == 2 || col == 5)
                            {//bishops
                                Piece bishop = new Piece(3, 7, col);
                                board[7, col] = bishop;
                            }
                            else if (col == 3)
                            {//queen
                                Piece queen = new Piece(4, 7, col);
                                board[7, col] = queen;
                            }
                            else if (col == 4)
                            {//king
                                Piece king = new Piece(5, 7, col);
                                board[7, col] = king;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Takes an int[row, col] of the selected position in the wpf and if there is a piece present returns
        /// a List<int[row, col]> of the available moves and targets
        /// </summary>
        /// <param name="position"></param>
        /// <returns>  List<int[row, col]> of the available moves and targets </returns>
        public List<int[]> select(int[] position){
            int row = position[0];
            int col = position[1];
            List<int[]> highlightSquares = new List<int[]>(); //to be returned, list of int[row,col] to be highlighted when selected

            Piece selected;
            if (board[row, col] != null){
                int[] pos = { row, col };
                selectedPosition = pos;
                selected = board[row, col];
                highlightSquares = selected.getMoves(board);
                possibleMoves = selected.getMoves(board);
            }
            
            return highlightSquares;
        }

        /// <summary>
        /// Takes in the internal int[] position and returns the corresponding [Letter][Num]
        /// Position (e.g. a5)
        /// </summary>
        /// <param name="input"></param>
        /// <returns>String corresponding alpha numerical position</returns>
        public static String AlphaNumPosition(int[] input){
            String[] alpha = { "a", "b", "c", "d", "e", "f", "g", "h" };
            int[] nums = { 8, 7, 6, 5, 4, 3, 2, 1 };
            String coord = alpha[input[1]] + (nums[input[0]]).ToString();
            return coord;
        }

        /// <summary>
        /// Checks if an inputted position[row, column] is contained within the list of target/possible moves.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool isATarget(int[] position){
            for(int i = 0; i < possibleMoves.Count(); i++){
                int[] posMod = { possibleMoves[i][0], possibleMoves[i][1] };
                if(posMod.SequenceEqual(position))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Resets selectedPosition[] to its initial value and clears possibleMoves.
        /// </summary>
        public void resetSelectedPosition(){
            int[] res = { -1, -1};
            selectedPosition = res;
            possibleMoves.Clear();
        }

        /// <summary>
        /// ret Void; Iterates over pieces on board and updates positions based on the internal int row, int column of the pieces within. 
        /// </summary>
        public void updateBoard(){
            for(int r = 0; r < board.GetLength(0); r++){
                for(int c = 0; c < board.GetLength(1); c++){
                    if(board[r, c] != null){//if position contains a piece
                        Piece currentPiece = board[r, c];
                        int pieceRow = currentPiece.getRow();
                        int pieceCol = currentPiece.getCol();
                        if(pieceRow != r || pieceCol != c){//if internal position does not match board position (needs to be updated)
                            if (board[pieceRow, pieceCol] == null){//if target is empty
                                board[pieceRow, pieceCol] = currentPiece;
                                currentPiece.updateFirstMove();
                            }
                            else{//if target has a piece in it

                            }
                            board[r, c] = null; //empties original position
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Effectively a tostring for the board; Writes a grid of coordinates & accompanying pieces on the board to the console.
        /// </summary>
        public void output()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] != null)
                        Console.Write("[" + board[i, j].getRow().ToString() + "," + board[i, j].getCol().ToString() + "]" + board[i, j].getName() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

