namespace DatabaseContract
{
    public struct Profile
    {
        public static Profile DefaultLeft = new Profile()
        {
            Name = "Profile L",
            Up = 87,    // W
            Down = 83,  // S
            Left = 65,  // A
            Right = 8,  // D
            Shoot = 160 // LeftShift
        };

        public static Profile DefaultRight = new Profile()
        {
            Name = "Profile R",
            Up = 38,    // Up
            Down = 40,  // Down
            Left = 37,  // Left
            Right = 39, // Right
            Shoot = 161 // RightShift
        };

        public static Profile New = DefaultLeft;

        public string Name;

        /// <summary>
        /// Keyboard key id.
        /// </summary>
        public int Up, Down, Left, Right, Shoot;
    }
}