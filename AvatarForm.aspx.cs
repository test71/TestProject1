
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
using System.Text.RegularExpressions;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

using COR = Casino.Core.BusinessLayer;
using Casino.Multipoker.BusinessLayer;

using Ecommerce.Collections;
using Ecommerce.Collections.Generic;
using Ecommerce.Web.Admin.Controls;

#endregion

public partial class AvatarForm : BaseDialogPage
{
    #region Members

	private GameFacade facade = new GameFacade();
	private Avatar avatar;

    #endregion

	#region private int? AvatarID
	private int? AvatarID
    {
        get 
        {
            int result;
            if (Page.Request.Params["id"] != null && int.TryParse(Page.Request.Params["id"].ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
    #endregion

	#region private Avatar Avatar
	private Avatar Avatar
    {
        get { return this.avatar; }
		set { this.avatar = value; }
    }
    #endregion

	#region private bool NewAvatar
	private bool NewAvatar
    {
		get { return AvatarID == null || AvatarID == 0; }
    }
    #endregion

	#region protected void Page_Init(object sender, EventArgs e)
	protected void Page_Init(object sender, EventArgs e)
	{
		this.repAvatarImages.ItemCreated += new RepeaterItemEventHandler(repAvatarImage_ItemCreated);
		this.subAvatarImageUpload.Click += new EventHandler(subAvatarImageUpload_Click);
	}
	#endregion


    #region public override void  Page_FirstLoad()
    public override void  Page_FirstLoad()
    {
		//	Right validation
		if (!PageUser.HasRight("kas_game_edit"))
			this.ReportError("kas_game_edit");
		
		base.Page_FirstLoad();

		if (!NewAvatar)
        {
            LoadAvatar();
        }
        else
        {
			txtAvatarID.Visible = false;
			lblAvatarFaceFile.Visible = false;
			imgAvatarFace.Visible = false;
            SetFormDefaultValues();
        }
    }
    #endregion

    #region public override void  Page_EveryLoad)
    public override void  Page_EveryLoad()
    {
		base.Page_EveryLoad();

        if (!NewAvatar)
        {
            this.Avatar = facade.GetAvatar((int)AvatarID);
        }

        #region Validatory
		// validace zakladnich vstupnich udaju
		txtAvatarName.Validators.Add(new cValidatorRequired((string)GetLocalResourceObject("Validation.AvatarNameEmpty")));
		txtAvatarName.Validators.Add(new cValidatorMaxLength((string)GetLocalResourceObject("Validation.AvatarNameLength"), 64));
		txtAvatarDescription.Validators.Add(new cValidatorMaxLength((string)GetLocalResourceObject("Validation.AvatarDescriptionLength"), 255));
		// validace vstupnich udaju na karte Images
		#endregion

        #region Tabs
        PrepareTabs();
        if (!Page.IsPostBack && Page.Request["tab"] != null)
        {
            SelectTab((cTab)FindControlRecursive(this, Page.Request["tab"].ToString()));
        }
        #endregion

    }
    #endregion

    #region protected override void OnLoadComplete(EventArgs e)
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        ((_DialogPage)this.Master).CancelButtonText = NewAvatar ? (string)GetLocalResourceObject("StornoButton.Storno") : (string)GetLocalResourceObject("StornoButton.Close");
        ((_DialogPage)this.Master).HeaderTitle = NewAvatar ? (string)GetLocalResourceObject("Page.Title.New") : String.Format((string)GetLocalResourceObject("Page.Title.Edit"), this.Avatar.Name);
    }
    #endregion

    // Private functions

    #region private void PrepareForm()
    private void PrepareForm(bool editEnabled)
    {
		imgAvatarFace.Visible = !NewAvatar;
		btnSave.Visible = editEnabled;
		lblAvatarFaceFileUpload.Visible = editEnabled;
		fupAvatarFaceFile.Visible = editEnabled;
		phAvatarImageNew.Visible = editEnabled;
		lblMissingImages.Visible = editEnabled;

        if (!editEnabled)
        {
            this.lblError.Text = (string)GetLocalResourceObject("AvatarReadOnly");
        }
    }
    #endregion

    #region private void PrepareTabs()
    private void PrepareTabs()
    {
		ctlTabGeneral.Visible = true;
		ctlTabImages.Visible = true;
		if (NewAvatar)
		{
			ctlTabImages.Visible = false;
		}
        ctlTabGeneral.Display = true;
        ctlTabImages.Display = false;
    }
    #endregion

    #region private void SelectTab(cTab tab)
    private void SelectTab(cTab tab)
    {
        ctlTabGeneral.Display = false;
        ctlTabImages.Display = false;
        tab.Display = true;
    }
    #endregion

    #region private void SetFormDefaultValues()
    private void SetFormDefaultValues()
    {
		txtAvatarID.Text = String.Empty;
		txtAvatarName.Text = String.Empty;
		txtAvatarDescription.Text = String.Empty;
		imgAvatarFace.ImageUrl = String.Empty;
    }
    #endregion

	#region private void ClearNewAvatarImageForm()
	private void ClearNewAvatarImageForm()
	{
		selAvatarImagePosition.Value = "1";
	}
	#endregion


	#region private void LoadAvatar()
	private void LoadAvatar()
    {
        this.Avatar = facade.GetAvatar((int)AvatarID);
		if (this.Avatar != null)
        {
            PrepareForm(!this.Avatar.Published);
			lblAvatarFaceFile.Visible = false;
			imgAvatarFace.Visible = false;

			txtAvatarID.Text = this.Avatar.Id.ToString();
			txtAvatarName.Text = this.Avatar.Name;
			txtAvatarDescription.Text = this.Avatar.Description;

			if (this.Avatar.Face != null)
			{
				if (System.IO.File.Exists(MultipokerSettings.AvatarImagesPhysicalPath + this.Avatar.Id.ToString() + @"\" + this.Avatar.Face))
				{
					lblAvatarFaceFile.Visible = true;
					imgAvatarFace.Visible = true;
					imgAvatarFace.ImageUrl = MultipokerSettings.AvatarImagesPath + this.Avatar.Id.ToString() + "/" + this.Avatar.Face;
				}
			}
            LoadImages();
        }
        else
        {
            throw new Exception("Avatar nebyl nalezen !");
        }
    }
    #endregion

	#region private void LoadImages()
	private void LoadImages()
    {
        if (!NewAvatar)
        {
            if (this.Avatar == null)
            {
                this.Avatar = facade.GetAvatar((int)AvatarID);
            }
            if (this.Avatar != null)
            {
				string missingImagesString = String.Empty;
				if (this.Avatar.AvatarImages.Count == 0)
				{
					missingImagesString = (string)GetLocalResourceObject("missingImages.All");
				}
				else
				{
					for (int placeToCheck = 1; placeToCheck < 11; placeToCheck++)
					{
						bool placeSet = false;
						foreach (AvatarImage avatarImage in this.Avatar.AvatarImages)
						{
							if (avatarImage.Place == placeToCheck)
							{
								placeSet = true;
								break;
							}
						}
						if (!placeSet)
						{
							if (missingImagesString.Length > 0) { missingImagesString += ", "; }
							missingImagesString += placeToCheck;
						}
					}
					if (missingImagesString.Length > 0)
					{
						missingImagesString = String.Format((string)GetLocalResourceObject("missingImages.List"), missingImagesString);
					}
				}
				lblMissingImages.Text = missingImagesString;

				repAvatarImages.Visible = this.Avatar.AvatarImages.Count > 0;
				repAvatarImages.DataSource = this.Avatar.AvatarImages;
				repAvatarImages.DataBind();
			}
        }
    }
    #endregion

	#region private bool HandleAvatarImageUpload(ref Avatar avatar, FileUpload fup)
	private bool HandleAvatarImageUpload(ref Avatar avatar, AvatarImageType imageType, FileUpload fup)
    {
		AvatarImageValidationResult v = AvatarImageValidationResult.OK;
		if (fup.HasFile && fup.PostedFile != null)
		{
			v = avatar.ValidateImage(imageType, fup.PostedFile.InputStream);
			if (v == AvatarImageValidationResult.OK)
			{
				Utilities.AvatarFaceSave(ref avatar, fup);
			}
			else
			{
				lblError.Text = Utilities.AvatarImageValidationString(v) + "<br />";
				return false;
			}
		}
		return true;
	}
    #endregion



	// Event handlers

	#region protected void repAvatarImage_ItemCreated(object sender, RepeaterItemEventArgs e)
	protected void repAvatarImage_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
		try
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					{
						AvatarImage i = ((AvatarImage)e.Item.DataItem);
						((Label)e.Item.FindControl("lblPosition")).Text = String.Format((string)GetLocalResourceObject("lblPosition"), i.Place);
						((Image)e.Item.FindControl("imgAvatarImage")).ImageUrl = MultipokerSettings.AvatarImagesPath + i.Avatar.Id + "/" + i.Image;
						((Image)e.Item.FindControl("imgAvatarImage")).Width = Unit.Pixel(i.ResolutionX);
						((Image)e.Item.FindControl("imgAvatarImage")).Height = Unit.Pixel(i.ResolutionY);
						((ImageButton)e.Item.FindControl("imbAvatarDelete")).Visible = !this.Avatar.Published;
						((ImageButton)e.Item.FindControl("imbAvatarDelete")).ToolTip = Resources.Strings.DeleteItem;
						((ImageButton)e.Item.FindControl("imbAvatarDelete")).OnClientClick = String.Format("return confirm('{0}');", (string)GetLocalResourceObject("AvatarImage.ConfirmDelete"));
						break;
					}
			}
		}
		catch (Exception ex)
		{
			Casino.Core.Logging.LogToFile(ex);
		}
	}
    #endregion

	#region protected void subAvatarImageUpload_Click(object sender, EventArgs e)
	protected void subAvatarImageUpload_Click(object sender, EventArgs e)
	{
		lblError.Text = String.Empty;
		SelectTab(ctlTabImages);

		LoadAvatar();

		#region Validace - neni co validovat, soubor se validuje dale
		#endregion

		#region Ulozeni souboru & databaze
		string place = selAvatarImagePosition.Value;

		AvatarImageValidationResult v = AvatarImageValidationResult.OK;
		if (fupAvatarImage.HasFile && fupAvatarImage.PostedFile != null)
		{
			v = this.Avatar.ValidateImage(AvatarImageType.Image, fupAvatarImage.PostedFile.InputStream);
		}
		else
		{
			v = AvatarImageValidationResult.InvalidImageInputFormat;
		}
		if (v == AvatarImageValidationResult.OK)
		{
			bool exists = false;
			AvatarImage avatarImage = facade.GetAvatarImage(this.Avatar.Id, Convert.ToByte(place));
			exists = avatarImage != null;
			if (!exists)
			{
				avatarImage = new AvatarImage(this.Avatar, Convert.ToByte(place));
			}

			System.Drawing.Image i = System.Drawing.Image.FromStream(fupAvatarImage.PostedFile.InputStream);
			avatarImage.ResolutionX = i.Width;
			avatarImage.ResolutionY = i.Height;
			Utilities.AvatarImageSave(ref avatarImage, fupAvatarImage);
			if (!exists)
			{
				facade.InsertAvatarImage(avatarImage);
			}
			else
			{
				facade.UpdateAvatarImage(avatarImage);
			}
			LoadAvatar();
		}
		else
		{
			lblError.Text += Utilities.AvatarImageValidationString(v) + "<br />";
		}
		ClearNewAvatarImageForm();
		#endregion

		#region Nastaveni promennych
		// SetLotControl();
		#endregion

	}
	#endregion

    #region protected void btnSave_Click(object sender, EventArgs e)
    /// <summary>
    /// Ulozeni avataru
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        cTab displayedTab = ctlTabGeneral;

        #region Validace zakladnich parametru

        lblError.Text = String.Empty;
        if (!Page.IsValid)
        {
            lblError.Text = (string)GetLocalResourceObject("Validation.PageNotValid");
            if (!NewAvatar) { LoadImages(); }
            return;
        }
        #endregion

		#region Ulozeni avataru
		if (NewAvatar)
		{
			Avatar avatar = new Avatar();
			avatar.Name = txtAvatarName.Text;
			avatar.Description = txtAvatarDescription.Text;

			if (facade.InsertAvatar(avatar))
			{
				HandleAvatarImageUpload(ref avatar, AvatarImageType.Face, fupAvatarFaceFile);
				facade.UpdateAvatar(avatar);
				Page.Response.Redirect(String.Format("AvatarForm.aspx?id={0}", avatar.Id));
			}
			else
			{
				throw new Exception("Chyba pøi vkládání avataru");
			}
		}
		else
		{
			Avatar avatar = facade.GetAvatar((int)AvatarID);
			if (avatar != null)
			{
				avatar.Name = txtAvatarName.Text;
				avatar.Description = txtAvatarDescription.Text;
				HandleAvatarImageUpload(ref avatar, AvatarImageType.Face, fupAvatarFaceFile);

				if (facade.UpdateAvatar(avatar))
				{
					LoadAvatar();
				}
				else
				{
					throw new Exception("Chyba pøi ukládání avataru");
				}
			}
			else
			{
				throw new Exception("Avatar nebyl nalezen");
			}
		}

        #endregion

		#region Obnoveni + nastaveni tabu + dalsi grafika
		PrepareTabs();
		SelectTab(displayedTab);
		ClearNewAvatarImageForm();
		#endregion


    }
    #endregion

	#region protected void imbImageDelete_Click(object sender, ImageClickEventArgs e)
	protected void imbAvatarDelete_Click(object sender, ImageClickEventArgs e)
    {
        lblError.Text = String.Empty;
        SelectTab(ctlTabImages);

        #region Smazani symbolu
        string place = ((ImageButton)sender).CommandArgument;
		AvatarImage avatarImage = facade.GetAvatarImage(this.Avatar.Id, Convert.ToByte(place));
        facade.DeleteAvatarImage(avatarImage);
        LoadAvatar();
        #endregion 

        #region Nastaveni dalsi grafiky
        ClearNewAvatarImageForm();
		#endregion

	}
    #endregion

}
