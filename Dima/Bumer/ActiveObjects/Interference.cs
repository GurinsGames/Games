using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bumer.ActiveObjects
{
  public class Interference : IActiveObject
  {
    double canvasWidth;
    double canvasHeight;
    double positionX;
    double positionY;
    double step;
    double pposX;

    BitmapImage bmp;
    Image image;

    public Interference(int variant, double step, double pposX)
    {
      canvasWidth = canvasHeight = 0;
      positionX = positionY = 0;
      this.step = step;
      this.pposX = pposX;

      bmp = new BitmapImage(new Uri("pack://application:,,,/Bumer;component/Images/Interference" + variant.ToString() + ".png"));
      image = new Image();
      image.Height = bmp.Height;
      image.Width = bmp.Width;
      image.Source = bmp;
      image.Stretch = Stretch.None;
    }

    public void Initialize(Canvas canvas)
    {
      canvasWidth = canvas.ActualWidth;
      canvasHeight = canvas.ActualHeight;
      positionX = pposX * (canvasWidth - bmp.Width) / 10000;
      positionY = -bmp.Height;

      canvas.Children.Add(image);
      Canvas.SetLeft(image, positionX);
      Canvas.SetTop(image, positionY);
    }

    public bool Execute(int time, Canvas canvas)
    {
      positionY += step;
      Canvas.SetTop(image, positionY);
      return positionY < canvasHeight;
    }

    public System.Drawing.Rectangle Rect
    {
      get { return new System.Drawing.Rectangle((int)positionX, (int)positionY, (int)bmp.Width, (int)bmp.Height); }
    }
  }
}
