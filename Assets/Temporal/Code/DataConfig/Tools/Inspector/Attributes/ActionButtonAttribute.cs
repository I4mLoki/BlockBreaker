using System;
using UnityEngine;
namespace Code.DataConfig.Tools.Inspector.Attributes
{
    public class ActionButtonAttribute : Attribute
    {
        public string Action;

        public ActionButtonAttribute(string action)
        {
            this.Action = action;
        }
    }
}