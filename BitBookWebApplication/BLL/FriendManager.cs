using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookWebApplication.DAL;
using BitBookWebApplication.Models;

namespace BitBookWebApplication.BLL
{
    public class FriendManager
    {
        FriendGateway friendGateway=new FriendGateway();

        public List<Person> GetPersonInformation(string name)
        {
            return friendGateway.GetPersonInformation(name);
        }

        public Friend IsFriendExist(int rId, int aId)
        {
            return friendGateway.IsFriendExist(rId, aId);
        }
        public Friend IsFriendExists(int rId, int aId)
        {
            return friendGateway.IsFriendExists(rId, aId);
        }

        public bool SetFriend(int rId, int aId)
        {
            return friendGateway.SetFriend(rId, aId);
        }

        public bool UpdateFriend(Friend friend)
        {
            return friendGateway.UpdateFriend(friend);
        }

        public bool DeleteFriend(Friend friend)
        {
            return friendGateway.DeleteFriend(friend);
        }

        public bool DeleteFriendByBothID(Friend friend)
        {
            return friendGateway.DeleteFriendByBothID(friend);
        }
    }
}