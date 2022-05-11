using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MazeRunner
{
    class Program
    {
       static string OpenFile()
        {
            return File.ReadAllText(@"D:\\Aleksi Irpola\\ohjelmointi\\Maze\\Maze_Second.txt");
        }

        static int[][] _moves = {
        new int[] { -1, 0 },
        new int[] { 0, -1 },
        new int[] { 0, 1 },
        new int[] { 1, 0 } };

        static int[][] GetMazeArray(string maze)
        {
            string[] lines = maze.Split(new char[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);
            int[][] array = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                var row = new int[line.Length];
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case '#':
                            row[x] = -1;
                            break;
                        case '^':
                            row[x] = 1;
                            break;
                        case 'E':
                            row[x] = -3;
                            break;
                        default:
                            row[x] = 0;
                            break;
                    }
                }
                array[i] = row;
            }
            return array;
        }

        static void Display(int[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var row = array[i];
                for (int x = 0; x < row.Length; x++)
                {
                    switch (row[x])
                    {
                        case -1:
                            Console.Write('#');
                            break;
                        case 1:
                            Console.Write('^');
                            break;
                        case -3:
                            Console.Write('E');
                            break;
                        case 0:
                            Console.Write(' ');
                            break;
                        default:
                            Console.Write('.');
                            break;
                    }
                }
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        static bool IsValidPos(int[][] array, int row, int newRow, int newColumn)
        {
            if (newRow < 0) return false;
            if (newColumn < 0) return false;
            if (newRow >= array.Length) return false;
            if (newColumn >= array[row].Length) return false;
            return true;
        }

        static int ModifyPath(int[][] array)
        {
            
            for (int rowIndex = 0; rowIndex < array.Length; rowIndex++)
            {
                var row = array[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    int value = array[rowIndex][columnIndex];
                    if (value >= 1)
                    {
                        foreach (var movePair in _moves)
                        {
                            int newRow = rowIndex + movePair[0];
                            int newColumn = columnIndex + movePair[1];
                            if (IsValidPos(array, rowIndex, newRow, newColumn))
                            {
                                int testValue = array[newRow][newColumn];
                                if (testValue == 0)
                                {
                                    array[newRow][newColumn] = value + 1;
                                    return 0;
                                }
                                else if (testValue == -3)
                                {
                                    return 1;
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }


        static void Main(string[] args)
        {
            string maze = OpenFile();
            var numberMaze = GetMazeArray(maze);
            Display(numberMaze);
            int count = 0;

            while (true)
            {
                int result = ModifyPath(numberMaze);
                if (result == 1)
                {
                    Console.WriteLine($"DONE: {count} moves");
                    break;
                }
                else if (result == -1)
                {
                    Console.WriteLine($"FAIL: {count} moves");
                    break;
                }
                else
                {
                    Display(numberMaze);
                }
                count++;
            }

        }
    }
}
