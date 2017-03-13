using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookWebApplication.App_Start.DAL;
using BitBookWebApplication.Models;

namespace BitBookWebApplication.DAL
{
    public class NotificationGateway:CommonGateway
    {
        public List<int> GetAllFriendList(int id)
        {
            List<int> friendList = new List<int>();
            GenarateConnection();
            string query = "select * from Friend where R_PersonID=@ID or A_PersonID=@ID and Status=@Status;";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("@ID", SqlDbType.VarChar);
            Command.Parameters["@ID"].Value = id;
            Command.Parameters.Add("@Status", SqlDbType.VarChar);
            Command.Parameters["@Status"].Value = 2;

            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    int aId = Convert.ToInt32(Reader["R_PersonID"].ToString());
                    int bId = Convert.ToInt32(Reader["A_PersonID"].ToString());
                    if (aId == id)
                    {
                        friendList.Add(bId);
                    }
                    else
                    {
                        friendList.Add(aId);
                    }

                }
            }
            Connection.Close();
            return friendList;
        }
          
        public List<Photo> GetAllProfilePhotoByID(int signupID)
        {
            List<Photo> photos = new List<Photo>();
            GenarateConnection();
            string query = "SELECT * FROM ProfilePhoto WHERE SignupID=@SignupID ORDER BY ID DESC";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("@SignupID", SqlDbType.VarChar);
            Command.Parameters["@SignupID"].Value = signupID;
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Photo photo = new Photo();
                    photo.ID = Convert.ToInt32(Reader["ID"].ToString());
                    photo.SignupID = Convert.ToInt32(Reader["SignupID"].ToString());
                    photo.PhotoInByte = (Reader["Photo"]) as byte[];
                    if (photo.PhotoInByte!=null)
                    {
                        photo.PhotoInString = Convert.ToBase64String(photo.PhotoInByte);
                    }
                    photo.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                    photos.Add(photo);
                }
            }
            Connection.Close();
            return photos;
        }
        public List<Photo> GetAllCoverPhotoByID(int signupID)
        {
            List<Photo> photos = new List<Photo>();
            GenarateConnection();
            string query = "SELECT * FROM CoverPhoto WHERE SignupID=@SignupID ORDER BY ID DESC;";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("@SignupID", SqlDbType.VarChar);
            Command.Parameters["@SignupID"].Value = signupID;
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Photo photo = new Photo();
                    photo.ID = Convert.ToInt32(Reader["ID"].ToString());
                    photo.SignupID = Convert.ToInt32(Reader["SignupID"].ToString());
                    photo.PhotoInByte = (Reader["Photo"]) as byte[];
                    if (photo.PhotoInByte != null)
                    {
                        photo.PhotoInString = Convert.ToBase64String(photo.PhotoInByte);
                    }
                    photo.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                    photos.Add(photo);
                }
            }
            Connection.Close();
            return photos;
        }
        public List<PostInfo> GetAllPostInformationByID(int signupId)
        {
            List<PostInfo> postInfos = new List<PostInfo>();
            GenarateConnection();
            string query = "SELECT * FROM Post WHERE SignUpID=@SignUpID ORDER BY ID DESC;";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("@SignUpID", SqlDbType.VarChar);
            Command.Parameters["@SignUpID"].Value = signupId;
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    PostInfo postInfo = new PostInfo();
                    postInfo.Id = Convert.ToInt32(Reader["ID"].ToString());
                    postInfo.PostDetail = Reader["Detail"].ToString();
                    postInfo.NoOfLike = Convert.ToInt32(Reader["NoOfLike"].ToString());
                    postInfo.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                    postInfo.SignUpID = Convert.ToInt32(Reader["SignUpID"].ToString());
                    postInfo.PostPhotoInByte = Reader["Photo"] as byte[];
                    if (postInfo.PostPhotoInByte != null)
                    {
                        postInfo.PostPhotoInString = Convert.ToBase64String(postInfo.PostPhotoInByte);
                    }
                    postInfos.Add(postInfo);
                }
            }
            Connection.Close();
            return postInfos;
        }
        public List<PostLike> GetPostLikeList(int owenerId)
        {
            List<PostLike> likeList = new List<PostLike>();
            GenarateConnection();
            using (Connection)
            {
                Connection.Open();
                string query = "select * from LikePost where OwenerID=@OwenerID ORDER BY ID DESC;";
                Command = new SqlCommand(query, Connection);
                Command.Parameters.Clear();

                Command.Parameters.Add("@OwenerID", SqlDbType.VarChar);
                Command.Parameters["@OwenerID"].Value = owenerId;
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        PostLike postLike=new PostLike();
                        postLike.ID = Convert.ToInt32(Reader["ID"].ToString());
                        postLike.PostID = Convert.ToInt32(Reader["PostID"].ToString());
                        postLike.OwenerID = Convert.ToInt32(Reader["OwenerID"].ToString());
                        postLike.SignupID = Convert.ToInt32(Reader["SignUpID"].ToString());
                        postLike.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                        likeList.Add(postLike);
                    }
                }
                return likeList;
            }
        }
        public List<PostLike> GetMutualPostLikeList(List<int> friendList)
        {
            List<PostLike> likeList = new List<PostLike>();
            if (friendList.Count>1)
            {
                GenarateConnection();
                using (Connection)
                {
                    int i = 0;
                    Connection.Open();
                    string query = "select * from LikePost where ";
                    foreach (int owenerId in friendList)
                    {                       
                        foreach (int signUpId in friendList)
                        {
                            if (i != 0)
                            {
                                query += "or ";
                            }
                            i = i + 1;
                            query += "(OwenerID='"+owenerId+"' and SignUpID='"+signUpId+"')";
                        }
                    }                   
                    query+=" ORDER BY ID DESC;";
                    Command = new SqlCommand(query, Connection);
                    Command.Parameters.Clear();
                    Reader = Command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            PostLike postLike = new PostLike();
                            postLike.ID = Convert.ToInt32(Reader["ID"].ToString());
                            postLike.PostID = Convert.ToInt32(Reader["PostID"].ToString());
                            postLike.OwenerID = Convert.ToInt32(Reader["OwenerID"].ToString());
                            postLike.SignupID = Convert.ToInt32(Reader["SignUpID"].ToString());
                            postLike.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                            likeList.Add(postLike);
                        }
                    }
                    return likeList;
                }
            }
            else
            {
                return likeList;
            }
            
        }
        public List<PostLike> GetPostCommentList(int owenerId)
        {
            List<PostLike> likeList = new List<PostLike>();
            GenarateConnection();
            using (Connection)
            {
                Connection.Open();
                string query = "select * from Comment where OwenerID=@OwenerID ORDER BY ID DESC;";
                Command = new SqlCommand(query, Connection);
                Command.Parameters.Clear();

                Command.Parameters.Add("@OwenerID", SqlDbType.VarChar);
                Command.Parameters["@OwenerID"].Value = owenerId;
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        PostLike postLike = new PostLike();
                        postLike.ID = Convert.ToInt32(Reader["ID"].ToString());
                        postLike.PostID = Convert.ToInt32(Reader["PostID"].ToString());
                        postLike.OwenerID = Convert.ToInt32(Reader["OwenerID"].ToString());
                        postLike.SignupID = Convert.ToInt32(Reader["PersonID"].ToString());
                        postLike.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                        likeList.Add(postLike);
                    }
                }
                return likeList;
            }
        }

        public List<PostLike> GetMutualPostCommentList(List<int> friendList)
        {
            List<PostLike> likeList = new List<PostLike>();
            if (friendList.Count > 1)
            {
                GenarateConnection();
                using (Connection)
                {
                    int i = 0;
                    Connection.Open();
                    string query = "select * from Comment where ";
                    foreach (int owenerId in friendList)
                    {                       
                        foreach (int signUpId in friendList)
                        {
                            if (i != 0)
                            {
                                query += "or ";
                            }
                            i = i + 1;
                            query += "(OwenerID='" + owenerId + "' and PersonID='" + signUpId + "')";
                        }
                    }    
                    query += " ORDER BY ID DESC;";
                    Command = new SqlCommand(query, Connection);
                    Command.Parameters.Clear();
                    Reader = Command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            PostLike postLike = new PostLike();
                            postLike.ID = Convert.ToInt32(Reader["ID"].ToString());
                            postLike.PostID = Convert.ToInt32(Reader["PostID"].ToString());
                            postLike.OwenerID = Convert.ToInt32(Reader["OwenerID"].ToString());
                            postLike.SignupID = Convert.ToInt32(Reader["PersonID"].ToString());
                            postLike.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                            likeList.Add(postLike);
                        }
                    }
                    return likeList;
                }
            }
            else
            {
                return likeList;
            }
        }

        public List<PostLike> GetAllFriendNotificationList(int id)
        {
            List<PostLike> friendList = new List<PostLike>();
            GenarateConnection();
            string query = "select * from Friend where R_PersonID=@ID or A_PersonID=@ID and Status=@Status ORDER BY ID DESC;";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("@ID", SqlDbType.VarChar);
            Command.Parameters["@ID"].Value = id;
            Command.Parameters.Add("@Status", SqlDbType.VarChar);
            Command.Parameters["@Status"].Value = 2;

            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    PostLike postLike=new PostLike();
                    postLike.ID=Convert.ToInt32(Reader["ID"].ToString());
                    int aId = Convert.ToInt32(Reader["R_PersonID"].ToString());
                    int bId = Convert.ToInt32(Reader["A_PersonID"].ToString());
                    if (aId == id)
                    {
                        postLike.SignupID = bId;
                    }
                    else
                    {
                        postLike.SignupID = aId;
                    }
                    postLike.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                    friendList.Add(postLike);

                }
            }
            Connection.Close();
            return friendList;
        }
        public List<PostLike> GetAllFriendRequestList(int id)
        {
            List<PostLike> friendList = new List<PostLike>();
            GenarateConnection();
            string query = "select * from Friend where A_PersonID=@ID and Status=@Status ORDER BY ID DESC;";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("@ID", SqlDbType.VarChar);
            Command.Parameters["@ID"].Value = id;
            Command.Parameters.Add("@Status", SqlDbType.VarChar);
            Command.Parameters["@Status"].Value = 1;

            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    PostLike postLike = new PostLike();
                    postLike.ID = Convert.ToInt32(Reader["ID"].ToString());
                    postLike.SignupID = Convert.ToInt32(Reader["R_PersonID"].ToString());
                    postLike.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                    friendList.Add(postLike);

                }
            }
            Connection.Close();
            return friendList;
        }        
    }
}