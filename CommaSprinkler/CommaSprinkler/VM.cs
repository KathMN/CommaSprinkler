//Group 3
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CommaSprinkler
{
    public class VM : INotifyPropertyChanged
    {
        //fields for the filename and the output (like variables but available anywhere in this class.)
        private string fileName;
        private string output;
        //VM constructor
        public VM()
        {
            //creating an instance of the commands which are bound to our buttons in the xaml
            SelectFileCommand = new SelectFileCommand(this);
            ProcessFileCommand = new ProcessFileCommand(this);
        }
        //auto properties of type ICommand (set to private so no one can set the command from outside)
        public ICommand SelectFileCommand
        {
            get; private set;
        }
        public ICommand ProcessFileCommand
        {
            get; private set;
        }
        //Full Properties for the fileName and the output so we can provide notifications when things change.
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                NotifyChanged();
            }
        }
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                NotifyChanged(); }
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
