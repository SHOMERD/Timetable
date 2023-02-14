using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Todo;
using Timetable.Models;

namespace Timetable.Data
{
    public class NotesDB
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<NotesDB> Instance = new AsyncLazy<NotesDB>(async () =>
        {
            var instance = new NotesDB();
            CreateTableResult result = await Database.CreateTableAsync<Note>();
            return instance;
        });

        public NotesDB()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Note>> GetItemsAsync()
        {
            return Database.Table<Note>().ToListAsync();
        }


        public Task<Note> GetItemAsync(int id)
        {
            return Database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Note item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Note item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
