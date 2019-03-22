//Group 3
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;

namespace CommaSprinkler
{
    //The ProcessFileCommand definition implimenting ICommand
    public class ProcessFileCommand : ICommand
    {
        //instance of vm becuase we are going to communicate with the view model
        private readonly VM vm;
        //file content field so we can read in the content of the file when they click show button
        private string fileContent;
        //list of strings for things preceeding commas and things following commas.
        private readonly List<string> precedes = new List<string>();
        private readonly List<string> follows = new List<string>();
        //commaDistributor and outputBuilder fields invoking their constructors
        private CommaDistributor commaDistributor = new CommaDistributor();
        private OutputBuilder outputBuilder = new OutputBuilder();
        //event handler delegate required for our ProcessFileCommand. This tells us the internal state has changed now we need to check whether we can execute or not.
        public event EventHandler CanExecuteChanged;
        //CanExecute required for our ProcessFileCommand. It is a boolean that tells us if we can execute.
        public bool CanExecute(object parameter)
        {
            //if the filename is not selected this returns false and the command can't execute.
            return !string.IsNullOrEmpty(vm.FileName);
        }
        //Constructor to process the file command
        public ProcessFileCommand(VM vm)
        {
            this.vm = vm;
            //we are telling this class to listen if the property changes on our view model because when it does we want to execute some code. 
            vm.PropertyChanged += RaiseCanExecuteChanged;
        }
        //Method signaling that somthing has happened and that it needs to check the CanExecute logic above to see if it should execute.
        private void RaiseCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            //invoke is the method we use to raise the event. "This" perameter is reffering to this particular instance of the ProcessFileCommand
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        //execute method with no parameters passed in
        public void Execute(object parameter)
        {
            //variable called fileContent contains the content read from the file
            fileContent = File.ReadAllText(vm.FileName);
            //variable called result that just has our file content so far
            string result = fileContent;
            //do executes the code inside the do at least once, then checks the while condition to see whether to continue looping.
            do
            {
                //assign the value of SprinkleCommas(passing in the reuslt) to the variable called temp 
                var temp = SprinkleCommas(result);
                //check if the temp variable is equal to our result variable. 
                if (temp == result)
                {
                    //If it is we break out of the loop.
                    break;
                }
                //if its false then assign our temp variable to result content. Now temp is not just our raw file content, it has some commas in it.
                result = temp;
              //loop forever until you hit the break statement
            } while (true);
            //when no more commas could be added the break brings us down here when we assign this result to the output for the view model.
            vm.Output = result;
        }
        private string SprinkleCommas(string source)
        {
            //clearing our two comma collections(precedes list and follows list)
            precedes.Clear();
            follows.Clear();
            //passing the empty lists to the comma distributor
            commaDistributor.SprinkleCommas(source, precedes, follows);
            //the commaDistributor modifies the precedes and follows list. We take the results of that and pass it into our output builder. 
            var result = outputBuilder.AssembleOutput(source, precedes, follows);
            //this result ends up in the temp above
            return result;
        }
    }
}
