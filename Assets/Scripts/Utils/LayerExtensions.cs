using UnityEngine;

public static class LayerExtensions
{
    public static bool HasLayer(this Component component, LayerMask layerMask) => 
        layerMask == (layerMask | (1 << component.gameObject.layer));
    
}
