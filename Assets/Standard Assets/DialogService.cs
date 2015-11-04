using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.Standard_Assets
{
    public class DialogService
    {
        private Text dialogText;

        public DialogService(Text dialog)
        {
            dialogText = dialog;
        }

        public void ShowDialog(string sender, string text) 
        {
            dialogText.text = sender +": " + text;
        }
    }
}
