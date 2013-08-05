using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;
using System.Windows.Threading;


namespace MovieStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mwvm;

        public MainWindow()
        {
            StarCache.CreateCache();
            Thread t = new Thread(CreateViewModel);
            t.Start(Dispatcher);
            InitializeComponent();
        }

        private void FileDropped(object sender, DragEventArgs e)
        {
            if (mwvm != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    mwvm.AddFiles(files.ToList(), this);
                }
            }
        }



        private void CreateViewModel(object d)
        {
            Dispatcher disp = d as Dispatcher;
            mwvm = new MainWindowViewModel();
            disp.BeginInvoke(new Action(() => DataContext = mwvm));
        }

    }
}
