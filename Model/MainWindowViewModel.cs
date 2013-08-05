using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MovieStation;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;



class MainWindowViewModel : INotifyPropertyChanged
{

    public MovieDataConnection mdc{get;private set;}

    public MainWindowViewModel()
    {
        MovieChoice = new List<Movie>();
        mdc = new MovieDataConnection();

        Search = new SearchCommand(this);

        return;
        mdc.Add(IMDb.GetMovieByID("tt0111161"));
        mdc.Add(IMDb.GetMovieByID("tt0068646"));
        mdc.Add(IMDb.GetMovieByID("tt0071562"));
        mdc.Add(IMDb.GetMovieByID("tt0060196"));
        mdc.Add(IMDb.GetMovieByID("tt0110912"));
        mdc.Add(IMDb.GetMovieByID("tt0108052"));
        mdc.Add(IMDb.GetMovieByID("tt0050083"));
        mdc.Add(IMDb.GetMovieByID("tt1375666"));
        mdc.Add(IMDb.GetMovieByID("tt0073486"));
        mdc.Add(IMDb.GetMovieByID("tt0468569"));
        mdc.Add(IMDb.GetMovieByID("tt0080684"));
        mdc.Add(IMDb.GetMovieByID("tt0167260"));
        mdc.Add(IMDb.GetMovieByID("tt0047478"));
        mdc.Add(IMDb.GetMovieByID("tt0137523"));
        mdc.Add(IMDb.GetMovieByID("tt0076759"));
        mdc.Add(IMDb.GetMovieByID("tt0099685"));
        mdc.Add(IMDb.GetMovieByID("tt0034583"));
        mdc.Add(IMDb.GetMovieByID("tt0317248"));
        mdc.Add(IMDb.GetMovieByID("tt0120737"));
        mdc.Add(IMDb.GetMovieByID("tt0064116"));
        mdc.Add(IMDb.GetMovieByID("tt0047396"));
        mdc.Add(IMDb.GetMovieByID("tt0082971"));
        mdc.Add(IMDb.GetMovieByID("tt0133093"));
        mdc.Add(IMDb.GetMovieByID("tt0054215"));
        mdc.Add(IMDb.GetMovieByID("tt0114814"));
        mdc.Add(IMDb.GetMovieByID("tt0102926"));
        mdc.Add(IMDb.GetMovieByID("tt0114369"));
        mdc.Add(IMDb.GetMovieByID("tt0038650"));
        mdc.Add(IMDb.GetMovieByID("tt0435761"));
        mdc.Add(IMDb.GetMovieByID("tt0209144"));
        mdc.Add(IMDb.GetMovieByID("tt0167261"));
        mdc.Add(IMDb.GetMovieByID("tt0043014"));
        mdc.Add(IMDb.GetMovieByID("tt0109830"));
        mdc.Add(IMDb.GetMovieByID("tt0110413"));
        mdc.Add(IMDb.GetMovieByID("tt0057012"));
        mdc.Add(IMDb.GetMovieByID("tt0078788"));
        mdc.Add(IMDb.GetMovieByID("tt0033467"));
        mdc.Add(IMDb.GetMovieByID("tt0053125"));
        mdc.Add(IMDb.GetMovieByID("tt0169547"));
        mdc.Add(IMDb.GetMovieByID("tt0120586"));
        mdc.Add(IMDb.GetMovieByID("tt0075314"));
        mdc.Add(IMDb.GetMovieByID("tt0103064"));
        mdc.Add(IMDb.GetMovieByID("tt0120815"));
        mdc.Add(IMDb.GetMovieByID("tt0052357"));
        mdc.Add(IMDb.GetMovieByID("tt0078748"));
        mdc.Add(IMDb.GetMovieByID("tt0211915"));
        mdc.Add(IMDb.GetMovieByID("tt0910970"));
        mdc.Add(IMDb.GetMovieByID("tt0245429"));
        mdc.Add(IMDb.GetMovieByID("tt0081505"));
        mdc.Add(IMDb.GetMovieByID("tt0050825"));
        mdc.Add(IMDb.GetMovieByID("tt0056172"));
        mdc.Add(IMDb.GetMovieByID("tt0036775"));
        mdc.Add(IMDb.GetMovieByID("tt0253474"));
        mdc.Add(IMDb.GetMovieByID("tt0066921"));
        mdc.Add(IMDb.GetMovieByID("tt0021749"));
        mdc.Add(IMDb.GetMovieByID("tt0405094"));
        mdc.Add(IMDb.GetMovieByID("tt0022100"));
        mdc.Add(IMDb.GetMovieByID("tt0407887"));
        mdc.Add(IMDb.GetMovieByID("tt0056592"));
        mdc.Add(IMDb.GetMovieByID("tt0947798"));
        mdc.Add(IMDb.GetMovieByID("tt0090605"));
        mdc.Add(IMDb.GetMovieByID("tt0338013"));
        mdc.Add(IMDb.GetMovieByID("tt0180093"));
        mdc.Add(IMDb.GetMovieByID("tt0082096"));
        mdc.Add(IMDb.GetMovieByID("tt0041959"));
        mdc.Add(IMDb.GetMovieByID("tt0105236"));
        mdc.Add(IMDb.GetMovieByID("tt0119488"));
        mdc.Add(IMDb.GetMovieByID("tt0071315"));
        mdc.Add(IMDb.GetMovieByID("tt0027977"));
        mdc.Add(IMDb.GetMovieByID("tt0118799"));
        mdc.Add(IMDb.GetMovieByID("tt0040897"));
        mdc.Add(IMDb.GetMovieByID("tt0088763"));
        mdc.Add(IMDb.GetMovieByID("tt0482571"));
        mdc.Add(IMDb.GetMovieByID("tt0071853"));
        mdc.Add(IMDb.GetMovieByID("tt0457430"));
        mdc.Add(IMDb.GetMovieByID("tt0081398"));
        mdc.Add(IMDb.GetMovieByID("tt0095765"));
        mdc.Add(IMDb.GetMovieByID("tt0045152"));
        mdc.Add(IMDb.GetMovieByID("tt0053291"));
        mdc.Add(IMDb.GetMovieByID("tt0050212"));
        mdc.Add(IMDb.GetMovieByID("tt0042876"));
        mdc.Add(IMDb.GetMovieByID("tt0087843"));
        mdc.Add(IMDb.GetMovieByID("tt0120689"));
        mdc.Add(IMDb.GetMovieByID("tt0086879"));
        mdc.Add(IMDb.GetMovieByID("tt0042192"));
        mdc.Add(IMDb.GetMovieByID("tt0093058"));
        mdc.Add(IMDb.GetMovieByID("tt0040522"));
        mdc.Add(IMDb.GetMovieByID("tt0032553"));
        mdc.Add(IMDb.GetMovieByID("tt0062622"));
        mdc.Add(IMDb.GetMovieByID("tt0361748"));
        mdc.Add(IMDb.GetMovieByID("tt0112573"));
        mdc.Add(IMDb.GetMovieByID("tt0053604"));
        mdc.Add(IMDb.GetMovieByID("tt0363163"));
        mdc.Add(IMDb.GetMovieByID("tt1049413"));
        mdc.Add(IMDb.GetMovieByID("tt1504320"));
        mdc.Add(IMDb.GetMovieByID("tt1205489"));
        mdc.Add(IMDb.GetMovieByID("tt0017136"));
        mdc.Add(IMDb.GetMovieByID("tt0172495"));
        mdc.Add(IMDb.GetMovieByID("tt0070735"));
        mdc.Add(IMDb.GetMovieByID("tt0105695"));
        mdc.Add(IMDb.GetMovieByID("tt0364569"));
        mdc.Add(IMDb.GetMovieByID("tt0080678"));
        mdc.Add(IMDb.GetMovieByID("tt0033870"));
        mdc.Add(IMDb.GetMovieByID("tt0031679"));
        mdc.Add(IMDb.GetMovieByID("tt0401792"));
        mdc.Add(IMDb.GetMovieByID("tt0047296"));
        mdc.Add(IMDb.GetMovieByID("tt0032976"));
        mdc.Add(IMDb.GetMovieByID("tt0086190"));
        mdc.Add(IMDb.GetMovieByID("tt0097576"));
        mdc.Add(IMDb.GetMovieByID("tt0095016"));
        mdc.Add(IMDb.GetMovieByID("tt0057115"));
        mdc.Add(IMDb.GetMovieByID("tt0119698"));
        mdc.Add(IMDb.GetMovieByID("tt0372784"));
        mdc.Add(IMDb.GetMovieByID("tt0050976"));
        mdc.Add(IMDb.GetMovieByID("tt0073195"));
        mdc.Add(IMDb.GetMovieByID("tt0395169"));
        mdc.Add(IMDb.GetMovieByID("tt0083658"));
        mdc.Add(IMDb.GetMovieByID("tt0116282"));
        mdc.Add(IMDb.GetMovieByID("tt0017925"));
        mdc.Add(IMDb.GetMovieByID("tt0477348"));
        mdc.Add(IMDb.GetMovieByID("tt0113277"));
        mdc.Add(IMDb.GetMovieByID("tt1010048"));
        mdc.Add(IMDb.GetMovieByID("tt0059578"));
        mdc.Add(IMDb.GetMovieByID("tt0032138"));
        mdc.Add(IMDb.GetMovieByID("tt0052311"));
        mdc.Add(IMDb.GetMovieByID("tt0055630"));
        mdc.Add(IMDb.GetMovieByID("tt0089881"));
        mdc.Add(IMDb.GetMovieByID("tt0051201"));
        mdc.Add(IMDb.GetMovieByID("tt0095327"));
        mdc.Add(IMDb.GetMovieByID("tt0050986"));
        mdc.Add(IMDb.GetMovieByID("tt0208092"));
        mdc.Add(IMDb.GetMovieByID("tt0167404"));
        mdc.Add(IMDb.GetMovieByID("tt0075686"));
        mdc.Add(IMDb.GetMovieByID("tt0077416"));
        mdc.Add(IMDb.GetMovieByID("tt0246578"));
        mdc.Add(IMDb.GetMovieByID("tt0118715"));
        mdc.Add(IMDb.GetMovieByID("tt0061512"));
        mdc.Add(IMDb.GetMovieByID("tt0044079"));
        mdc.Add(IMDb.GetMovieByID("tt1136608"));
        mdc.Add(IMDb.GetMovieByID("tt0025316"));
        mdc.Add(IMDb.GetMovieByID("tt0044706"));
        mdc.Add(IMDb.GetMovieByID("tt0110357"));
        mdc.Add(IMDb.GetMovieByID("tt0266697"));
        mdc.Add(IMDb.GetMovieByID("tt0091763"));
        mdc.Add(IMDb.GetMovieByID("tt0758758"));
        mdc.Add(IMDb.GetMovieByID("tt0114709"));
        mdc.Add(IMDb.GetMovieByID("tt0469494"));
        mdc.Add(IMDb.GetMovieByID("tt0038787"));
        mdc.Add(IMDb.GetMovieByID("tt0405159"));
        mdc.Add(IMDb.GetMovieByID("tt0499549"));
        mdc.Add(IMDb.GetMovieByID("tt0064115"));
        mdc.Add(IMDb.GetMovieByID("tt0018455"));
        mdc.Add(IMDb.GetMovieByID("tt0031381"));
        mdc.Add(IMDb.GetMovieByID("tt0117951"));
        mdc.Add(IMDb.GetMovieByID("tt0015864"));
        mdc.Add(IMDb.GetMovieByID("tt0086250"));
        mdc.Add(IMDb.GetMovieByID("tt0032551"));
        mdc.Add(IMDb.GetMovieByID("tt0052618"));
        mdc.Add(IMDb.GetMovieByID("tt1125849"));
        mdc.Add(IMDb.GetMovieByID("tt0107048"));
        mdc.Add(IMDb.GetMovieByID("tt0056218"));
        mdc.Add(IMDb.GetMovieByID("tt0038355"));
        mdc.Add(IMDb.GetMovieByID("tt0061722"));
        mdc.Add(IMDb.GetMovieByID("tt0245712"));
        mdc.Add(IMDb.GetMovieByID("tt0079470"));
        mdc.Add(IMDb.GetMovieByID("tt0266543"));
        mdc.Add(IMDb.GetMovieByID("tt0440963"));
        mdc.Add(IMDb.GetMovieByID("tt0088247"));
        mdc.Add(IMDb.GetMovieByID("tt0036868"));
        mdc.Add(IMDb.GetMovieByID("tt0012349"));
        mdc.Add(IMDb.GetMovieByID("tt0044741"));
        mdc.Add(IMDb.GetMovieByID("tt0120735"));
        mdc.Add(IMDb.GetMovieByID("tt1305806"));
        mdc.Add(IMDb.GetMovieByID("tt0046268"));
        mdc.Add(IMDb.GetMovieByID("tt0092005"));
        mdc.Add(IMDb.GetMovieByID("tt0084787"));
        mdc.Add(IMDb.GetMovieByID("tt0112641"));
        mdc.Add(IMDb.GetMovieByID("tt1285016"));
        mdc.Add(IMDb.GetMovieByID("tt0892769"));
        mdc.Add(IMDb.GetMovieByID("tt0114746"));
        mdc.Add(IMDb.GetMovieByID("tt0434409"));
        mdc.Add(IMDb.GetMovieByID("tt0072890"));
        mdc.Add(IMDb.GetMovieByID("tt0046911"));
        mdc.Add(IMDb.GetMovieByID("tt0382932"));
        mdc.Add(IMDb.GetMovieByID("tt0083987"));
        mdc.Add(IMDb.GetMovieByID("tt0096283"));
        mdc.Add(IMDb.GetMovieByID("tt0056801"));
        mdc.Add(IMDb.GetMovieByID("tt0796366"));
        mdc.Add(IMDb.GetMovieByID("tt0055031"));
        mdc.Add(IMDb.GetMovieByID("tt0093779"));
        mdc.Add(IMDb.GetMovieByID("tt0048424"));
        mdc.Add(IMDb.GetMovieByID("tt0054997"));
        mdc.Add(IMDb.GetMovieByID("tt0119217"));
        mdc.Add(IMDb.GetMovieByID("tt0047528"));
        mdc.Add(IMDb.GetMovieByID("tt0049406"));
        mdc.Add(IMDb.GetMovieByID("tt0053198"));
        mdc.Add(IMDb.GetMovieByID("tt0317705"));
        mdc.Add(IMDb.GetMovieByID("tt0074958"));
        mdc.Add(IMDb.GetMovieByID("tt0058946"));
        mdc.Add(IMDb.GetMovieByID("tt0401383"));
        mdc.Add(IMDb.GetMovieByID("tt0780536"));
        mdc.Add(IMDb.GetMovieByID("tt0065214"));
        mdc.Add(IMDb.GetMovieByID("tt0044081"));
        mdc.Add(IMDb.GetMovieByID("tt0046359"));
        mdc.Add(IMDb.GetMovieByID("tt0070047"));
        mdc.Add(IMDb.GetMovieByID("tt0060827"));
        mdc.Add(IMDb.GetMovieByID("tt0061184"));
        mdc.Add(IMDb.GetMovieByID("tt0046912"));
        mdc.Add(IMDb.GetMovieByID("tt0019254"));
        mdc.Add(IMDb.GetMovieByID("tt0041546"));
        mdc.Add(IMDb.GetMovieByID("tt0206634"));
        mdc.Add(IMDb.GetMovieByID("tt0020629"));
        mdc.Add(IMDb.GetMovieByID("tt0978762"));
        mdc.Add(IMDb.GetMovieByID("tt1139797"));
        mdc.Add(IMDb.GetMovieByID("tt0109707"));
        mdc.Add(IMDb.GetMovieByID("tt1403865"));
        mdc.Add(IMDb.GetMovieByID("tt0075148"));
        mdc.Add(IMDb.GetMovieByID("tt0083922"));
        mdc.Add(IMDb.GetMovieByID("tt0319061"));
        mdc.Add(IMDb.GetMovieByID("tt0175880"));
        mdc.Add(IMDb.GetMovieByID("tt0079522"));
        mdc.Add(IMDb.GetMovieByID("tt0327056"));
        mdc.Add(IMDb.GetMovieByID("tt0072684"));
        mdc.Add(IMDb.GetMovieByID("tt0063522"));
        mdc.Add(IMDb.GetMovieByID("tt0120382"));
        mdc.Add(IMDb.GetMovieByID("tt0154420"));
        mdc.Add(IMDb.GetMovieByID("tt0378194"));
        mdc.Add(IMDb.GetMovieByID("tt0066206"));
        mdc.Add(IMDb.GetMovieByID("tt0050783"));
        mdc.Add(IMDb.GetMovieByID("tt0046250"));
        mdc.Add(IMDb.GetMovieByID("tt0032599"));
        mdc.Add(IMDb.GetMovieByID("tt0964517"));
        mdc.Add(IMDb.GetMovieByID("tt0338564"));
        mdc.Add(IMDb.GetMovieByID("tt0347149"));
        mdc.Add(IMDb.GetMovieByID("tt0015324"));
        mdc.Add(IMDb.GetMovieByID("tt0498380"));
        mdc.Add(IMDb.GetMovieByID("tt0325980"));
        mdc.Add(IMDb.GetMovieByID("tt0023969"));
        mdc.Add(IMDb.GetMovieByID("tt0036613"));
        mdc.Add(IMDb.GetMovieByID("tt0032904"));
        mdc.Add(IMDb.GetMovieByID("tt0069281"));
        mdc.Add(IMDb.GetMovieByID("tt0042546"));
        mdc.Add(IMDb.GetMovieByID("tt0056217"));
        mdc.Add(IMDb.GetMovieByID("tt0118694"));
        mdc.Add(IMDb.GetMovieByID("tt0111495"));
        mdc.Add(IMDb.GetMovieByID("tt0040746"));
        mdc.Add(IMDb.GetMovieByID("tt1542344"));
        mdc.Add(IMDb.GetMovieByID("tt0375679"));
        mdc.Add(IMDb.GetMovieByID("tt0198781"));
        mdc.Add(IMDb.GetMovieByID("tt0087544"));

    }

    private MovieFinder mf;

    public void AddFiles(IEnumerable<string> fileNames, Window mainWindow)
    {
        foreach (string file in fileNames)
        {
            ChosenMovie = null;
            CurrentPath = file;

            mf = new MovieFinder();
            mf.Owner = mainWindow;
            mf.DataContext = mainWindow.DataContext;
            mf.ShowDialog();
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

