using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Labirint
{
  public partial class MainWindow : Window
  {
    public const int xcount = 17;
    public const int ycount = 14;
    public const int roomSize = 450;
    public const int doorSize = 200;
    public const int manSize = 400;

    private int manX;
    private int manY;
    private ImageSource manL;
    private ImageSource manR;

    private int[,] maze = new int[ycount, xcount]
    {
       { 2,1,1,0,1,1,1,1,1,1,1,0,0,1,3,0,0 },
       { 0,0,1,0,1,0,0,1,0,0,1,0,0,1,0,1,0 },
       { 1,1,1,0,1,0,1,1,1,0,1,1,1,1,0,1,0 },
       { 1,0,1,0,0,1,1,0,1,0,0,0,0,0,0,1,0 },
       { 1,1,0,1,1,0,1,0,1,1,1,0,1,0,0,1,0 },
       { 0,1,0,0,1,0,1,0,1,0,1,0,1,0,0,1,0 },
       { 1,1,0,0,1,0,0,0,1,0,0,0,1,0,0,1,0 },
       { 1,0,0,0,1,1,1,1,1,0,0,1,1,1,1,1,0 },
       { 1,1,1,1,0,0,1,0,1,1,1,0,0,0,0,1,0 },
       { 0,0,0,1,0,0,1,0,1,0,1,0,0,0,0,1,0 },
       { 1,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,0 },
       { 1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,0 },
       { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1 },
       { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0 }
    };
    /*
    private int[,] maze = new int[ycount, xcount]
    {
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
    };
    private int[,] maze = new int[ycount, xcount]
    {
       { 2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
       { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1 },
       { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1 },
       { 1,0,1,1,1,1,1,1,1,1,1,1,1,0,1,0,1 },
       { 1,0,1,0,0,0,0,0,0,0,0,0,3,0,1,0,1 },
       { 1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,1 },
       { 1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,1 },
       { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
       { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
    };
    */

    private Room[,] rooms = new Room[ycount, xcount];

    public MainWindow()
    {
      InitializeComponent();

      manL = new BitmapImage(new Uri("pack://application:,,,/Labirint;component/Images/SlowL.png"));
      manR = new BitmapImage(new Uri("pack://application:,,,/Labirint;component/Images/SlowR.png"));

      for (int y = 0; y < ycount; ++y)
      {
        for (int x = 0; x < xcount; ++x)
        {
          if (maze[y, x] >= 1)
            rooms[y, x] = new Room(maze[y, x]);
        }
      }

      for (int y = 0; y < ycount; ++y)
      {
        for (int x = 0; x < xcount; ++x)
        {
          if (rooms[y, x] != null)
          {
            if (x > 0 && rooms[y, x - 1] != null)
            {
              rooms[y, x].Left = rooms[y, x - 1];
              rooms[y, x - 1].Right = rooms[y, x];
            }
            if (x < (xcount - 1) && rooms[y, x + 1] != null)
            {
              rooms[y, x].Right = rooms[y, x + 1];
              rooms[y, x + 1].Left = rooms[y, x];
            }
            if (y > 0 && rooms[y - 1, x] != null)
            {
              rooms[y, x].Top = rooms[y - 1, x];
              rooms[y - 1, x].Bottom = rooms[y, x];
            }
            if (y < (ycount - 1) && rooms[y + 1, x] != null)
            {
              rooms[y, x].Bottom = rooms[y + 1, x];
              rooms[y + 1, x].Top = rooms[y, x];
            }
          }
        }
      }

      for (int y = 0; y < ycount; ++y)
      {
        for (int x = 0; x < xcount; ++x)
        {
          if (rooms[y, x] != null)
            rooms[y, x].Init(canvas, x, y);
        }
      }

      RedrawMan();
    }

    private void RedrawMan()
    {
      Canvas.SetLeft(man, manX * roomSize + roomSize / 2 - manSize / 2);
      Canvas.SetTop(man, manY * roomSize + roomSize / 2 - manSize / 2);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Left)
      {
        if (manX > 0 && rooms[manY, manX - 1] != null)
        {
          --manX;
          if (!ReferenceEquals(man.Source, manL))
            man.Source = manL;
          RedrawMan();
          if (manX * roomSize < scroller.HorizontalOffset)
            scroller.ScrollToHorizontalOffset(scroller.HorizontalOffset - roomSize);
        }
      }
      else if (e.Key == Key.Right)
      {
        if (manX < (xcount - 1) && rooms[manY, manX + 1] != null)
        {
          ++manX;
          if (!ReferenceEquals(man.Source, manR))
            man.Source = manR;
          RedrawMan();
          if ((manX + 1) * roomSize > (scroller.HorizontalOffset + ActualWidth))
            scroller.ScrollToHorizontalOffset(scroller.HorizontalOffset + roomSize);
        }
      }
      else if (e.Key == Key.Up)
      {
        if (manY > 0 && rooms[manY - 1, manX] != null)
        {
          --manY;
          RedrawMan();
          if (manY * roomSize < scroller.VerticalOffset)
            scroller.ScrollToVerticalOffset(scroller.VerticalOffset - roomSize);
        }
      }
      else if (e.Key == Key.Down)
      {
        if (manY < (ycount - 1) && rooms[manY + 1, manX] != null)
        {
          ++manY;
          RedrawMan();
          if ((manY + 1) * roomSize > (scroller.VerticalOffset + ActualHeight))
            scroller.ScrollToVerticalOffset(scroller.VerticalOffset + roomSize);
        }
      }
      else if (e.Key == Key.Space)
        Close();
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      canvas.Width = xcount * roomSize;
      canvas.Height = ycount * roomSize;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void mnuExit_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void mnuWindow_Click(object sender, RoutedEventArgs e)
    {
      WindowStyle = WindowStyle.SingleBorderWindow;
    }

    private void mnuFullScrean_Click(object sender, RoutedEventArgs e)
    {
      WindowStyle = WindowStyle.None;
    }

    private void mnuNoMusic_Click(object sender, RoutedEventArgs e)
    {
      meMusic.Stop();
      meMusic.Source = null;
    }

    private void mnuMusic1_Click(object sender, RoutedEventArgs e)
    {
      meMusic.Stop();
      meMusic.Source = new Uri("D:\\Gsv\\Dima\\Labirint\\Music1.mp3");
      meMusic.Play();
    }

    private void mnuMusic2_Click(object sender, RoutedEventArgs e)
    {
      meMusic.Stop();
      meMusic.Source = new Uri("D:\\Gsv\\Dima\\Labirint\\Music2.mp3");
      meMusic.Play();
    }

    private void mnuMusic3_Click(object sender, RoutedEventArgs e)
    {
      meMusic.Stop();
      meMusic.Source = new Uri("D:\\Gsv\\Dima\\Labirint\\Music3.mp3");
      meMusic.Play();
    }

    private void mnuMusic4_Click(object sender, RoutedEventArgs e)
    {
      meMusic.Stop();
      meMusic.Source = new Uri("D:\\Gsv\\Dima\\Labirint\\Music4.mp3");
      meMusic.Play();
    }

    private void mnuMusic5_Click(object sender, RoutedEventArgs e)
    {
      meMusic.Stop();
      meMusic.Source = new Uri("D:\\Gsv\\Dima\\Labirint\\Music5.mp3");
      meMusic.Play();
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      meMusic.Volume = e.NewValue;
    }
  }
}
