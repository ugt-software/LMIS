﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Lmis.Portal.Web.Pages.User.Dashboard" %>

<%@ Register Src="~/Controls/DataDisplay/ReportUnitsControl.ascx" TagPrefix="lmis" TagName="ReportUnitsControl" %>
<%@ Register Src="~/Controls/DataDisplay/CategoriesControl.ascx" TagPrefix="lmis" TagName="CategoriesControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table>
		<tr>
			<td>
				<lmis:CategoriesControl runat="server" ID="categoriesControl" />
			</td>
			<td>
				<lmis:ReportUnitsControl runat="server" ID="reportUnitsControl" />
			</td>
		</tr>
	</table>
</asp:Content>

