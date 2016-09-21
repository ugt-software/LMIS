﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ColumnControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.SchemaManipulation.ColumnControl" %>
<%@ Register Src="~/Controls/Common/HiddenFieldValueControl.ascx" TagPrefix="lmis" TagName="HiddenFieldValueControl" %>

<lmis:HiddenFieldValueControl runat="server" ID="hdID" Property="ColumnModel.ID" />
<lmis:HiddenFieldValueControl runat="server" ID="hdTableID" Property="ColumnModel.TableID" />

<table>
	<tr>
		<td>Name</td>
		<td>
			<asp:TextBox runat="server" ID="tbxName" Property="ColumnModel.Name"></asp:TextBox></td>
	</tr>
	<tr>
		<td>Type</td>
		<td>
			<asp:DropDownList runat="server" ID="ddlType" Property="ColumnModel.Type">
				<Items>
					<asp:ListItem Text="String" Value="String" />
					<asp:ListItem Text="Integer" Value="Integer" />
					<asp:ListItem Text="Float" Value="Float" />
					<asp:ListItem Text="DateTime" Value="DateTime" />
				</Items>
			</asp:DropDownList>
		</td>
	</tr>
</table>