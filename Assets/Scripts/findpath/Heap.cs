using UnityEngine;
using System.Collections;
using System;

public class Heap<heapType> where heapType : IHeapItem<heapType>
{
	
    //Array
    heapType[] items;
    //Array
	
    //Int
    int currentItemCount;
    //Int
	
	
	public Heap(int maxHeapSize) 
    {
        items = new heapType[maxHeapSize];
	}

    public void Add(heapType item)
    {
		item.HeapIndex = currentItemCount;
		items[currentItemCount] = item;
		SortUp(item);
		currentItemCount++;
	}

    public heapType RemoveFirst()
    {
        heapType firstItem = items[0];
		currentItemCount--;
		items[0] = items[currentItemCount];
		items[0].HeapIndex = 0;
		SortDown(items[0]);
		return firstItem;
	}

    public void UpdateItem(heapType item) 
    {
		SortUp(item);
	}

	public int Count
    {
		get
        {
			return currentItemCount;
		}
	}

    public bool Contains(heapType item) 
    {
		return Equals(items[item.HeapIndex], item);
	}

    void SortDown(heapType item)
    {
		while (true) 
        {
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < currentItemCount) 
            {
				swapIndex = childIndexLeft;

				if (childIndexRight < currentItemCount)
                {
					if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
						swapIndex = childIndexRight;
					}
				}

				if (item.CompareTo(items[swapIndex]) < 0) 
                {
					Swap (item,items[swapIndex]);
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

    void SortUp(heapType item)
    {
		int parentIndex = (item.HeapIndex-1)/2;
		
		while (true) {
            heapType parentItem = items[parentIndex];
			if (item.CompareTo(parentItem) > 0) {
				Swap (item,parentItem);
			}
			else {
				break;
			}

			parentIndex = (item.HeapIndex-1)/2;
		}
	}

    void Swap(heapType itemA, heapType itemB)
    {
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
	
	
	
}

public interface IHeapItem<heapType> : IComparable<heapType>
{
	int HeapIndex {
		get;
		set;
	}
}
