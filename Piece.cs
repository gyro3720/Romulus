using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romulus
{
    class Piece
    {
        private int row;
        private int column;
        private String name;
        private bool White;
        private bool firstMove = true;
        private int type; //(White) Pawn = 0, Rook = 1, Knight = 2, Bishop = 3, Queen = 4, King = 5
        public Piece(int t, int r, int c, bool w = true){
            row = r;
            column = c;
            White = w;
            type = t;
            name = getName(t);
        }

        /// <summary>
        /// Returns whether a piece is white or not.
        /// </summary>
        /// <returns>bool White</returns>
        public bool isWhite(){
            return White;
        }

        /// <summary>
        /// Returns the enumeration of the piece.
        /// 0 = Pawn;
        /// 1 = Rook;
        /// 2 = Knight;
        /// 3 = Bishop;
        /// 4 = Queen;
        /// 5 = King;
        /// </summary>
        /// <returns>int type</returns>
        public int getType(){
            return type;
        }

        /// <summary>
        /// Returns the name of the piece.
        /// </summary>
        /// <returns></returns>
        public String getName(){
            return name;
        }

        /// <summary>
        /// Returns what row the piece is in.
        /// </summary>
        /// <returns></returns>
        public int getRow(){
            return row;
        }

        /// <summary>
        /// Returns what column the piece is in.
        /// </summary>
        /// <returns></returns>
        public int getCol(){
            return column;
        }

        /// <summary>
        /// Sets what row the piece is in.
        /// </summary>
        /// <param name="r">New row</param>
        public void setRow(int r){
            row = r;
        }

        /// <summary>
        /// Sets what column the piece is in.
        /// </summary>
        /// <param name="c">New Column</param>
        public void setCol(int c){
            column = c;
        }

        /// <summary>
        /// Sets firstMove to false.
        /// </summary>
        public void updateFirstMove(){
            firstMove = false;
        }
            
        /// <summary>
        /// Static Method; Returns the piece name corresponding to an inputted type.
        /// </summary>
        /// <param name="n"></param>
        /// <returns>String name</returns>
        public static String getName(int n){
            switch(n){
                case 0:
                    return "pawn"; 
                case 1:
                    return "rook";
                case 2:
                    return "knight";
                case 3:
                    return "bishop";
                case 4:
                    return "queen";
                case 5:
                    return "king";
                default:
                    return "na";
            }
        }

        /// <summary>
        /// Considers piecetype, color, and position and determines possible moves. Requires
        /// current board to check viability of attacks (so no conditionals are needed in the board class)
        /// </summary>
        /// <param name="board">current board[,] in board class</param>
        /// <returns>List<int[]> moves; Contains an int[row, col] of every available move.</int></returns>
        public List<int[]> getMoves(Piece[,] board){
            List<int[]> moves = new List<int[]>();
                switch(type){
                case 0: //pawn
                    moves = pawnMove(board);
                    break;
                case 1: //rook
                    moves = rookMove(board);
                    break;
                case 2: //knight
                    moves = knightMove(board);
                    break;
                case 3: //bishop
                    moves = bishopMove(board);
                    break;
                case 4: //queen
                    moves = queenMove(board);
                    break;
                case 5: //king
                    moves = kingMove(board);
                    break;
                default: 
                    break;
                }
            return moves;
        }

        /// <summary>
        /// Returns a List<> of int[row, col]s of all available positions.
        /// </summary>
        /// <param name="board">Current Board</param>
        /// <returns></returns>
        private List<int[]> pawnMove(Piece[,] board){
            List<int[]> moves = new List<int[]>();
            int[] m0 = { row, column, 0};
            moves.Add(m0);
            if (White){//white pawn
                if (firstMove){//pawns move x2 on the first turn
                    if(board[row - 1, column] == null){//no piece right in front of pawn
                        int[] m1 = { row - 1, column, 0}; moves.Add(m1); //row 0
                        if(board[row - 2, column] == null){//no piece +2 in front of pawn
                            int[] m2 = { row - 2, column, 0}; moves.Add(m2); //row -1
                        }
                        if(column > 0){
                            if (board[row - 1, column - 1] != null)
                            {
                                if (board[row - 1, column - 1].isWhite() != White)
                                {
                                    int[] a1 = { row - 1, column - 1, 1 }; moves.Add(a1);
                                }
                            }
                        }
                        if(column < 7){
                            if (board[row - 1, column + 1] != null)
                            {
                                if (board[row - 1, column + 1].isWhite() != White)
                                {
                                    int[] a1 = { row - 1, column + 1, 1 }; moves.Add(a1);
                                }
                            }
                        }
                    }
                }
                else{//if not first turn
                    if (row - 1 > 0){
                        if(board[row - 1, column] == null){
                            int[] m1 = { row - 1, column, 0}; moves.Add(m1);
                        }
                        if(column < 7){
                            if (board[row - 1, column + 1] != null)
                            {
                                if (board[row - 1, column + 1].isWhite() != White)
                                {
                                    int[] a1 = { row - 1, column + 1, 1 }; moves.Add(a1);
                                }
                            }
                        }
                        if (column > 0)
                        {
                            if (board[row - 1, column - 1] != null)
                            {
                                if (board[row - 1, column - 1].isWhite() != White)
                                {
                                    int[] a1 = { row - 1, column - 1, 1 }; moves.Add(a1);
                                }
                            }
                        }
                    }
                    else{
                        throw new Exception("Need to implement behavior for when a pawn reaches the end of the board");
                    }
                }
            }
            else{//black pawn
                if (firstMove){//first move black pawn
                    int[] m1 = { row + 1, column, 0}; moves.Add(m1); //row -1
                    int[] m2 = { row + 2, column, 0}; moves.Add(m2); //row -2 
                }
                else{//not first move
                    if (row < 7){
                        int[] m1 = { row + 1, column, 0}; moves.Add(m1);
                    }
                    else{
                        throw new Exception("Need to implement behavior for when a pawn reaches the end of the board");
                    }
                }
            }
            return moves;
        }

        private List<int[]> knightMove(Piece[,] board){
            List<int[]> moves = new List<int[]>();
            int[] m1 = { row + 2, column + 1, 0}; moves.Add(m1);
            int[] m2 = { row + 1, column + 2, 0}; moves.Add(m2);
            int[] m3 = { row - 2, column + 1, 0}; moves.Add(m3);
            int[] m4 = { row - 1, column + 2, 0}; moves.Add(m4);
            int[] m5 = { row - 2, column - 1, 0}; moves.Add(m5);
            int[] m6 = { row - 1, column - 2, 0}; moves.Add(m6);
            int[] m7 = { row + 2, column - 1, 0}; moves.Add(m7);
            int[] m8 = { row + 1, column - 2, 0}; moves.Add(m8);
            for(int i = 0; i < moves.Count; i++){
                int[] pos = moves[i];
                if(pos[0] > 7 || pos[0] < 0 || pos[1] > 7 || pos[1] < 0){
                    moves.RemoveAt(i); // Removes out of bounds squares
                    i--;
                }
            }
            for(int i = 0; i < moves.Count; i++){
                int[] pos = moves[i];
                if (board[pos[0], pos[1]] != null){
                    if (board[pos[0], pos[1]].isWhite() == White){
                        moves.RemoveAt(i); //Removes squares with a piece of the same color
                        i--;
                    }
                    else{
                        moves[i][2] = 1; //sets 3rd term as attack
                    }
                }
            }
            int[] m0 = { row, column, 0}; moves.Add(m0);//Current position
            return moves;
        }

        /// <summary>
        /// Returns a List<int[row, col]> of the possible moves for a given rook
        /// </summary>
        /// <param name="board"> Current board config </param>
        /// <returns>List<int[row, col]> list of possible moves</returns>
        private List<int[]> rookMove(Piece[,] board){
            List<int[]> moves = new List<int[]>();
            int pRow = row;
            int pCol = column;
            int[] m0 = { row, column, 0}; moves.Add(m0);
            if(pRow > 0){
                while(pRow > 0){//up
                    if(board[pRow - 1, column] == null){//empty square above
                        int[] m = { pRow - 1, column, 0};
                        moves.Add(m);
                        pRow--;
                    }
                    else if(!(board[pRow - 1, column].isWhite() == White)){//enemy piece above
                        int[] m = { pRow - 1, column, 1 };
                        moves.Add(m);
                        pRow = 0;
                    }
                    else{
                        pRow = 0;
                    }
                }
            }
            pRow = row;
            if (pRow < 7){
                while(pRow < 7){//down
                    if(board[pRow + 1, column] == null){//empty square below
                        int[] m = { pRow + 1, column, 0};
                        moves.Add(m);
                        pRow++;
                    }
                    else if(!(board[pRow + 1, column].isWhite() == White)){//enemy piece below
                        int[] m = { pRow + 1, column, 1};
                        moves.Add(m);
                        pRow = 7;
                    }
                    else{
                        pRow = 7;
                    }
                }
            }
            if(pCol > 0){
                while(pCol > 0){//left
                    if(board[row, pCol - 1] == null){
                        int[] m = { row, pCol - 1, 0};
                        moves.Add(m);
                        pCol--;
                    }
                    else if(!(board[row, pCol - 1].isWhite() == White)){
                        int[] m = { row, pCol - 1, 1};
                        moves.Add(m);
                        pCol = 0;
                    }
                    else{
                        pCol = 0;
                    }
                }
            }
            pCol = column;
            if(pCol < 7){
                while (pCol < 7){//right
                    if(board[row, pCol + 1] == null){
                        int[] m = { row, pCol + 1, 0};
                        moves.Add(m);
                        pCol++;
                    }
                    else if(!(board[row, pCol + 1].isWhite() == White)){
                        int[] m = { row, pCol + 1, 1 };
                        moves.Add(m);
                        pCol = 7;
                    }
                    else{
                        pCol = 7;
                    }
                }
            }
            return moves;
        }

        /// <summary>
        /// Returns a List<int[row, column, type]> of the possible moves for a bishop given the current board config.
        /// </summary>
        /// <returns>List<int[row, col, type]> Possible moves & their corresponding types</int></returns>
        private List<int[]> bishopMove(Piece[,] board){
            List<int[]> moves = new List<int[]>();
            int[] m0 = { row, column, 0 }; moves.Add(m0);
            int pRow = row;
            int pCol = column;
            if(pRow > 0 && pCol > 0){//top left diagonal
                while(pRow > 0 && pCol > 0){
                    if(board[pRow - 1, pCol - 1] == null){//empty square
                        int[] m = { pRow - 1, pCol - 1, 0 };
                        moves.Add(m);
                        pRow--; pCol--;
                    }
                    else if(!(board[pRow - 1, pCol - 1].isWhite() == White)){//enemy piece
                        int[] m = { pRow - 1, pCol - 1, 1 };
                        moves.Add(m);
                        pRow = 0; pCol = 0;
                    }
                    else{//friendly piece
                        pRow = 0; pCol = 0;
                    }
                }
            }
            pRow = row; pCol = column;
            if(pRow < 7 && pCol < 7){
                while(pRow < 7 && pCol < 7){//top right diagonal
                    if(board[pRow + 1, pCol + 1] == null){//empty square
                        int[] m = { pRow + 1, pCol + 1, 0};
                        moves.Add(m);
                        pRow++; pCol++;
                    }
                    else if(!(board[pRow + 1, pCol + 1].isWhite() == White)){//enemy piece
                        int[] m = { pRow + 1, pCol + 1, 1 };
                        moves.Add(m);
                        pRow = 7; pCol = 7;
                    }
                    else{//friendly piece
                        pRow = 7; pCol = 7;
                    }
                }
            }
            pRow = row; pCol = column;
            if(pRow > 0 && pCol < 7){//top right diagonal
                while(pRow > 0 && pCol < 7){
                    if(board[pRow - 1, pCol + 1] == null){//empty square
                        int[] m = { pRow - 1, pCol + 1, 0 };
                        moves.Add(m);
                        pRow--; pCol++;
                    }
                    else if(!(board[pRow - 1, pCol + 1].isWhite() == White)){//enemy piece
                        int[] m = { pRow - 1, pCol + 1, 1 };
                        moves.Add(m);
                        pRow = 0; pCol = 7;
                    }
                    else{//friendly piece
                        pRow = 0; pCol = 7;
                    }
                }
            }
            pRow = row; pCol = column;
            if(pRow < 7 && pCol > 0){//bottom left diagonal
                while(pRow < 7 && pCol > 0){
                    if(board[pRow + 1, pCol - 1] == null){//empty square
                        int[] m = { pRow + 1, pCol - 1, 0 };
                        moves.Add(m);
                        pRow++; pCol--;
                    }
                    else if(!(board[pRow + 1, pCol - 1].isWhite() == White)){//enemy piece
                        int[] m = { pRow + 1, pCol - 1, 1 };
                        moves.Add(m);
                        pRow = 7; pCol = 0;
                    }
                    else{//friendly piece
                        pRow = 7; pCol = 0;
                    }
                }
            }
            return moves;
        }

        /// <summary>
        /// Returns a List<int[row, col, type]> of all possible moves for a queen given a board config. Just combines rooks and bishops for now haha.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>List<int[row, col, type]>Possible moves</returns>
        private List<int[]> queenMove(Piece[,] board){
            List<int[]> moves = new List<int[]>();
            moves = bishopMove(board);
            List<int[]> moves2 = rookMove(board);
            for(int i = 0; i < moves2.Count; i++){
                moves.Add(moves2[i]);
            }
            return moves;
        }

        /// <summary>
        /// Returns a List<int[row, col, type]> of all possible moves for a king given a board config.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>List<int[row, col, type]> possible king moves</returns>
        private List<int[]> kingMove(Piece[,] board) {
            List<int[]> moves = new List<int[]>();
            int[] m0 = { row, column, 0 }; moves.Add(m0);
            int[] m1 = { row + 1, column, 0 }; moves.Add(m1);
            int[] m2 = { row - 1, column, 0 }; moves.Add(m2);
            int[] m3 = { row + 1, column + 1, 0 }; moves.Add(m3);
            int[] m4 = { row + 1, column - 1, 0 }; moves.Add(m4);
            int[] m5 = { row, column - 1, 0 }; moves.Add(m5);
            int[] m6 = { row, column + 1, 0 }; moves.Add(m6);
            int[] m7 = { row - 1, column - 1, 0 }; moves.Add(m7);
            int[] m8 = { row - 1, column + 1, 0 }; moves.Add(m8);
            for (int i = 0; i < moves.Count; i++){
                int[] pos = moves[i];
                if (pos[0] > 7 || pos[0] < 0 || pos[1] > 7 || pos[1] < 0){
                    moves.RemoveAt(i); // Removes out of bounds squares
                    i--;
                }
            }
            for(int i = 0; i < moves.Count; i++){
                int[] pos = moves[i];
                if(board[pos[0], pos[1]] != null){
                    if(board[pos[0], pos[1]].isWhite() != White){
                        pos[2] = 1;
                        moves[i] = pos;
                    }
                    else{
                        if(!(moves[i].SequenceEqual(m0))){
                            moves.RemoveAt(i); //removes squares with alike pieces
                            i--;
                        }
                    }
                }
            }
            return moves;
        }

        /// <summary>
        /// Returns a dictionary containing all the possible row:col movements for the piece as a function of its type and position.
        /// </summary>
        /// <param name="t">Piece Type</param>
        /// <param name="row">Row in board[,]</param>
        /// <param name="col">Column in board[,]</param>
        /// <returns>Dictionary<int row, int col></int></returns>
       // public static Dictionary<int, int> getMoves(int t, int row, int col, bool white);
    }
}
