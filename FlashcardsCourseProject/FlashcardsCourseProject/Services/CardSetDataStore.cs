using FlashcardsCourseProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlashcardsCourseProject.Services
{
    public class CardSetDataStore : IDataStore<CardSet>
    {
        private List<CardSet> _items;
        private ApplicationContext _db => DependencyService.Get<ApplicationContext>();
        public CardSetDataStore()
        {
            _db.Database.EnsureCreated();
        }

        public async Task<bool> AddItemAsync(CardSet item)
        {
            _items.Add(item);
            _db.CardSet.Add(item);
            _db.SaveChanges();
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CardSet item)
        {
            var oldItem = _items.Where(a => a.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);
            _db.CardSet.Update(item);
            _db.SaveChanges();


            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.Where(a => a.Id == id).FirstOrDefault();
            _items.Remove(oldItem);
            _db.CardSet.Remove(oldItem);
            _db.SaveChanges();


            return await Task.FromResult(true);
        }

        public async Task<CardSet> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(a => a.Id == id));
        }

        public async Task<IEnumerable<CardSet>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == true)
            {
                _items = _db.CardSet.ToList();
            }
            return await Task.FromResult(_items);
        }
    }
}