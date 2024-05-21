using System;
using System.Threading.Tasks;
using DSA.Structures;

namespace DSA
{
    public delegate double Function(double x);
}
namespace DSA.Utils
{
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

