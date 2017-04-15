using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichaelUseTxtFile
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //Get four letters from User
            
            FilterLetter firstLetter = new FilterLetter();
            FilterLetter secondLetter = new FilterLetter();
            FilterLetter thirdLetter = new FilterLetter();
            FilterLetter fourthLetter = new FilterLetter();

            Console.WriteLine("\nAbout to search dictionary for:"+ char.ToUpper(firstLetter.Input) + char.ToUpper(secondLetter.Input) + char.ToUpper(thirdLetter.Input) + char.ToUpper(fourthLetter.Input) + "\n");
            Console.WriteLine("Press any key to begin\n");
            Console.ReadKey();

            //Load File
            string[] dictionary = File.ReadAllLines(@"E:/glich_000/Documents/NEWDictionary.txt");
            //Starting StreamWriter to write a txt document as we go down the list
            using (StreamWriter list = new StreamWriter(@"E:/glich_000/Documents/dictionaryFilterList" + char.ToUpper(firstLetter.Input) + char.ToUpper(secondLetter.Input) + char.ToUpper(thirdLetter.Input) + char.ToUpper(fourthLetter.Input) + ".txt"))
            {
                //Tests each word in the dictionary list
                foreach (string word in dictionary)
                {
                    bool [] currentWord = new bool[word.Length]; //This ensures that two teammates will not take up the same letter. Suck it, Team SSSN!
                    for (int x = 0; x < word.Length; x++)
                        currentWord[x] = true;
                    if(WordTester(firstLetter, secondLetter, thirdLetter, fourthLetter, word, list, currentWord))
                    {//Writes the word into the results .txt file
                        for(int x = 0; x < 20; x++)
                        {
                            if (x == firstLetter.position)
                            {
                                list.Write(char.ToUpper(firstLetter.Input));
                                continue;
                            }
                            if (x == secondLetter.position)
                            {
                                list.Write(char.ToUpper(secondLetter.Input));
                                continue;
                            }
                            if (x == thirdLetter.position)
                            {
                                list.Write(char.ToUpper(thirdLetter.Input));
                                continue;
                            }
                            if (x == fourthLetter.position)
                            {
                                list.Write(char.ToUpper(fourthLetter.Input));
                                continue;
                            }
                        }
                        list.WriteLine(" - " + word);
                    }
                    firstLetter.position = -1;
                    secondLetter.position = -1;
                    thirdLetter.position = -1;
                    fourthLetter.position = -1;
                    firstLetter.Active = true;
                    secondLetter.Active = true;
                    thirdLetter.Active = true;
                    fourthLetter.Active = true;
                    firstLetter.tempLetterGroup = ".";
                    secondLetter.tempLetterGroup = ".";
                    thirdLetter.tempLetterGroup = ".";
                    fourthLetter.tempLetterGroup = ".";
                    
                }
            }
            Console.WriteLine("List complete. Press any key to close program.");
            Console.ReadKey();
        }

        private static bool WordTester(FilterLetter firstLetter, FilterLetter secondLetter, FilterLetter thirdLetter, FilterLetter fourthLetter, string word, StreamWriter list, bool[] currentWord)
        {
            int wordPosition = 0;
            foreach (char letter in word) //first pass-through, using all the primary letters, we'll do letterGroups later
            {
                if (firstLetter.Input == letter && firstLetter.Active && currentWord[wordPosition])
                {
                    firstLetter.position = wordPosition;
                    firstLetter.Active = false;
                    currentWord[wordPosition] = false;
                    wordPosition++;
                    continue;
                }
                else if (secondLetter.Input == letter && secondLetter.Active && currentWord[wordPosition])
                {
                    secondLetter.position = wordPosition;
                    secondLetter.Active = false;
                    currentWord[wordPosition] = false;
                    wordPosition++;
                    continue;
                }
                else if (thirdLetter.Input == letter && thirdLetter.Active && currentWord[wordPosition])
                {
                    thirdLetter.position = wordPosition;
                    thirdLetter.Active = false;
                    currentWord[wordPosition] = false;
                    wordPosition++;
                    continue;
                }
                else if (fourthLetter.Input == letter && fourthLetter.Active && currentWord[wordPosition])
                {
                    fourthLetter.position = wordPosition;
                    fourthLetter.Active = false;
                    currentWord[wordPosition] = false;
                    wordPosition++;
                    continue;
                }
                else
                    wordPosition++;
            }
            if (!(firstLetter.Active) && !(secondLetter.Active) && !(thirdLetter.Active) && !(fourthLetter.Active))
                return true; //All letters appear in the word as they are
            int wordLocation = 0;
            foreach (char letter in word) //second run through
            {
                if (currentWord[wordLocation])
                {
                    if (firstLetter.Active) //If the letter hasn't been used yet
                    {
                        foreach (char Groupingletter in firstLetter.LetterGroup) //check its LetterGroup, one character at a time
                        {
                            if (Groupingletter == letter) //If there's a match
                            {
                                firstLetter.position = wordLocation;
                                firstLetter.tempLetterGroup = char.ToString(Groupingletter); //Change letterGroup from "." to the match, so that a later if statement uses this instead of the main letter.
                                firstLetter.Active = false; //Deactivate this letter
                                break; //break out of the foreach loop of this letter's letterGroup
                            }
                        }
                    }
                    if (secondLetter.Active) //Repeats for each letter.
                    {
                        foreach (char Groupingletter in secondLetter.LetterGroup)
                        {
                            if (Groupingletter == letter)
                            {
                                secondLetter.position = wordLocation;
                                secondLetter.tempLetterGroup = char.ToString(Groupingletter);
                                secondLetter.Active = false;
                                break;
                            }
                        }
                    }
                    if (thirdLetter.Active)
                    {
                        foreach (char Groupingletter in thirdLetter.LetterGroup)
                        {
                            if (Groupingletter == letter)
                            {
                                thirdLetter.position = wordLocation;
                                thirdLetter.tempLetterGroup = char.ToString(Groupingletter);
                                thirdLetter.Active = true;
                                break;
                            }
                        }
                    }
                    if (fourthLetter.Active)
                    {
                        foreach (char Groupingletter in fourthLetter.LetterGroup)
                        {
                            if (Groupingletter == letter)
                            {
                                fourthLetter.position = wordLocation;
                                fourthLetter.tempLetterGroup = char.ToString(Groupingletter);
                                fourthLetter.Active = true;
                                break;
                            }
                        }
                    }
                }
                
                wordLocation++;
            }
            if (!(firstLetter.Active) && !(secondLetter.Active) && !(thirdLetter.Active) && !(fourthLetter.Active))
                return true; //All letters appear in the word as they are
            else
                return false;

        }
    }
}

public class FilterLetter
{
    public char Input;
    public string LetterGroup = "";
    public string tempLetterGroup = ".";
    public int position;
    public bool Active = true;
    string[] OutputLetterGrouping = new string[26] { "ae", "b", "ckqsz", "d", "ey", "f", "gj", "h", "ei", "gj", "ckq", "l", "m", "n", "ou", "p", "ckq", "r", "csz", "t", "uvw", "uvw", "uvw", "ckqs", "ey", "sz" };
    public FilterLetter()
    {
        Console.WriteLine("Enter a Letter\n");
        Input = Console.ReadKey().KeyChar;
        Console.WriteLine();
        Input = char.ToLower(Input);
        LetterGroup = OutputLetterGrouping[Input - 97];
    }
}