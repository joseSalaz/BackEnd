using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Models.ResponseResponse;
using Repository.Generic;

namespace Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {

        public List<Usuario> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public Usuario GetByUserName(string userName)
        {
            Usuario user = dbSet.Where(x => x.Username == userName).FirstOrDefault();
            return user;
        }

        public async Task<List<string>> GetNotificationTokensAsync() 
        { 
            return await dbSet.Where(u => !string.IsNullOrEmpty(u.NotificationToken)).Select(u => u.NotificationToken).ToListAsync(); 
        }
    }
}