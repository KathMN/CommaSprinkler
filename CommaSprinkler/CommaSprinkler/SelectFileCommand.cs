//Group 3
using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace CommaSprinkler
{
    //The SelectFileCommand definition implimenting ICommand
    public class SelectFileCommand : ICommand
    {
        //field to store the view model that we are working with
        private readonly VM vm;
        //Constructor for SelectFileCommand storing the vm instance that was passed in. Public so it can be instantiated from outside. We use 'this' because the perameter vm matches the field name. 'This' is helping us differentiate between the two.
        public SelectFileCommand(VM vm)
        {
            this.vm = vm;
        }
        //event handler delegate required for our SelectFileCommand. This tells us the internal state has changed now we need to check whether we can execute or not.
        public event EventHandler CanExecuteChanged;
        //CanExecute required for our SelectFileCommand. It is a boolean that tells us if we can execute.
        public bool CanExecute(object parameter)
        {
            return true;
        }
        //execute method with no parameters passed in
        public void Execute(object parameter)
        {
            //creating an openFileDialog 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //if the user selects a file
            if (openFileDialog.ShowDialog() == true)
            {
                //we take the name of the file selected, and assign it to the vm's FileName property, and we clear the previous output from the window if any.
                vm.FileName = openFileDialog.FileName;
                vm.Output = "";
            }
        }
    }
}
