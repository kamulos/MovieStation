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
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace MovieStation
{
    /// <summary>
    /// Interaction logic for ChooseStars.xaml
    /// </summary>
    public partial class ChooseStars : UserControl
    {
        public ChooseStars()
        {
            InitializeComponent();
        }
        private void FillStars(double rating)
        {
            RatingLabel.Content = string.Format(new CultureInfo(1), "{0:0.0}", rating);


            UIElementCollection children = StarsGrid.Children;
            Path button = null;
            for (double i = 0; i < rating; i++)
            {
                button = children[(int)i] as Path;
                button.Fill = new SolidColorBrush(Colors.Yellow);
            }


            button = children[(int)rating] as Path;
            double part = rating - ((double)((int)rating));

//            part = (part / 0.5 - 1) * (part / 0.5 - 1) * (part / 0.5 - 1) * 0.5 + 0.5;

            button.Fill = new LinearGradientBrush(new GradientStopCollection { new GradientStop(Colors.Yellow, part), new GradientStop(Colors.Gray, part) }, 0);

            for (double i = rating + 1; i < 10; i++)
            {
                button = children[(int)i] as Path;
                button.Fill = new SolidColorBrush(Colors.Gray);
            }
        }

        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("Rating", typeof(double), typeof(ChooseStars), new PropertyMetadata(0.0, new PropertyChangedCallback(RatingChanged)));

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
            ChooseStars sr = sender as ChooseStars;

            sr.FillStars((double)e.NewValue);
        }

        private void StarsGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(sender as UniformGrid);

            double sn = (p.X * 10 / (sender as UniformGrid).ActualWidth);
            FillStars(sn > 9.9 ? 9.9 : sn);

        }

        private void StarsGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(sender as UniformGrid);

            double sn = (p.X * 10 / (sender as UniformGrid).ActualWidth);
            Rating = sn > 9.9 ? 9.9 : sn;
        }

        private void StarsGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            FillStars(Rating);
        }


    }
}
