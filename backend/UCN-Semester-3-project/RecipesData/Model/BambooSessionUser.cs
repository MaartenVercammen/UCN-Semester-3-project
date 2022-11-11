using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Model
{
    public class BambooSessionUser
    {
        public Guid sessionId { get; set; }
        public User user { get; set; }

        public BambooSessionUser(Guid sessionId, User user)
        {
            this.sessionId = sessionId;
            this.user = user;
        }
    }
}