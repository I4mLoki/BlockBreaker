using System;
namespace Code.DataConfig.Tools
{
    public class ListItemSelectorAttribute : Attribute
    {
        public string SetSelectedMethod;

        public ListItemSelectorAttribute(string setSelectedMethod)
        {
            this.SetSelectedMethod = setSelectedMethod;
        }
    }
}