﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainLinkControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Management.MainLinkControl" %>

<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="LinkModel.ID" />

<table>
    <tr>
        <td>
            <ce:Label runat="server">Title</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxTitle" Property="LinkModel.Title"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Url</ce:Label></td>
        <td>
            <asp:TextBox runat="server" ID="tbxUrl" Property="LinkModel.Url"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Order Index</ce:Label></td>
        <td>
            <dx:ASPxSpinEdit runat="server" Width="150" ID="seOrderIndex" Property="LinkModel.OrderIndex"></dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <td>
            <ce:Label runat="server">Image</ce:Label></td>
        <td>
            <asp:FileUpload runat="server" ID="fuImage" Property="LinkModel.Image"></asp:FileUpload>
        </td>
    </tr>
</table>
