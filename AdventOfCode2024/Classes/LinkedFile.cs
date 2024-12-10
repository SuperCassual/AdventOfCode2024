using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
    class LinkedFile
    {
        private int _id;
        private int _position;
        private int _size;
        private LinkedFile _prev;
        private LinkedFile _next;

        public LinkedFile(int id, int position, int size)
        {
            _id = id;
            _position = position;
            _size = size;
        }

        public void Remove()
        {
            if (_prev != null)
            {
                _prev.Next = null;
            }
            if(_next != null)
            {
                _next.Prev = null;
            }
        }

        public void Replace(LinkedFile replacement)
        {
            if (_prev != null)
            {
                _prev.Next = replacement;
                replacement.Prev = _prev;
            }
            if (_next != null)
            {
                _next.Prev = replacement;
                replacement.Next = _next;
            }
        }

        public void AddAfter(LinkedFile newFile)
        {
            _next = newFile;
            newFile.Prev = this;
        }

        public void AddThisInBetween(LinkedFile previous, LinkedFile next, LinkedFile currentSpace)
        {
            if(currentSpace.Id != -1)
            {
                throw new Exception("Something's wrong");
            }
            previous.Next = this;
            _prev = previous;

            LinkedFile futureNext = this;
            if(currentSpace.Size > _size)
            {
                int emptySpace = currentSpace.Size - _size;
                futureNext = new LinkedFile(-1, currentSpace.Position + _size, emptySpace);
                AddAfter(futureNext);
            }

            next.Prev = futureNext;
            futureNext.Next = next;

            _position = currentSpace.Position;
        }

        public long CalculateCheckSum()
        {
            long result = 0;
            if(_id == -1)
            {
                return result;
            }
            for(int i = _position; i < _position + _size; i++)
            {
                result += i * _id;
            }
            return result;
        }

        public LinkedFile First()
        {
            LinkedFile output = this;
            while (output.Prev != null)
            {
                output = output.Prev;
            }
            return output;
        }

        public LinkedFile Last()
        {
            LinkedFile output = this;
            while(output.Next != null)
            {
                output = output.Next;
            }
            return output;
        }

        public void WriteList()
        {
            string write = "";
            LinkedFile file = First();
            while(file != null)
            {
                for(int i = 0; i < file.Size; i++)
                {
                    if(file.Id == -1)
                    {
                        write += '.';
                    }
                    else
                    {
                        write += file.Id;
                    }
                }
                file = file.Next;
            }
            Console.WriteLine(write);
        }

        public LinkedFile Prev { get { return _prev; } set { _prev = value; } }
        public LinkedFile Next { get { return _next; } set {_next = value; } }
        public int Id { get { return _id; } }
        public int Size { get { return _size; } }
        public int Position { get { return _position; } }
    }
}
