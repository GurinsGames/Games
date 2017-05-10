using System.Windows.Controls;

namespace Labirint
{
  public class Room
  {
    private RoomDrawing drawing;

    public Room(int state)
    {
      drawing = new RoomDrawing(this, state);
    }

    public void Init(Canvas canvas, int x, int y)
    {
      drawing.Init(canvas, x, y);
    }

    public Room Left;
    public Room Right;
    public Room Top;
    public Room Bottom;
  }
}
