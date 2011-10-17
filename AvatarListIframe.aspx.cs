
#region Using directives

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Ecommerce.Collections.Generic;
using Ecommerce.Collections;

using Casino.Core.BusinessLayer;
using PTH = Casino.Multipoker.BusinessLayer;
using Ecommerce.Web.Admin.Controls;

#endregion

public partial class AvatarListIframe : BaseListPage
{
    #region protected void Page_Load(object sender, EventArgs e)
    protected void Page_Load(object sender, EventArgs e)
    {
		//	Right validation
		if (!PageUser.HasRight("kas_game_list"))
			this.ReportError("kas_game_list");

		ColumnCollection.Add(new cListIframeColumn((string)GetLocalResourceObject("ID"), 40));
		ColumnCollection.Add(new cListIframeColumn((string)GetLocalResourceObject("Name"), 200, 9));
		ColumnCollection.Add(new cListIframeColumn((string)GetLocalResourceObject("Description"), 0, 1));
		ColumnCollection.Add(new cListIframeColumn((string)GetLocalResourceObject("Published"), 60));
		ColumnCollection.Add(new cListIframeColumn((string)GetLocalResourceObject(""), 30));
		ColumnCollection.Add(new cListIframeColumn((string)GetLocalResourceObject(""), 20));
		ColumnCollection.SetDataGridColumnsWidth(ref dgList);

		dgList.Columns[dgList.Columns.Count - 1].Visible = PageUser.HasRight("kas_game_edit");
		dgList.Columns[dgList.Columns.Count - 2].Visible = PageUser.HasRight("kas_game_edit");
		dgList.Columns[dgList.Columns.Count - 3].Visible = PageUser.HasRight("kas_game_edit");

		DataGridPageSize = CasinoSettings.AdminPageSize;
		DataGridName = this.dgList.ClientID;
		IframeID = "ctlData";

		if (Request.QueryString["msg"] != null)
		{
			string msg = Request.QueryString["msg"].ToString();
			string jsMessage = (string)GetLocalResourceObject(msg);
			RegisterStartupJavaScriptFunction("showMessage", "alert('" + jsMessage + "'); document.location.href='AvatarListIframe.aspx';");
			DataGridRecordCount = 0;
			dgList.DataSource = new PagedList<PTH.Avatar>(new Pager(DataGridPageSize, DataGridCurrentPageIndex, PagerType.ItemsAndCount));
		}
		else
		{
			PTH.GameFacade facade = new PTH.GameFacade();
			Pager p = new Pager(DataGridPageSize, DataGridCurrentPageIndex, PagerType.ItemsAndCount);
			p.SortKey = ColumnOrder;
			p.DescendingOrder = ColumnOrderDesc;
			PagedList<PTH.Avatar> avatars = new PagedList<PTH.Avatar>(p);
			facade.GetAvatars(avatars, null, null);

			DataGridRecordCount = avatars.Pager.ItemsCount;
			dgList.DataSource = avatars;
		}
        dgList.DataBind();
    }
    #endregion

    #region protected void ItemBound(Object sender, DataGridItemEventArgs e)
    /// <summary>
    /// This method adds additional attributes to each datagrid row. This one specially adds context menu definition.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ItemBound(Object sender, DataGridItemEventArgs e)
    {
    }
    #endregion

	#region protected string GetPublishStatusIcon(PTH.Avatar avatar)
	protected string GetPublishStatusIcon(PTH.Avatar avatar)
	{
		if (avatar.Published)
		{
			return "images/is.gif";
		}
		else
		{
			return "images/isnt.gif";
		}
	}
	#endregion

	#region protected bool GetEditVisibilityStatus(PTH.Avatar avatar)
	protected bool GetEditVisibilityStatus(PTH.Avatar avatar)
	{
		return true;
	}
	#endregion

	#region protected bool GetDeleteVisibilityStatus(PTH.Avatar avatar)
	protected bool GetDeleteVisibilityStatus(PTH.Avatar avatar)
    {
		if (avatar.Published)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	#endregion

	#region protected bool GetPublishVisibilityStatus(PTH.Avatar avatar)
	protected bool GetPublishVisibilityStatus(PTH.Avatar avatar)
	{
		return true;
	}
	#endregion

	#region protected string GetPublishUrl(PTH.Avatar avatar)
	protected string GetPublishUrl(PTH.Avatar avatar)
	{
		string callbackUrl = "AvatarListIframe.aspx";
		string url = String.Format("javascript:PTH_AvatarPublish({0}, '{1}')", avatar.Id.ToString(), callbackUrl);
		return url;
	}
	#endregion

	#region protected string GetDeleteUrl(PTH.Avatar avatar)
	protected string GetDeleteUrl(PTH.Avatar avatar)
    {
        string callbackUrl = "AvatarListIframe.aspx";
        string url = String.Format("javascript:PTH_AvatarRemove({0}, '{1}')", avatar.Id.ToString(), callbackUrl);
        return url;
	}
	#endregion

}
