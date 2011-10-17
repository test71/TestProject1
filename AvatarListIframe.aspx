<%@ Page Language="C#" MasterPageFile="~/ListPage.master" AutoEventWireup="true" CodeFile="AvatarListIframe.aspx.cs" Inherits="AvatarListIframe" Title="Untitled Page" Culture="auto" meta:resourcekey="Page" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" Runat="Server">

    <GUI:cIframeDataGrid id="dgList" runat="server" OnItemBound="ItemBound">
		<Columns>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="left">
				<ItemTemplate>
					<GUI:cIframeColumnText Runat="server" Width="40" ToolTip="-"
						NavigateUrl='<%# "javascript:PTH_AvatarEdit(" + ((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem).Id.ToString() + ");" %>' 
						>
					    <%# ((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem).Id.ToString() %>
					</GUI:cIframeColumnText>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="left">
				<ItemTemplate>
					<GUI:cIframeColumnText Runat="server" Width="200" ToolTip="-">
					    <%# ((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem).Name.ToString()%>
					</GUI:cIframeColumnText>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="left">
				<ItemTemplate>
					<GUI:cIframeColumnText Runat="server" Width="300" ToolTip="-">
					    <%# ((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem).Description%>
					</GUI:cIframeColumnText>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="center">
				<ItemTemplate>
					<GUI:cIframeColumnIcon
						runat="server"
						ToolTip='<%# ((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem).Published ? (string)GetLocalResourceObject("UnpublishAvatar") : (string)GetLocalResourceObject("PublishAvatar") %>'
						Src='<%# GetPublishStatusIcon((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem) %>'
						Visible='<%# GetPublishVisibilityStatus((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem) %>'
						NavigateUrl='<%# GetPublishUrl((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem) %>' 
    					/>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="center">
				<ItemTemplate>
					<GUI:cIframeColumnIcon
						runat="server"
						ToolTip='<%# Resources.Strings.EditItem %>'
						Visible='<%# GetEditVisibilityStatus((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem) %>'
						Src="images/rename.gif"
						NavigateUrl='<%# "javascript:PTH_AvatarEdit(" + ((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem).Id.ToString() + ");" %>' 
    					/>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn ItemStyle-HorizontalAlign="center">
				<ItemTemplate>
					<GUI:cIframeColumnIcon
						runat="server"
						ToolTip='<%# Resources.Strings.DeleteItem %>'
						Visible='<%# GetDeleteVisibilityStatus((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem) %>'
						Src="images/delete.gif"
						NavigateUrl='<%# GetDeleteUrl((Casino.Multipoker.BusinessLayer.Avatar)Container.DataItem) %>' 
    					/>
				</ItemTemplate>
			</asp:TemplateColumn>

		</Columns>
	</GUI:cIframeDataGrid>

</asp:Content>
