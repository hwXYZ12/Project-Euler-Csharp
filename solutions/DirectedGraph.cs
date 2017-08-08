using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeGrown.DirectedGraph
{

    public class DirectedGraph<T>
    {

        public class GraphNode
        {
            public List<GraphNode> Children { get; set; }
            public T Data { get; set; }

            public enum State { NOT_VISITED, VISITING, VISITED }
            public State Visited { get; set; }

            public GraphNode(T data, List<GraphNode> children)
            {
                this.Data = data;
                this.Children = children;
                Visited = State.NOT_VISITED;
            }
        }

        // keeps a list of nodes
        // each node contains a list of its children
        List<GraphNode> nodeList;

        public DirectedGraph(List<GraphNode> nodeList)
        {
            this.nodeList = nodeList;
        }

        // prints the graph to the console
        public void printGraphToConsole()
        {
            foreach(GraphNode node in nodeList)
            {
                Console.Write(node.Data+" : ");
                foreach(GraphNode children in node.Children)
                {
                    Console.Write(children.Data + " ");
                }
                Console.WriteLine();
            }
        }
    }

    public class MinDirectedGraph
    {
        public class MinGraphNode
        {            
            public List<MinGraphNode> Children { get; set; }
            public int Data { get; set; }

            public enum State { NOT_VISITED, VISITING, VISITED }
            public State Visited { get; set; }

            public int MinPath { get; set; }

            public MinGraphNode(int data, List<MinGraphNode> children)
            {
                this.Data = data;
                this.Children = children;
                Visited = State.NOT_VISITED;
                MinPath = -1;
            }
        }
        
        // keeps a list of nodes
        // each node contains a list of its children
        List<MinGraphNode> nodeList;

        public MinDirectedGraph(List<MinGraphNode> nodeList)
        {
            this.nodeList = nodeList;
        }

        // prints the graph to the console
        public void printGraphToConsole()
        {
            foreach(MinGraphNode node in nodeList)
            {
                Console.Write(node.Data+" : ");
                foreach(MinGraphNode children in node.Children)
                {
                    Console.Write(children.Data + " ");
                }
                Console.WriteLine();
            }
        }


        // performs breadth-first search, starting from the 'start' node
        // the 'MinPath' member of each node is updated as the search is executed
        // in order to track the shortest path length from 'start' to which ever
        // node is currently being searched. The end result ought to return the
        // 'MinPath' member of the 'end' node.
        // This will return -1 if there is no path between the nodes. This function
        // makes some assumptions..................
        public int BFS_MinPathLength(MinGraphNode start, MinGraphNode end)
        {
            LinkedList<MinDirectedGraph.MinGraphNode> q = new LinkedList<MinDirectedGraph.MinGraphNode>();
            q.AddLast(start);

            // set starting node 'MinPath'
            start.MinPath = start.Data;

            while (q.Count != 0)
            {
                // dequeue the head
                MinDirectedGraph.MinGraphNode which = q.First.Value;
                q.RemoveFirst();

                // set state of 'which'
                which.Visited = MinGraphNode.State.VISITING;

                // enqueue children and set state
                foreach (MinDirectedGraph.MinGraphNode child in which.Children)
                {
                    // checks the child node's MinPath against
                    // it's parents and, if it's not already the case,
                    // each child node will be queued up in order to repeat the
                    // process on its children. In the case that the child node
                    // has already been queued up, in other words its state is 'VISITING',
                    // then requeuing the nodes children will be skipped
                    int newMinPath = which.MinPath + child.Data;
                    if (child.MinPath == -1)
                    {
                        child.MinPath = newMinPath;
                    }
                    else if (child.MinPath > newMinPath)
                    {
                        // when a min path is re-written,
                        // the node must be re-appended to
                        // the queue as it may influence each of its
                        // children
                        child.MinPath = newMinPath;
                        q.AddLast(child);
                    }

                    if (child.Visited == MinGraphNode.State.NOT_VISITED)
                    {
                        q.AddLast(child);
                        child.Visited = MinGraphNode.State.VISITING;
                    }
                }
                which.Visited = MinGraphNode.State.VISITED;
            }

            int ret = end.MinPath;

            //// reset properties of each node
            //foreach(MinDirectedGraph.MinGraphNode node in nodeList)
            //{
            //    node.Visited = MinGraphNode.State.NOT_VISITED;
            //    node.MinPath = -1;
            //}

            return ret;
        }
    }
}
