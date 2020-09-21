// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace NewUser
{
	[Register ("UserListController")]
	partial class UserListController
	{
		[Outlet]
		UIKit.UIBarButtonItem addNewUserButton { get; set; }

		[Outlet]
		UIKit.UIView emptyListView { get; set; }

		[Outlet]
		UIKit.UIButton registerButton { get; set; }

		[Outlet]
		UIKit.UITableView tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (emptyListView != null) {
				emptyListView.Dispose ();
				emptyListView = null;
			}

			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}

			if (registerButton != null) {
				registerButton.Dispose ();
				registerButton = null;
			}

			if (addNewUserButton != null) {
				addNewUserButton.Dispose ();
				addNewUserButton = null;
			}
		}
	}
}
