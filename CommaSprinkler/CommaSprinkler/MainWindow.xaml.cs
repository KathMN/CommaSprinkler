using CommaSprinkler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommaSprinkler
{
    public partial class MainWindow : Window
    {
        //initializing the vm field of type VM
        VM vm = new VM();
        //constructor to initialize and assign the view model instance to the DataContext
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
