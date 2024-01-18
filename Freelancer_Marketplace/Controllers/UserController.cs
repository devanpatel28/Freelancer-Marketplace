using Freelancer_Marketplace.Entity;
using Freelancer_Marketplace.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Data.Entity;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Freelancer_Marketplace.Controllers
{
    public class UserController : Controller
    {
        private Freelancer_Marketplace_DBEntities db = new Freelancer_Marketplace_DBEntities();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Username"] == null)
            {
                Session["OriginalUrl"] = filterContext.HttpContext.Request.Url.ToString();

                filterContext.Result = RedirectToAction("Index", "Login");
            }
        }

        // GET: User
        public ActionResult Index()
        {
            List<ProjectViewModel> projects;

            try
            {
                using (var db = new Freelancer_Marketplace_DBEntities())
                {
                    // Execute the query using LINQ to Entities
                    projects = (from p in db.projects
                                         where !db.takenprojects.Any(tp => tp.project_id == p.project_id)
                                         join u in db.usersdatas on p.client_id equals u.userid
                                         select new ProjectViewModel
                                         {
                                             ProjectId = p.project_id,
                                             ClientId = p.client_id,
                                             Title = p.title,
                                             BrandName = p.brand_name,
                                             ProjectDescription = p.project_description,
                                             Budget = p.budget,
                                             Deadline = p.deadline,
                                             ProjectStatus = p.project_status,
                                             DateCreated = p.date_created,
                                             ClientUsername = u.username,
                                         }).AsEnumerable()
                                  .Select(p => new ProjectViewModel
                                  {
                                      ProjectId = p.ProjectId,
                                      ClientId = p.ClientId,
                                      Title = p.Title,
                                      BrandName = p.BrandName,
                                      ProjectDescription = p.ProjectDescription,
                                      Budget = p.Budget,
                                      Deadline = p.Deadline,
                                      ProjectStatus = p.ProjectStatus,
                                      DateCreated = p.DateCreated,
                                      ClientUsername = p.ClientUsername,
                                      FormattedStartDate = (p.DateCreated != null) ? p.DateCreated.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : null,
                                      FormattedDeadline = (p.Deadline != null) ? p.Deadline.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : null
                                  }).ToList();


                    // Log information for debugging
                    Console.WriteLine($"Number of projects retrieved: {projects.Count}");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow the exception for further analysis
            }

            return View(projects);
            //return View();
        }
        public ActionResult BidTheProject(int? projectId)
        {
            if (projectId == null)
            {
                return RedirectToAction("Index");
            }

            int userId;
            int? sessionUserId = Session["UserId"] as int?;

            if (sessionUserId.HasValue)
            {
                userId = sessionUserId.Value;
            }
            else
            {
                // Handle the case where UserId in the session is not a valid int or is null
                return RedirectToAction("Index");
            }

            BidTheProjectViewModel bidmodel = new BidTheProjectViewModel
            {
                ProjectId = projectId.Value,
                Freelancer_id = userId,
            };

            return View(bidmodel);
            
        }

        [HttpPost]
        public ActionResult BidTheProject(BidTheProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Here, you can add the data to the database using your data access logic
                // For example, using Entity Framework or another data access framework

                // Assuming you have a data context (replace YourDbContext with your actual DbContext class)
                using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
                {
                    bid newBid = new bid
                    {
                        project_id = model.ProjectId,
                        freelancer_id = model.Freelancer_id,
                        bid_amount = model.Bid_amount,
                        proposal_text = model.Proposal_text,
                        isapproved = false,
                        bidstime = DateTime.Now // Assuming you want to store the current date and time
                    };

                    // Add the newBid to the database
                    dbContext.bids.Add(newBid);
                    dbContext.SaveChanges();
                }

                // Redirect to a success page or another action
                return RedirectToAction("Index");
            }

            // If the model is not valid, return to the form with validation errors
            return View(model);
        }
        public ActionResult MyBids()
        {
            if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int userId))
            {
                using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
                {
                    // Join projects and bids tables on project_id
                    var bidsForUser = (from bid in dbContext.bids
                                       join project in dbContext.projects on bid.project_id equals project.project_id
                                       where bid.freelancer_id == userId
                                       select new MyBidsViewModel
                                       {
                                           // Populate properties from both tables
                                           ProjectId = project.project_id,
                                           ClientId = project.client_id,
                                           Title = project.title,
                                           BrandName = project.brand_name,
                                           ProjectDescription = project.project_description,
                                           Budget = (decimal)project.budget,
                                           Deadline = (DateTime)project.deadline,
                                           ProjectStatus = project.project_status,
                                           DateCreated = (DateTime)project.date_created,

                                           BidId = bid.bid_id,
                                           FreelancerId = bid.freelancer_id,
                                           BidAmount = bid.bid_amount,
                                           ProposalText = bid.proposal_text,
                                           IsApproved = (bool)bid.isapproved,
                                           BidTime = (DateTime)bid.bidstime
                                       }).ToList();

                    return View(bidsForUser);
                }
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Myprojects()
        {
            // Check if the user is logged in
            if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int clientId))
            {
                // Use Entity Framework to retrieve projects where client_id matches Session["UserId"]
                using (var db = new Freelancer_Marketplace_DBEntities())
                {
                    var clientProjects = db.projects
                        .Where(p => p.client_id == clientId)
                        .Select(p => new ProjectViewModel
                        {
                            ProjectId = p.project_id,
                            ClientId = p.client_id,
                            Title = p.title,
                            BrandName = p.brand_name,
                            ProjectDescription = p.project_description,
                            Budget = p.budget,
                            Deadline = p.deadline,
                            ProjectStatus = p.project_status,
                            DateCreated = p.date_created
                        })
                        .ToList();

                    return View(clientProjects);
                }
            }
            else
            {
                // Redirect to a login page or handle the case when the user is not logged in
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult OnGoingProject()
        {
            List<OnGoingProjectViewModel> OnGoingProjectList = null;

            try
            {
                using (var db = new Freelancer_Marketplace_DBEntities())
                {
                    int freelancerId = (int)Session["UserId"];

                    // Get projects assigned to the freelancer
                    OnGoingProjectList = (from tp in db.takenprojects
                                        where tp.freelancer_id == freelancerId
                                        join p in db.projects on tp.project_id equals p.project_id
                                        join u in db.usersdatas on p.client_id equals u.userid
                                        select new OnGoingProjectViewModel
                                        {
                                            ProjectId = p.project_id,
                                            ClientId = p.client_id,
                                            Title = p.title,
                                            BrandName = p.brand_name,
                                            ProjectDescription = p.project_description,
                                            Budget = (decimal)p.budget,
                                            Deadline = p.deadline,
                                            ProjectStatus = p.project_status,
                                            DateCreated = p.date_created,
                                            ClientUsername = u.username,
                                            TakenProjectsId = tp.takenprojects_id,
                                            FreelancerId = tp.freelancer_id,
                                            States = tp.states
                                        }).ToList();

                    // Log information for debugging
                    Console.WriteLine($"Number of combined projects retrieved: {OnGoingProjectList.Count}");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow the exception for further analysis
            }

            return View(OnGoingProjectList);
        }
        
        public ActionResult UploadWork(int Id)
        {
            int freelancerId = (int)Session["UserId"];

            // Create an instance of the uploadedproject class
            var uploadProject = new uploadedproject
            {
                project_id = Id,
                freelancer_id = freelancerId,
            };

            return View(uploadProject);
        }
        public string HandleFileUpload(HttpPostedFileBase file, int freelancerId, int projectId)
        {
            // Check if the file is not null and has content
            if (file != null && file.ContentLength > 0)
            {
                // Save the file to a local folder with a name like (userid_projectid_currenttime.zip)
                string fileName = $"{freelancerId}_{projectId}_{DateTime.Now.Ticks}.zip";
                string relativePath = $"/Uploads/{fileName}"; // Adjust the folder path as needed

                // Save the file to the local folder
                string absolutePath = Server.MapPath($"~{relativePath}");
                string uploadsFolder = Path.GetDirectoryName(absolutePath);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                file.SaveAs(absolutePath);
                return absolutePath;
            }
            else {
                return "file not found!";
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadWork(uploadedproject model, HttpPostedFileBase formValidationFile)
        {
            // Get freelancer ID from the session
            int freelancerId = model.freelancer_id;

            // Set other properties of the model if needed

            // Call the HandleFileUpload method to handle the file upload
            string uploadedFilePath= HandleFileUpload(formValidationFile, freelancerId, model.project_id);

            //model.filepath= uploadedFilePath;

            // Save the model to the database
            using (var context = new Freelancer_Marketplace_DBEntities())
            {
                uploadedproject newUpload = new uploadedproject
                {
                    project_id = model.project_id,
                    freelancer_id = model.freelancer_id,
                    filepath = uploadedFilePath,
                    isverified = false,
                    formessage=model.formessage,
                    uploadtime = DateTime.Now // Assuming you want to store the current date and time
                };
                context.uploadedprojects.Add(newUpload);
                context.SaveChanges();
            }
            return View(model);
        }

        public ActionResult AddProject()
        {
            var newProject = new project();

            // Set any default values or initialize collections if needed
            // newProject.SomeProperty = defaultValue;

            return View(newProject);
        }
        [HttpPost]
        public ActionResult AddProject(project model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the entity model (you might want to use AutoMapper or similar)
                if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int userId))
                {
                    var project2 = new project
                    {
                        client_id = userId,
                        title = model.title,
                        brand_name = model.brand_name,
                        project_description = model.project_description,
                        budget = model.budget,
                        deadline = model.deadline,
                        project_status = model.project_status,
                        date_created = DateTime.Now,
                    };

                    // Add the project to the database
                    using (var db = new Freelancer_Marketplace_DBEntities())
                    {
                        db.projects.Add(project2);
                        db.SaveChanges();
                    }

                    // Redirect to a success page or do something else
                    return RedirectToAction("Myprojects");
                }
                return RedirectToAction("Index", "Login");
            }

            // If the model state is not valid, return to the form with validation errors
            return View(model);
        }
        public ActionResult EditProject(int projectId)
        {
            using (var db = new Freelancer_Marketplace_DBEntities())
            {
                // Retrieve the project based on the project ID
                var project = db.projects.Find(projectId);

                if (project == null)
                {
                    // Handle the case where the project is not found
                    return HttpNotFound();
                }

                // Map the entity model to the view model (you might want to use AutoMapper or similar)
                var viewModel = new project
                {
                    project_id = projectId,
                    title = project.title,
                    brand_name = project.brand_name,
                    project_description = project.project_description,
                    budget = project.budget,
                    deadline = project.deadline,
                    project_status = project.project_status
                    // Map other properties as needed
                };

                // Pass the view model to the view for editing
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult EditProject(project model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Freelancer_Marketplace_DBEntities())
                {
                    if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int userId))
                    {
                        // Retrieve the project from the database
                        var existingProject = db.projects.Find(model.project_id);

                        if (existingProject == null)
                        {
                            // Handle the case where the project is not found
                            return HttpNotFound();
                        }

                        // Update the project properties with the edited values
                        existingProject.client_id = userId;
                        existingProject.title = model.title;
                        existingProject.brand_name = model.brand_name;
                        existingProject.project_description = model.project_description;
                        existingProject.budget = model.budget;
                        existingProject.deadline = model.deadline;
                        existingProject.project_status = model.project_status;
                        // Update other properties as needed

                        // Save changes to the database
                        db.SaveChanges();

                        // Redirect to a success page or do something else
                        return RedirectToAction("MyProjects");
                    }
                }
            }

            // If the model state is not valid, return to the edit form with validation errors
            return View("EditProject", model);
        }

        public ActionResult ShowMyProjectBid(int id)
        {
            using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
            {
                var bidData = dbContext.bids
                    .Where(b => b.project_id == id)
                    .Join(
                        dbContext.usersdatas,
                        bid => bid.freelancer_id,
                        user => user.userid,
                        (bid, user) => new ShowBidOfMyProjectViewModel
                        {
                            // Populate properties from bids table
                            BidId = bid.bid_id,
                            ProjectId = bid.project_id,
                            FreelancerId = bid.freelancer_id,
                            BidAmount = bid.bid_amount,
                            ProposalText = bid.proposal_text,
                            IsApproved = (bool)bid.isapproved,
                            BidTime = (DateTime)bid.bidstime,

                            // Populate properties from usersdata table
                            UserId = user.userid,
                            Username = user.username,
                            Email = user.email,
                            ProfilePicture = user.profile_picture,
                            RegistrationDate = (DateTime)user.registration_date,
                            LastLoginDate = (DateTime)user.last_login_date,
                            IsActive = (bool)user.is_active
                        })
                    .ToList();

                return View(bidData);
            }
        }
        [HttpPost]
        public async Task<JsonResult> ApproveBidAsync(int userId, int projectId,string Username,string Email)
        {
            try
            {
                using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
                {
                    // Update the database (takenprojects table)
                    takenproject newRecord = new takenproject
                    {
                        project_id = projectId,
                        freelancer_id = userId,
                        states = 00
                    };

                    dbContext.takenprojects.Add(newRecord);
                    dbContext.SaveChanges();

                    //// Send an email
                    //EmailModel emailModel = new EmailModel
                    //{
                    //    To = Email,
                    //    Subject = "Bid Approval Notification",
                    //    Body = $"Dear {Username},\nYour bid for project ID {projectId} has been approved. Check your Project page for more information."
                    //};

                    //await emailModel.SendMailAsync();

                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return Json(new { success = false });
            }
        }
        public ActionResult ShowUploadedData(int projectId)
        {
            using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
            {
                var data = (from uploaded in dbContext.uploadedprojects
                            join taken in dbContext.takenprojects
                            on uploaded.project_id equals taken.project_id
                            where uploaded.project_id == projectId
                            select new UploadedDataViewModel
                            {
                                FilesId = uploaded.files_id,
                                ProjectId = uploaded.project_id,
                                FreelancerId = uploaded.freelancer_id,
                                FilePath = uploaded.filepath,
                                IsVerified = uploaded.isverified,
                                UploadTime = uploaded.uploadtime,
                                ForMessage = uploaded.formessage,

                                TakenProjectsId = taken.takenprojects_id,
                                States = (int)taken.states
                            }).ToList();

                return View(data);
            }
        }
        public ActionResult DownloadFile(int fileId)
        {
            // Retrieve file path based on fileId (query database or other logic)
            string filePath = GetFilePathFromDatabase(fileId);

            // Check if the file path is valid
            if (System.IO.File.Exists(filePath))
            {
                // Read the file content
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Determine the file's content type based on the file extension
                string contentType = MimeMapping.GetMimeMapping(filePath);

                // Provide the file for download
                FileContentResult result = File(fileBytes, contentType, Path.GetFileName(filePath));
                return result;
            }
            else
            {
                // If the file doesn't exist, return a HttpNotFound result
                return HttpNotFound();
            }
        }
        private string GetFilePathFromDatabase(int fileId)
        {
            using (var dbContext = new Freelancer_Marketplace_DBEntities())
            {
                // Assuming 'uploadedproject' is your model for uploaded files
                var file = dbContext.uploadedprojects.FirstOrDefault(f => f.files_id == fileId);

                return file?.filepath;
            }
        }
        [HttpPost]
        public ActionResult UpdateStates(int takenprojects_id, string newState)
        {
            try
            {
                using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
                {
                    // Retrieve the taken project from the database based on the projectId
                    var takenProject = dbContext.takenprojects.FirstOrDefault(tp => tp.takenprojects_id == takenprojects_id);

                    if (takenProject != null)
                    {
                        // Update the 'states' field with the new state value
                        takenProject.states = int.Parse(newState);

                        // Save changes to the database
                        dbContext.SaveChanges();

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, error = "Taken project not found." });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);
                return Json(new { success = false, error = "An error occurred while updating states." });
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Login", "Login");
        }
        public ActionResult AccountSettings()
        {
            using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
            {
                int userId = (int)Session["UserId"];

                // Fetch user data
                var userData = dbContext.usersdatas
                    .Where(u => u.userid == userId)
                    .FirstOrDefault();

                // Fetch user profile data
                var userProfileData = dbContext.user_profiles
                    .Where(up => up.user_id == userId)
                    .FirstOrDefault();

                // Create a new ViewModel with default values
                var viewModel = new UserProfileViewModel
                {
                    UserId = userId,
                    Username = "",
                    Email = "",
                    ProfilePicture = "",
                    RegistrationDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    IsActive = false,
                    ProfileId = 0,
                    Bio = "",
                    Portfolio = "",
                    FullName = "",
                    Location = "",
                    Skills = "",
                    ContactInformation = "",
                    SocialMediaLinks = ""
                };

                // If user data is found, update the ViewModel
                if (userData != null)
                {
                    viewModel.Username = userData.username;
                    viewModel.Email = userData.email;
                    viewModel.ProfilePicture = userData.profile_picture;

                    // Check if registration_date is not null before assigning
                    viewModel.RegistrationDate = userData.registration_date ?? DateTime.Now;

                    // Check if last_login_date is not null before assigning
                    viewModel.LastLoginDate = userData.last_login_date ?? DateTime.Now;

                    // Check if is_active is not null before assigning
                    viewModel.IsActive = userData.is_active ?? false;
                }

                // If profile data is found, update the ViewModel
                if (userProfileData != null)
                {
                    viewModel.ProfileId = userProfileData.profile_id;
                    viewModel.Bio = userProfileData.bio;
                    viewModel.Portfolio = userProfileData.portfolio;
                    viewModel.FullName = userProfileData.full_name;
                    viewModel.Location = userProfileData.location;
                    viewModel.Skills = userProfileData.skills;
                    viewModel.ContactInformation = userProfileData.contact_information;
                    viewModel.SocialMediaLinks = userProfileData.social_media_links;
                }

                return View(viewModel);
            }

        }

        private string SaveUploadedProfilePicture(HttpPostedFileBase uploadedImage)
        {
            if (uploadedImage != null && uploadedImage.ContentLength > 0)
            {
                // Generate a unique file name based on user ID, current date, and file extension
                string fileName = $"{Session["UserId"]}{Path.GetExtension(uploadedImage.FileName)}";

                // Specify the folder where the file will be saved (replace "YourUploadsFolderPath" with the actual path)
                string uploadsFolderPath = Server.MapPath("/Uploads/Userphoto/");

                // Check if the folder exists, and create it if it doesn't
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                // Combine the folder path with the file name to get the full path
                string filePath = Path.Combine(uploadsFolderPath, fileName);

                // Save the file to the specified path
                uploadedImage.SaveAs(filePath);

                // Return the full path
                return filePath;
            }
            else
            {
                return "File not found!";
            }
        }

        [HttpPost]
        public ActionResult AccountSettings(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
                {
                    int userId = (int)Session["UserId"];

                    // Check if a user profile already exists for the user
                    var existingProfile = dbContext.user_profiles
                        .FirstOrDefault(up => up.user_id == userId);

                    if (existingProfile != null)
                    {
                        // If a profile exists, update the existing profile
                        existingProfile.bio = model.Bio;
                        existingProfile.portfolio = model.Portfolio;
                        existingProfile.location = model.Location;
                        existingProfile.skills = model.Skills;
                        existingProfile.contact_information = model.ContactInformation;

                        // Handle uploaded profile picture
                        if (model.UploadedImage != null && model.UploadedImage.ContentLength > 0)
                        {
                            // Save the uploaded image to a local folder
                            string imagePath = SaveUploadedProfilePicture(model.UploadedImage);

                            // Update the profile picture path in the database
                            existingProfile.usersdata.profile_picture = imagePath;
                        }

                        // Save changes to the database
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        // If no profile exists, create a new profile and add it to the database
                        var newProfile = new user_profiles
                        {
                            user_id = userId,
                            bio = model.Bio,
                            portfolio = model.Portfolio,
                            location = model.Location,
                            skills = model.Skills,
                            contact_information = model.ContactInformation
                        };

                        // Handle uploaded profile picture
                        if (model.UploadedImage != null && model.UploadedImage.ContentLength > 0)
                        {
                            // Save the uploaded image to a local folder
                            string imagePath = SaveUploadedProfilePicture(model.UploadedImage);

                            // Set the profile picture path in the new profile
                            newProfile.usersdata.profile_picture = imagePath;
                        }

                        // Add the new profile to the database
                        dbContext.user_profiles.Add(newProfile);
                        dbContext.SaveChanges();
                    }

                    ViewBag.SuccessMessage = "Profile updated successfully!";
                    return View(model);
                }
            }

            // If the model is not valid or an error occurred, return to the form view
            return View(model);
        }

        public ActionResult AccountSettingsSecurity()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AccountSettingsSecurity(string currentPassword, string newPassword, string confirmPassword)
        {
            // Basic password validation
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                TempData["ErrorMessage"] = "Please fill in all password fields";
                return RedirectToAction("AccountSettingsSecurity");
            }

            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "New password and confirm password do not match";
                return RedirectToAction("AccountSettingsSecurity");
            }
            if (ModelState.IsValid)
            {
                using (Freelancer_Marketplace_DBEntities dbContext = new Freelancer_Marketplace_DBEntities())
                {
                    int userId = (int)Session["UserId"];

                    // Check if a user profile already exists for the user
                    var existingrow = dbContext.usersdatas
                        .FirstOrDefault(up => up.userid == userId);

                    // If a profile exists, update the existing profile
                    existingrow.userpassword = newPassword;

                    // Save changes to the database
                    dbContext.SaveChanges();
                    ViewBag.SuccessMessage = "Profile updated successfully!";
                    TempData["SuccessMessage"] = "Password updated successfully!";
                    return View();
                    //return View();
                }
            }
            return View();
        }
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult UserProfileProject()
        {
            return View();
        }
        public ActionResult UserProfileConnections()
        {
            return View();
        }
        public ActionResult UserProfileTeams()
        {
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
        


    }
}