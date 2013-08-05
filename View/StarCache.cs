using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading;


static class StarCache
{
    private static BitmapSource[] bmpcache = new BitmapSource[100];
    private static bool initialized = false;
    private static Thread t;

    public static void CreateCache()
    {
/*        t = new Thread(CreationThread);
        t.SetApartmentState(ApartmentState.STA);
        t.Start();*/
        CreationThread();
    }

    public static BitmapSource GetStars(double rating)
    {
        //        if (!initialized)
        //            t.Join();


        while (!initialized) 
            Thread.Sleep(0);

        double r = rating;

        if (rating < 0) { r = 0; }
        else if (rating > 9.9) { r = 9.9; }

        return bmpcache[(int)(r * 10)];
    }

    private static void CreationThread()
    {
        BitmapSource s = MovieStation.StarsRenderer.Render(0);
        for (int i = 0; i < 100; i++)
        {
            bmpcache[i] = s;
        }
        initialized = true;
        for (int i = 0; i < 100; i++)
        {
            bmpcache[i] = MovieStation.StarsRenderer.Render((double)i / 10.0);
        }
    }
}

