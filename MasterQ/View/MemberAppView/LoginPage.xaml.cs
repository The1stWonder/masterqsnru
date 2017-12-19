﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace MasterQ
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            if (CrossConnectivity.Current.IsConnected)
            {
                if (App.Thai == true)
                {
                    Utils.changeAppLanguageToThai();
                    LanguageThai.IsVisible = false;
                    LanguageThai.IsEnabled = false;

                    LanguageEng.IsVisible = true;
                    LanguageEng.IsEnabled = true;

                    Signin1.IsVisible = false;
                    Signin2.IsVisible = true;
                }
                else
                {
                    Utils.changeAppLanguageToEng();
                    LanguageThai.IsVisible = true;
                    LanguageThai.IsEnabled = true;

                    LanguageEng.IsVisible = false;
                    LanguageEng.IsEnabled = false;

                    Signin1.IsVisible = true;
                    Signin2.IsVisible = false;
                }

                mUsernameEntry.Placeholder = Utils.getLabel(LabelConstants.MAPVIEW_PAGE_EMAIL);
                mPasswordEntry.Placeholder = Utils.getLabel(LabelConstants.LOGIN_PAGE_PASSWORD);
                forgetpassword.Text = Utils.getLabel(LabelConstants.LOGIN_PAGE_FORGETPASSWORD);
                DonAccount.Text = Utils.getLabel(LabelConstants.LOGIN_PAGE_DONACCOUNT);
                SignUP.Text = Utils.getLabel(LabelConstants.LOGIN_PAGE_SIGNUP);


                //         var username = "t@t.co";
                //var password = "1";
                //Login MasQLogin = new Login(username, password);
                //UIReturn uiReturn = LoginController.getInstance().LoginMember(MasQLogin);

                //if (uiReturn.isSuccess)
                //{
                //	Navigation.PushAsync(new MainPage());
                //}
                //else
                //{
                //	DisplayAlert("Click", uiReturn.getDescription(), "Close");
                //}

                //string A = Utils.getLabel(Constants.PAGE_USERNAME);

                if (SessionModel.loginMember != null)
                {
                    App.fristtime = true;
                    Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                DisplayAlert("", Utils.getLabel(LabelConstants.LOGIN_PAGE_NOINTERNET), "Close");

                LanguageThai.IsVisible = false;
                LanguageThai.IsEnabled = false;

                LanguageEng.IsVisible = true;
                LanguageEng.IsEnabled = false;

                Signin1.IsVisible = false;
                Signin2.IsVisible = true;

                mUsernameEntry.Placeholder = "อีเมล";
                mPasswordEntry.Placeholder = "รหัสผ่าน";
                forgetpassword.Text = "ลืมรหัสผ่าน | ช่วยเหลือ";
                DonAccount.Text = "คุณยังไม่ได้สมัครสมาชิกใช่ใหม?";
                SignUP.Text = "สมัครสมาชิก";
            }
        }

        public void OnLabelSignUp(object sender, System.EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushAsync(new RegisterPage());
                mUsernameEntry.Text = "";
                mPasswordEntry.Text = "";
            }
            else
            {
                DisplayAlert("", Utils.getLabel(LabelConstants.LOGIN_PAGE_NOINTERNET), "Close");
            }
        }

        public void OnImageSignin(object sender, System.EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var username = mUsernameEntry.Text;
                var password = mPasswordEntry.Text;
                Login MasQLogin = new Login(username, password);
                UIReturn uiReturn = LoginController.getInstance().LoginMember(MasQLogin);

                if (uiReturn.isSuccess)
                {
                    App.fristtime = true;
                    Navigation.PushAsync(new MainPage());
                }
                else
                {
                    DisplayAlert("", uiReturn.descriptionEN, "Close");
                }
            }
            else
            {
                DisplayAlert("", Utils.getLabel(LabelConstants.LOGIN_PAGE_NOINTERNET), "Close");
            }
        }

        public void OnLabelForgetpassword(object sender, System.EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushAsync(new ForgetPasswordPage());
            }
            else
            {
                DisplayAlert("", Utils.getLabel(LabelConstants.LOGIN_PAGE_NOINTERNET), "Close");
            }
        }

        public void OnImageMainchangeAppLanguageThai(object sender, System.EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Utils.changeAppLanguageToThai();
                App.Thai = true;

                Navigation.InsertPageBefore(new LoginPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("", Utils.getLabel(LabelConstants.LOGIN_PAGE_NOINTERNET), "Close");
            }
        }

        public void OnImageMainchangeAppLanguageEng(object sender, System.EventArgs args)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Utils.changeAppLanguageToEng();
                App.Thai = false;

                Navigation.InsertPageBefore(new LoginPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("", Utils.getLabel(LabelConstants.LOGIN_PAGE_NOINTERNET), "Close");
            }

        }
    }
}
