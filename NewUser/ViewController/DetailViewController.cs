
using System;
using NewUser.Models;
using UIKit;

namespace NewUser
{
	public partial class DetailViewController : UIViewController
	{
        public Registration Values { set; get; }
        public string UserName { get; set; }

        public DetailViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Random rand = new Random();
            int roll = rand.Next(1, 4);

            //Setting random image
            bgImage.Image = UIImage.FromBundle($"Img{roll}.png");

            var alert = UIAlertController.Create($"Hello,{Values.FullName} ",$"Your username is {Values.UserName}", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);
        }
    }
}
