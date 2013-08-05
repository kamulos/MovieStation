using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieStation;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Specialized;



class MovieDataConnection : INotifyPropertyChanged
{
    public MovieDatabase mdb = new MovieDatabase("movies.sqlite"); // TODO: private machen

    private void resetSearch()
    {
        mdb.searchString = null;
        OnPropertyChanged("FilterText");

        mdb.exactMatch = false;
        OnPropertyChanged("ExactMatch");

        mdb.minRating = 0;
        _filterMinRating = "0.0";
        OnPropertyChanged("FilterMinRating");

        CheckedGenres.Clear();
        OnPropertyChanged("CheckedGenres");

        mdb.languageBitmask = 0; // TODO: crap
        OnPropertyChanged("SelectedLanguage");

        mdb.hd = false;
        OnPropertyChanged("Hd");

        updateData();
    }

    private IEnumerable<Movie> _movies;
    public IEnumerable<Movie> Movies
    {
        get { return _movies; }
        private set
        {
            _movies = value;
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs("Movies"));
        }
    }

    private ICommand _reset;
    public ICommand Reset
    {
        get
        {
            if (_reset == null)
            {
                _reset = new RelayCommand(
                    p => resetSearch(),
                    p => true);
            }
            return _reset;
        }
    }


    public List<string[]> OrderBy { get; private set; }
    public List<Genre> Genres { get; private set; }
    public ObservableCollection<string> CheckedGenres { get; set; } // TODO: <Genre> ??

    public string FilterText
    {
        get { return mdb.searchString; }
        set
        {
            mdb.searchString = value;
            updateData();
        }
    }

    public bool ExactMatch
    {
        get { return mdb.exactMatch; }
        set
        {
            mdb.exactMatch = value;
            if (!string.IsNullOrEmpty(FilterText))
            {
                if (FilterText.Contains(' '))
                {
                    updateData();
                }
            }
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs("ExactMatch"));
        }
    }



    private string _filterMinRating = "0.0";
    public string FilterMinRating
    {
        get { return _filterMinRating; }
        set
        {
            _filterMinRating = value;
            try
            {
                mdb.minRating = double.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                mdb.minRating = 0;
            }
            updateData();
        }
    }

    public int _numberOfMovies;
    public int numberOfMovies
    {
        get { return _numberOfMovies; }
        set
        {
            _numberOfMovies = value;
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs("numberOfMovies"));
        }
    }

    public bool Hd
    {
        get { return mdb.hd; }
        set
        {
            mdb.hd = value;

            if (null != PropertyChanged) // TODO: yay / nay?
                PropertyChanged(this, new PropertyChangedEventArgs("Hd"));

            updateData();
        }
    }

    public bool _updatingData;
    public bool updatingData
    {
        get { return _updatingData; }
        set
        {
            _updatingData = value;
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs("updatingData"));
        }
    }


    private Dictionary<string, string> Filters = new Dictionary<string, string>();

    private int _selectedOrder = 0;
    public int SelectedOrder
    {
        get { return _selectedOrder; }
        set
        {
            _selectedOrder = value;
            mdb.orderBy = OrderBy[value][1];
            updateData();
        }
    }

    public int SelectedLanguage // TODO: Bullshit funktioniert nur mit 2 Sprachen
    {
        get { return mdb.languageBitmask; }
        set
        {
            mdb.languageBitmask = value;
            updateData();
        }
    }

    public MovieDataConnection()
    {
        CheckedGenres = new ObservableCollection<string>();
        CheckedGenres.CollectionChanged += new NotifyCollectionChangedEventHandler(CheckedGenres_CollectionChanged);




        OrderBy = new List<string[]>{new string[]{"Title", "title"},
                                     new string[]{"Rating", "rating desc"},
                                     new string[]{"Release Date", "year desc"},
                                     new string[]{"Runtime", "runtime"}
                                    };



        Genres = new List<Genre>
        {
            new Genre("Action", CheckedGenres),
            new Genre("Adventure", CheckedGenres),
            new Genre("Animation", CheckedGenres),
            new Genre("Biography", CheckedGenres),
            new Genre("Comedy", CheckedGenres),
            new Genre("Crime", CheckedGenres),
            new Genre("Documentary", CheckedGenres),
            new Genre("Drama", CheckedGenres),
            new Genre("Family", CheckedGenres),
            new Genre("Fantasy", CheckedGenres),
            new Genre("Film-Noir", CheckedGenres),
            new Genre("History", CheckedGenres),
            new Genre("Horror", CheckedGenres),
            new Genre("Independent", CheckedGenres),
            new Genre("Music", CheckedGenres),
            new Genre("Musical", CheckedGenres),
            new Genre("Mystery", CheckedGenres),
            new Genre("Romance", CheckedGenres),
            new Genre("Sci-Fi", CheckedGenres),
            new Genre("Short", CheckedGenres),
            new Genre("Sport", CheckedGenres),
            new Genre("Thriller", CheckedGenres),
            new Genre("TVmini-series", CheckedGenres),
            new Genre("War", CheckedGenres),
            new Genre("Western", CheckedGenres)
        };

        mdb.orderBy = OrderBy[SelectedOrder][1];
        mdb.filterGenres = CheckedGenres;

        updateData();
    }

    void CheckedGenres_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Reset)
        {
            updateData();
        }
    }


    private object sync = new object();
    private Thread t;
    public void updateData()
    {
        lock (sync)
        {
            if (updatingData == true)
            {
                t.Abort();
                t = new Thread(updateData1);
                t.Start();
            }
            else
            {
                updatingData = true;

                t = new Thread(updateData1);
                t.Start();
            }
        }
    }



    private void updateData1() // TODO: SQL Escape
    {
        IEnumerable<Movie> temp = mdb.query();
        numberOfMovies = temp.Count();

        lock (sync)
        {

            updatingData = false;
            Movies = temp;
        }


    }


    public void Add(Movie movie)
    {
        mdb.insert(movie);
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

