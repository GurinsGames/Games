using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bumer.ActiveObjects
{
  public class UserObject : IActiveObject
  {
    const int maxAccelerate = 100;
    const int keyPressTimeout = 50;
    const double minStep = 2;
    const double maxStep = 12;

    double canvasWidth;
    double canvasHeight;
    double positionX;
    double positionY;
    double step;
    int direction;
    int keyTime;
    int accelerate;
    bool reverse;
    bool hit;

    BitmapImage bmp;
    BitmapImage bmphit;
    Image image;
    Label debugLabel;

    private void ChangeStep(int time)
    {
      if (keyTime == 0)
        return;

      int pressTime = time - keyTime;
      bool pressed = pressTime < keyPressTimeout;
      if (!pressed)
      {
        step = 0;
        return;
      }

      if (!reverse)
      {
        if (pressed)
          ++accelerate;
        else
          --accelerate;
        if (accelerate < 0)
          accelerate = 0;
        else if (accelerate >= maxAccelerate)
          accelerate = maxAccelerate;
      }

      reverse = false;
      step = minStep + accelerate * maxStep / maxAccelerate;
    }

    public UserObject()
    {
      canvasWidth = canvasHeight = 0;
      positionX = positionY = 0;
      step = 0;
      direction = 0;
      keyTime = 0;
      accelerate = 0;
      reverse = false;
      hit = false;

      bmp = new BitmapImage(new Uri("pack://application:,,,/Bumer;component/Images/bumer.png"));
      bmphit = new BitmapImage(new Uri("pack://application:,,,/Bumer;component/Images/bumerhit.png"));
      image = new Image();
      image.Height = bmp.Height;
      image.Width = bmp.Width;
      image.Source = bmp;
      image.Stretch = Stretch.None;

      debugLabel = new Label();
      debugLabel.Foreground = Brushes.Yellow;
      debugLabel.Background = Brushes.Transparent;
    }

    public void Initialize(Canvas canvas)
    {
      canvasWidth = canvas.ActualWidth;
      canvasHeight = canvas.ActualHeight;
      positionX = canvasWidth / 2 - bmp.Width;
      positionY = canvasHeight - bmp.Height;

      canvas.Children.Add(image);
      Canvas.SetTop(image, positionY);
      Canvas.SetLeft(image, positionX);

      canvas.Children.Add(debugLabel);
      Canvas.SetTop(debugLabel, 10);
      Canvas.SetLeft(debugLabel, 10);
    }

    public bool Execute(int time, Canvas canvas)
    {
      int prevX = (int)positionX;

      ChangeStep(time);
      positionX = positionX + step * direction;
      if (positionX < bmp.Width / 2)
        positionX = bmp.Width / 2;
      if (positionX > canvasWidth - bmp.Width / 2)
        positionX = canvasWidth - bmp.Width / 2;
      if (prevX != (int)positionX)
        Canvas.SetLeft(image, positionX);

      debugLabel.Content = accelerate.ToString();
      return true;
    }

    public void MoveLeft(int time)
    {
      if (direction > 0)
      {
        reverse = true;
        accelerate = 0;
      }
      direction = -1;
      keyTime = time;
    }

    public void MoveRight(int time)
    {
      if (direction < 0)
      {
        reverse = true;
        accelerate = 0;
      }
      direction = 1;
      keyTime = time;
    }

    public void Hit()
    {
      if (hit)
        return;
      image.Source = bmphit;
      hit = true;
    }

    public void NoHit()
    {
      if (!hit)
        return;
      image.Source = bmp;
      hit = false;
    }

    public System.Drawing.Rectangle Rect
    {
      get { return new System.Drawing.Rectangle((int)positionX, (int)positionY, (int)bmp.Width, (int)bmp.Height); }
    }
  }
}
