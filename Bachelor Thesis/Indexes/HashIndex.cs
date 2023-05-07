using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Thesis.Indexes
{
    public class HashIndex<T>
    {
        private Dictionary<int, List<T>> _elements;
        private LinkedList<T> _head;
        private List<PropertyInfo> _indexAttributes;
        private int _count;
        private PropertyInfo _timestampProperty;

        public HashIndex(List<PropertyInfo> indexAttributes)
        {
            _elements = new Dictionary<int, List<T>>();
            _head = new LinkedList<T>();
            _indexAttributes = indexAttributes;
            _count = 0;
        }

        public void AddElement(T element)
        {
            int hashValue = ComputeHashCode(element);

            if (!_elements.ContainsKey(hashValue))
            {
                _elements[hashValue] = new List<T>();
            }
            _elements[hashValue].Add(element);
            _head.AddLast(element);
            _count++;
        }

        public T? RemoveFirstElementAdded()
        {
            T? firstElement = default;
            if(_head.First != null)
            {
                firstElement = _head.First.Value;
                int hashValue = ComputeHashCode(firstElement);
                if (_elements.ContainsKey(hashValue))
                {
                    var collisionList = _elements[hashValue];
                    collisionList.Remove(firstElement);
                    _head.RemoveFirst();
                    _count--;
                }
            }
            return firstElement;
        }

        public List<T> RemoveElementsBeforeDate(DateTime date)
        {
            var elementsRemoved = new List<T>();
            if(_head != null && _head.Any())
            {
                T? firstElement = _head.FirstOrDefault();
                while(firstElement != null)
                {
                    if(IsTimestampBeforeDate(firstElement, date))
                    {
                        int hashValue = ComputeHashCode(firstElement);
                        if (_elements.ContainsKey(hashValue))
                        {
                            var collisionList = _elements[hashValue];
                            collisionList.Remove(firstElement);
                            _head.RemoveFirst();
                            _count--;

                            elementsRemoved.Add(firstElement);
                        }
                        firstElement = _head.FirstOrDefault();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return elementsRemoved;
        }

        public void PrintIndexDictionary()
        {
            foreach (var pair in _elements)
            {
                foreach (var elem in pair.Value)
                {
                    Console.WriteLine(elem.ToString());
                }
            }
        }

        public void PrintIndexList()
        {
            var firstElem = _head.First;
            while(firstElem != null)
            {
                Console.WriteLine(firstElem.Value.ToString());
                firstElem = firstElem.Next;
            }
        }

        public int Count()
        {
            return _count;
        }

        public void SetTimestampProperty(PropertyInfo timestampProperty)
        {
            _timestampProperty = timestampProperty;
        }

        private bool IsTimestampBeforeDate(T? element, DateTime date)
        {
            var timestamp = _timestampProperty.GetValue(element);
            return timestamp != null && _timestampProperty.PropertyType == typeof(DateTime) && ((DateTime) timestamp < date);
        }

        private int ComputeHashCode(T element)
        {
            int hashValue = 0;
            foreach (PropertyInfo property in _indexAttributes)
            {
                var propertyValue = property.GetValue(element);
                hashValue += propertyValue.GetHashCode();
            }
            return hashValue;
        }
    }
}
