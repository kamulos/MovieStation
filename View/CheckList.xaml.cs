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
using System.Collections;
using System.Collections.ObjectModel;

namespace MovieStation
{
    /// <summary>
    /// Interaction logic for CheckList.xaml
    /// </summary>
    public partial class CheckList : UserControl
    {
        public CheckList()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
             DependencyProperty.Register("ItemsSource", typeof(IEnumerable),
             typeof(CheckList));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty CheckedObjectsProperty =
     DependencyProperty.Register("CheckedObjects", typeof(ObservableCollection<string>),
     typeof(CheckList));

        public ObservableCollection<string> CheckedObjects
        {
            get { return (ObservableCollection<string>)GetValue(CheckedObjectsProperty); }
            set { SetValue(CheckedObjectsProperty, value); }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Object src = (sender as CheckBox).Tag;
            CheckedObjects.Add(src.ToString());
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Object src = (sender as CheckBox).Tag;
            CheckedObjects.Remove(src.ToString());
        }
    }

 



}
