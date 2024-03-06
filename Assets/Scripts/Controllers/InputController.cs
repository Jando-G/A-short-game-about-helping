using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
     public abstract class InputController : ScriptableObject
     {
          public abstract float RetrieveMoveInput();
          public abstract bool RetrieveJumpInput();
          public abstract bool RetrieveJumpHoldInput();
          public abstract bool RetrieveStompInput();
     }

}

