using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using BitBookWebApplication.App_Start.DAL;
using BitBookWebApplication.Models;

namespace BitBookWebApplication.DAL
{
    public class EditGateway:CommonGateway
    {
        public AllPersonalInformation GetPersonInformation(int id)
        {
            AllPersonalInformation person = new AllPersonalInformation();
            GenarateConnection();
            string query = "SELECT * FROM Person WHERE ID =@SignupID";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("@SignupID", SqlDbType.VarChar);
            Command.Parameters["@SignupID"].Value = id;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            if (Reader.HasRows)
            {
                person.AdditionalInfoID = Convert.ToInt32(Reader["ID"].ToString());
                person.Name = Reader["Name"].ToString();
                person.Email = Reader["Email"].ToString();
                person.Password = Reader["Password"].ToString();
                person.ConfirmPassword = Reader["Password"].ToString();
                person.MobileNumber = Reader["MobileNo"].ToString();
                person.DateOfBirth = Reader["DateOfBirth"].ToString();
                person.Gender = Convert.ToInt32(Reader["Gender"].ToString());
                person.AdditionalInfoID = Convert.ToInt32(Reader["ProfileID"].ToString());
            }
            Connection.Close();
            return person;
        }
        public AllPersonalInformation GetAdditionInformation(int id)
        {
            AllPersonalInformation person = new AllPersonalInformation();
            GenarateConnection();
            string query = "SELECT * FROM PersonalInformation WHERE SignupID =@ID";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("@ID", SqlDbType.VarChar);
            Command.Parameters["@ID"].Value = id;
            Connection.Open();
            Reader = Command.ExecuteReader();
            Reader.Read();
            if (Reader.HasRows)
            {
                person.AdditionalInfoID = Convert.ToInt32(Reader["ID"].ToString());
                person.ProfilePhotoId = Convert.ToInt32(Reader["ProfilePhoto"].ToString());
                person.CoverPhotoId = Convert.ToInt32(Reader["CoverPhoto"].ToString());
                person.AboutMe = Reader["AboutMe"].ToString();
                person.Religion = Reader["Religion"].ToString();
                person.EducationID = Convert.ToInt32(Reader["EducationID"].ToString());
                person.JobID = Convert.ToInt32(Reader["JobID"].ToString());                
                person.AreaOfInterest = Reader["AreaOfInterest"].ToString();
                person.Address = Reader["Address"].ToString();
            }
            Connection.Close();
            return person;
        }
        public bool UpdatePersonInformation(AllPersonalInformation allPersonalInformation)
        {
            GenarateConnection();
            using (Connection)
            {
                Connection.Open();
                string query =
                    "UPDATE Person SET Name=@Name,MobileNo=@MobileNo,Password=@Password,DateOfBirth=@DateOfBirth,Gender=@Gender WHERE ID=@ID;";
                Command = new SqlCommand(query, Connection);
                Command.Parameters.Clear();

                Command.Parameters.Add("@ID", SqlDbType.Int);
                Command.Parameters["@ID"].Value = allPersonalInformation.SignupID;
                Command.Parameters.Add("@Name", SqlDbType.VarChar);
                Command.Parameters["@Name"].Value = allPersonalInformation.Name;
                Command.Parameters.Add("@MobileNo", SqlDbType.VarChar);
                Command.Parameters["@MobileNo"].Value = allPersonalInformation.MobileNumber;
                Command.Parameters.Add("@Password", SqlDbType.VarChar);
                Command.Parameters["@Password"].Value = allPersonalInformation.NewPassword;
                Command.Parameters.Add("@DateOfBirth", SqlDbType.VarChar);
                Command.Parameters["@DateOfBirth"].Value = allPersonalInformation.DateOfBirth;
                Command.Parameters.Add("@Gender", SqlDbType.Int);
                Command.Parameters["@Gender"].Value = allPersonalInformation.Gender;       

                try
                {
                    Command.ExecuteNonQuery();
                    Connection.Close();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception("Error While Entering data into database");
                }
            }
        }
        public bool UpdateAdditionalInformation(AllPersonalInformation allPersonalInformation)
        {
            GenarateConnection();
            using (Connection)
            {
                Connection.Open();
                string query =
                    "UPDATE PersonalInformation SET ProfilePhoto=@ProfilePhoto,CoverPhoto=@CoverPhoto,AboutMe=@AboutMe,Religion=@Religion,EducationID=@EducationID,JobID=@JobID,AreaOfInterest=@AreaOfInterest,Address=@Address WHERE SignupID=@SignupID;";
                Command = new SqlCommand(query, Connection);
                Command.Parameters.Clear();

                Command.Parameters.Add("@SignupID", SqlDbType.Int);
                Command.Parameters["@SignupID"].Value = allPersonalInformation.SignupID;
                Command.Parameters.Add("@ProfilePhoto", SqlDbType.Int);
                Command.Parameters["@ProfilePhoto"].Value = allPersonalInformation.ProfilePhotoId;
                Command.Parameters.Add("@CoverPhoto", SqlDbType.Int);
                Command.Parameters["@CoverPhoto"].Value = allPersonalInformation.CoverPhotoId;
                Command.Parameters.Add("@AboutMe", SqlDbType.VarChar);
                Command.Parameters["@AboutMe"].Value = allPersonalInformation.AboutMe;
                Command.Parameters.Add("@Religion", SqlDbType.VarChar);
                Command.Parameters["@Religion"].Value = allPersonalInformation.Religion;
                Command.Parameters.Add("@EducationID", SqlDbType.Int);
                Command.Parameters["@EducationID"].Value = allPersonalInformation.EducationID;
                Command.Parameters.Add("@JobID", SqlDbType.Int);
                Command.Parameters["@JobID"].Value = allPersonalInformation.JobID;
                Command.Parameters.Add("@AreaOfInterest", SqlDbType.VarChar);
                Command.Parameters["@AreaOfInterest"].Value = allPersonalInformation.AreaOfInterest;
                Command.Parameters.Add("@Address", SqlDbType.VarChar);
                Command.Parameters["@Address"].Value = allPersonalInformation.Address;

                try
                {
                    Command.ExecuteNonQuery();
                    Connection.Close();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception("Error While Entering data into database");
                }
            }
        }
        public Photo GetProfilePhotoByID(int ID)
        {
            Photo photo = new Photo();
            GenarateConnection();
            string query = "SELECT * FROM ProfilePhoto WHERE ID=@ID ORDER BY ID DESC";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("@ID", SqlDbType.VarChar);
            Command.Parameters["@ID"].Value = ID;
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {

                    photo.ID = Convert.ToInt32(Reader["ID"].ToString());
                    photo.SignupID = Convert.ToInt32(Reader["SignupID"].ToString());
                    photo.PhotoInByte = (Reader["Photo"]) as byte[];
                    photo.PhotoInString = Convert.ToBase64String(photo.PhotoInByte);
                    photo.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                }
            }
            Connection.Close();
            return photo;
        }
        public Photo GetCoverPhotoByID(int ID)
        {
            Photo photo = new Photo();
            GenarateConnection();
            string query = "SELECT * FROM CoverPhoto WHERE ID=@ID ORDER BY ID DESC";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("@ID", SqlDbType.VarChar);
            Command.Parameters["@ID"].Value = ID;
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {

                    photo.ID = Convert.ToInt32(Reader["ID"].ToString());
                    photo.SignupID = Convert.ToInt32(Reader["SignupID"].ToString());
                    photo.PhotoInByte = (Reader["Photo"]) as byte[];
                    photo.PhotoInString = Convert.ToBase64String(photo.PhotoInByte);
                    photo.DateTime = Convert.ToDateTime(Reader["DateTime"].ToString());
                }
            }
            Connection.Close();
            return photo;
        }
    }
}