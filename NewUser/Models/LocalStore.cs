using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

namespace NewUser.Models
{
    public class LocalStore
    {
        const string RECENT_USER = "NewUserKey";

        //Lazy initalization that is thread safe
        static readonly Lazy<LocalStore> lazy = new Lazy<LocalStore>(() => new LocalStore());
        public static LocalStore SharedInstance { get { return lazy.Value; } }

        //Get user data from DB
        public async Task<List<Registration>> GetUserAsync()
        {
            List<Registration> list = await BlobCache.UserAccount.GetObject<List<Registration>>(RECENT_USER)
                                  .Catch(Observable.Return(new List<Registration>()));
            return list;
        }

        //Set to DB
        public async Task SetUserAsync(List<Registration> users)
        {
            await BlobCache.UserAccount.InsertObject(RECENT_USER, users);
        }
    }
}
