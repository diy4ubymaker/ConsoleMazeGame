using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Items.ItemObject
{
    public class ItemList
    {
        public List<Item> itemList { get; } = null;

        public ItemList()
        {
            itemList = new List<Item>();

        }

        /* Helper: Determine if the item is in the given list */
        public bool IsInList(Item item)
        {
            foreach (Item itemInList in itemList)
            {
                if (item == itemInList)
                    return true;
            }
            return false;
        }

        /* Find the item associated with a given name */
        public Item FindItem(string itemName)
        {
            /* safety check on the parameters (also terminates recursion) */
            if ((itemList == null) || (itemName == null))
            {
                return null;  /* return a NULL result if the parameters are invalid (terminates recursion) */
            }

            foreach (Item item in itemList)
            {
                if (item.itemname == itemName)
                {
                    return item;
                }
            }

            return null;
        }

        /* Adds an item to a given list, which might be NULL (an empty list).  Returns the new head of the list */
        public void Add(Item item)
        {
            /* safety check on the parameters */
            //if ((item == null) || IsInList(item))
            if (item == null)
            {
                /* return if the parameters are invalid or if the item is already in the list */
                return;
            }
            //itemList.Insert(0, item);
            itemList.Add(item);
        }

        /* Removes an item from a given list.  Returns the new head of the list */
        public void Remove(Item item)
        {
            /* safety check on the parameters (also terminates recursion) */
            if ((itemList == null) || (item == null))
            {
                return;  /* if the parameters are invalid, return the existing list (terminates recursion) */
            }

            itemList.Remove(item);


        }

        /* Frees the memory associated with an ItemList chain and its included items */
        public void Free()
        {
            /* safety check on the parameters (terminates recursion) */
            if ((itemList == null))
            {
                return; /* take no action if the parameters are invalid (terminates recursion) */
            }

            itemList.Clear();
        }

        /* Helper: Retrieve the number of items in the list */
        public uint GetCount()
        {
            /* if the provided list is NULL, then there are zero items in (terminates recursion) */
            if (itemList == null)
            {
                return 0;
            }

            /* add one to the total provided by the value of this function called on the remainder of the list */
            return (uint)itemList.Count;
        }

        /*Gets a specific element in list*/
        public Item GetItemByIndex(int index)
        {
            Item item = null;

            if (itemList == null || index >= itemList.Count || index < 0)
            {
                return item;
            }

            return itemList[index];
        }

    }
}
