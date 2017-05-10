using System.Windows.Controls;

namespace Bumer.ActiveObjects
{
  public interface IActiveObject
  {
    void Initialize(Canvas canvas);
    bool Execute(int time, Canvas canvas);

    System.Drawing.Rectangle Rect { get; }
  }
}
