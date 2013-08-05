using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using System.Collections.Specialized;

class Genre : INotifyPropertyChanged
{
    private ObservableCollection<string> _check;
    public string Name { get; private set; }

    private bool _enabled;
    public bool enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            if (value == false)
            {
                _check.Remove(Name);
            }
            else
            {
                _check.Add(Name);
            }

            OnPropertyChanged("enabled");
        }
    }

    public Genre(string g, ObservableCollection<string> check)
    {
        Name = g;
        _check = check;
        _check.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged);
    }

    void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Reset)
        {
            _enabled = false;
            OnPropertyChanged("enabled");
        }
    }


    void OnPropertyChanged(string Property)
    {
        if (null != PropertyChanged)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(Property));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

}
