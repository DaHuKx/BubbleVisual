using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BubbleVisual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rectangle> rectangle;
        DispatcherTimer timer = new DispatcherTimer();
        int time;
        int step;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InstallBorders(object sender, RoutedEventArgs e)
        {
            BordPanel.Children.Clear();
            rectangle = new List<Rectangle>();
            double size = 0;

            while (Griddy.RenderSize.Width > size)
            {
                var bord = GetNewRandBorder();
                BordPanel.Children.Add(bord);
                rectangle.Add(bord);
                size += 14;
            }

            InstallButton.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Visible;
        }

        private Rectangle GetNewRandBorder()
        {
            Random random = new Random();
            Rectangle border = new Rectangle();
            border.Width = 10;
            border.Fill = Brushes.White;
            border.Height = (random.Next() % 50 + 1) * 10 + 1;
            border.Margin = new Thickness(2, 0, 2, 0);
            border.VerticalAlignment = VerticalAlignment.Bottom;

            return border;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timerStart();
        }

        private void timerStart()
        {
            StartButton.Visibility = Visibility.Hidden;

            time = 0;
            step = 0;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(1000);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (step >= rectangle.Count - 1 - time)
            {
                time++;
                step = 0;
            }

            if (time >= rectangle.Count - 1)
            {
                foreach (var br in rectangle)
                {
                    br.Fill = Brushes.Green;
                }

                timer.Stop();

                InstallButton.Visibility = Visibility.Visible;

                return;
            }

            Step();
            
        }


        private void Step()
        {
            foreach (var rect in rectangle.FindAll(x => x.Fill == Brushes.Red))
            {
                rect.Fill = Brushes.White;
            }

            rectangle[step].Fill = Brushes.Red;
            if (rectangle[step].Height > rectangle[step + 1].Height)
            {
                var temp = rectangle[step].Height;
                rectangle[step].Height = rectangle[step + 1].Height;
                rectangle[step + 1].Height = temp;
                BordPanel.UpdateLayout();
            }
            step++;
        }
    }
}
