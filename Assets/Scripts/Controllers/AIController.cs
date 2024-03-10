using Controls;
using UnityEngine;

namespace Controls
{
     [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]

     public class AIController : InputController
     {

          public override bool RetrieveJumpHoldInput()
          {
               return false;
          }

          public override bool RetrieveJumpInput()
          {
               return false;
          }

          public override float RetrieveMoveInput()
          {
               return 0;
          }

          public override bool RetrieveStompInput()
          {
               return false;
          }
     }
}

