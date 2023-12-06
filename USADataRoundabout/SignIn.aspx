<%@ Page Title="SignIn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="USADataRoundabout.SignIn" %>

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
        align-items: center;
    }
</style>
    <div class="big-flex">
        <div class="flex-container">
            <dl>
                <dt>Username:</dt>
                <dd><input type="text" id="txtUsername"  runat="server" /></dd>
            </dl>
            <dl>
                <dt>Password:</dt>
                <dd><input type="password" id="txtPassword"  runat="server" /></dd>
            </dl>
            <button type="submit" runat="server" id="btnLogin" onserverclick="btnLogin_Click">Log In</button>
            <br/>
            <var id="loginSuccess" style="color: green" runat="server">Login Successful!</var>
            <var id="loginFailure" style="color: red" runat="server">Username or Password incorrect.</var>
        </div>
    </div>
</asp:Content>
