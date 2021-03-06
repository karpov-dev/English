﻿using System;
using System.Collections.ObjectModel;

namespace Coursework.Models.Classes.User.WordCollections
{
    [Serializable]
    class WordCollection
    {
        private ObservableCollection<OneCollection> _collections;


        public WordCollection()
        {
            _collections = new ObservableCollection<OneCollection>();
        }


        public OneCollection GetOneCollection(int index) => _collections[index];
        public ObservableCollection<OneCollection> GetCollections() => _collections;
        public void AddCollection(OneCollection newCollection) => _collections.Add(newCollection);
        public void DeleteCollection(OneCollection deletedCollection) => _collections.Remove(deletedCollection);
        public int Count => _collections.Count;
        public void DeleteCollections(int[] collectionIndexes)
        {
            for ( int i = 0; i < collectionIndexes.Length; i++ )
            {
                _collections.RemoveAt(collectionIndexes[i]);
            }
        }
        public void DeleteAllCollections()
        {
            _collections.Clear();
        }
    }
}
