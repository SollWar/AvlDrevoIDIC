using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlDrevoIDIC
{
    public class AVLTree<TKey, TValue> : IDictionary<TKey, TValue>
    {

        private class Node
        {
            public Node left;
            public Node right;

            public TKey key; // ключ
            public TValue value; // значение
            public int height; // высота

            public Node(TKey x, TValue y)
            {
                key = x;
                value = y;
            }
        }

        private IComparer<TKey> comparer;

        private Node root;

        public AVLTree()
        {
            comparer = Comparer<TKey>.Default;
        }

        private int height(Node p)
        {
            return p == null ? 0 : p.height;
        }
        private Node insert(Node p, TKey x, TValue y)
        {
            if (p == null)
                return new Node(x, y);
            if (comparer.Compare(x, p.key) < 0) //(x < p.key)
                p.left = insert(p.left, x, y);
            else
                p.right = insert(p.right, x, y);

            p.height = 1 + Math.Max(height(p.left), height(p.right));

            int balance = p == null ? 0 : height(p.left) - height(p.right);

            if (balance > 1 && comparer.Compare(x, p.left.key) < 0) // x < p.left.key) // left left
                return RightRotate(p);
            if (balance < -1 && comparer.Compare(x, p.right.key) > 0) // x > p.right.key) // right right
                return LeftRotate(p);
            if (balance > 1 && comparer.Compare(x, p.left.key) > 0) // x > p.left.key)  // left right
            {
                p.left = LeftRotate(p.left);
                return RightRotate(p);
            }
            if (balance < -1 && comparer.Compare(x, p.right.key) < 0) // x < p.right.key) // right left
            {
                p.right = RightRotate(p.right);
                return LeftRotate(p);
            }
            return p;
        }

        public
            TValue this[TKey key]
        { 
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

    public ICollection<TKey>
        Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        private
            int count;
        public
            int Count
        {
            get
            {
                return count;
            }
        }

        private int getBalance(Node p)
        {
            return p == null ? 0 : height(p.left) - height(p.right);
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            ++count;
            root = insert(root, key, value);
            // Print();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            count = 0;
            root = null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            return contains(root, key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            if (count > 0)
            {
                if (ContainsKey(key))
                {
                    //Print();
                    //Console.WriteLine("\n\n");
                    --count;
                    root = remove(root, key);
                    //Print();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }


        private Node min(Node p)
        {
            var t = p;
            while (t.left != null)
                t = t.left;
            return t;
        }

        private bool contains(Node n, TKey x)
        {
            bool result = false;

            if (comparer.Compare(x, n.key) == 0)
                result = true;
            else if (comparer.Compare(x, n.key) > 0)
                if (n.right != null)
                    result = contains(n.right, x);
            else if (comparer.Compare(x, n.key) < 0)
                if (n.left != null)
                    result = contains(n.right, x);
            return result;
        }

        private Node remove(Node p, TKey x)
        {
            if (p == null)
                return p;
            if (comparer.Compare(x, p.key) < 0) //(x < p.key)
                p.left = remove(p.left, x);
            else if (comparer.Compare(x, p.key) > 0)
                p.right = remove(p.right, x);
            else
            {
                if (p.left == null || p.right == null)
                {
                    Node t = null;
                    if (t == p.left)
                        t = p.right;
                    else
                        t = p.left;

                    if (t == null) // нет потомков
                    {
                        t = p;
                        p = null; // удаляем узел
                    }
                    else
                        p = t;
                }
                else
                {
                    var t = min(p.right);
                    p.key = t.key;
                    p.right = remove(p.right, p.key);
                }
            }
            if (p == null)
                return p;

            p.height = 1 + Math.Max(height(p.left), height(p.right));

            int balance = p == null ? 0 : height(p.left) - height(p.right);

            if (balance > 1 && getBalance(p.left) >= 0) // x < p.left.key) // left left
                return RightRotate(p);

            if (balance > 1 && getBalance(p.left) < 0) // x > p.left.key)  // left right
            {
                p.left = LeftRotate(p.left);
                return RightRotate(p);
            }

            if (balance < -1 && getBalance(p.right) <= 0) // x > p.right.key) // right right
                return LeftRotate(p);

            if (balance < -1 && getBalance(p.right) > 0) // x < p.right.key) // right left
            {
                p.right = RightRotate(p.right);
                return LeftRotate(p);
            }
            return p;
        }

        public
            bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public
            bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private
            Node RightRotate(Node y)
        {
            var x = y.left;
            var T2 = x.right;
            x.right = y;
            y.left = T2;
            y.height = Math.Max(height(y.left), height(y.right)) + 1;
            x.height = Math.Max(height(x.left), height(x.right)) + 1;
            return x;
        }
        private
            Node LeftRotate(Node x)
        {
            var y = x.right;
            var T2 = y.left;
            y.left = x;
            x.right = T2;
            x.height = Math.Max(height(x.left), height(x.right)) + 1;
            y.height = Math.Max(height(y.left), height(y.right)) + 1;
            return y;
        }
        private
            void Print()
        {
            print(root, 0);
        }
        private
            void print(Node p, int shift)
        {
            if (p.left != null)
                print(p.left, shift + 1);
            for (int i = 0; i != shift; i++)
                Console.Write("  ");
            Console.WriteLine(p.key);
            if (p.right != null)
                print(p.right, shift + 1);
        }

    }
}
