<%@ Page Title="EditUsers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUsers.aspx.cs" Inherits="USADataRoundabout.EditUsers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .big-flex {
        display: flex;
        flex-direction: row;
        justify-content: space-evenly;
        padding-top: 50px;
    }
    .flex-container {
        display: flex;
        flex-direction: column;
        align-items: start;
    }
</style>
    <div class="big-flex">
        <div class="flex-container" id="addUsers" runat="server">
            <dl>
                <dt>New User:</dt>
                <dt>Username:</dt>
                <dd><input type="text" id="txtNewUsername" runat="server" /></dd>
                <dt>Temporary Password:</dt>
                <dd><input type="password" id="txtNewTempPassword" runat="server" /></dd>
                <dt>Permissions</dt>
                <dd><select id="ddlPermissions" runat="server">
                    <option value="admin">Administrator</option>
                    <option value="general">General User</option>
                </select></dd>
                <dt>Represented School:</dt>
                <dd><input type="text" id="txtRepresentedSchool" runat="server" /></dd>
                <dd><button type="submit" runat="server" id="btnNewUser" onserverclick="btnNewUser_Click">Submit</button></dd>
            </dl>
        </div>
        <div class="flex-container" id="changePassword" runat="server">
            <dl>
                <dt>Change Password:</dt>
                <dt>Previous Password:</dt>
                <dd><input type="password" id="txtOldPassword" runat="server" /></dd>
                <dt>New Password:</dt>
                <dd><input type="password" id="txtNewPassword" runat="server" /></dd>
                <dt>Confirm New Password:</dt>
                <dd><input type="password" id="txtConfNewPassword" runat="server" /></dd>
                <dd><button type="submit" runat="server" id="btnChangePassword" onserverclick="btnChangePassword_Click">Submit</button></dd>
            </dl>
        </div>
    </div>
            <var id="output" runat="server">imhere</var>
</asp:Content>
