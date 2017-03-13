using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookWebApplication.BLL;
using BitBookWebApplication.Models;

namespace BitBookWebApplication.Controllers
{
    public class SearchController : Controller
    {
        FriendManager friendManager = new FriendManager();
        HomeManager homeManager=new HomeManager();
        public ActionResult Index(string name)
        {
            Authentication authentication = (Authentication)Session["authentication"];
            if (authentication == null)
            {
                TempData["msg"] = "<script>alert('Please Log in First');</script>";
                return RedirectToAction("SignUpView", "SignUp");
            }
            ViewBag.SearchName = name;
            ViewBag.Name = authentication.Name;
            ViewBag.SignupID = authentication.Id;
            ViewBag.Password = authentication.Password;
            ViewBag.NotificationList = homeManager.GetAllNotification(authentication.Id);
            List<FriendShip> persons = GetFriendShipList(name, authentication);
            ViewBag.PersonList = persons;
            ViewBag.Message = null;
            if (persons==null)
            {
                ViewBag.Message = "No Person Found of that Name";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(Search search)
        {
            Authentication authentication = (Authentication)Session["authentication"];
            if (authentication == null)
            {
                TempData["msg"] = "<script>alert('Please Log in First');</script>";
                return RedirectToAction("SignUpView", "SignUp");
            }
            ViewBag.SearchName = search.SearchDetail;
            ViewBag.Name = authentication.Name;
            ViewBag.SignupID = authentication.Id;
            ViewBag.Password = authentication.Password;
            ViewBag.NotificationList = homeManager.GetAllNotification(authentication.Id);
            List<FriendShip> persons = GetFriendShipList(search.SearchDetail, authentication);
            ViewBag.PersonList = persons;
            ViewBag.Message = null;
            if (persons == null)
            {
                ViewBag.Message = "No Person Found of that Name";
            }
            return View();
        }

        public ActionResult DoFriend(int id)
        {
            Authentication authentication = (Authentication)Session["authentication"];
            if (authentication == null)
            {
                TempData["msg"] = "<script>alert('Please Log in First');</script>";
                return RedirectToAction("SignUpView", "SignUp");
            }
            ViewBag.Name = authentication.Name;
            ViewBag.SignupID = authentication.Id;
            ViewBag.Password = authentication.Password;
            ViewBag.NotificationList = homeManager.GetAllNotification(authentication.Id);
            if (friendManager.SetFriend(authentication.Id,id))
            {
                ViewBag.Message = "Friend Request Send sucessfully";
            }
            else
            {
                ViewBag.Message = "Friend Request Send failed";
            }
            return View("Index");
        }
        public ActionResult RemoveFriend(int id)
        {
            Authentication authentication = (Authentication)Session["authentication"];
            if (authentication == null)
            {
                TempData["msg"] = "<script>alert('Please Log in First');</script>";
                return RedirectToAction("SignUpView", "SignUp");
            }
            ViewBag.Name = authentication.Name;
            ViewBag.SignupID = authentication.Id;
            ViewBag.Password = authentication.Password;
            ViewBag.NotificationList = homeManager.GetAllNotification(authentication.Id);
            Friend friend=new Friend();
            friend.ID = id;
            if (friendManager.DeleteFriend(friend))
            {
                ViewBag.Message = "Sucessfully Unfriend";
            }
            else
            {
                ViewBag.Message = "Failed to Unfriend";
            }
            return View("Index");
        }
        public ActionResult UpdateFriend(int id)
        {
            Authentication authentication = (Authentication)Session["authentication"];
            if (authentication == null)
            {
                TempData["msg"] = "<script>alert('Please Log in First');</script>";
                return RedirectToAction("SignUpView", "SignUp");
            }
            ViewBag.Name = authentication.Name;
            ViewBag.SignupID = authentication.Id;
            ViewBag.Password = authentication.Password;
            ViewBag.NotificationList = homeManager.GetAllNotification(authentication.Id);
            Friend friend = new Friend();
            friend.ID = id;
            if (friendManager.UpdateFriend(friend))
            {
                ViewBag.Message = "Sucessfully friend request Accepted";
            }
            else
            {
                ViewBag.Message = "Failed to friend request Accept";
            }
            return View("Index");
        }





        public List<FriendShip> GetFriendShipList(string name,Authentication authentication)
        {
            List<Person> persons = friendManager.GetPersonInformation(name);
            List<FriendShip> friendShips=new List<FriendShip>();
            if (persons!=null)
            {
                foreach (Person person in persons)
                {
                    Friend rFriend = friendManager.IsFriendExist(authentication.Id, person.ID);
                    Friend aFriend = friendManager.IsFriendExists(authentication.Id, person.ID);
                    if (rFriend!=null)
                    {
                        FriendShip friendShip=new FriendShip();
                        friendShip.ID = person.ID;
                        friendShip.FriendshipID = rFriend.ID;
                        friendShip.Name = person.Name;
                        friendShip.Email = person.Email;
                        if (rFriend.Status==1)
                        {
                            friendShip.Status = "Friend Request Already send";
                            friendShip.StatusID = 1;
                        }
                        else if (rFriend.Status == 2)
                        {
                            friendShip.Status = "friend";
                            friendShip.StatusID = 3;
                        }
                        else
                        {
                            friendShip.Status = "error in determining friend";
                            friendShip.StatusID = 0;
                        }
                        friendShips.Add(friendShip);
                        
                    }
                    else if (aFriend != null)
                    {
                        FriendShip friendShip = new FriendShip();
                        friendShip.ID = person.ID;
                        friendShip.FriendshipID = aFriend.ID;
                        friendShip.Name = person.Name;
                        friendShip.Email = person.Email;
                        
                        if (aFriend.Status == 1)
                        {
                            friendShip.Status = "Accept Friend Request";
                            friendShip.StatusID = 2;
                        }
                        else if (aFriend.Status == 2)
                        {
                            friendShip.Status = "friend";
                            friendShip.StatusID = 3;
                        }
                        else
                        {
                            friendShip.Status = "error in determining friend";
                            friendShip.StatusID = 0;
                        }
                        friendShips.Add(friendShip);
                    }
                    else
                    {
                        FriendShip friendShip = new FriendShip();
                        friendShip.ID = person.ID;
                        friendShip.FriendshipID = 0;
                        friendShip.Name = person.Name;
                        friendShip.Email = person.Email;
                        friendShip.StatusID = 0;
                        friendShip.Status = "Send Friend Request";
                        friendShips.Add(friendShip);
                    }
                }
            }
            return friendShips;
        }
	}
}