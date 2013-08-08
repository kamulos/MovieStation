using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MovieStation;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;



class MainWindowViewModel : INotifyPropertyChanged
{

    public MovieDataConnection mdc { get; private set; }

    public MainWindowViewModel()
    {
        MovieChoice = new List<Movie>();
        mdc = new MovieDataConnection();

        Search = new SearchCommand(this);

        return;
    }

    private MovieFinder mf;

    public void AddFiles(IEnumerable<string> fileNames, Window mainWindow)
    {
        lock (filesToAdd)
        {
            foreach (string file in fileNames)
            {
                filesToAdd.Enqueue(file);

                if (faThread == null || !faThread.IsAlive)
                {
                    faThread = new Thread(AddFilesThread);
                    faThread.Start(mainWindow);
                }
            }
        }
    }

    Queue<string> filesToAdd = new Queue<string>();
    private Thread faThread;

    private void AddFilesThread(object d)
    {
        Window mainWindow = d as Window;

        while(filesToAdd.Count > 0)
        {
            lock (filesToAdd)
            {
                CurrentPath = filesToAdd.Dequeue();
            }

            ChosenMovie = null;

            IdText = null;
            TitleText = detectTitleFromFile(CurrentPath);
            YearText = null;

            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => showMovieFinder(mainWindow)));
            }
        }
    }

    string detectTitleFromFile(string file)
    {
        return Regex.Replace(Path.GetFileNameWithoutExtension(file), @"[^A-Za-z0-9]+", " ");
    }

    void showMovieFinder(Window mainWindow)
    {
        mf = new MovieFinder();
        mf.Owner = mainWindow;
        mf.DataContext = mainWindow.DataContext;
        mf.ShowDialog();
    }

    private string _currentPath;
    public string CurrentPath
    {
        get { return _currentPath; }
        set
        {
            _currentPath = value;
            OnPropertyChanged("CurrentPath");
        }
    }

    private Movie _selectedMovie;
    public Movie SelectedMovie
    {
        get { return _selectedMovie; }
        set
        {
            _selectedMovie = value;
            OnPropertyChanged("SelectedMovie");
        }
    }

    private string _idText;
    public string IdText
    {
        get { return _idText; }
        set
        {
            _idText = value;
            OnPropertyChanged("IdText");
        }
    }

    private string _titleText;
    public string TitleText
    {
        get { return _titleText; }
        set
        {
            _titleText = value;
            OnPropertyChanged("TitleText");
        }
    }

    private string _yearText;
    public string YearText
    {
        get { return _yearText; }
        set
        {
            _yearText = value;
            OnPropertyChanged("YearText");
        }
    }

    public class SearchCommand : ICommand
    {
        MainWindowViewModel _mwvm;

        public SearchCommand(MainWindowViewModel mwvm)
        {
            _mwvm = mwvm;
        }

        public bool CanExecute(object parameter) { return true; }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            _mwvm.MovieChoice = IMDb.SearchMovie(_mwvm.TitleText, _mwvm.YearText);

            if (_mwvm.MovieChoice != null && _mwvm.MovieChoice.Count() != 0)
            {
                if (_mwvm.MovieChoice.Count() == 1)
                {
                    _mwvm.ChosenMovie = _mwvm.MovieChoice.First();
                }
                else
                {
                    _mwvm.ChosenMovie = _mwvm.MovieChoice.First();

                    SearchResults sr = new SearchResults();
                    MovieFinder mf = (MovieFinder)parameter;
                    sr.Owner = mf;
                    sr.DataContext = mf.DataContext;
                    bool? res = sr.ShowDialog();
                    if (!res.HasValue || res == false)
                    {
                        _mwvm.ChosenMovie = null;
                    }
                }
            }
            else
            {
                _mwvm.ChosenMovie = null;
            }
        }
    }


    public SearchCommand Search { get; private set; }


    private ICommand _searchById;
    public ICommand SearchById
    {
        get
        {
            if (_searchById == null)
            {
                _searchById = new RelayCommand(
                    p => ChosenMovie = IMDb.GetMovieByID(IdText),
                    p => true);
            }
            return _searchById;
        }
    }

    private ICommand _addMovie;
    public ICommand AddMovie
    {
        get
        {
            if (_addMovie == null)
            {
                _addMovie = new RelayCommand(
                    p => this.insertMovie(),
                    p => true);
            }
            return _addMovie;
        }
    }

    private ICommand _deleteMovie;
    public ICommand DeleteMovie
    {
        get
        {
            if (_deleteMovie == null)
            {
                _deleteMovie = new RelayCommand(
                    p => deleteSelected(),
                    p => true);
            }
            return _deleteMovie;
        }
    }

    private void deleteSelected()
    {
        if (SelectedMovie != null)
        {
            if (MessageBox.Show("Do you want to delete the movie \"" + SelectedMovie.Title + "\" from your collection?", "Delete Movie?", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK) == MessageBoxResult.OK)
            {
                mdc.mdb.delete(SelectedMovie);
                mdc.updateData();
            }
        }
    }

    private ICommand _play;
    public ICommand Play
    {
        get
        {
            if (_play == null)
            {
                _play = new RelayCommand(
                    p => playSelected(),
                    p => true);
            }
            return _play;
        }
    }

    private void playSelected()
    {
        if (!string.IsNullOrEmpty(SelectedMovie.Path))
        {
            Process.Start(SelectedMovie.Path);
        }
    }

    private void insertMovie()
    {
        ChosenMovie.Path = CurrentPath;
        mdc.mdb.insert(ChosenMovie); // TODO: check for null? / executable
        mf.Close();
        mdc.updateData();
    }

    private IEnumerable<Movie> _movieChoice;
    public IEnumerable<Movie> MovieChoice
    {
        get { return _movieChoice; }
        private set
        {
            _movieChoice = value;
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs("MovieChoice"));
        }
    }

    private Movie _chosenMovie;
    public Movie ChosenMovie
    {
        get { return _chosenMovie; }
        set
        {
            _chosenMovie = value;
            OnPropertyChanged("ChosenMovie");
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

