using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Bumer.ActiveObjects
{
  public class ActiveObjectDispatcher
  {
    LinkedList<IActiveObject> list;
    UserObject userObject;
    List<Interference> interferences;
    Random random;
    int nextInterferenceTime;

    private bool Intersect()
    {
      if (userObject == null)
        return false;
      if (interferences.Count == 0)
        return false;
      System.Drawing.Rectangle r = userObject.Rect;
      foreach (Interference obj in interferences)
      {
        if (r.IntersectsWith(obj.Rect))
          return true;
      }
      return false;
    }

    public ActiveObjectDispatcher()
    {
      list = new LinkedList<IActiveObject>();
      userObject = null;
      interferences = new List<Interference>();
      random = new Random();
      nextInterferenceTime = 0;
    }

    public void Add(UserObject obj)
    {
      if (obj == null)
        return;
      list.AddLast(obj);
      userObject = obj;
    }

    public void Initialize(Canvas canvas)
    {
      foreach (IActiveObject obj in list)
        obj.Initialize(canvas);
    }

    public void Execute(int time, Canvas canvas)
    {
      if (interferences.Count < 20 && time > nextInterferenceTime)
      {
        Interference obj = new Interference(random.Next(1, 10), random.Next(1000, 6000) / 1000.0, random.Next(0, 10000));
        list.AddLast(obj);
        interferences.Add(obj);
        obj.Initialize(canvas);
        nextInterferenceTime += random.Next(100, 2000);
      }

      LinkedListNode<IActiveObject> node = list.First;
      while (node != null)
      {
        LinkedListNode<IActiveObject> next = node.Next;
        if (!node.Value.Execute(time, canvas))
        {
          list.Remove(node);
          interferences.Remove(node.Value as Interference);
        }
        node = next;
      }

      if (Intersect())
        userObject.Hit();
      else
        userObject.NoHit();
    }
  }
}
