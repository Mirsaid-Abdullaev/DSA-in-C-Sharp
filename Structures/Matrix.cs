using System;
using System.Text;
using System.Security.Cryptography;
using DSA.Utils;

namespace DSA.Structures
{
    internal class Matrix
    {
        private double[][] matrix;
        public readonly int Rows;
        public readonly int Cols;
        public readonly int Size;

        /// <summary>
        /// Getter and setter for individual matrix elements
        /// </summary>
        /// <param name="row">0-based index of the row to access</param>
        /// <param name="col">0-based index of the column to access</param>
        /// <returns>matrix[row][column]</returns>
        public double this[int row, int col]
        {
            get
            {
                if (row < Rows && row > -1 && col < Cols && col > -1)
                {
                    return matrix[row][col];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (!double.IsInfinity(value) && !double.IsNaN(value))
                {
                    if (row < Rows && row > -1 && col < Cols && col > -1)
                    {
                        matrix[row][col] = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        /// <summary>
        /// Constructor for the matrix class - initialises an empty matrix of the given size (rows x cols)
        /// </summary>
        /// <param name="rows">Number of rows in the matrix</param>
        /// <param name="cols">Number of cols in the matrix</param>
        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Size = Rows * Cols;
            matrix = new double[Rows][];
            for (int i = 0; i < Rows; i += 1)
            {
                matrix[i] = new double[Cols];
            }
        }
        /// <summary>
        /// Constructor for matrix class - initialises a matrix from a 2d array, where rows and columns stay the same
        /// </summary>
        /// <param name="input">the 2d array with the matrix elements to be converted into a matrix class instance</param>
        public Matrix(double[][] input)
        {
            matrix = input;
            Rows = input.Length;
            Cols = input[0].Length;
            Size = Rows * Cols;
        }
        /// <summary>
        /// Resets the matrix instance to default values
        /// </summary>
        public void Clear()
        {
            matrix = new double[Rows][];
            for (int i = 0; i < Rows; i += 1)
            {
                matrix[i] = new double[Cols];
            }
        }
        /// <summary>
        /// Adds the given double value to all the elements in the matrix
        /// </summary>
        public void AddToAll(double value)
        {
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Cols; j += 1)
                {
                    this[i, j] += value;
                }
            }
        }
        /// <summary>
        /// Add a given value to the specified element in the matrix
        /// </summary>
        public void AddToElement(double value, int row, int col)
        {
            if (!(row < Rows && row > -1 && col < Cols && col > -1))
            {
                throw new ArgumentOutOfRangeException("Error: specified element is outside the range of the current matrix.");
            }
            this[row, col] += value;
        }
        /// <summary>
        /// Adds a given value to an entire row, specified by the 0-based row index
        /// </summary>
        public void AddToRow(double value, int row)
        {
            if (!(row < Rows && row > -1 ))
            {
                throw new ArgumentOutOfRangeException("Error: specified row is outside the range of the current matrix.");
            }

            for (int i = 0; i < Cols; i += 1)
            {
                this[row, i] += value;
            }
        }
        /// <summary>
        /// Adds a given value to an entire column, specified by the 0-based column index
        /// </summary>
        public void AddToColumn(double value, int col)
        {
            if (!(col < Cols && col > -1))
            {
                throw new ArgumentOutOfRangeException("Error: specified row is outside the range of the current matrix.");
            }

            for (int i = 0; i < Rows; i += 1)
            {
                this[i, col] += value;
            }
        }
        /// <summary>
        /// Checks whether another matrix m is the exact same as the current matrix instance - every element must be the same, and the size must be the same
        /// </summary>
        public bool Equals(Matrix m)
        {
            if (m == null || !(Rows == m.Rows && Cols == m.Cols))
            {
                return false;
            }
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Cols; j += 1)
                {
                    if (!(matrix[i][j] == m.matrix[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Returns a new row matrix from the current matrix instance, specified by the 0-based row index
        /// </summary>
        public Matrix GetRow(int row)
        {
            if (!(row < Rows && row > -1))
            {
                throw new ArgumentOutOfRangeException("Error: specified row is outside the range of the current matrix.");
            }
            return MatrixOps.CreateRowMatrix(matrix[row]);
        }
        /// <summary>
        /// Returns a new column matrix from the current matrix instance, specified by the 0-based column index
        /// </summary>
        public Matrix GetCol(int col)
        {
            if (!(col < Cols && col > -1))
            {
                throw new ArgumentOutOfRangeException("Error: specified row is outside the range of the current matrix.");
            }
            Matrix m = new Matrix(Rows, 1);
            for (int i = 0; i < Rows; i += 1)
            {
                m.matrix[i][0] = matrix[i][col];
            }
            return m;
        }
        /// <summary>
        /// Sets a specified 0-based column to a given column vector of the same size as the current matrix instance columns
        /// </summary>
        public void SetColumn(int col, Matrix m)
        {
            if (!(col < Cols && col > -1))
            {
                throw new ArgumentOutOfRangeException("Error: specified row is outside the range of the current matrix.");
            }
            if (!m.IsVector() || m.Cols > 1 || m.Rows != this.Rows)
            {
                throw new Exception("Error: trying to set column with a non-column matrix value or an invalid size matrix.");
            }
            for (int i = 0; i < Rows; i += 1)
            {
                matrix[i][col] = m[i, 0];
            }
        }
        /// <summary>
        /// Returns whether a matrix instance is a vector (either column or row)
        /// </summary>
        public bool IsVector()
        {
            return Rows == 1 || Cols == 1;
        }
        /// <summary>
        /// Returns true if all elements in the matrix are set to 0.
        /// </summary>
        public bool IsZero()
        {
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Cols; j += 1)
                {
                    if (!(matrix[i][j] == 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Sets all elements to a specific value
        /// </summary>
        public void SetAllElements(double value)
        {
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Cols; j += 1)
                {
                    matrix[i][j] = value;
                }
            }
        }
        /// <summary>
        /// Returns the total sum of every element in the current matrix instance
        /// </summary>
        public double SumAllElements()
        {
            double total = 0;
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Cols; j += 1)
                {
                    total += matrix[i][j];
                }
            }
            return total;
        }
        /// <summary>
        /// Creates a 1d array of a column or a row matrix
        /// </summary>
        /// <returns></returns>
        public double[] ToPackedArray()
        {
            if (Rows == 1)
            {
                return matrix[0];
            }
            else if (Cols == 1)
            {
                double[] m = new double[Rows];
                for (int i = 0; i < Rows; i += 1)
                {
                    m[i] = matrix[i][0];
                }
                return m;
            }
            else
            {
                throw new FormatException("Error: cannot format a non-column or a non-row matrix into a packed array.");
            }
        }
        /// <summary>
        /// Returns the underlying matrix 2d array of elements
        /// </summary>
        /// <returns></returns>
        public double[][] To2DArray()
        {
            return this.matrix;
        }

        /// <summary>
        /// Sets all elements randomly to any value between two specified values (min and max)
        /// </summary>
        public void Randomise(double minValue, double maxValue)
        {
            RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();
            double randomDouble;
            byte[] randomBytes = new byte[8];
            for (int i = 0; i < Rows; i += 1)
            {
                for (int j = 0; j < Cols; j += 1)
                {
                    RNG.GetBytes(randomBytes);
                    randomDouble = BitConverter.ToUInt64(randomBytes, 0) / Math.Pow(2, 64);
                    matrix[i][j] = minValue + ((maxValue - minValue) * randomDouble);
                }
            }
        }
        /// <summary>
        /// Overriden method to print and format a matrix as a string for output
        /// </summary>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("["); //start
            for (int i = 0; i < this.Rows; i++)
            {
                stringBuilder.Append("[");
                for (int j = 0; j < this.Cols; j++)
                {
                    if (j < this.Cols - 1)
                    {
                        stringBuilder.Append(this[i, j] + ", ");
                    }
                    else
                    {
                        stringBuilder.Append(this[i, j]);
                    }
                }
                if (i < this.Rows - 1)
                {
                    stringBuilder.Append($"],{System.Environment.NewLine} ");
                }
                else
                {
                    stringBuilder.Append($"]]");
                }
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Method to store a matrix instance in a deserialised format - can be then serialised using MatrixOps.GetMatrixFromString(Matrix m);
        /// </summary>
        public string GetMatrixData()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Rows + "," + Cols);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    stringBuilder.Append("," + this[i, j]);
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the dot product of m1 and m2
        /// </summary>
        public static double operator *(Matrix m1, Matrix m2)
        {
            return MatrixOps.DotProduct(m1, m2);
        }
        /// <summary>
        /// Returns the scalar product of scalar and m1: scalar * m1
        /// </summary>

        public static Matrix operator *(double scalar, Matrix m1)
        {
            return MatrixOps.ScalarMultiply(m1, scalar);
        }
        /// <summary>
        /// Returns m1 - m2
        /// </summary>

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return MatrixOps.Subtract(m1, m2);
        }
        /// <summary>
        /// Returns m1 + m2
        /// </summary>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return MatrixOps.Add(m1, m2);
        }
    }
}
