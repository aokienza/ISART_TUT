using UnityEngine;
using System.Collections;

interface EventHandler
{
    void OnDestroy();
    void Unregister();
}
