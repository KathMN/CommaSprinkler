//Group 3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaSprinkler
{
    public class OutputBuilder
    {
        //AssembleOutput method containing perameters of the file content, a list of words with a comma before them and a list of words with a comma after. The input string is from process file command.
        public string AssembleOutput(string input, List<string> precedes, List<string> follows)
        {
            //using stringBuilder to piece together our output string
            StringBuilder stringBuilder = new StringBuilder();
            //declaring the words array and populating it split on spaces.
            string[] words = input.Split(new[] { ' ' },StringSplitOptions.RemoveEmptyEntries);
            //checking if each word in our words list ends with a comma or a period. If it does add a word plus a space to the string of output we are building.
            foreach (string word in words)
            {
                if (word.EndsWith(",") || word.EndsWith("."))
                { stringBuilder.Append(word + " "); }
                else
                {
                    //if the word in our words list has no punctuation we loop over the elements in our follows list and check if our word matches any of them. If it does we add it to the string builder with a comma after it. If no match we just put the word with a space after it
                    bool handled = false;
                    foreach (string item in follows)
                    {
                        if (word.Contains(item))
                        {
                            stringBuilder.Append(word + ", ");
                            handled = true;
                        }
                    }
                    //if we looped all the way through the follows collection and haven't had a match we put the word in our output string with a space after it.
                    if (!handled)
                    {
                        stringBuilder.Append(word + " ");
                    }
                }
            }
            //initializing a variable for the prior word and giving it a value of null just so it has a value for now
            string priorWord = null;
            //ToString is building the string and we are splitting it based on spaces and assigning it to words. Now we have an array of all the words that the stringBuilder had in it.
            words = stringBuilder.ToString().Split(' ');
            //clearing the contents of the string builder because we will be adding stuff to it again.
            stringBuilder.Clear();
            //looping over the words in the words array
            foreach (string word in words)
            {
                bool exists = false;
                //for each item in the precedes list check if it matches the word we are looking at (minus the trailing commas or dots) then we set the flag to true
                foreach (string item in precedes)
                {
                    if (item == word.TrimEnd(',', '.'))
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    //if we have a prior word and that word does not contain punctuation we are going to append our prior word with a comma otherwise we will append the prior word with a space.
                    if (priorWord != null && !priorWord.Contains('.') && !priorWord.Contains(','))
                    {
                        stringBuilder.Append(priorWord + ", ");
                    }
                    else
                    {
                        stringBuilder.Append(priorWord + " ");
                    }
                }
                //if exists is still false we did not have a match in the precedes list, we append the prior word with a space.
                else
                {
                    if (priorWord != null)
                    {
                        stringBuilder.Append(priorWord + " ");
                    }
                }
                //assigning the current word to the prior word so we have a reference to it the next time we loop through.
                priorWord = word;
            }
            //at the end of all of the input we still have a word in 'priorWord' that has not been put in the string builder. We add it to the string builder here.
            stringBuilder.Append(priorWord);
            //stringBuilder gluing the words together into a string and giving this string back to whoever called it(ProcessFileCommand)
            return stringBuilder.ToString();
            //ProcessFileCommand temporarily stores it in "result". The do while loop uses this result and when all the commas have been added it breaks out and gives the output back to our view model. This causes the view model to raise a property changed notification which the UI is listening for, and thats what updates our output textbox.
        }
    }
}

