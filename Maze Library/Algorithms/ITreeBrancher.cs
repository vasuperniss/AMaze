namespace Maze_Library.Algorithms
{
    interface ITreeBrancher<T>
    {
        SearchTreeResult Branch(ISearchable<T> searchable);
    }
}