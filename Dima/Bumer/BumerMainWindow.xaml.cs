using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bumer
{
  public partial class MainWindow : Window
  {
    DispatcherTimer mainTimer;
    ActiveObjects.ActiveObjectDispatcher activeObjectDispatcher;
    ActiveObjects.UserObject userObject;
    int startTime;
    int currentTime;
    bool started;

    public MainWindow()
    {
      started = false;
      startTime = currentTime = 0;

      InitializeComponent();

      activeObjectDispatcher = new ActiveObjects.ActiveObjectDispatcher();
      userObject = new ActiveObjects.UserObject();
      activeObjectDispatcher.Add(userObject);

      mainTimer = new DispatcherTimer();
      mainTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
      mainTimer.Tick += MainTimer_Tick;
    }

    private void MainTimer_Tick(object sender, EventArgs e)
    {
      if (startTime == 0)
        startTime = Environment.TickCount;
      currentTime = Environment.TickCount - startTime;
      activeObjectDispatcher.Execute(currentTime, canvas);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Escape)
        Close();

      if (e.Key == Key.Left)
        userObject.MoveLeft(currentTime);
      else if (e.Key == Key.Right)
        userObject.MoveRight(currentTime);
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
      if (!started)
      {
        started = true;
        activeObjectDispatcher.Initialize(canvas);
        mainTimer.Start();
      }
    }
  }
}
