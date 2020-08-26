using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList
{
    internal class NewList<T>
    {
        public NewList(T data) => Data = data;
        public T Data { get; set; }
        public NewList<T> Prev { get; set; }
        public NewList<T> Next { get; set; }
    }

    class DoubleLinkList<T>
    {
        private NewList<T> Head;
        private NewList<T> Tail;
        private int Size;

        /// <summary>
        /// Поиск обьекта по данным
        /// </summary>
        /// <param name="Wanted">Объект</param>
        /// <param name="data">Данные для поиска</param>
        /// <returns>Искомый объекет или null</returns>
        private NewList<T> Search (NewList<T> Wanted, T data)
        {
            while (Wanted != null)
            {
                if (Wanted.Data.Equals(data))
                {
                    break;
                }
                Wanted = Wanted.Next;
            }
            return Wanted;
        }

        /// <summary>
        /// Поиск объекта по индексу
        /// </summary>
        /// <param name="Wanted">Объект</param>
        /// <param name="index">Индекс</param>
        /// <returns>Искомый объект или null</returns>
        private NewList<T> Search(NewList<T> Wanted, int index)
        {
            int item = 0;
            if (index < Size)
            {
                while (Wanted != null)
                {
                    if (item == index)
                    {
                        break;
                    }
                    item++;
                    Wanted = Wanted.Next;
                }
                return Wanted;
            }
            return null;
        }

        /// <summary>
        /// Обращение по индексу к объекту списка
        /// </summary>
        /// <param name="index">Индекс объекта</param>
        /// <returns>Данные Объекта</returns>
        public T this[int index] 
        {
            get
            {
                NewList<T> temp = Head;
                temp = Search(temp, index);
                if (temp != null)
                 {
                    return temp.Data;
                 }
                 else
                     throw new IndexOutOfRangeException();
            }
            set
            {
                NewList<T> temp = Head;
                temp = Search(temp, index);
                if (temp != null)
                {
                    temp.Data = value;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }


        /// <summary>
        /// Добовляет объет в конец списка
        /// </summary>
        /// <param name="data">Объект</param>
        public void Add (T data)
        {
            NewList<T> node = new NewList<T>(data);

            if (Head == null)
            {
                Head = node;
            }
            else 
            {
                Tail.Next = node;
                node.Prev = Tail;
            }

            Tail = node;
            Size++;
        }

        /// <summary>
        /// Добовляет объет в начало списка
        /// </summary>
        /// <param name="data">Объект</param>
        public void AddBeginning(T data)
        {
            NewList<T> node = new NewList<T>(data);
            NewList<T> temp = Head;

            node.Next = Head;
            Head = node;
            if (Size == 0)
            {
                Tail = Head;
            }
            else
            {
                temp.Prev = Head;
            }
            Size++;
        }

        /// <summary>
        /// Удаляет указанный объет
        /// </summary>
        /// <param name="data">Объект</param>
        public bool Remove (T data)
        {
            NewList<T> temp = Head;

            temp = Search(temp, data);

            if (temp != null)
            {
                if (temp.Prev != null && temp.Next != null)
                {
                    temp.Prev.Next = temp.Next;
                    temp.Next.Prev = temp.Prev;
                }
                else if (temp.Prev == null)
                {
                    Head = temp.Next;    
                }
                else
                {
                    Tail = temp.Prev;
                    Tail.Next = null;
                }

                temp = null;
                Size--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет объет по указанному индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        public bool RemoveAt (int index)
        {
            NewList<T> temp = Head;
            if (index < Size && index >= 0)
            {
                temp = Search(temp, index);
                
                if (temp.Prev != null && temp.Next != null)
                {
                    temp.Prev.Next = temp.Next;
                    temp.Next.Prev = temp.Prev;
                }
                else if (temp.Prev == null)
                {
                    Head = temp.Next;
                }
                else
                {
                    Tail = temp.Prev;
                    Tail.Next = null;
                }

                temp = null;
                Size--;
                return true;
            }
            temp = null;
            return false;
        }

        /// <summary>
        /// Выводит в консоль весь список
        /// </summary>
        public void Print ()
        {
            NewList<T> temp = Head;
            while (temp != null)
            {
                Console.Write($"{temp.Data} ");
                temp = temp.Next;
            }
        }

        /// <summary>
        /// Возвращает размер списка
        /// </summary>
        public int SizeList() => Size;

        /// <summary>
        /// Удаляет весь список
        /// </summary>
        public void Clear ()
        {
            NewList<T> temp = Head;
            while (Head != null)
            {
                Head = null;
                Head = temp.Next;
            }
            temp = null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DoubleLinkList<int> X = new DoubleLinkList<int>();
            

            for(int i = 0; i < 15; i++)
            {
                X.Add(i);
            }
            X.Print();
            Console.Write("\n");
            X.Remove(5);
            X[11] = 10;
            X.Print();
            Console.Write($"\nSize = {X.SizeList()} {X[11]}");
            Console.Read();
        }
    }
}
