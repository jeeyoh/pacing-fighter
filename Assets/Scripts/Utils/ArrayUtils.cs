using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ArrayUtils 
{
	/// <summary>
	/// Add the object to the given array and return the new array.
	/// Note: Inefficient. The original array is not altered.
	/// </summary>
	public static T[] Add<T>(T[] array, T item)
	{
		List<T> list = new List<T>(array);
		list.Add (item);
		return list.ToArray ();
	}

	/// <summary>
	/// Add the object to the given array and return the new array.
	/// Note: Inefficient. The original array is not altered.
	/// </summary>
	public static System.Array Add<T>(System.Array array, T item)
	{
		// Copy the array into a new one
		List<T> list = new List<T>();
		for(int i = 0; i < array.Length; i++)
			list.Add ((T)array.GetValue (i));

		list.Add (item);
		return (System.Array)list.ToArray();
	}

	/// <summary>
	/// Remove the object from the given array and return the new array.
	/// Note: Inefficient. The original array is not altered.
	/// </summary>
	public static T[] Remove<T>(T[] array, T item)
	{
		List<T> list = new List<T>(array);
		list.Remove (item);
		return list.ToArray ();
	}

	/// <summary>
	/// Returns the reference to a shallow copy of the given array
	/// </summary>
	public static T[] Copy<T>(T[] array)
	{
		T[] copy = new T[array.Length];
		for(int i = 0; i < array.Length; i++)
			copy[i] = array[i];
		return copy;
	}

	/// <summary>
	/// Returns a deep copy of the given HitBox array. A deep copy creates a duplicate of each 
	/// element in the given array, so that the two arrays don't share any references.
	/// </summary>
	public static HitBox[] DeepCopy(HitBox[] array)
	{
		HitBox[] copy = new HitBox[array.Length];
		for(int i = 0; i < array.Length; i++)
			copy[i] = new HitBox(array[i]);
		return copy;
	}

	/// <summary>
	/// Returns a deep copy of the given Force array. A deep copy creates a duplicate of each 
	/// element in the given array, so that the two arrays don't share any references.
	/// </summary>
	public static Force[] DeepCopy(Force[] array)
	{
		Force[] copy = new Force[array.Length];
		for(int i = 0; i < array.Length; i++)
			copy[i] = new Force(array[i]);
		return copy;
	}

	/// <summary>
	/// Returns a deep copy of the given array of events. A deep copy creates a duplicate of each 
	/// element in the given array, so that the two arrays don't share any references.
	/// </summary>
	public static Brawler.Event[] DeepCopy(Brawler.Event[] array)
	{
		Brawler.Event[] copy = new Brawler.Event[array.Length];
		for(int i = 0; i < array.Length; i++)
			copy[i] = new Brawler.Event(array[i]);
		return copy;
	}

	/// <summary>
	/// Removes the object at the given index from the given array and return the new array.
	/// Note: Inefficient. The original array is not altered.
	/// </summary>
	public static T[] RemoveAt<T>(T[] array, int index)
	{
		List<T> list = new List<T>(array);
		list.RemoveAt (index);
		return list.ToArray ();
	}

	/// <summary>
	/// Returns a random element from the given array
	/// </summary>
	public static T RandomElement<T>(T[] array)
	{
		// Return a default value (null, if T is a reference value) if the list is empty 
		if(array.Length == 0)
			return default(T);

		// Return a random element from the array
		return array[UnityEngine.Random.Range (0,array.Length)];
	}

	/// <summary>
	/// Returns a random element from the given list.
	/// AFAIK, this implementation is efficient.
	/// </summary>
	public static T RandomElement<T>(List<T> list)
	{
		// Return a default value (null, if T is a reference value) if the list is empty 
		if(list.Count == 0)
			return default(T);
		
		// Return a random element from the array
		return list[UnityEngine.Random.Range (0,list.Count)];
	}

	/// <summary>
	/// Returns a random index within the bounds of the given array.
	/// AFAIK, this implementation is efficient.
	/// </summary>
	public static int RandomIndex<T>(T[] array)
	{
		return UnityEngine.Random.Range (0,array.Length);
	}
}
