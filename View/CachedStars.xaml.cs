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

namespace MovieStation
{
    /// <summary>
    /// Interaction logic for CachedStars.xaml
    /// </summary>
    public partial class CachedStars : UserControl
    {
        public CachedStars()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("Rating", typeof(double), typeof(CachedStars), new PropertyMetadata(0.0, new PropertyChangedCallback(RatingChanged)));

        public double Rating
        {
            get
            {
                return (double)GetValue(RatingProperty);
            }
            set
            {
                if (value < 0.0)
                {
                    SetValue(RatingProperty, 0.0);
                }
                else if (value > 9.9)
                {
                    SetValue(RatingProperty, 9.9);
                }
                else
                {
                    SetValue(RatingProperty, value);
                }
            }
        }


        private static void RatingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UserControl uc = sender as UserControl;
            Image img = uc.Content as Image;

            img.Source = StarCache.GetStars((double)e.NewValue);
        }

    }
}
