using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MovieStation;
using System.Globalization;
using System.Xml;
using System.Windows.Media.Imaging;

public static class IMDb // TODO: Async!!
{


    public static List<Movie> SearchMovie(string title, string year)
    {
        string url = "http://www.omdbapi.com/?s=" + title + "&y=" + year + "&r=XML";
        WebClient webClient = new WebClient();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(webClient.DownloadString(url));

        if (xmlDoc["root"].Attributes["response"].Value != "True")
        {
            return null;
        }
        else
        {
            List<Movie> ret = new List<Movie>();

            XmlNode root = xmlDoc["root"];
            foreach (XmlNode xnode in root.ChildNodes)
            {
                if (xnode.Name == "Movie" && xnode.Attributes["Type"].Value == "movie")
                {
                    ret.Add(GetMovieByID(xnode.Attributes["imdbID"].Value));
                }
            }

            return ret;
        }
    }

    public static Movie GetMovieByID(string id) // TODO: unicode conversion hier!!!!!
    {
        string url = "http://www.omdbapi.com/?i=" + id + "&r=XML";
        WebClient webClient = new WebClient();
        webClient.Encoding = Encoding.UTF8;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(webClient.DownloadString(url));

        if (xmlDoc["root"].Attributes["response"].Value != "True")
        {
            return null;
        }
        else
        {

            XmlAttributeCollection movieinf = xmlDoc["root"]["movie"].Attributes;

            Movie data = new Movie();
            data.Id = movieinf["imdbID"].Value;
            data.Title = movieinf["title"].Value;
            data.Year = Convert.ToInt32(movieinf["year"].Value);
            try
            {
                data.Rating = Convert.ToDouble(movieinf["imdbRating"].Value, new CultureInfo("en-US"));
            }
            catch (Exception e)
            {
                data.Rating = 0;
            }
            data.Genres = movieinf["genre"].Value.Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            data.Directors = movieinf["director"].Value.Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            data.Cast = movieinf["actors"].Value.Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            data.Plot = movieinf["plot"].Value;
            data.ReleaseDate = movieinf["released"].Value;
            string rt = movieinf["runtime"].Value;
            Match m = Regex.Match(rt, "((\\d+) h)?\\s?((\\d+) min)?");

            int h = 0, min = 0;
            try
            {
                h = Convert.ToInt32(m.Groups[2].Value);
            }
            catch (FormatException) { }
            try
            {
                min = Convert.ToInt32(m.Groups[4].Value);
            }
            catch (FormatException) { }

            data.Runtime = 60 * h + min;

            string posterurl = movieinf["poster"].Value;
            try
            {
                if (posterurl == null || posterurl.Substring(0, 7) != "http://") // TODO: crap
                {
                    data.CompressedPoster = null;
                }
                else
                {
                    data.CompressedPoster = getUrlBinary(posterurl); // TODO: exceptionhandling
                }
            }
            catch (Exception e)
            {
                data.CompressedPoster = null;
            }

            return data;
        }
    }

    private static byte[] getUrlBinary(string url)
    {
        WebClient client = new WebClient();
        client.Headers["Accept-Language"] = "en";
        Stream datastream = client.OpenRead(url);
        MemoryStream bin = new MemoryStream();
        datastream.CopyTo(bin);
        return bin.ToArray();
    }

}