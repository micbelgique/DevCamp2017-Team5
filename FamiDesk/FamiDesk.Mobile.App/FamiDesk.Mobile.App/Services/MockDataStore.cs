using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using FamiDesk.Mobile.App.Models;

namespace FamiDesk.Mobile.App.Services
{
    public abstract class MockDataStore<T> : IDataStore<T> where T : BaseDataObject
    {
        protected bool isInitialized;
        protected List<T> items = new List<T>();

        public async Task<bool> AddItemAsync(T item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            await InitializeAsync();

            var _item = items.Where((T arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(T item)
        {
            await InitializeAsync();

            var _item = items.Where((T arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate, bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items.Where(predicate.Compile()));
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public abstract Task InitializeAsync();

        //{
        //    if (isInitialized)
        //        return;

        //    var _items = new List<Item>
        //    {
        //        new Item { Id = Guid.NewGuid().ToString(), Text = "Buy some cat food", Description="The cats are hungry"},
        //        new Item { Id = Guid.NewGuid().ToString(), Text = "Learn F#", Description="Seems like a functional idea"},
        //        new Item { Id = Guid.NewGuid().ToString(), Text = "Learn to play guitar", Description="Noted"},
        //        new Item { Id = Guid.NewGuid().ToString(), Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
        //        new Item { Id = Guid.NewGuid().ToString(), Text = "Complete holiday shopping", Description="Keep it a secret!"},
        //        new Item { Id = Guid.NewGuid().ToString(), Text = "Finish a todo list", Description="Done"},
        //    };

        //    foreach (T item in _items)
        //    {
        //        items.Add(item);
        //    }

        //    isInitialized = true;
        //}
    }
}
