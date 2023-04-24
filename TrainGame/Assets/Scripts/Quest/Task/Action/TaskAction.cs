using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskAction : ScriptableObject
{
    // Start is called before the first frame update

    public abstract int Run(Task task, int currentSuccess, int successCount);
    
}
