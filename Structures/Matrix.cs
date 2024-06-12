using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Data;

namespace DSA
{
    public delegate double Function(double x);
}

namespace DSA.Structures
{
    internal class Matrix
    {
        private double[][] matrix;
        private int rows;
        private int cols;
        private int size;
        public int Rows
        {
            get
            {
                return rows;
            }
        }
        public int Cols
        {
            get
            {
                return cols;
            }
        }
        public int Size
        {
            get
            {
                return size;
            }
        }

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
            this.rows = rows;
            this.cols = cols;
            this.size = Rows * Cols;
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
            this.rows = input.Length;
            this.cols = input[0].Length;
            this.size = Rows * Cols;
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
        /// Adds a row to the end of the current matrix instance, at index [Rows]
        /// </summary>
        public void AddRow()
        {
            rows += 1;
            double[][] temp = new double[rows][];
            Array.Copy(matrix, temp, rows - 1);
            temp[rows - 1] = new double[cols];
            for (int i = 0; i < temp[rows - 1].Length; i++)
            {
                temp[rows - 1][i] = 0;
            }
            matrix = temp;
        }
        /// <summary>
        /// Deletes a specified column from the matrix instance
        /// </summary>
        /// <param name="index">The 0-based index of the row to delete</param>
        public void DeleteRow(int index)
        {
            rows -= 1;
            if (rows == 0)
            {
                matrix = new double[0][];
                return;
            }
            double[][] temp = new double[rows][];
            Array.Copy(matrix, 0, temp, 0, index); //copies across the rows before the row to delete
            Array.Copy(matrix, index + 1, temp, index, matrix.Length - index - 1); //copies the rows after the row to delete
            matrix = temp;
        }
        /// <summary>
        /// Deletes a specified column from the matrix instance
        /// </summary>
        /// <param name="index">The 0-based index of the row to delete</param>
        public void DeleteCol(int index)
        {
            cols -= 1;
            if (cols == 0) //empty matrix
            {
                matrix = new double[0][];
                rows = 0;
                size = 0;
                return;
            }
            double[][] temp = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                temp[i] = new double[cols];
                Array.Copy(matrix[i], 0, temp[i], 0, index);
                Array.Copy(matrix[i], index + 1, temp[i], index, cols - index); 
            }
            matrix = temp;
        }

        /// <summary>
        /// Adds a column to the end of the current matrix instance, at index [Cols]
        /// </summary>
        public void AddCol()
        {
            cols += 1;
            for (int i = 0; i < matrix.Length; i++)
            {
                double[] temp = new double[cols];
                Array.Copy(matrix[i], temp, cols - 1);
                temp[^1] = 0; 
                matrix[i] = temp;
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
            if (size == 0)
            {
                return "Empty matrix: size is 0, all elements deleted";
            }
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
    internal static class MatrixOps
    {
        /// <summary>
        /// Returns the result of m1 + m2
        /// </summary>
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            //check if matrices are same-size, else throw exception
            if (!(m1.Cols == m2.Cols && m1.Rows == m2.Rows))
            {
                throw new FormatException($"Matrices provided are not the same size: m1 has {m1.Cols} columns and {m1.Rows} but m2 has {m2.Cols} columns and {m2.Rows} rows.");
            }
            Matrix m3 = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    m3[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return m3;
        }


        /// <summary>
        /// Returns the result of 1/scalar * m1 (multiply each entry in m1 by 1/scalar)
        /// </summary>
        /// <param name="scalar">Double parameter to divide each m1 entry by</param>
        /// <returns>1/scalar * m1, new matrix instance</returns>
        public static Matrix Divide(Matrix m1, double scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Scalar passed in was 0, cannot divide by 0.");
            }
            else if (double.IsNaN(scalar) || double.IsInfinity(scalar))
            {
                throw new FormatException("Scalar passed in was invalid.");
            }
            Matrix m2 = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    m2[i, j] = m1[i, j] / scalar;
                }
            }
            return m2;
        }

