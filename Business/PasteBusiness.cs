using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Business
{
    public class PasteBusiness
    {
        private PasteContext pasteContext;

        public List<Paste> GetAll()
        {
            using (pasteContext = new PasteContext())
            {
                return pasteContext.Pastes.Where(x => !x.IsHidden && DateTime.Now < x.Expieres).ToList();
            }
        }

        public Paste Get(int? id)
        {
            if(id == null)
            {
                throw new ArgumentException("Id cannot be null");
            }
            using (pasteContext = new PasteContext())
            {
                var result = pasteContext.Pastes.Find(id);
                if(result == null)
                {
                    throw new KeyNotFoundException("No such a record in the DB");
                }
                return result;
            }
        }

        public void Add(Paste paste)
        {
            using (pasteContext = new PasteContext())
            {
                pasteContext.Pastes.Add(paste);
                pasteContext.SaveChanges();
            }
        }

        public void Update(Paste paste)
        {
            using (pasteContext = new PasteContext())
            {
                var item = pasteContext.Pastes.Find(paste.Id);
                if (item != null)
                {
                    pasteContext.Entry(item).CurrentValues.SetValues(paste);
                    pasteContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (pasteContext = new PasteContext())
            {
                var paste = pasteContext.Pastes.Find(id);
                if (paste != null)
                {
                    pasteContext.Pastes.Remove(paste);
                    pasteContext.SaveChanges();
                }
            }
        }
    }
}
