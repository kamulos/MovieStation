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
using System.Windows.Controls.Primitives;
using System.Globalization;

namespace MovieStation
{
    /// <summary>
    /// Interaction logic for Stars.xaml
    /// </summary>
    public partial class Stars : UserControl
    {
        public Stars()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RatingValueProperty = DependencyProperty.Register("RatingValue", typeof(double), typeof(Stars), new PropertyMetadata(0.0, new PropertyChangedCallback(RatingValueChanged)));

        public double RatingValue
        {
            get
            {
                return (double)GetValue(RatingValueProperty);
            }
            set
            {
                if (value < 0.0)
                {
                    SetValue(RatingValueProperty, 0.0);
                }
                else if (value > 9.9)
                {
                    SetValue(RatingValueProperty, 9.9);
                }
                else
                {
                    SetValue(RatingValueProperty, value);
                }
            }
        }


        private static void RatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Stars parent = sender as Stars;
            double ratingValue = (double)e.NewValue;
            UIElementCollection children = ((UniformGrid)((DockPanel)parent.Content).Children[1]).Children;
            ((Label)((DockPanel)parent.Content).Children[0]).Content = string.Format(new CultureInfo(1), "{0:0.0}", ratingValue);

            Path button = null;
            for (double i = 0; i < ratingValue; i++)
            {
                button = children[(int)i] as Path;
                button.Fill = new SolidColorBrush(Colors.Yellow);
            }


            button = children[(int)ratingValue] as Path;
            double part = ratingValue - ((double)((int)ratingValue));
            part = part * 10;
            part = (part / 5 - 1) * (part / 5 - 1) * (part / 5 - 1) * 5 + 5;

            DockPanel d = new DockPanel();
            d.Children.Add(new Rectangle { Fill = new SolidColorBrush(Colors.Yellow), Height = 1, Width = part });
            d.Children.Add(new Rectangle { Fill = new SolidColorBrush(Colors.Gray), Height = 1, Width = 10 - part  });

            button.Fill = new VisualBrush(d);

            for (double i = ratingValue + 1; i < children.Count; i++)
            {
                button = children[(int)i] as Path;
                button.Fill = new SolidColorBrush(Colors.Gray);
            }
        }

    }
}
