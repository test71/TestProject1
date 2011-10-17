
#region Using directives

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ecommerce.Web.Admin.Controls;
using Ecommerce.Common;

#endregion

public partial class _AvatarList2 : BasePage
{
	//Methods

	#region protected void Page_Load(object sender, EventArgs e)
	protected void Page_Load(object sender, EventArgs e)
	{	
		//Right validation
		if (!PageUser.HasRight("kas_game_list"))
		{
			this.ReportError("kas_game_list");
		}

		// Buttons
		//btnNewTable.Visible = PageUser.HasRight("kas_game_edit");

		// Strings
		((MasterPage)this.Master).HeaderTitle = (string)GetLocalResourceObject("Page.Title");

		// IFrame
		this.DataGrid.Source = "AvatarList2Iframe.aspx";
	}
	#endregion
}