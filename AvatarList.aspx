<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AvatarList.aspx.cs" Inherits="AvatarList" meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" Runat="Server">

    <script language="javascript" src="./Javascript/COR_Forms.js" type="text/javascript"></script>
    
    <table width="100%" height="100%" cellpadding="5">
    <tr>
        <td height="30">
            <table width="100%" height="100%" class="panel" cellpadding="5" style="MARGIN-TOP:4px">
                <tr>
                    <td style="vertical-align:top;">
                        <GUI:cButton runat="server" id="btnNewAvatar" OnClick="PTH_NewAvatar();" width="70" meta:resourcekey="NewAvatar" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td height="*">
            <GUI:cIframeDataGridContainer runat="server" id="ctlData" Source="" height="100%" />
        </td>
    </tr>
    </table>
</asp:Content>
