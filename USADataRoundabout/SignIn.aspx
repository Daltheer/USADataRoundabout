<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="USADataRoundabout.SignIn" %>

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
            <p>Username:</p>
            <input type="text" id="txtUsername"  runat="server" />
            <p>Password:</p>
            <input type="text" id="txtPassword"  runat="server" />
            <button type="submit" runat="server" id="btnLogin" onserverclick="btnLogin_Click">Log In</button>
        </div>
    </div>
</asp:Content>
