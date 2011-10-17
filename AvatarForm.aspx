<%@ Page Language="C#" MasterPageFile="~/DialogPage.master" AutoEventWireup="true" CodeFile="AvatarForm.aspx.cs" Inherits="AvatarForm" Culture="auto" UICulture="auto" %>

<%@ Register Tagprefix="ScratchControls" Namespace="Casino.Admin.ScratchGames.Controls"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">
<table width="100%" height="100%" cellspacing="0" cellpadding="0">
<tr>
    <td height="100%">
    <GUI:cTabStrip id="ctlTabStrip" runat="server" meta:resourcekey="ctlTabStrip">
	    <GUI:cTab id="ctlTabGeneral" runat="server" Display="True" meta:resourcekey="ctlTabGeneral">
            <table width="100%" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top" colspan="2"><GUI:cTextBox runat="server" ID="txtAvatarID" Enabled="false" Width="50" ReadOnly="true" Disabled="true" meta:resourcekey="txtAvatarID" /></td>
	            </tr>
	            <tr><td valign="top" colspan="2"><GUI:cTextBox runat="server" ID="txtAvatarName" Width="384" MaxLength="64" Required="true" meta:resourcekey="txtAvatarName" /></td></tr>
	            <tr><td valign="top" colspan="2"><GUI:cTextBox runat="server" ID="txtAvatarDescription" Width="384" MaxLength="512" TextMode="MultiLine" Height="50" meta:resourcekey="txtAvatarDescription" /></td></tr>
	            <tr>
					<td valign="top" style="padding-top: 10px;"><GUI:cLabel runat="server" ID="lblAvatarFaceFile" meta:resourcekey="lblAvatarFaceFile" /></td>
					<td valign="top" style="padding-top: 10px; vertical-align: bottom;"><asp:Image runat="server" ID="imgAvatarFace" /></td>
				</tr>
	            <tr>
					<td valign="middle"><GUI:cLabel runat="server" ID="lblAvatarFaceFileUpload" meta:resourcekey="lblAvatarFaceFileUpload" /></td>
					<td valign="top"><asp:FileUpload runat="server" CssClass="input_normal" Width="220" ID="fupAvatarFaceFile" /></td>
				</tr>
            </table>
	    </GUI:cTab>
	    <GUI:cTab id="ctlTabImages" runat="server" meta:resourcekey="ctlTabImages">
	        <table width="100%" height="100%" cellspacing="0" cellpadding="0">
			    <tr>
					<td valign="top">
						<GUI:cLabel runat="server" ID="lblMissingImages" style="line-height: 200%; font-weight: bold;" />
					</td>
				</tr>
				<tr>
			        <td valign="top" height="100%">
                        <div style="width: 100%; height: 100%; padding: 3px; overflow-y: auto; border: 1px solid #ACA899; " runat="server" id="divAvatarImage">
			            <asp:Repeater runat="server" ID="repAvatarImages" meta:resourcekey="repAvatarImages">
			                <HeaderTemplate>
			                    <div>
			                </HeaderTemplate>
			                <ItemTemplate>
								<div runat="server" id="divAvatar" style="text-align: center; float: left; border: 2px solid; border-color: Silver Gray Gray Silver; margin: 3px; padding: 3px 5px 3px 5px; position: relative; background-color: #ebf6ec;">
									<GUI:cLabel runat="server" ID="lblPosition" style="font-weight: bold;" /><br />
									<asp:ImageButton runat="server" ID="imbAvatarDelete" ImageUrl="images/delete.gif" Style="position: absolute; top: 2px; right: 2px;" OnClick="imbAvatarDelete_Click" CommandArgument='<%# ((Casino.Multipoker.BusinessLayer.AvatarImage)Container.DataItem).Place.ToString() %>' />
									<asp:Image runat="server" ID="imgAvatarImage" Style="margin: 3px 0px 0px 0px;" />
								</div>
							</ItemTemplate>
							<FooterTemplate>
							    </div>
							</FooterTemplate>
			            </asp:Repeater>
        			    </div>
			        </td>
			    </tr>
			    <asp:PlaceHolder runat="server" ID="phAvatarImageNew">
	                <!-- definice noveho obrazku avataru -->
	                <tr>
						<td style="height: 5px; line-height: 20%;">&nbsp;</td>
	                </tr>
			        <tr>
			            <td height="10%" style="padding: 2px; border: 1px solid #ACA899;">
			                <table cellpadding="0" cellspacing="2" border="0" width="100%">
								<tr>
									<td colspan="3" style="padding-bottom: 3px;"><b><GUI:cLabel runat="server" ID="lblAvatarImageUploadLabel" meta:resourcekey="lblAvatarImageUploadLabel" /></b></td>
								</tr>
								<tr>
									<td valign="bottom" style="padding-left: 3px;" Width="100px">
										<GUI:cSelectList runat="server" ID="selAvatarImagePosition" meta:resourcekey="selAvatarImagePosition">
											<GUI:cSelectItem runat="server" Value="1" Text="1" />
											<GUI:cSelectItem runat="server" Value="2" Text="2" />
											<GUI:cSelectItem runat="server" Value="3" Text="3" />
											<GUI:cSelectItem runat="server" Value="4" Text="4" />
											<GUI:cSelectItem runat="server" Value="5" Text="5" />
											<GUI:cSelectItem runat="server" Value="6" Text="6" />
											<GUI:cSelectItem runat="server" Value="7" Text="7" />
											<GUI:cSelectItem runat="server" Value="8" Text="8" />
											<GUI:cSelectItem runat="server" Value="9" Text="9" />
											<GUI:cSelectItem runat="server" Value="10" Text="10" />
										</GUI:cSelectList>
									</td>
									<td valign="bottom" Width="350px"><asp:FileUpload runat="server" CssClass="input_normal" ID="fupAvatarImage" /></td>
									<td valign="bottom" align="right" style="padding: 0px 5px 5px 0px;"><GUI:cSubmitButton runat="server" ID="subAvatarImageUpload" width="50px" CausesValidation="false" meta:resourcekey="subAvatarImageUpload" /></td>
								</tr>
			                </table>
			            </td>
			        </tr>
			    </asp:PlaceHolder>
		    </table>
	    </GUI:cTab>
    </GUI:cTabStrip>
    </td>
</tr>
<tr>
    <td style="padding: 5px;"><GUI:cLabel ID="lblError" runat="server" ForeColor="red" /></td>
</tr>
</table>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Buttons" Runat="Server">
    <GUI:cSubmitButton id="btnSave" runat="server" width="50px" meta:resourcekey="btnSave" OnClick="btnSave_Click" />
</asp:Content>
