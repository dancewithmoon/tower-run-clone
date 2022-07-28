using UnityEngine;

namespace UserInput
{
    public class StandaloneInputService : IInputService
    {
        public bool IsJumpButtonPressed()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}
