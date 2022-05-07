using System;
using System.Collections.Generic;
using System.Text;

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

}
