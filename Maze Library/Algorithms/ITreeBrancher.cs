namespace Maze_Library.Algorithms
{
    interface ITreeBrancher<T>
    {
        SearchTree Branch(ISearchable<T> searchable);
    }
}