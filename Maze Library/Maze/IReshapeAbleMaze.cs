namespace Maze_Library.Maze
{
    internal interface IReshapeAbleMaze : IMaze
    {
        void OpenAllDoors();

        void CloseAllDoors();

        void ChangeDoorStateBetween(MazePosition first, MazePosition second,
                                                            DoorState state);
    }
}
