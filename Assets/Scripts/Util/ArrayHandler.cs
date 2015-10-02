using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Util
{
    public static class ArrayHandler
    {
        public static void Clear<T>(ref T[] original)
        {
            original = new T[original.Length];
        }

        public static void AddToArray<T>(ref T[] original, T itemToAdd)
        {
            T[] finalArray = new T[original.Length + 1];
            for (int i = 0; i < original.Length; i++)
            {
                finalArray[i] = original[i];
            }
            finalArray[finalArray.Length - 1] = itemToAdd;
            original = finalArray;
        }

        public static void AddToArray<T>(ref T[] original, T[] itemsToAdd)
        {
            T[] finalArray = new T[original.Length + itemsToAdd.Length];
            for (int i = 0; i < original.Length; i++)
            {
                finalArray[i] = original[i];
            }
            for (int i = original.Length; i < itemsToAdd.Length; i++)
            {
                finalArray[i] = itemsToAdd[i - itemsToAdd.Length];
            }
            original = finalArray;
        }

        public static void Shuffle<T>(T[] original)
        {
            for (int i = 0; i < original.Length; i++)
            {
                int r = i + (int)(Random.Range(0f, 1f) * (original.Length - i));
                T temp = original[r];
                original[r] = original[i];
                original[i] = temp;
            }
        }

        public static T[] Remove<T>(ref T[] original, int startIndex, int count)
        {
            if (count < 0)
            {
                Debug.LogError("Count(" + count + ") cannot be negative. Emptying array.");
                return new T[0];
            }
            if (original.Length - count <= 0)
            {
                Debug.LogWarning("Count(" + count + ") cannot be greater than the length(" + original.Length + ") of the array. Setting count to length.");
                count = original.Length;
            }
            if (startIndex + count > original.Length)
            {
                Debug.LogWarning("Count(" + count + ") + the startIndex(" + startIndex + ") will go out of range of the array's length(" + original.Length + "). Setting count to be length - startIndex.");
                count = original.Length - startIndex;
            }

            T[] finalArray = new T[original.Length - count];
            T[] removedObjects = new T[count];

            int index = 0;
            for (int i = 0; i < original.Length; i++)
            {
                if (i < startIndex || i >= startIndex + count)
                {
                    finalArray[index++] = original[i];
                }
                else
                {
                    removedObjects[i - startIndex] = original[i];
                }
            }
            original = finalArray;
            return removedObjects;
        }

        public static T RemoveFromEnd<T>(ref T[] original)
        {
            T[] finalArray = new T[original.Length];
            T removedObject = original[0];

            for (int i = 0; i < original.Length-1; i++)
            {
                if (original[i + 1] != null)
                {
                    finalArray[i] = original[i];
                }
                else
                {
                    removedObject = original[i];
                }
            }
            if (removedObject == null) removedObject = original[original.Length - 1];
            original = finalArray;
            return removedObject;
        }

        public static bool IsEmpty<T>(T[] original)
        {
            bool empty = true;

            if(original != null)
            {
                for(int i = 0; i < original.Length; i++)
                {
                    if (original[i] != null) empty = false;
                }
            }
            return empty;
        }
    }
}
