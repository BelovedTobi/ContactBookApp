//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ContactBookApp
//{
//    class ContactRepository
//    {
//    }
//}

using System;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ContactBookApp
{
    public class ContactRepository
    {
        SQLiteAsyncConnection database;

        public ContactRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ContactClass>().Wait();
        }

        public async Task<int> CreateContact(ContactClass contact)
        {
            return await database.InsertAsync(contact);
        }

        public async Task<int> DeleteContact(ContactClass contact)
        {
            return await database.DeleteAsync(contact);
        }

        public async Task<int> UpdateContact(ContactClass contact)
        {
            //database.GetAsync<ContactClass>(id)
            ContactClass editedContact = await database.Table<ContactClass>().Where(c=>c.Id == contact.Id).FirstOrDefaultAsync();
            // ContactClass editedContact = await database.GetAsync<ContactClass>(contact.Id);

            //if (editedContact != null)
            //{
            //    editedContact.Name = contact.Name;
            //    editedContact.Phone = contact.Phone;
            //    editedContact.ImagePath = contact.ImagePath;
            //    editedContact.Email = contact.Email;

            //    return await database.UpdateAsync(editedContact);
            //}

            //else
            {

                int uu = 99;
                return await database.UpdateAsync(contact);
            }

           //
           
        }

        public async Task<ContactClass> GetContact(int id)
        {
            return await database.GetAsync<ContactClass>(id);
        }

        public async Task<List<ContactClass>> GetAllContact()
        {
            return await database.Table<ContactClass>().ToListAsync();
        }


    }
}
