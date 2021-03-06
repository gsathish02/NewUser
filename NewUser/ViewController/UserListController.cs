﻿
using System;
using Foundation;
using UIKit;
using NewUser.Models;
using System.Collections.Generic;

namespace NewUser
{
	public partial class UserListController : UIViewController, IUITableViewDelegate, IUITableViewDataSource
	{
        public List<Registration> savedValues = new List<Registration>();
        public List<string> TableItems = new List<string>();
        readonly string CellIdentifier = "TableCell";
       
        public UserListController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
           
            //get db data
            var Registry = await LocalStore.SharedInstance.GetUserAsync();

            //clear table data to prevent the repetations
            savedValues.Clear();
            if (Registry == null || Registry.Count <1)
            {
                tableView.Hidden = true;
                emptyListView.Hidden = false;
                return;
            }
            foreach (var singleRegistry in Registry)
            {
                if (singleRegistry.UserName != null)
                {
                    tableView.Hidden = false;
                    emptyListView.Hidden = true;
                    savedValues.Add(singleRegistry);
                    tableView.ReloadData();
                }
                else
                {
                    tableView.Hidden = true;
                    emptyListView.Hidden = false;
                }
            }
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            var item = savedValues[indexPath.Row];

            //if there are no cells to reuse, create a new one
            if (cell == null)
            { 
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier); 
            }
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
            cell.TextLabel.Text = item.UserName;
            cell.DetailTextLabel.Text = item.FullName;
            return cell;
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return savedValues.Count;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var detailVC = (DetailViewController)UIStoryboard.FromName("Main", NSBundle.MainBundle).InstantiateViewController("detailVC");
            detailVC.Values = savedValues[indexPath.Row];
            NavigationController.PushViewController(detailVC, true);
        }

        [Export("tableView:viewForFooterInSection:")]
        public UIView GetViewForFooter(UITableView tableView, nint section)
        {
            //Display white space for unused table cells
            return new UIView();
        }
    }
}
