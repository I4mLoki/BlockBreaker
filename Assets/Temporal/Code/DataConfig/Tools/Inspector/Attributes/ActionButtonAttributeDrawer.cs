using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ActionResolvers;
using UnityEngine;
namespace Code.DataConfig.Tools.Inspector.Attributes
{
    public class ActionButtonAttributeDrawer : OdinAttributeDrawer<ActionButtonAttribute>
    {
        private ActionResolver _actionResolver;

        protected override void Initialize()
        {
            this._actionResolver = ActionResolver.Get(this.Property, this.Attribute.Action);
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (GUILayout.Button("Add Behaviour To List", GUILayout.Height(30), GUILayout.Width(200)))
            {
                this._actionResolver.DoActionForAllSelectionIndices();
            }

            this.CallNextDrawer(label);
        }
    }
}