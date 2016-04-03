using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// a Seachable Maze that Randomizes it's searching proccess
    /// </summary>
    /// <seealso cref="Maze_Library.Maze.SearchableMaze" />
    internal class RandomSearchableMaze : SearchableMaze
    {
        /// <summary>
        /// The scrambl times to scramble
        /// </summary>
        private const int SCRAMBLE_ROUNDS = 7;

        /// <summary>
        /// The maze to search
        /// </summary>
        private IMaze maze;
        /// <summary>
        /// Random Object for the randomizations
        /// </summary>
        private Random r;
        /// <summary>
        /// HashSet to determin if a MazePosition has been removed or not
        /// </summary>
        private HashSet<MazePosition> removedOnce;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomSearchableMaze"/> class.
        /// </summary>
        /// <param name="maze">the searched maze</param>
        public RandomSearchableMaze(IMaze maze) : base(maze)
        {
            this.maze = maze;
            this.r = new Random();
            this.removedOnce = new HashSet<MazePosition>();
        }

        /// <summary>
        /// Gets the reachable states from the given State.
        /// </summary>
        /// <param name="state">The state to move from.</param>
        /// <returns>
        /// all reachable States
        /// </returns>
        public override List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            List<State<MazePosition>> states = base.GetReachableStatesFrom(state);
            this.Scramble(states);
            //this.RandomlyRemoveAState(states);
            //this.RandomlyRemoveAState(states);
            return states;
        }

        /// <summary>
        /// Randomly Removes a State from states
        /// </summary>
        /// <param name="states">The List of states.</param>
        private void RandomlyRemoveAState(List<State<MazePosition>> states)
        {
            if (states.Count > 2 && this.RandomBool())
            {
                // only remove if there are more than 2 states in the list
                for (int i = 0; i < states.Count; i++)
                {
                    if (!this.removedOnce.Contains(states[i].TState))
                    {
                        // add the state to the Removed atleast once Set
                        this.removedOnce.Add(states[i].TState);
                        states.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Scrambles the specified List of states.
        /// </summary>
        /// <param name="states">The states to scramble.</param>
        private void Scramble(List<State<MazePosition>> states)
        {
            State<MazePosition> tempState;
            for (int i = 0; i < SCRAMBLE_ROUNDS; i++)
            {
                for (int j = 0; j < states.Count; j++)
                {
                    int random = r.Next(states.Count);
                    tempState = states[j];
                    states[j] = states[random];
                    states[random] = tempState;
                }
            }
        }

        /// <summary>
        /// Randoms a bool value.
        /// </summary>
        /// <returns>the random boolean value</returns>
        private bool RandomBool()
        {
            int random = r.Next(2);
            if (random == 0)
            {
                return true;
            }
            return false;
        }
    }
}
