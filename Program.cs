using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AdventOfCode_2021_Day10
{
    public static class Program
    {
        public static void Main()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(currentDirectory, @"..\..\..\input.txt");
            string path = Path.GetFullPath(file);
            string[] text = File.ReadAllText(path).Replace("\r", "").Split("\n");

            int result = 0;
            List<int> illegalLines = new();

            for(int i = 0; i < text.Length; i++)
            {
                Stack<char> lastItem = new ();

                for(int j = 0; j < text[i].Length; j++)
                {
                    if(text[i][j] == '{' || text[i][j] == '[' ||text[i][j] == '(' || text[i][j] == '<')
                    {
                        lastItem.Push(text[i][j]);
                    }
                    else if(text[i][j] == '}')
                    {
                        if(lastItem.Peek() != '{' )
                        {
                            Console.WriteLine($"Expected {lastItem.Pop()} but found }} instead");
                            result+=1197;
                            illegalLines.Add(i);
                            break;
                        }
                        lastItem.Pop();
                    }
                    else if(text[i][j] == ']')
                    {
                        if(lastItem.Peek() != '[')
                        {
                            Console.WriteLine($"Expected {lastItem.Pop()} but found ] instead");
                            result+=57;
                            illegalLines.Add(i);
                            break;
                        }
                        lastItem.Pop();
                    }
                    else if(text[i][j] == ')')
                    {
                        if(lastItem.Peek() != '(')
                        {
                            Console.WriteLine($"Expected {lastItem.Pop()} but found ) instead");
                            result+=3;
                            illegalLines.Add(i);
                            break;
                        }
                        lastItem.Pop();
                    }
                    else if(text[i][j] == '>')
                    {
                        if(lastItem.Peek() != '<')
                        {
                            Console.WriteLine($"Expected {lastItem.Pop()} but found > instead");
                            result+=25137;
                            illegalLines.Add(i);
                            break;
                        }
                        lastItem.Pop();
                    }
                }
            }
            Console.WriteLine("Part 1 result: " + result);
            // Part 2
            List<ulong> result2 = new();
            for(int i = 0; i < text.Length; i++)
            {
                if( illegalLines.Contains(i))
                {
                    continue;
                }
                List<char> lastItem = new ();
                ulong score = 0;
                for(int j = 0; j < text[i].Length; j++)
                {
                    if(text[i][j] == '{' || text[i][j] == '[' ||text[i][j] == '(' || text[i][j] == '<')
                    {
                        lastItem.Add(text[i][j]);
                    }
                    else
                    {
                        if(text[i][j] == '}')
                        {
                            lastItem.RemoveAt(lastItem.LastIndexOf('{'));
                        }
                        else if(text[i][j] == ')')
                        {
                            lastItem.RemoveAt(lastItem.LastIndexOf('('));
                        }
                        else if(text[i][j] == '>')
                        {
                            lastItem.RemoveAt(lastItem.LastIndexOf('<'));
                        }
                        else if(text[i][j] == ']')
                        {
                            lastItem.RemoveAt(lastItem.LastIndexOf('['));
                        }
                    }
                }
                int len = lastItem.Count-1;
                for(int k = 0; k <= len; k++)
                {
                    score *= 5;
                    char c = lastItem[len-k];
                    if(c == '[')
                    {
                        score+=2;
                    }
                    else if(c == '(')
                    {
                        score++;
                    }
                    else if(c =='{')
                    {
                        score+=3;
                    }
                    else if(c == '<')
                    {
                        score+=4;
                    }
                }
                result2.Add(score);
            }
            result2.Sort();
            for(int i = 0; i < result2.Count; i++)
            {
                Console.WriteLine($"Result {i} contains {result2[i]}");
            }
            Console.WriteLine($"\n\n\nAnswer to part 2 is {result2[result2.Count/2]}");
        }
    }
}
