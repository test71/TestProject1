
#region Usings
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
#endregion

public partial class AvatarList : BasePage
{
	#region protected void Page_Load(object sender, EventArgs e)
	protected void Page_Load(object sender, EventArgs e)
    {
		//	Right validation
		if (!PageUser.HasRight("kas_game_list"))
			this.ReportError("kas_game_list");
		btnNewAvatar.Visible = PageUser.HasRight("kas_game_edit");

        ((MasterPage)this.Master).HeaderTitle = (string)GetLocalResourceObject("Page.Title");
        ctlData.Source = "AvatarListIframe.aspx";
	}
	#endregion
}
