namespace DraughtBoardPuzzle.Dlx
{
    public class Column
    {
        public Column()
        {
            Previous = Next = this;
            Node = new Node(null, -1);
        }

        public Node Node { get; private set; }
        public Column Previous { get; private set; }
        public Column Next { get; private set; }
        public int Size { get; private set; }

        public void AppendColumnHeader(Column columnHeader)
        {
            Previous.Next = columnHeader;
            columnHeader.Next = this;
            columnHeader.Previous = Previous;
            Previous = columnHeader;
        }

        public void UnlinkColumnHeader()
        {
            Next.Previous = Previous;
            Previous.Next = Next;
        }

        public void RelinkColumnHeader()
        {
            Next.Previous = this;
            Previous.Next = this;
        }

        public void AppendNode(Node node)
        {
            Node.AppendColumnNode(node);
            Size++;
        }

        public void UnlinkNode(Node node)
        {
            node.UnlinkColumnNode();
            Size--;
        }

        public void RelinkNode(Node node)
        {
            node.RelinkColumnNode();
            Size++;
        }
    }
}
