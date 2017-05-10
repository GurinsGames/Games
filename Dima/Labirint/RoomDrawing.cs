using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Labirint
{
  public class RoomDrawing
  {
    private Room room;
    private int type;
    private Path path;

    private static List<Point> points = new List<Point>
    {
      new Point(0, 0),
      new Point(0, (MainWindow.roomSize - MainWindow.doorSize) / 2),
      new Point(0, MainWindow.roomSize - (MainWindow.roomSize - MainWindow.doorSize) / 2),
      new Point(0, MainWindow.roomSize),
      new Point((MainWindow.roomSize - MainWindow.doorSize) / 2, MainWindow.roomSize),
      new Point(MainWindow.roomSize - (MainWindow.roomSize - MainWindow.doorSize) / 2, MainWindow.roomSize),
      new Point(MainWindow.roomSize, MainWindow.roomSize),
      new Point(MainWindow.roomSize, MainWindow.roomSize - (MainWindow.roomSize - MainWindow.doorSize) / 2),
      new Point(MainWindow.roomSize, (MainWindow.roomSize - MainWindow.doorSize) / 2),
      new Point(MainWindow.roomSize, 0),
      new Point(MainWindow.roomSize - (MainWindow.roomSize - MainWindow.doorSize) / 2, 0),
      new Point((MainWindow.roomSize - MainWindow.doorSize) / 2, 0)
    };

    private void AddGeometry(GeometryGroup group, int index1, int index2)
    {
      group.Children.Add(new LineGeometry(points[index1], points[index2]));
    }

    private void AddLeftWall(GeometryGroup group, bool door)
    {
      if (door)
      {
        //AddGeometry(group, 0, 1);
        //AddGeometry(group, 2, 3);
      }
      else
        AddGeometry(group, 0, 3);
    }

    private void AddBottomWall(GeometryGroup group, bool door)
    {
      if (door)
      {
        //AddGeometry(group, 3, 4);
        //AddGeometry(group, 5, 6);
      }
      else
        AddGeometry(group, 3, 6);
    }

    private void AddRightWall(GeometryGroup group, bool door)
    {
      if (door)
      {
        //AddGeometry(group, 6, 7);
        //AddGeometry(group, 8, 9);
      }
      else
        AddGeometry(group, 6, 9);
    }

    private void AddTopWall(GeometryGroup group, bool door)
    {
      if (door)
      {
        //AddGeometry(group, 9, 10);
        //AddGeometry(group, 11, 0);
      }
      else
        AddGeometry(group, 9, 0);
    }

    public RoomDrawing(Room room, int type)
    {
      this.room = room;
      this.type = type;
    }

    public void Init(Canvas canvas, int x, int y)
    {
      GeometryGroup group = new GeometryGroup();
      AddLeftWall(group, room.Left != null);
      AddBottomWall(group, room.Bottom != null);
      AddRightWall(group, room.Right != null);
      AddTopWall(group, room.Top != null);

      path = new Path();
      path.Width = MainWindow.roomSize;
      path.Height = MainWindow.roomSize;
      if (type == 1)
      {
        path.Stroke = Brushes.DarkSlateBlue;
        path.StrokeThickness = 20;
      }
      else if (type == 2)
      {
        path.Stroke = Brushes.Red;
        path.StrokeThickness = 40;
      }
      else if (type == 3)
      {
        path.Stroke = Brushes.Green;
        path.StrokeThickness = 40;
      }
      path.Data = group;
      canvas.Children.Add(path);

      Canvas.SetLeft(path, x * MainWindow.roomSize);
      Canvas.SetTop(path, y * MainWindow.roomSize);
    }
  }
}
