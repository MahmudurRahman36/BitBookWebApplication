using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookWebApplication.DAL;
using BitBookWebApplication.Models;

namespace BitBookWebApplication.BLL
{
    public class ProfileManager
    {
        NotificationGateway notificationGateway=new NotificationGateway();

        public List<Photo> GetAllProfilePhotoByID(int signupID)
        {
            return notificationGateway.GetAllProfilePhotoByID(signupID);
        }

        public List<Photo> GetAllCoverPhotoByID(int signupID)
        {
            return notificationGateway.GetAllCoverPhotoByID(signupID);
        }

        public List<PostInfo> GetAllPostPhotoByID(int signupId)
        {
            List<PostInfo> postInfos=notificationGateway.GetAllPostInformationByID(signupId);
            List<PostInfo> newPostInfos=new List<PostInfo>();
            foreach (PostInfo postInfo in postInfos)
            {
                if (postInfo.PostPhotoInString != null)
                {
                    newPostInfos.Add(postInfo);
                }
            }
            return newPostInfos;
        }
    }
}