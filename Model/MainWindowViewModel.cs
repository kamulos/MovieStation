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

        while (filesToAdd.Count > 0)
        {
            lock (filesToAdd)
            {
                CurrentPath = filesToAdd.Dequeue();
            }

            Movie m = FileRecognizer.recognize(Path.GetFileNameWithoutExtension(CurrentPath));
            if (m != null)
            {
                m.Path = CurrentPath;

                // TODO: redundanz (showMovieFinder)
                mdc.mdb.insert(m); // TODO: check for null? / executable
                mdc.updateData();
            }
            else
            {
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
        bool? dialogResult = mf.ShowDialog();

        if (dialogResult.HasValue && dialogResult.Value == true && ChosenMovie != null)            // TODO: zum addmovie command oder den addmoviecommand abschaffen??
        {
            string oldfile = CurrentPath;
            string dir = Path.GetDirectoryName(oldfile);
            string ext = Path.GetExtension(oldfile);

//            ChosenMovie.Path = dir + @"\" + FileRecognizer.memorize(ChosenMovie) + ext;
            ChosenMovie.Path = oldfile;

//            File.Move(oldfile, ChosenMovie.Path);

            mdc.mdb.insert(ChosenMovie); // TODO: check for null? / executable
            mf.Close();
            mdc.updateData();
        }
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

    public bool _searching;
    public bool Searching
    {
        get { return _searching; }
        set
        {
            _searching = value;
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs("Searching"));
        }
    }


    private ICommand _search;
    public ICommand Search
    {
        get
        {
            if (_search == null)
            {
                _search = new RelayCommand(
                    p => SearchMovie(),
                    p => true);
            }
            return _search;
        }
    }

    private Thread searchThread;


    private void SearchMovie()
    {
        Searching = true;

        if (searchThread != null)
        {
            if (searchThread.IsAlive)
            {
                searchThread.Abort();
            }
        }

        searchThread = new Thread(SearchMovieThread);
        searchThread.Start();
    }

    private void SearchMovieThread()
    {
        IEnumerable<Movie> results = IMDb.SearchMovie(TitleText, YearText);

        Searching = false;

        if (Application.Current != null)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => showSearchResults(results)));
        }
    }

    private void showSearchResults(IEnumerable<Movie> results)
    {
        MovieChoice = results;

        if (MovieChoice != null && MovieChoice.Count() != 0)
        {
            ChosenMovie = MovieChoice.First();
            if (MovieChoice.Count() > 1)
            {

                SearchResults sr = new SearchResults();
                sr.Owner = mf;
                sr.DataContext = mf.DataContext;
                bool? res = sr.ShowDialog();
                if (!res.HasValue || res == false)
                {
                    ChosenMovie = null;
                }
            }
        }
        else
        {
            ChosenMovie = null;
        }
    }

    private ICommand _searchById;
    public ICommand SearchById
    {
        get
        {
            if (_searchById == null)
            {
                _searchById = new RelayCommand(
                    p => SearchByID(),
                    p => true);
            }
            return _searchById;
        }
    }


    private void SearchByID()
    {
        Searching = true;

        if (searchThread != null)
        {
            if (searchThread.IsAlive) // TODO: vermtutlich gar nicht nötig
            {
                searchThread.Abort();
            }
        }

        searchThread = new Thread(SearchByIDThread);
        searchThread.Start();
    }

    private void SearchByIDThread()
    {
        Movie m = IMDb.GetMovieByID(IdText);

        Searching = false;

        if (Application.Current != null)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => ChosenMovie = m));
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

    private ICommand _goIMDb;
    public ICommand GoIMDb
    {
        get
        {
            if (_goIMDb == null)
            {
                _goIMDb = new RelayCommand(
                    p => openIMDbPage(),
                    p => true);
            }
            return _goIMDb;
        }
    }

    private void openIMDbPage()
    {
        Process.Start(@"http://www.imdb.com/title/" + SelectedMovie.Id + @"/");
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
            if (!File.Exists(SelectedMovie.Path))
            {
                MessageBox.Show("Could not find the file \"" + SelectedMovie.Path + "\"");
            }
            else
            {
                try
                {
                    Process.Start(SelectedMovie.Path);
                }
                catch (Win32Exception e)
                {
                    MessageBox.Show("Could not start movie :(");
                }
            }
        }
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

