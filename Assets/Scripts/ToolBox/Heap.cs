using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.heapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public T RemoveFirst() {
        T firstItem = items[0];

        currentItemCount--;

        items[0] = items[currentItemCount];
        items[0].heapIndex = 0;

        SortDown(items[0]);

        return firstItem;
    }

    public bool Contains(T item)
    {
        return Equals(items[item.heapIndex], item);
    }

    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
        SortDown(item);//Not used yet
    }



    void SortUp(T item)
    {
        int parentIndex = (item.heapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.heapIndex - 1) / 2;
        }
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childeIdxLeft = item.heapIndex * 2 + 1;
            int childeIdxRight = item.heapIndex * 2 + 2;
            int swapIdx = 0;
            if ( childeIdxLeft < currentItemCount) {
                swapIdx = childeIdxLeft;

                if (childeIdxRight < currentItemCount)
                {
                    if (items[childeIdxLeft].CompareTo(items[childeIdxRight]) < 0)
                    {
                        swapIdx = childeIdxRight;
                    }
                }

                if(item.CompareTo(items[swapIdx]) < 0)
                {
                    Swap(item, items[swapIdx]);
                }
                else
                {
                    return;
                }

            }
            else
            {
                return;
            }


        }
    }

    void Swap(T a, T b)
    {
        items[a.heapIndex] = b;
        items[b.heapIndex] = a;
        int aux = a.heapIndex;
        a.heapIndex = b.heapIndex;
        b.heapIndex = aux;
    }
    
}

public interface IHeapItem<T>:IComparable<T>
{
    int heapIndex
    {
        get;
        set;
    }
}
