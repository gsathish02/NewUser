
using System;
using UIKit;
using NewUser.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NewUser
{
	public partial class RegisterViewController : UIViewController
	{
        bool validUserName = false;
        bool validPassword = false;
        bool validName = false;
        readonly string Message = "1) Password consist of letters and numerical digits only, with at least one of each. \r\n2) Password must be between 5 and 12 characters in length. \r\n3) Password must not contain any sequence of characters immediately.";

        public RegisterViewController (IntPtr handle) : base (handle)
		{
		}

        public List<Registration> Registration = new List<Registration>();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Buttons Action
            saveButton.TouchUpInside += async (sender, e) => await UpdateDataAsync();
            cancelButton.TouchUpInside += (sender, e) => this.NavigationController.PopViewController(true);

            //Textfield value changes
            userNameTextField.AddTarget(UserNameCheck, UIControlEvent.EditingChanged);
            passwordTextField.AddTarget(PasswordCheck, UIControlEvent.EditingChanged);
            fullNameTextField.AddTarget(FullNameCheck, UIControlEvent.EditingChanged);

            infoLabel.Text = Message;
            infoView.Hidden = true;
        }

        private void FullNameCheck(object sender, EventArgs e)
        {
            infoView.Hidden = true;
            if (!string.IsNullOrWhiteSpace(fullNameTextField.Text))
            {
                validName = true;
            }
        }

        private void UserNameCheck(object sender, EventArgs e)
        {
            infoView.Hidden = true;
            if (!string.IsNullOrWhiteSpace(userNameTextField.Text))
            {
                validUserName = true;
            }
        }

        private void PasswordCheck(object sender, EventArgs e)
        {
            infoView.Hidden = false;
            validPassword = IsValidPassword(passwordTextField.Text);
           
             if(validPassword || string.IsNullOrEmpty(passwordTextField.Text))
                infoView.Hidden = true;
        }

        public static bool IsValidPassword(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }

            // regex to check all 3checklist
            return Regex.IsMatch(strIn,
                   "^(?=.*[0-9])(?=.*[a-zA-Z])(?!.*(.{1,})\\1).{5,12}$", RegexOptions.IgnoreCase);
        }

        public async Task UpdateDataAsync()
        {
            if (!validName)
            {
                //Show alert if Fullname is not valid
                var alert = UIAlertController.Create("Error", "Name should not be empty.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, true, null);
            }
            else if(!validUserName)
            {
                //Show alert if username is not valid
                var alert = UIAlertController.Create("Error", "UseName should not be empty.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, true, null);
            }
            else if (!validPassword)
            {
                //Show alert if password is not valid
                var alert = UIAlertController.Create("Error", Message, UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(alert, true, null);
            }
            else
            {
                var newData = new Registration
                {
                    UserName = userNameTextField.Text,
                    Password = passwordTextField.Text,
                    FullName = fullNameTextField.Text
                };
                var oldRecord = await LocalStore.SharedInstance.GetUserAsync();
                oldRecord.Add(newData);

                // saving oldRecord + NewRecord
                await LocalStore.SharedInstance.SetUserAsync(oldRecord);
                NavigationController.PopViewController(true);
            }
        }
    }
}
