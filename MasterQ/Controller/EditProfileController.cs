﻿using System;
namespace MasterQ
{
    public class EditProfileController
    {
        private static EditProfileController instance = new EditProfileController();

        EditProfileController()
        {

        }
        public static EditProfileController getInstance()
        {
            return instance;
        }
        public UIReturn editProfile(Member input)
        {
			if (String.IsNullOrEmpty(input.email)) return Constants.uiErrorEmptyEmail;
			if (String.IsNullOrEmpty(input.password)) return Constants.uiErrorEmptyPassword;
			if (String.IsNullOrEmpty(input.confirmPassword)) return Constants.uiErrorEmptyConfirmPassword;
			if (String.IsNullOrEmpty(input.firstName)) return Constants.uiErrorEmptyFirstName;
			if (String.IsNullOrEmpty(input.lastName)) return Constants.uiErrorEmptyLastName;
			if (String.IsNullOrEmpty(input.birthDate)) return Constants.uiErrorEmptyBirthdate;
			if (!Validate.email(input.email)) return Constants.uiErrorInvalidEmail;
			if (!isSamePassword(input)) return Constants.uiErrorPasswordNotMatch;

            EditProfileRs res = MemberService.getInstance().CallEditProfile(input);

            UIReturn ret = new UIReturn(res.header);
            return ret;
        }
        private bool isSamePassword(Member input)
        {
            return input.password.Equals(input.confirmPassword);
        }

    }
}
