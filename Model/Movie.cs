using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

public class LanguageField : INotifyPropertyChanged
{
    private bool _English;
    public bool English
    {
        get { return _English; }
        set
        {
            _English = value;
            OnPropertyChanged("English");
        }
    }

    private bool _German;
    public bool German
    {
        get { return _German; }
        set
        {
            _German = value;
            OnPropertyChanged("German");
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

public class Movie : INotifyPropertyChanged
{

    public Movie()
    {
        _Id = ""; // TODO: privates ansprechen?
        _Title = "";
        _Year = 0;
        _Rating = 0;
        _Genres = new string[0];
        _Directors = new string[0];
        _Cast = new string[0];
        _ReleaseDate = "";
        _Plot = "";
        _CompressedPoster = null;
        _Runtime = 0;
        _Path = "";
        _Hd = false;

        Languages = new LanguageField();
    }


    private string _Id;
    public string Id 
    {
        get { return _Id; }
        set
        {
            _Id = value;
            OnPropertyChanged("Id");
        }
    }

    private string _Title;
    public string Title
    {
        get { return _Title; }
        set
        {
            _Title = value;
            OnPropertyChanged("Title");
        }
    }

    private int _Year;
    public int Year
    {
        get { return _Year; }
        set
        {
            _Year = value;
            OnPropertyChanged("Year");
        }
    }

    private double _Rating;
    public double Rating
    {
        get { return _Rating; }
        set
        {
            _Rating = value;
            OnPropertyChanged("Rating");
        }
    }

    private string[] _Genres;
    public string[] Genres
    {
        get { return _Genres; }
        set
        {
            _Genres = value;
            OnPropertyChanged("Genres");
        }
    }

    private string[] _Directors;
    public string[] Directors
    {
        get { return _Directors; }
        set
        {
            _Directors = value;
            OnPropertyChanged("Directors");
        }
    }

    private string[] _Cast;
    public string[] Cast
    {
        get { return _Cast; }
        set
        {
            _Cast = value;
            OnPropertyChanged("Cast");
        }
    }

    private string _ReleaseDate;
    public string ReleaseDate
    {
        get { return _ReleaseDate; }
        set
        {
            _ReleaseDate = value;
            OnPropertyChanged("ReleaseDate");
        }
    }

    private string _Plot;
    public string Plot
    {
        get { return _Plot; }
        set
        {
            _Plot = value;
            OnPropertyChanged("Plot");
        }
    }

    private byte[] _CompressedPoster;
    public byte[] CompressedPoster
    {
        get { return _CompressedPoster; }
        set
        {
            _CompressedPoster = value;
            decoded = false;
            OnPropertyChanged("CompressedPoster");
        }
    }

    private int _Runtime;
    public int Runtime
    {
        get { return _Runtime; }
        set
        {
            _Runtime = value;
            OnPropertyChanged("Runtime");
        }
    }

    private string _Path;
    public string Path
    {
        get { return _Path; }
        set
        {
            _Path = value;
            OnPropertyChanged("Path");
        }
    }

    private LanguageField _Languages;
    public LanguageField Languages
    {
        get { return _Languages; }
        set
        {
            _Languages = value;
            value.PropertyChanged += new PropertyChangedEventHandler(delegate(Object o, PropertyChangedEventArgs a) // TODO: nochmal über Garbagecollection nachdenken
            {
                OnPropertyChanged("Languages");
            });
            OnPropertyChanged("Languages");
        }
    }

    private bool _Hd;
    public bool Hd
    {
        get { return _Hd; }
        set
        {
            _Hd = value;
            OnPropertyChanged("Hd");
        }
    }



    private bool decoded = false;
    private BitmapSource _Poster;
    public BitmapSource Poster // TODO: im Thread decodieren?
    {
        get
        {
            if (decoded)
            {
                return _Poster;
            }
            else
            {
                try
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad; //TODO: crap??
                    bmp.StreamSource = new MemoryStream(CompressedPoster);
                    bmp.EndInit();
                    bmp.Freeze();
                    _Poster = bmp;
                }
                catch (Exception) { _Poster = null; }

                decoded = true;

                return _Poster;
            }
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

    public override string ToString()
    {
        string s = "id:\t" + Id + "\n";
        s += "title:\t" + Title + "\n";
        s += "year:\t" + Year + "\n";
        s += "rating:\t" + Rating + "\n";
        s += "genres:\t" + Genres + "\n";
        s += "directors:\t" + Directors + "\n";
        s += "cast:\t" + Cast + "\n";
        s += "release:\t" + ReleaseDate + "\n";
        s += "plot:\t" + Plot + "\n";
        s += "runtime:\t" + Runtime + "\n";
        s += "path:\t" + Path + "\n";
        s += "languages:\t" + Languages + "\n";
        s += "hd:\t" + Hd + "\n";

        return s;
    }
}