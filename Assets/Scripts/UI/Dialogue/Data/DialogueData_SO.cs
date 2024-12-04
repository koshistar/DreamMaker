using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{

    //对话文本存储config
    [CreateAssetMenu(fileName = "DialogueData_SO", menuName = "Dialogue/DialogueData_SO")]
    public class DialogueData_SO : ScriptableObject
    {
        public List<string> dialogueList;
    }
}