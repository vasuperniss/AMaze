namespace Maze_Library.Algorithms
{
    interface ITreeBrancher<T>
    {
        TreeSearchResult<T> Branch(ISearchable<T> searchable);
    }
}