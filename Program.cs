using System;
using System.Collections.Generic;

namespace Class_Matrix
{
    public class Point : IComparable<Point>
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Point(int newRow, int newCol)
        {
            this.Row = newRow;
            this.Col = newCol;
        }

        public int CompareTo(Point other)
        {
            int comp = this.Row.CompareTo(other.Row);

            if (comp == 0)
            {
                comp = this.Col.CompareTo(other.Col);
            }

            return comp;
        }
    }
        
    public class Matrix<T> where T : IComparable<T>
    {
        private T[][] matrix;
        private Point startPoint;
        private Point currentPoint;
        private Point chekPoint;
        private T startElement;
        private int maxLengthRow;
        private int currentRowLength;
        private string spaceSeparator;
        public Point StartPoint
        {
            get => this.startPoint;

            private set
            {

            }
        }

        public Point CurrentPoint
        {
            get => this.currentPoint;

            set
            {
                if (this.IsValidIndex(value))
                {
                    this.currentPoint = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public Point ChekPoint
        {
            get => this.chekPoint;

            set
            {
                if (this.IsValidIndex(value))
                {
                    this.chekPoint = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public bool IsValidIndex(Point point)
        {
            bool isValid = ((point.Row >= 0) && (point.Row < this.MaxLengthRow));
            isValid = ((isValid) && (point.Col >= 0) && (point.Col < this.matrix[point.Row].Length));

            return isValid;
        }

        public int MaxLengthRow
        {
            get => this.maxLengthRow;

            private set
            {

            }
        }

		public int CurrentRowLength
        {
            get => this.currentRowLength;
            private set
            {

            }
        }

        public Matrix(string newSpaceSeparator, int newRow, int newCol = 0)
        {
            this.CreateMatrix(null, newSpaceSeparator, newRow, newCol);
        }

        public Matrix(T[,] newMatrix, string newSpaceSeparator)
        {
            int newRow = newMatrix.GetLength(0);
            int newCol = newMatrix.GetLength(1);

            this.CreateMatrix(newMatrix, newSpaceSeparator, newRow, newCol);
        }

        public void SetStartElement(T element)
        {
            this.startElement = element;
            this.SetStartPoint(this.startElement);
        }

        public void SetStartPoint(T newStartElement)
        {
            this.startElement = newStartElement;

            bool isLoopExit = false;
            int row = 0;

            while ((row < MaxLengthRow) && (!isLoopExit))
            {
                int col = 0;

                while ((col < this.matrix[row].Length) && (!isLoopExit))
                {
                    if (this.matrix[row][col].Equals(newStartElement))
                    {
                        this.startPoint = new Point(row, col);
                        isLoopExit = true;
                    }

                    col++;
                }

                row++;
            }

            this.SetStartToCurrentPoint();
        }

        public void SetStartToCurrentPoint()
        {
            this.CurrentPoint = new Point(this.startPoint.Row, this.startPoint.Col);
        }

        public void SetMatrixRow(int row, T[] colArr)
        {
            this.matrix[row] = colArr;

            this.SetLength();
        }

        public void SetCurrentElement(T element)
        {
            this.matrix[currentPoint.Row][currentPoint.Col] = element;
        }

        public T GetCurrentElement() => this.matrix[currentPoint.Row][currentPoint.Col];

        public void SetElementAtPosition(T element, Point point)
        {
            this.matrix[point.Row][point.Col] = element;
        }

        public T GetElementAtPosition(Point point) => this.matrix[point.Row][point.Col];

        public void SetSpaceSeparator(string newSpaceSeparator)
        {
            this.spaceSeparator = newSpaceSeparator;
        }

        public IEnumerable<Point> FoundAllElementPositions(T element)
        {
            Queue<Point> result = new Queue<Point>();

            for (int row = 0; row < this.matrix.Length; row++)
            {
                int maxCol = this.matrix[row].Length;

                for (int col = 0; col < maxCol; col++)
                {
                    if (this.matrix[row][col].Equals(element))
                    {
                        Point newPoint = new Point(row, col);
                        result.Enqueue(newPoint);
                    }
                }
            }

            return result as IEnumerable<Point>;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < this.maxLengthRow; row++)
            {
                StringBuilder temp = new StringBuilder();

                int colLessOne = this.matrix[row].Length - 1;

                for (int col = 0; col < colLessOne + 1; col++)
                {
                    if (col == colLessOne)
                    {
                        temp.Append(this.matrix[row][col].ToString());
                    }
                    else
                    {
                        temp.Append(this.matrix[row][col].ToString() + this.spaceSeparator);
                    }
                }

                sb.AppendLine(temp.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string GetChessCoordinates(Point position) => $"{((char)((int)'a' + position.Col)).ToString()}{position.Row + 1}";

        public Point ChangeCurrentPosition(Point newPoint, int stepRow, int stepCol)
        {
            Point result = null;
            if (this.currentPoint.Row == newPoint.Row)
            {
                result = new Point(newPoint.Row, this.matrix[this.currentPoint.Row].Length + (-1) * newPoint.Col * stepCol);
            }
            else if (currentPoint.Col == newPoint.Col)
            {
                result = new Point(this.MaxLengthRow + (-1) * newPoint.Row * stepRow, newPoint.Col);
            }

            this.CurrentPoint = result;

            return result;
        }

        private void CreateMatrix(T[,] newMatrix, string newSpaceSeparator, int newRow, int newCol)
        {
            this.matrix = new T[newRow][];

            if (newMatrix == null)
            {
                for (int row = 0; row < newRow; row++)
                {
                    T[] temp = new T[newCol];
                    this.matrix[row] = temp;
                }
            }
            else
            {
                for (int row = 0; row < newRow; row++)
                {
                    T[] temp = new T[newCol];
                    this.matrix[row] = temp;

                    for (int col = 0; col < newCol; col++)
                    {
                        this.matrix[row][col] = newMatrix[row, col];
                    }
                }
            }

            this.SetLength();
            this.SetSpaceSeparator(newSpaceSeparator);
        }

        private void SetLength()
        {
            this.maxLengthRow = this.matrix.Length;
        }

        public int GetCurrentCol(int row)
        {
            this.SetCurrentCol(row);
            return this.CurrentRowLength;
        }
        private void SetCurrentCol(int row)
        {
            this.CurrentRowLength = this.matrix[row].Length;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           
        }
    }
}
