﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EcmMobileShop.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using FireSharp.Interfaces;
using Firebase.Auth;
using FireSharp.Response;

namespace EcmMobileShop.Controllers
{
    public class AccountController : Controller
    {

        private static string ApiKey = "AIzaSyChvF1RW2IL-N7gbgf_5LHAyOSWT5WK4w0";
        private static string Bucket = "ecmmobileshop-default-rtdb.firebaseio.com";
        EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();

        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "PtNahcVALDcleR71F1jFTbj4j8AM4RED2PamGprQ",
           
            BasePath = "https://ecmmobileshop-default-rtdb.firebaseio.com/"
           
        };
        IFirebaseClient client;

        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(LoginViewModel model)
        
        {
            try
            {
                // Verification.
                if (this.Request.IsAuthenticated)
                {

                    return this.RedirectToLocal("Index","Home");
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string action,string controller)
        {

            try
            {
                // Verification.
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    string token = ab.FirebaseToken;
                    var user = ab.User;
                    if (token != "")
                    {

                        this.SignInUser(user.Email, token, false);
                        return this.RedirectToLocal(action, controller);

                    }
                    else
                    {
                        // Setting.
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }
        private async Task SignInUser(string email, string token, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();

            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));

                // Khởi tạo client
                IFirebaseClient client = new FireSharp.FirebaseClient(config);

                var response = client.Get("Khachhang/");
                Dictionary<string, SignUpModel> data = response.ResultAs<Dictionary<string, SignUpModel>>();
                SignUpModel customer = data.Values.FirstOrDefault(x => x.Email == email);

                if (customer != null)
                {
                    // Add the user's role(s) to the claims.
                    claims.Add(new Claim(ClaimTypes.Role, customer.Roles)); // assuming customer.Roles is a string containing the user's role(s)

                    var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var ctx = Request.GetOwinContext();
                    var authenticationManager = ctx.Authentication;
                    // Sign In.
                    authenticationManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = isPersistent
                    }, claimIdenties);
                }
                else
                {
                    // Handle case where user not found in database.
                }
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }

        private void ClaimIdentities(string username, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();
            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }

        private ActionResult RedirectToLocal(string action,string controller)
        {
            try
            {
                // Verification.
                //if (Url.IsLocalUrl(action))
                //{
                // Info.
                if (action != null && controller != null)
                {
                    return this.RedirectToAction(action, controller);
                }
                //}
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Info.
            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        private void AddKhachHangToFirebase(SignUpModel model)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = model;
            PushResponse response = client.Push("Khachhang/", data);
            data.id = response.Result.name;
            SetResponse setResponse = client.Set("Khachhang/" + data.id, data);
        }


        public ActionResult SignUp()
        
        {
            SignUpModel model = new SignUpModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));

                var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
                AddKhachHangToFirebase(model);
                ModelState.AddModelError(string.Empty, "Please Verify your email then login Plz.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult ForgotPassword()
        {
            ForgotPasswordViewModel model = new ForgotPasswordViewModel();
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));

            await auth.SendPasswordResetEmailAsync(model.Email);



            return RedirectToAction("Index", "Home");
        }
    }
}