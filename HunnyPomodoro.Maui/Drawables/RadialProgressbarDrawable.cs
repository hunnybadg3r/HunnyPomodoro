using HunnyPomodoro.Maui.Controls;

namespace HunnyPomodoro.Maui.Drawables;

public class RadialProgressbarDrawable : IDrawable
{
    private readonly RadialProgressBarControl _radialProgressBarControl;

    public RadialProgressbarDrawable(RadialProgressBarControl radialProgressBarControl) => _radialProgressBarControl = radialProgressBarControl;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        var isClockWise = true;
        var progress = _radialProgressBarControl.Progress;
        var isFullCircle = progress == 100;
        var progressAngle = progress / 100;
        var centerX = dirtyRect.Width / 2;
        var centerY = dirtyRect.Height / 2;
        var radius = (dirtyRect.Width / 2);

        // Explaination for coordinators.
        // Start 0 is at 3 o'clock. We travel anticlockwise. So 12 o'clock is 90 degrees.
        // The end angle is calculated from 3 o'clock. If we want 4 o'clock we need to subract from 0, ie -20 degrees. If we want 2 o'clock we need to add 20 degrees.
        // The Clockwise boolean tells if we travel clockwise or anticlockwise. In this case we travel clockwise from startAngle to endAngle and by this we get a slice that is what we want.
        // If we pick anticlockvwise, we get the rest of the circle, and the slice not highlighted.
        var startAngle = 90f;
        var endAngle = startAngle - (float)Math.Round(progressAngle * 360, 1);

        canvas.SaveState();

        canvas.StrokeSize = (float)_radialProgressBarControl.BarThickness;
        canvas.StrokeColor = new Color(243, 242, 238);
        canvas.DrawCircle(centerX, centerY, radius);

        // Reduce the radius of the filled circle by half the stroke's width
        float fillRadius = radius - (float)_radialProgressBarControl.BarThickness / 2;

        canvas.FillColor = _radialProgressBarControl.BarBackgroundColor ?? Colors.Transparent;
        canvas.FillCircle(centerX, centerY, fillRadius);

        if (isFullCircle)
        {
            canvas.FillColor = Color.FromArgb("#a785f6");
            canvas.FillCircle(centerX, centerY, radius);
            canvas.StrokeColor = Color.FromArgb("#a785f6");
            canvas.DrawCircle(centerX, centerY, radius);
        }
        else
        {
            if (progress > 0)
            {
                var path = new PathF();
                path.MoveTo(centerX, centerY);
                path.AddArc(centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, endAngle, isClockWise);

                canvas.FillColor = Color.FromArgb("#a785f6");
                canvas.FillPath(path);

                canvas.StrokeColor = Color.FromArgb("#a785f6");
                canvas.DrawArc(centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, endAngle, isClockWise, false);
            }
        }
        canvas.RestoreState();
    }
}