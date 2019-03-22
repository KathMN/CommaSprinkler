//Group 3
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommaSprinkler
{
    public class CommaDistributor
    {
        //SprinkleCommas method takes in the input string that was the result from the ProcessFileCommand. Each loop it has more commas.
        public void SprinkleCommas(string input, List<string> precedes, List<string> follows)
        {
            //get all the sentences from the input text for this sentances array
            string[] sentences = input.Split(new[] { '.' },StringSplitOptions.RemoveEmptyEntries);
            //if there were no periods there is nothing to process so we return immediately. No values are returned because it is a void method.
            if (sentences.Length < 1) return;
            // looping over each sentance in the sentance array
            foreach (string sentence in sentences)
            {
                if (sentence.Contains(','))
                {
                    //splitting sentance into phrases when there is a comma. 
                    string[] phrases = sentence.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < phrases.Length; i++)
                    {
                        //Looping over each phrase splitting each one into words based on the spaces between them.
                        string phrase = phrases[i];
                        if (i < phrases.Length - 1)
                        {
                            string[] words = phrase.Split(' ');
                           //if the last word in the phrase is not in the follows list, then we add it to the follows collection
                            if (!follows.Any(p=>p==words.Last()))
                                follows.Add(words.Last());
                        }
                        //if it is the second or subsequent time through the loop we need to check for preceeding commas.
                        if (i > 0)
                        {
                            //if it is the first word in the phrase is not null, and it is not in the precedes list, we add it to the precedes collection.
                            string[] precedingWith = phrase.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (precedingWith.First()!= null && !precedes.Any(p=>p==precedingWith.First()))
                            {
                                precedes.Add(precedingWith.First());
                            }
                        }
                    }
                }
            }
        }
    }
}

