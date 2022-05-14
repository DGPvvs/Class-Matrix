using System;
using System.Collections.Generic;

namespace Class_Matrix
{
    /// <summary>
    /// class Point : IComparable<Point> - Помощен клас ползван за запазване на координати
    /// на позиции от определени елементи в матрицата
    /// </summary>
    public class Point : IComparable<Point>
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Point(int newRow, int newCol)
        {
            this.Row = newRow;
            this.Col = newCol;
        }// Point(int newRow, int NewCol)

        public int CompareTo(Point other)
        {
            int comp = this.Row.CompareTo(other.Row);

            if (comp == 0)
            {
                comp = this.Col.CompareTo(other.Col);
            }

            return comp;
        }// int CompareTo(Point other)
    }// class Point : IComparable<Point>

    /// <summary>
    ///  class Matrix<T> where T : IComparable<T> основен клас съдържащ функционалности с чиято помощ
    ///  могат да се решават някой от изпитните задачи за матрици
    /// </summary>
    public class Matrix<T> where T : IComparable<T>
    {
        /// <summary>
        /// T[][] matrix - вътвешно представяне на матрицата
        /// </summary>
        private T[][] matrix;

        /// <summary>
        /// Point startPoint - поле съдържащо в себе си стартовата позиция от която започва движение в матрицата
        /// </summary>
        private Point startPoint;

        /// <summary>
        /// Point currentPoint - поле съдържащо в себе си текущата позиция при движение в матрицата
        /// </summary>
        private Point currentPoint;

        /// <summary>
        /// Point chekPoint - поле съдържащо координати
        /// </summary>
        private Point chekPoint;

        /// <summary>
        /// T startElement - символ, приет за маркиране на стартовата позиция от която започва движението в матрицата
        /// </summary>
        private T startElement;

        /// <summary>
        /// int maxLengthRow - поле съдържащо в себе си броя на редовете в матрицата
        /// </summary>
        private int maxLengthRow;

        /// <summary>
        /// int currentRowLength; поле съдържащо текущият брой колони в текущия ред
        /// </summary>
        private int currentRowLength;

        /// <summary>
        /// string spaceSeparator - поле съдържащо символ/и използван/и за сепаратор между елементите при отпечатване на матрицата
        /// </summary>
        private string spaceSeparator;

        /// <summary>
        /// Point StartPoint - пропърти връщащо координатите на стартовата позиция
        /// </summary>
        public Point StartPoint
        {
            get => this.startPoint;

            private set
            {

            }
        }

        /// <summary>
        /// Point CurrentPoint - пропърти връщащо координатите на текущата позиция и задаващ нова стойност на текущата позиция. При опит да се зададат
        /// ред или колона извън размера на матрицата хвърля изключение IndexOutOfRangeException
        /// Обикновенно се използва за да се определи дали движението вече е извън рамките на матрицата
        /// </summary>
        public Point CurrentPoint
        {
            get => this.currentPoint;

            set
            {
                bool isValid = ((value.Row >= 0) && (value.Row < this.MaxLengthRow));
                isValid = ((isValid) && (value.Col >= 0) && (value.Col < this.matrix[value.Row].Length));

                if (isValid)
                {
                    this.currentPoint = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Point ChekPoint пропърти връщащо координати. При опит да се зададат
        /// ред или колона извън размера на матрицата хвърля изключение IndexOutOfRangeException
        /// Използва се за проверка дали индексите са в рамките на матрицата
        /// </summary>
        public Point ChekPoint
        {
            get => this.chekPoint;

            set
            {
                bool isValid = ((value.Row >= 0) && (value.Row < this.MaxLengthRow));
                isValid = ((isValid) && (value.Col >= 0) && (value.Col < this.matrix[value.Row].Length));

                if (isValid)
                {
                    this.chekPoint = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// int MaxLengthRow пропърти връщащо броя на редовете в матрицата
        /// </summary>
        public int MaxLengthRow
        {
            get => this.maxLengthRow;

            private set
            {

            }
        }

        /// <summary>
        /// int CurrentRowLength пропърти връщащо броя на колоните в текущия ред
        /// </summary>
		public int CurrentRowLength
        {
            get => this.currentRowLength;
            private set
            {

            }
        }

        /// <summary>
        /// Matrix(string newSpaceSeparator, int newRow, int newCol = 0) - конструктор създаващ празна матрица с размери newRow и newCol
        /// задава стойностите на maxLengthRow, rangeIndexes и spaceSeparator 
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newSpaceSeparator"></param>
        public Matrix(string newSpaceSeparator, int newRow, int newCol = 0)
        {
            this.CreateMatrix(null, newSpaceSeparator, newRow, newCol);
        }// Matrix(string newSpaceSeparator, int newRow, int newCol = 0)

        /// <summary>
        /// Matrix(T[,] newMatrix, string newSpaceSeparator) Конструктор задаващ външна матрица и установяващ newSpaceSeparator
        /// задава стойностите на maxLengthRow, rangeIndexes и spaceSeparator
        /// </summary>
        /// <param name="newMatrix"></param>
        /// <param name="newIsWall"></param>
        /// <param name="newSpaceSeparator"></param>
        public Matrix(T[,] newMatrix, string newSpaceSeparator)
        {
            int newRow = newMatrix.GetLength(0);
            int newCol = newMatrix.GetLength(1);

            this.CreateMatrix(newMatrix, newSpaceSeparator, newRow, newCol);
        }// Matrix(T[,] newMatrix, string newSpaceSeparator)                 

        /// <summary>
        /// void SetStartElement(T element) метод задаващ стойност на startElement и намиращ startPoint в матрицата 
        /// </summary>
        /// <param name="element"></param>
        public void SetStartElement(T element)
        {
            this.startElement = element;
            this.SetStartPoint(this.startElement);
        }// void SetStartElement(T element)

        /// <summary>
        /// void SetStartPoint(T newStartElement) - задава стойност на startPoint с координати където се намира newStartElement 
        /// </summary>
        /// <param name="newStartElement"></param>
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
        }// void SetStartPoint(T startElement)

        /// <summary>
        /// void SetStartToCurrentPoint() - задава стойност на currentPoint идентична с тази на startPoint
        /// </summary>
        public void SetStartToCurrentPoint()
        {
            this.CurrentPoint = new Point(this.startPoint.Row, this.startPoint.Col);
        }// void SetCurrentPoint()

        /// <summary>
        /// void SetMatrixRow(int row, T[] colArr) - запълва редът row от матрицата с елементине на colArr
        /// </summary>
        /// <param name="row"></param>
        /// <param name="colArr"></param>
        public void SetMatrixRow(int row, T[] colArr)
        {
            this.matrix[row] = colArr;

            this.SetLength();
        }// void SetMatrixRow(int row, T[] colArr)

        /// <summary>
        /// void SetCurrentElement(T element) задава стойност element на клетка в матрицата сочена от currentPoint
        /// </summary>
        /// <param name="element"></param>
        public void SetCurrentElement(T element)
        {
            this.matrix[currentPoint.Row][currentPoint.Col] = element;
        }// void SetCurrentElement(T element)

        /// <summary>
        /// T GetCurrentElement() връща символ намиращ се в клетка от матрицата сочена от currentPoint
        /// </summary>
        /// <returns></returns>
        public T GetCurrentElement() => this.matrix[currentPoint.Row][currentPoint.Col];// T GetCurrentElement()

        /// <summary>
        /// void SetElementAtPosition(T element, Point point) задава стойност element на клетка в матрицата сочена от point
        /// Не прави проверка за валидността на point
        /// </summary>
        /// <param name="element"></param>
        /// <param name="point"></param>
        public void SetElementAtPosition(T element, Point point)
        {
            this.matrix[point.Row][point.Col] = element;
        }// void SetElementAtPosition(T element, Point point)

        /// <summary>
        /// T GetElementAtPosition(Point point) връща символ намиращ се в клетка от матрицата сочена от point
        /// Не прави проверка за валидността на point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public T GetElementAtPosition(Point point) => this.matrix[point.Row][point.Col];// T GetElementAtPosition(Point point)

        /// <summary>
        /// void SetSpaceSeparator(string newSpaceSeparator) задава стойност на полето spaceSeparator
        /// </summary>
        /// <param name="newSpaceSeparator"></param>
        public void SetSpaceSeparator(string newSpaceSeparator)
        {
            this.spaceSeparator = newSpaceSeparator;
        }// void SetSpaceSeparator(string newSpaceSeparator)

        /// <summary>
        /// Queue<Point> FoundAllElementPositions(T element) - метод връщашт колекция с координати на всички позиции където е намерен символа element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
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

            return result;
        }// Queue<Point> FoundAllElementPositions(T element)

        /// <summary>
        /// override string ToString() връща стринг съдържаш представянe на текущото състояние на матрицата
        /// елементите са разделени с spaceSeparator
        /// </summary>
        /// <returns></returns>
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
        }// override string ToString()    

        /// <summary>
        /// string GetChessCoordinates(Point position) Функция връщаща стринг съдържащ координати на шахматната дъска на позиция от матрицата position 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public string GetChessCoordinates(Point position) => $"{((char)((int)'a' + position.Col)).ToString()}{position.Row + 1}";//string GetChessCoordinates(Point position)

        /// <summary>
        /// Point ChangeCurrentPosition(Point newPoint, int stepRow, int stepCol) - метод превъртащ координатите на currentPoint при излизане от матрицата 
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="stepRow"></param>
        /// <param name="stepCol"></param>
        /// <returns></returns>
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

            return result;
        }// Point ChangeCurrentPosition(Point newPoint)

        /// <summary>
        /// void CreateMatrix(T[,] newMatrix, string newSpaceSeparator, int newRow, int newCol) създава самата матрица
        /// Метода и валиден както за правоъгълни матрици, така и за назъбени
        /// </summary>
        /// <param name="newMatrix"></param>
        /// <param name="newSpaceSeparator"></param>
        /// <param name="newRow"></param>
        /// <param name="newCol"></param>
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
        }// void CreateMatrix(T[,] newMatrix, int newRow, int newCol, string newSpaceSeparator)

        /// <summary>
        /// void SetLength() задава стойности на maxLengthRow и rangeIndexes 
        /// </summary>
        private void SetLength()
        {
            this.maxLengthRow = this.matrix.Length;
        }// void SetLength()

        /// <summary>
        /// int GetCurrentCol(int row) връща дължината на текущия ред   
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int GetCurrentCol(int row)
        {
            this.SetCurrentCol(row);
            return this.CurrentRowLength;
        }// int GetCurrentCol(int row)

        /// <summary>
        /// void SetCurrentCol(int row) задава стойност на полето currentRowLength 
        /// </summary>
        /// <param name="row"></param>
        private void SetCurrentCol(int row)
        {
            this.CurrentRowLength = this.matrix[row].Length;
        }// void SetCurrentCol(int row)
    }// class Matrix<T>
}
