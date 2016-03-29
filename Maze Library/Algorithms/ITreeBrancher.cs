namespace Maze_Library.Algorithms
{
    interface ITreeBrancher<T>
    {
        SearchTreeResult<T> Branch(ISearchable<T> searchable);
    }
}