        /// <summary>
        /// Computes the dot product between two column/row vectors and returns the result
        /// </summary>
        /// <returns>Dot product value of m1 and m2 vectors</returns>
        public static double DotProduct(Matrix m1, Matrix m2)
        {
            if (!(m1.IsVector() && m2.IsVector() && m1.Size == m2.Size))
            {
                //not a column or a row vector
                throw new FormatException("Matrices m1 and m2 were not 1xn or nx1 matrices. Needs to be same size/number of elements.");
            }
            double dot_product = 0;
            double[] new_m1 = m1.ToPackedArray();
            double[] new_m2 = m2.ToPackedArray();
            for (int i = 0; i < new_m1.Length; i++)
            {
                dot_product += new_m1[i] * new_m2[i];
            }
            return dot_product;
        }
        /// <summary>
        /// Provides an identity matrix instance of a given size
        /// </summary>
        public static Matrix Identity(int size)
        {
            Matrix Identity = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                Identity[i, i] = 1;
            }
            return Identity;
        }
        /// <summary>
        /// Returns the result of computing m1 * m2, using parallel multiplication. Matrix multiplication is not commutative, so m1 and m2 must be in correct order
        /// </summary>
        /// <returns>m3 = m1 * m2</returns>
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1.Cols != m2.Rows)
            {
                throw new FormatException("m1 and m2 are not multiplicatively conformative, m1 needs to have same number of columns as m2 has rows.");
            }
            Matrix m3 = new Matrix(m1.Rows, m2.Cols);

            Parallel.For(0, m1.Rows, i =>
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    m3[i, j] = MatrixOps.DotProduct(m1.GetRow(i), m2.GetCol(j));
                }
            });
            return m3;
        }

        /// <summary>
        /// Performs and returns the Hadamard product on two matrices. This is an element-wise multiplication algorithm on same-size matrices
        /// </summary>
        /// <returns>m1 ⊙ m2 (the Hadamard product)</returns>
        public static Matrix HadamardProduct(Matrix m1, Matrix m2)
        {
            if (!(m1.Cols == m2.Cols && m1.Rows == m2.Rows))
            {
                throw new FormatException("m1 and m2 are not Hadamard-conformative, m1 needs to have same dimensions as m2.");
            }
            Matrix m3 = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    m3[i, j] = m1[i, j] * m2[i, j];
                }
            }
            return m3;
        }

        /// <summary>
        /// Returns the result of scalar * m1 (scalar multiplication)
        /// </summary>
        public static Matrix ScalarMultiply(Matrix m1, double scalar)
        {
            if (double.IsNaN(scalar) || double.IsInfinity(scalar))
            {
                throw new FormatException("Scalar passed in was invalid.");
            }
            else if (scalar == 0)
            {
                return new Matrix(m1.Rows, m1.Cols); //multiplying by 0 results in init matrix
            }
            Matrix m2 = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    m2[i, j] = scalar * m1[i, j];
                }
            }
            return m2;
        }
        /// <summary>
        /// Returns the result from doing m1 - m2
        /// </summary>
        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            if (!(m1.Cols == m2.Cols && m1.Rows == m2.Rows))
            {
                throw new FormatException("Matrices m1 and m2 are not the same size - cannot perform subtraction");
            }
            Matrix m3 = new Matrix(m1.Rows, m1.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    m3[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return m3;
        }
        /// <summary>
        /// Transposes an input matrix m1 - rows and columns flipped
        /// </summary>
        public static Matrix Transpose(Matrix m1)
        {
            Matrix m2 = new Matrix(m1.Cols, m1.Rows);
            for (int i = 0; i < m2.Rows; i++)
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    m2[i, j] = m1[j, i];
                }
            }
            return m2;
        }

        /// <summary>
        /// Gets the magnitude of a vector matrix m1
        /// </summary>
        public static double Magnitude(Matrix m1)
        {
            if (!m1.IsVector())
            {
                throw new FormatException("Matrix m1 is not a 1xn or nx1 format - cannot compute magnitude");
            }

            double magnitude = 0;
            if (m1.Cols == 1) //column matrix
            {
                for (int i = 0; i < m1.Rows; i++)
                {
                    magnitude += m1[i, 0] * m1[i, 0];
                }
            }
            else //row matrix
            {
                for (int i = 0; i < m1.Cols; i++)
                {
                    magnitude += m1[0, i] * m1[0, i];
                }
            }

            return Math.Sqrt(magnitude);

        }
        /// <summary>
        /// Copies the data from m1 into the target matrix m2. m1 is not altered, m2 is overwritten.
        /// </summary>
        public static void Copy(Matrix m1, Matrix m2) //this sub will alter m2 contents
        {
            if (!(m1.Cols == m2.Cols && m1.Rows == m2.Rows))
            {
                throw new FormatException("Matrices m1 and m2 are not the same size - cannot perform copy");
            }
            //matrices are same size
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    m2[i, j] = m1[i, j];
                }
            }
            return;
        }


        /// <summary>
        /// Deletes a row at the specified 0-based index and returns a new matrix without that row
        /// </summary>
        public static Matrix DeleteRow(Matrix m1, int row)
        {
            if (row >= m1.Rows || row < 0)
            {
                throw new IndexOutOfRangeException($"Row input is invalid - matrix m1 only has {m1.Rows} rows, but wanted to delete row number {row}.");
            }
            //row in range, proceed to make a new matrix
            Matrix m2 = new Matrix(m1.Rows - 1, m1.Cols);
            for (int i = 0; i < m2.Rows; i++)
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    if (i >= row)
                    {
                        m2[i, j] = m1[i + 1, j];
                    }

                }
            }
            return m2;
        }
        /// <summary>
        /// Deletes a column at the specified 0-based index and returns a new matrix without that column
        /// </summary>
        public static Matrix DeleteCol(Matrix m1, int col)
        {
            if (col >= m1.Cols || col < 0)
            {
                throw new IndexOutOfRangeException($"Row input is invalid - matrix m1 only has {m1.Cols} columns, but wanted to delete column number {col}.");
            }
            //row in range, proceed to make a new matrix
            Matrix m2 = new Matrix(m1.Rows, m1.Cols - 1);
            for (int i = 0; i < m2.Cols; i++) //this hokey sub might not work, but it should
            {
                for (int j = 0; j < m2.Rows; j++)
                {
                    if (i >= col)
                    {
                        m2[i, j] = m1[i + 1, j];
                    }

                }
            }
            return m2;
        }

        /// <summary>
        /// Reverses the Matrix.GetMatrixData() sub which stringifies the Matrix instance, this sub serialises the string into a Matrix instance
        /// </summary>
        public static Matrix GetMatrixFromString(string StringMatrix)
        {
            string[] items = StringMatrix.Split(',');
            int Rows = Convert.ToInt32(items[0]);
            int Cols = Convert.ToInt32(items[1]);
            Matrix matrix = new Matrix(Rows, Cols);
            int Counter = 2;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    matrix[i, j] = Convert.ToDouble(items[Counter]);
                    Counter++;
                }
            }
            return matrix;
        }

        /// <summary>
        /// Returns a Matrix instance of size n x 1, where n is the number of items in the input[] array, all elements used for the column matrix
        /// </summary>
        public static Matrix CreateColumnMatrix(double[] input)
        {
            Matrix m = new Matrix(input.Length, 1);
            for (int i = 0; i < m.Rows; i += 1)
            {
                m[i, 0] = input[i];
            }
            return m;
        }
        /// <summary>
        /// Returns a Matrix instance of size 1 x n, where n is the number of items in the input[] array, all elements used for the row matrix
        /// </summary>
        public static Matrix CreateRowMatrix(double[] input)
        {
            Matrix m = new Matrix(1, input.Length);
            for (int i = 0; i < m.Cols; i++)
            {
                m[0, i] = input[i];
            }
            return m;
        }

        /// <summary>
        /// Returns a matrix where a function has been applied to every value in the matrix - higher order functionality
        /// </summary>
        /// <param name="f">The function to be applied to the input Matrix</param>
        /// <param name="m">The matrix instance for the function application</param>
        /// <returns>The matrix instance where f(m) has been applied, to every element individually</returns>
        public static Matrix ApplyToAll(Function f, Matrix m) //higher order function
        {
            Matrix m1 = new Matrix(m.Rows, m.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Cols; j++)
                {
                    m1[i, j] = f(m[i, j]); //applying the specified delegate function to all elements
                }

            }
            return m1;
        }
    }

}