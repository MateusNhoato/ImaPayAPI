using ImaPayAPI.Context;
using ImaPayAPI.Models;

namespace ImaPayAPI.Services.Token
{
    public class LinkUserToTokenService
    {
        private ImayPayContext _context;

        public LinkUserToTokenService(ImayPayContext context)
        {
            _context = context;
        }

        public void Link(string token, User user)
        {
            _context.userToken.Add(token, user);
        }
    }
}
