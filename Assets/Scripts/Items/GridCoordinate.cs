namespace Cyborg.Items
{
    [System.Serializable]
    public struct GridCoordinate
    {
        public GridCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;
        public int y;

        public static bool operator ==(GridCoordinate a, GridCoordinate b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(GridCoordinate a, GridCoordinate b)
        {
            return !(a == b);
        }
    }
}