﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Lmis.Portal.Web.Controls.Categories.CategoriesControl" %>
<div>
	<dx:ASPxTreeList runat="server" KeyFieldName="ID" ParentFieldName="ParentID" ID="tlData">
		<Columns>
			<dx:TreeListDataColumn>
				<DataCellTemplate>
					<div>
						<ce:ImageLinkButton runat="server" ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/edit.png" ID="btnEdit" OnCommand="btnEdit_OnCommand" />
						<ce:ImageLinkButton runat="server" ToolTip="Delete" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/delete.png" ID="btnDelete" OnCommand="btnDelete_OnCommand" />
						<ce:ImageLinkButton runat="server" ToolTip="Add" CommandArgument='<%# Eval("ID") %>' DefaultImageUrl="~/App_Themes/Default/images/add.png" ID="btnAddChild" OnCommand="btnAddChild_OnCommand" />
					</div>
				</DataCellTemplate>
			</dx:TreeListDataColumn>
			<dx:TreeListDataColumn FieldName="Name" Name="Name" Caption="Name">
			</dx:TreeListDataColumn>
		</Columns>
	</dx:ASPxTreeList>
</div>
