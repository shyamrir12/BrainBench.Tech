using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels.UserMangment
{
    public class TreeItemData
    {
        public TreeItemData Parent { get; set; } = null;

        public string Text { get; set; }
        public int Value { get; set; } = 0;

        public bool IsExpanded { get; set; } = false;

        public bool IsChecked { get; set; } = false;

        public bool HasChild => TreeItems != null && TreeItems.Count > 0;

        public HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();

        public TreeItemData(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public void AddChild(string itemName,int value)
        {
            TreeItemData item = new TreeItemData(itemName,value);
            item.Parent = this;
            this.TreeItems.Add(item);
            ///return item;
        }

        public bool HasPartialChildSelection()
        {
            int iChildrenCheckedCount = (from c in TreeItems where c.IsChecked select c).Count();
            return HasChild && iChildrenCheckedCount > 0 && iChildrenCheckedCount < TreeItems.Count();
        }

    }

}
