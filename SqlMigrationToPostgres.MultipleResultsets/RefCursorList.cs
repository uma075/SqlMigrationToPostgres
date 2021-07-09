using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Custom class to model refcusor list
/// </summary>
public class RefCursorList
{
    public List<string> RefCursors { get; set; } = new List<string>();
    public int CurrentIndex { get; set; }
    public int Count => RefCursors.Count;
    public string CurrentCursor => RefCursors[CurrentIndex++];

    public bool HasNextResult => CurrentIndex < Count;

}

