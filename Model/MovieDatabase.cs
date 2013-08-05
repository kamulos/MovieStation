using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;
using System.Globalization;


/**
 * Steps to install System.Data.SQLite:
 * 
 * 1. Download here: http://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki
 * 2. Add Reference to System.Data.SQLite.dll
 * 3. Copy SQLite.Interop.dll to program's directory
 * 4. When using the x64 version disable "prefer 32 bit" in Build
 */

class MovieDatabase // TODO: implement IDisposable
{
    private SQLiteConnection m_dbConnection;

    public MovieDatabase(string filename)
    {
        m_dbConnection = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
        m_dbConnection.Open();

        string moviestable = "CREATE TABLE IF NOT EXISTS movies(id TEXT primary key, title TEXT, year INTEGER, rating REAL, lgenres TEXT, ldirectors TEXT, lcast TEXT, release TEXT, plot TEXT, poster BLOB, runtime INTEGER, path TEXT, audiolangs INTEGER, hd INTEGER);";
        SQLiteCommand command = new SQLiteCommand(moviestable, m_dbConnection);
        command.ExecuteNonQuery();
    }

    public string searchString { get; set; }
    public bool exactMatch { get; set; }
    public IEnumerable<string> filterGenres { get; set; }
    public double minRating { get; set; }
    public bool hd { get; set; }
    public int languageBitmask { get; set; }

    public string orderBy { get; set; }


    public void reset()
    {
        searchString = null;
        exactMatch = false;
        filterGenres = null;
        minRating = 0;
        hd = false;
        languageBitmask = 0;
        orderBy = null;
    }

    private string buildQueryString()
    {
        LinkedList<string> filters = new LinkedList<string>();

        if (!string.IsNullOrEmpty(searchString))
        {
            if (exactMatch)
            {
                filters.AddLast("(lower(title) like '%" + searchString.ToLower() + "%' or lower(ldirectors) like '%" + searchString.ToLower() + "%' or lower(lcast) like '%" + searchString.ToLower() + "%' or lower(plot) like '%" + searchString.ToLower() + "%')");
            }
            else
            {
                string[] words = searchString.Split(' ');

                LinkedList<string> request = new LinkedList<string>();
                foreach (string word in words)
                {
                    request.AddLast("(lower(title) like '%" + word.ToLower() + "%' or lower(ldirectors) like '%" + word.ToLower() + "%' or lower(lcast) like '%" + word.ToLower() + "%' or lower(plot) like '%" + word.ToLower() + "%')");
                }
                filters.AddLast(string.Join(" and ", request.ToArray<string>()));
            }
        }

        if (filterGenres != null && filterGenres.Any())
        {
            LinkedList<string> request = new LinkedList<string>();
            foreach (string genre in filterGenres)
            {
                request.AddLast("lgenres  like '%" + genre + "%'");
            }

            filters.AddLast(string.Join(" and ", request.ToArray<string>()));
        }

        if (minRating > 0)
        {
            filters.AddLast("rating>=" + minRating.ToString(CultureInfo.InvariantCulture));
        }

        if (languageBitmask != 0)
        {
            filters.AddLast("audiolangs&" + languageBitmask);
        }

        if (hd)
        {
            filters.AddLast("hd<>0");
        }

        string ret = "";
        if (filters.Count() != 0)
        {
            ret = string.Join(" and ", filters.ToArray<string>());
        }

        return ret;
    }


