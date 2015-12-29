using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using ESRI.ArcGIS.Carto;


namespace AE_Environment.Utilities
{
           /// <summary>
        /// Implementation of interface IMaps which is eventually a collection of Maps
        /// </summary>
        public class Maps : IMaps,IDisposable
        {
        //class member - using internally an ArrayList to manage the Maps collection
           private ArrayList pArray = null;
        #region class constructor
            public Maps()
            {
                pArray = new ArrayList();
            }
        #endregion
        #region IDisposable Members
        /// <summary>
        /// Dispose the collection
        /// </summary>
        public void Dispose()
        {
        if (pArray != null)
        {
            pArray.Clear();
            pArray = null;
        }
        }
        #endregion
        #region IMaps Members
        /// <summary>
        /// Remove the Map at the given index
        /// </summary>
        /// <param name="Index"></param>
        public void RemoveAt(int Index)
        {
         if (Index > pArray.Count || Index < 0)
            throw new Exception("Maps::RemoveAt:\r\nIndex is out of range!");
            pArray.RemoveAt(Index);
        }
        /// <summary>
        /// Reset the Maps array
        /// </summary>
        public void Reset()
        {
           pArray.Clear();
        }
        /// <summary>
        /// Get the number of Maps in the collection//属性get/set/
        /// </summary>
        public int Count
        {
            get
            {
                 return pArray.Count;
            }
        }
        public IMap get_Item(int Index)
        {
            if (Index > pArray.Count || Index < 0)
               throw new Exception("Maps::get_Item:\r\nIndex is out of range!");
            return pArray[Index] as IMap;
        }
        /// <summary>
        /// Remove the instance of the given Map
        /// </summary>
        /// <param name="Map"></param>
        public void Remove(IMap Map)
        {
            pArray.Remove(Map);
         }
        /// <summary>
        /// Create a new Map， add it to the collection and return it to the caller
        /// </summary>
        /// <returns></returns>
        /// 
            public IMap Create()
        {
            IMap newMap = new MapClass();
            pArray.Add(newMap);
            return newMap;
        }
        /// <summary>
        /// Add the given Map to the collection
        /// </summary>
        /// <param name="Map"></param>
        public void Add(IMap Map)
        {
            if (Map == null)
               throw new Exception("Maps::Add:\r\nNew Map is mot initialized!");
               pArray.Add(Map);
        }
        #endregion
        }

}
