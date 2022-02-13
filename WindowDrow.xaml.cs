using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Course
{
    /// <summary>
    /// Interaction logic for WindowDrow.xaml
    /// </summary>
    public partial class WindowDrow : Window
    {
        public WindowDrow()
        {
            InitializeComponent();
        }

        public void DrowEllipse(int width, int height, int sizeEllips, Boolean colour)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = sizeEllips;
            ellipse.Height = sizeEllips;
            if (colour) ellipse.Fill = new SolidColorBrush(Colors.Black);
            else ellipse.Fill = new SolidColorBrush(Colors.Gray);
            ellipse.Margin = new Thickness(width, height, 0, 0);
            MyCanvas.Children.Add(ellipse);
        }

        public void DrowSwitchColourseEllipse(int width, int height, int sizeEllips, int colour)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = sizeEllips;
            ellipse.Height = sizeEllips;
            switch (colour)
            {
                case 1:
                    ellipse.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                case 2:
                    ellipse.Fill = new SolidColorBrush(Colors.Red);
                    break;
                case 3:
                    ellipse.Fill = new SolidColorBrush(Colors.Black);
                    break;
                case 4:
                    ellipse.Fill = new SolidColorBrush(Colors.Green);
                    break;
            }
            ellipse.Margin = new Thickness(width, height, 0, 0);
            MyCanvas.Children.Add(ellipse);
        }
    }
}
