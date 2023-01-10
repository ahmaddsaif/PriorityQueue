namespace PriorityQueue
{
    public class PriorityQueue<T>
    {
        public class Node
        {
            public int Priority { get; set; }
            public T Val { get; set; }
        }

        bool _isMinHeap;
        public PriorityQueue(bool isMinHeap = true)
        {
            _isMinHeap = isMinHeap;
        }
        
        int heapSize = -1;

        List<Node> queue = new List<Node>();

        private int ChildL(int i)
        {
            return (2*i) + 1;
        }
        private int ChildR(int i)
        {
            return (2*i) + 2;
        }

        public void Enqueue(int priority, T val)
        {
            Node node = new Node(){Priority = priority, Val = val};
            
            queue.Add(node);
            heapSize++;

            if(_isMinHeap) {
                BuildMinHeap(heapSize);
            }
            else
                BuildMaxHeap(heapSize);
        }

        public T Dequeue()
        {
            if(heapSize <= -1)
                throw new Exception("Heap is emtpy!");
            
            T returnVal = queue[heapSize].Val;
            queue[0] = queue[heapSize];
            queue.RemoveAt(heapSize);
            heapSize--;

            if(_isMinHeap)
                MinHeapify(0);
            else
                MaxHeapify(0);

            return returnVal;
        }

        private void MaxHeapify(int i)
        {
            int lChild = ChildL(i);
            int rChild = ChildR(i);

            int lowest = i;

            if(lChild <= heapSize && queue[lChild].Priority > queue[lowest].Priority)
                lowest = lChild;
            if(rChild <= heapSize && queue[rChild].Priority > queue[lowest].Priority)
                lowest = rChild;

            if(lowest != i) {
                Swap(i, lowest);
                MaxHeapify(lowest);
            }
        }

        private void MinHeapify(int i)
        {
            int lChild = ChildL(i);
            int rChild = ChildR(i);

            int lowest = i;

            if(lChild <= heapSize && queue[lChild].Priority < queue[lowest].Priority)
                lowest = lChild;
            if(rChild <= heapSize && queue[rChild].Priority < queue[lowest].Priority)
                lowest = rChild;

            if(lowest != i) {
                Swap(i, lowest);
                MinHeapify(lowest);
            }
        }

        private void BuildMaxHeap(int i)
        {
            while(i>=0 && queue[(i-1)/2].Priority < queue[i].Priority)
            {
                Swap(i, (i-1)/2);
                i = (i-1)/2;
            }
        }

        private void BuildMinHeap(int i)
        {
            while(i>=0 && queue[(i-1)/2].Priority > queue[i].Priority)
            {
                Swap(i, (i-1)/2);
                i = (i-1)/2;
            }
        }

        private void Swap(int i, int v)
        {
            Node temp = queue[i];
            queue[i] = queue[(i-1)/2];
            queue[(i-1)/2] = temp;
        }
    }
}