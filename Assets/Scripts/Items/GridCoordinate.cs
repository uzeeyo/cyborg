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
    }
}