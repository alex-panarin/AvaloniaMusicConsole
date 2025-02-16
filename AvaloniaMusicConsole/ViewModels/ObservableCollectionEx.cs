using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AvaloniaMusicConsole.ViewModels
{
    public class ObservableCollectionEx<TEntity>
        : ObservableCollection<TEntity>
        where TEntity : class
    {
        public ObservableCollectionEx()
            : base() 
        {
            
        }
        public ObservableCollectionEx(IEnumerable<TEntity> values)
            : base(values)
        {
            
        }
        class Locker : IDisposable
        {
            private readonly ObservableCollectionEx<TEntity> _collection;
            public Locker(ObservableCollectionEx<TEntity> collection)
            {
                _collection = collection;
                _collection.LockRaiseEvent = true;
            }
            public void Dispose()
            {
                _collection.LockRaiseEvent = false;
                _collection.RaiseCollectionChanged();
            }
        }

        private bool LockRaiseEvent { get; set; }
        private void RaiseCollectionChanged()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void AddRange(IEnumerable<TEntity> collection)
        {
            if (collection == null) return;

            using (IEnumerator<TEntity> enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    this.Add(enumerator.Current);
                }
            }
        }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (LockRaiseEvent) return;
            base.OnCollectionChanged(e);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (LockRaiseEvent) return;
            base.OnPropertyChanged(e);
        }
        public IDisposable LockChangedEvent()
        {
            return new Locker(this);
        }
    }
}