    public IEnumerable<Movie> query() // TODO: BuildQueryString
    {
        SQLiteCommand command = new SQLiteCommand(m_dbConnection);

        string filters = buildQueryString();
        command.CommandText = "SELECT * FROM movies" + (string.IsNullOrEmpty(filters) ? "" : " WHERE " + filters) + (string.IsNullOrEmpty(orderBy) ? "" : " ORDER BY " + orderBy);
        SQLiteDataReader reader = command.ExecuteReader();

        LinkedList<Movie> temp = new LinkedList<Movie>();

        while (reader.Read())
        {
            Movie m = new Movie();
            m.Id = reader.GetString(0);
            m.Title = reader.GetString(1);
            m.Year = reader.GetInt32(2);
            m.Rating = reader.GetDouble(3);
            m.Genres = reader.GetString(4).Split('|');
            m.Directors = reader.GetString(5).Split('|');
            m.Cast = reader.GetString(6).Split('|');
            m.ReleaseDate = reader.GetString(7);
            m.Plot = reader.GetString(8);
            m.CompressedPoster = (byte[])reader.GetValue(9);
            m.Runtime = reader.GetInt32(10);
            m.Path = reader.GetString(11);

            int langs = reader.GetInt32(12);
            m.Languages.English = (langs & 1) != 0;
            m.Languages.German = (langs & 2) != 0;

            m.Hd = reader.GetBoolean(13);

            temp.AddLast(m);
        }
        reader.Close();

        return temp;
    }


    public void insert(Movie movie)
    {
        SQLiteCommand command = new SQLiteCommand(m_dbConnection);

        command.CommandText = "INSERT INTO movies(id, title, year, rating, lgenres, ldirectors, lcast, release, plot, poster, runtime, path, audiolangs, hd) VALUES(@id, @title, @year, @rating, @lgenres, @ldirectors, @lcast, @release, @plot, @poster, @runtime, @path, @audiolangs, @hd);";

        DbParameter id = command.CreateParameter();
        id.ParameterName = "@id";
        id.Value = movie.Id;
        command.Parameters.Add(id);
        DbParameter title = command.CreateParameter();
        title.ParameterName = "@title";
        title.Value = movie.Title;
        command.Parameters.Add(title);
        DbParameter year = command.CreateParameter();
        year.ParameterName = "@year";
        year.Value = movie.Year;
        command.Parameters.Add(year);
        DbParameter rating = command.CreateParameter();
        rating.ParameterName = "@rating";
        rating.Value = movie.Rating;
        command.Parameters.Add(rating);
        DbParameter lgenres = command.CreateParameter();
        lgenres.ParameterName = "@lgenres";
        lgenres.Value = string.Join("|", movie.Genres);
        command.Parameters.Add(lgenres);
        DbParameter ldirectors = command.CreateParameter();
        ldirectors.ParameterName = "@ldirectors";
        ldirectors.Value = string.Join("|", movie.Directors);
        command.Parameters.Add(ldirectors);
        DbParameter lcast = command.CreateParameter();
        lcast.ParameterName = "@lcast";
        lcast.Value = string.Join("|", movie.Cast);
        command.Parameters.Add(lcast);
        DbParameter release = command.CreateParameter();
        release.ParameterName = "@release";
        release.Value = movie.ReleaseDate;
        command.Parameters.Add(release);
        DbParameter plot = command.CreateParameter();
        plot.ParameterName = "@plot";
        plot.Value = movie.Plot;
        command.Parameters.Add(plot);
        DbParameter poster = command.CreateParameter();
        poster.ParameterName = "@poster";
        poster.Value = movie.CompressedPoster;
        command.Parameters.Add(poster);
        DbParameter runtime = command.CreateParameter();
        runtime.ParameterName = "@runtime";
        runtime.Value = movie.Runtime;
        command.Parameters.Add(runtime);
        DbParameter path = command.CreateParameter();
        path.ParameterName = "@path";
        path.Value = movie.Path;
        command.Parameters.Add(path);
        DbParameter audiolangs = command.CreateParameter();
        audiolangs.ParameterName = "@audiolangs";
        int l = 0; l += (movie.Languages.English ? 1 : 0); l += 2 * (movie.Languages.German ? 1 : 0);
        audiolangs.Value = l;
        command.Parameters.Add(audiolangs);
        DbParameter hd = command.CreateParameter();
        hd.ParameterName = "@hd";
        hd.Value = movie.Hd;
        command.Parameters.Add(hd);

        command.ExecuteNonQuery();
    }

    public void close()
    {
        m_dbConnection.Close();
    }
}

