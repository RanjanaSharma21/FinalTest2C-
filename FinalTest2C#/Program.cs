using System;


class Program
{
    static void Main()
    {
        ColorChanger changer = new ColorChanger();
        ColorListener listener = new ColorListener();

        listener.Subscribe(changer);

        changer.CurrentColor = ConsoleColor.Red;
        changer.CurrentColor = ConsoleColor.Green;
        changer.CurrentColor = ConsoleColor.DarkYellow;
        changer.CurrentColor = ConsoleColor.DarkBlue;
    }
}



public class ColorChangedEventArgs : EventArgs
{
    public ConsoleColor NewColor { get; }

    public ColorChangedEventArgs(ConsoleColor newColor)
    {
        NewColor = newColor;
    }
}



public delegate void ColorChangedEventHandler(object sender, ColorChangedEventArgs e);



public class ColorChanger
{
    public event ColorChangedEventHandler ColorChanged;

    private ConsoleColor currentColor;

    public ConsoleColor CurrentColor
    {
        get { return currentColor; }
        set
        {
            if (currentColor != value)
            {
                currentColor = value;
                OnColorChanged(new ColorChangedEventArgs(value));
            }
        }
    }


    protected virtual void OnColorChanged(ColorChangedEventArgs e)
    {
        ColorChanged?.Invoke(this, e);
    }
}



public class ColorListener
{
    public void Subscribe(ColorChanger changer)
    {
        changer.ColorChanged += HandleColorChanged;
    }

    private void HandleColorChanged(object sender, ColorChangedEventArgs e)
    {
        Console.ForegroundColor = e.NewColor;
        Console.WriteLine($"Color changed to: {e.NewColor}");
        Console.ResetColor();
    }
}