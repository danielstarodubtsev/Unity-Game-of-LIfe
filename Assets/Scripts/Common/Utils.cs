enum FigureCodes
{
    SingleCell = 0,
    Glider = 1,
    SmallSpaceship = 2,
    MediumSpaceship = 3,
    LargeSpaceship = 4,
    GosperGun = 5,
    SpaceFiller = 6
}

public class Utils
{
    static public (int rotatedDeltaX, int rotatedDeltaY) RotateDelta(int deltaX, int deltaY, int rot)
    {
        if (rot == 0)
        {
            return (deltaX, deltaY);
        }
        if (rot == 1)
        {
            return (deltaY, -deltaX);
        }
        if (rot == 2)
        {
            return (-deltaX, -deltaY);
        }
        return (-deltaY, deltaX);
    }
}